using Buttplug.Client;
using Buttplug.Core.Messages;
using HotTail.manipulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HotTail
{
    public class Device
    {
        ButtplugClientDevice ClientDevice;
        Dictionary<Type, List<IManipulator>> ManipulatorMapping = new Dictionary<Type, List<IManipulator>>();
        private TabPage configurationPage;
        public TabPage ConfiguarionPage { get => configurationPage; }

        public Device(ButtplugClientDevice device)
        {
            // Set up device
            ClientDevice = device;

            // Set up device features
            var features = device.AllowedMessages.Keys.Intersect(new[] { typeof(VibrateCmd), typeof(RotateCmd), typeof(LinearCmd) }).ToArray();

            foreach (var feature in features)
            {
                ManipulatorMapping[feature] = new List<IManipulator>();
                for (int i = 0; i < device.AllowedMessages[feature].FeatureCount; i++)
                {
                    ManipulatorMapping[feature].Add(new NoActionManipulator());
                }
            }

            // Set up configuration panel
            configurationPage = new TabPage()
            {
                Text = ClientDevice.Name
            };

            var devicePanel = new DevicePanel();

            foreach (var mapping in ManipulatorMapping)
            {
                var typeName = "Unknown";

                if (mapping.Key == typeof(VibrateCmd))
                {
                    typeName = "Vibration";
                }
                else if (mapping.Key == typeof(RotateCmd))
                {
                    typeName = "Rotation";
                }
                else if (mapping.Key == typeof(LinearCmd))
                {
                    typeName = "Linear Action";
                }

                for (int i = 0; i < mapping.Value.Count; i++)
                {
                    var currentIndex = i;
                    var currentManipulator = mapping.Value[i];

                    // Setup Panel
                    DeviceFeatureConfigPanel configPanel = new DeviceFeatureConfigPanel();
                    configPanel.Dock = DockStyle.Top;

                    // Setup Level Display
                    configPanel.levelDisplay.Value = (int)Math.Round(currentManipulator.Level * 100);

                    EventHandler handleLevelChange = (o, e) =>
                    {
                        configPanel.levelDisplay.Value = (int)Math.Round(((LevelChangedEventArgs)e).Level * 100);
                    };
                    currentManipulator.LevelChanged += handleLevelChange;

                    // Setup Available Manipulators
                    configPanel.availableManipulators.Dock = DockStyle.Left;
                    configPanel.availableManipulators.BindingContext = new BindingContext();
                    configPanel.availableManipulators.DataSource = HotTailCore.Instance.AvailableManipulators.Select(x => ((IManipulator)Activator.CreateInstance(x)).Descriptor).ToList();
                    configPanel.availableManipulators.SelectedIndex = HotTailCore.Instance.AvailableManipulators.FindIndex(x => x == currentManipulator.GetType());
                    configPanel.availableManipulators.SelectedValueChanged += (o, e) =>
                    {
                        var index = configPanel.availableManipulators.SelectedIndex;
                        var availableManipulators = HotTailCore.Instance.AvailableManipulators;
                        var selectedManipulator = availableManipulators[index];
                        var manipInstance = (IManipulator)Activator.CreateInstance(selectedManipulator);
                        configPanel.levelDisplay.Value = (int)Math.Round(manipInstance.Level * 100);
                        manipInstance.LevelChanged += handleLevelChange;
                        configPanel.manipulatorSettings.Controls.Clear();
                        var manipSettings = manipInstance.ManipulatorSettings;
                        manipSettings.Dock = DockStyle.Fill;
                        configPanel.manipulatorSettings.Controls.Add(manipSettings);
                        mapping.Value[currentIndex] = manipInstance;
                    };
                    configPanel.manipulatorSettings.Controls.Clear();
                    var currentManipSettings = currentManipulator.ManipulatorSettings;
                    currentManipSettings.Dock = DockStyle.Fill;
                    configPanel.manipulatorSettings.Controls.Add(currentManipSettings);


                    // Display the thing
                    var featureTab = new TabPage
                    {
                        Text = typeName + " " + (i + 1)
                    };
                    featureTab.Controls.Add(configPanel);
                    devicePanel.featureTabs.TabPages.Add(featureTab);
                }
            }

            configurationPage.Controls.Add(devicePanel);
        }

        ~Device()
        {
            ClientDevice.StopDeviceCmd();
            foreach (var list in ManipulatorMapping.Values)
            {
                foreach (var manipulator in list)
                {
                    manipulator.Stop();
                }
            }
        }

        public void Update(long ticksElapsed)
        {
            foreach (var typeMappings in ManipulatorMapping)
            {
                var results = new List<double>();
                foreach (var manipulator in typeMappings.Value)
                {
                    manipulator.DataUpdate(ticksElapsed);
                    results.Add(manipulator.Level);
                }

                if (typeMappings.Key == typeof(VibrateCmd))
                {
                    ClientDevice.SendVibrateCmd(results);
                }
                else if (typeMappings.Key == typeof(RotateCmd))
                {
                    // ToDo: Figure out a way to integrate rotation direction. Current implementation only has clockwise motion for now.
                    ClientDevice.SendRotateCmd(results.Select(x => (x, true)));
                }
                else if (typeMappings.Key == typeof(LinearCmd))
                {
                    ClientDevice.SendLinearCmd(results.Select(x => ((uint)100, x)));
                }
            }
        }
    }
}
