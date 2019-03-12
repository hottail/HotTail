using System;
using System.Linq;
using System.Windows.Forms;

namespace HotTail
{
    public partial class ControlPanel : UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();
        }
        
        public void UpdateDeviceList()
        {
            SuspendLayout();
            
            var currentPages = HotTailCore.Instance.DeviceManagers.Values.Select(x => x.ConfiguarionPage);

            for (int i = deviceManagerTabs.TabCount - 1; i >= 0; i--)
            {
                var page = deviceManagerTabs.TabPages[i];
                if (!currentPages.Contains(page))
                {
                    RemoveDeviceConfigPage(page);
                }
            }

            foreach (TabPage page in currentPages)
            {
                if (!deviceManagerTabs.TabPages.Contains(page))
                {
                    AddDeviceConfigPage(page);
                }
            }

            ResumeLayout();
        }

        public void AddDeviceConfigPage(TabPage page)
        {
            if (InvokeRequired)
            {
                Invoke(
                    (MethodInvoker)delegate
                    {
                        AddDeviceConfigPage(page);
                    }
                );
                return;
            }
            deviceManagerTabs.TabPages.Add(page);
        }

        public void RemoveDeviceConfigPage(TabPage page)
        {
            if (InvokeRequired)
            {
                Invoke(
                    (MethodInvoker)delegate
                    {
                        RemoveDeviceConfigPage(page);
                    }
                );
                return;
            }
            deviceManagerTabs.TabPages.Remove(page);
        }
    }
}
