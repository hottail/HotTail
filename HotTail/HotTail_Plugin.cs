using Advanced_Combat_Tracker;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace HotTail
{
    public class HotTail_Plugin : IActPluginV1
    {
        private HotTailCore pluginCore;

        public HotTail_Plugin()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
            {
                try
                {
                    var pluginDirectory = ActGlobals.oFormActMain.PluginGetSelfData(this)?.pluginFile.DirectoryName;

                    var architect = Environment.Is64BitProcess ? "x64" : "x86";
                    var directories = new string[]
                    {
                        pluginDirectory,
                        Path.Combine(pluginDirectory, "references"),
                        Path.Combine(pluginDirectory, $@"{architect}"),
                        Path.Combine(pluginDirectory, $@"references\{architect}"),
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"Advanced Combat Tracker\Plugins"
                        ),
                    };

                    var asm = new AssemblyName(e.Name);

                    foreach (var directory in directories)
                    {
                        if (!string.IsNullOrWhiteSpace(directory))
                        {
                            var dll = Path.Combine(directory, asm.Name + ".dll");
                            if (File.Exists(dll))
                            {
                                return Assembly.LoadFrom(dll);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ActGlobals.oFormActMain.WriteExceptionLog(ex, "Assembly Resolution Error");
                }

                return null;
            };
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            pluginCore = new HotTailCore();
            pluginCore.Initialize(pluginScreenSpace, pluginStatusText);
        }

        public void DeInitPlugin()
        {
            pluginCore.DeInitialize();
        }
    }
}
