using Advanced_Combat_Tracker;
using Buttplug.Client;
using Buttplug.Server.Managers.HidSharpManager;
using Buttplug.Server.Managers.UWPBluetoothManager;
using Buttplug.Server.Managers.WinUSBManager;
using Buttplug.Server.Managers.XInputGamepadManager;
using HotTail.manipulators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotTail
{
    public class HotTailCore
    {
        #region Singleton
        private static HotTailCore instance;
        public static HotTailCore Instance { get => instance; }
        #endregion

        // UI References
        private Label pluginStatusText;
        private TabPage pluginScreenSpace;
        private ControlPanel controlPanel;

        // Task References
        protected System.Timers.Timer DataUpdateTimer;

        // Buttplug.io
        public ButtplugClient DeviceClient;

        // Manipulators
        public List<Type> AvailableManipulators = new List<Type>();

        // Configurations
        public Dictionary<uint, Device> DeviceManagers = new Dictionary<uint, Device>();

        public HotTailCore()
        {
            instance = this;

            AvailableManipulators.Add(typeof(NoActionManipulator));
            AvailableManipulators.Add(typeof(CritDirectHitManipulator));
        }

        ~HotTailCore()
        {
            DeInitialize();
        }

        public void SetStatus(string statusText)
        {
            pluginStatusText.Text = statusText;
        }

        public void SetRunningStatus()
        {
            SetStatus("Found " + DeviceClient.Devices.Length + " Devices.");
        }

        public async void Initialize(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            this.pluginScreenSpace = pluginScreenSpace;
            this.pluginStatusText = pluginStatusText;

            SetStatus("Initializing...");
            
            #if DEBUG
            pluginScreenSpace.Text = "HotTail (Debug)";
            #else
            pluginScreenSpace.Text = "HotTail";
            #endif

            // Buttplug IO init
            ButtplugEmbeddedConnector connector = new ButtplugEmbeddedConnector("HotTail Server");
            connector.Server.AddDeviceSubtypeManager((logger) => new UWPBluetoothManager(logger));
            connector.Server.AddDeviceSubtypeManager((logger) => new XInputGamepadManager(logger));
            connector.Server.AddDeviceSubtypeManager((logger) => new HidSharpManager(logger));
            connector.Server.AddDeviceSubtypeManager((logger) => new WinUSBManager(logger));

            DeviceClient = new ButtplugClient("Hot Tail Plugin", connector);

            DeviceClient.DeviceAdded += DeviceClient_DeviceAdded;
            DeviceClient.DeviceRemoved += DeviceClient_DeviceRemoved;

            await DeviceClient.ConnectAsync();
            await DeviceClient.StartScanningAsync();

            // Setup and Start Data Update
            DataUpdateTimer = new System.Timers.Timer
            {
                Interval = 100
            };
            DataUpdateTimer.Elapsed += (a, b) => DataUpdate();

            // UI Init
            controlPanel = new ControlPanel();
            controlPanel.Dock = DockStyle.Fill;
            pluginScreenSpace.Controls.Add(controlPanel);

            // Setup UI State and start tasks
            controlPanel.UpdateDeviceList();
            DataUpdateTimer.Start();

            SetRunningStatus();
        }

        private void DeviceClient_DeviceRemoved(object sender, DeviceRemovedEventArgs e)
        {
            // Unregister Plugin Settings Tab
            var configPage = DeviceManagers[e.Device.Index].ConfiguarionPage;
            controlPanel.RemoveDeviceConfigPage(configPage);

            // Remove Manager
            DeviceManagers.Remove(e.Device.Index);
        }

        private void DeviceClient_DeviceAdded(object sender, DeviceAddedEventArgs e)
        {
            var device = new Device(e.Device);

            // Add Manager
            DeviceManagers.Add(e.Device.Index, device);

            // Register Plugin Settings Tab
            controlPanel.AddDeviceConfigPage(device.ConfiguarionPage);
        }

        public async void DeInitialize()
        {
            // Stop our Data Update
            DataUpdateTimer.Enabled = false;

            // Stop Buttplug IO if still connected
            if (DeviceClient.Connected)
            {
                try
                {
                    await DeviceClient.StopScanningAsync();
                }
                catch { }
                foreach (var device in DeviceClient.Devices)
                {
                    try
                    {
                        await device.StopDeviceCmd();
                    }
                    catch { }
                }
                try
                {
                    await DeviceClient.DisconnectAsync();
                }
                catch { }
            }
        }

        public void DataUpdate()
        {
            foreach (var manager in DeviceManagers.Values)
            {
                manager.Update();
            }
        }

        // Helper method to get Data from ACT
        // Credit: anoyetta
        public List<KeyValuePair<CombatantData, Dictionary<string, string>>> GetCombatantList(List<CombatantData> allies)
        {
            var combatantList = new List<KeyValuePair<CombatantData, Dictionary<string, string>>>();
            Parallel.ForEach(allies, (ally) =>
            //foreach (var ally in allies)
            {
                var valueDict = new Dictionary<string, string>();
                foreach (var exportValuePair in CombatantData.ExportVariables)
                {
                    try
                    {
                        // NAME タグには {NAME:8} のようにコロンで区切られたエクストラ情報が必要で、
                        // プラグインの仕組み的に対応することができないので除外する
                        if (exportValuePair.Key == "NAME")
                        {
                            continue;
                        }

                        // ACT_FFXIV_Plugin が提供する LastXXDPS は、
                        // ally.Items[CombatantData.DamageTypeDataOutgoingDamage].Items に All キーが存在しない場合に、
                        // プラグイン内で例外が発生してしまい、パフォーマンスが悪化するので代わりに空の文字列を挿入する
                        if (exportValuePair.Key == "Last10DPS" ||
                            exportValuePair.Key == "Last30DPS" ||
                            exportValuePair.Key == "Last60DPS")
                        {
                            if (!ally.Items[CombatantData.DamageTypeDataOutgoingDamage].Items.ContainsKey("All"))
                            {
                                valueDict.Add(exportValuePair.Key, "");
                                continue;
                            }
                        }

                        var value = exportValuePair.Value.GetExportString(ally, "");
                        valueDict.Add(exportValuePair.Key, value);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

                lock (combatantList)
                {
                    combatantList.Add(new KeyValuePair<CombatantData, Dictionary<string, string>>(ally, valueDict));
                }
            }
            );

            return combatantList;
        }

        public static bool CheckIsActReady()
        {
            if (ActGlobals.oFormActMain != null &&
                ActGlobals.oFormActMain.ActiveZone != null &&
                ActGlobals.oFormActMain.ActiveZone.ActiveEncounter != null &&
                EncounterData.ExportVariables != null &&
                CombatantData.ExportVariables != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
