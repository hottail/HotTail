namespace HotTail
{
    partial class DevicePanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.featureTabs = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // featureTabs
            // 
            this.featureTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.featureTabs.Location = new System.Drawing.Point(0, 0);
            this.featureTabs.Name = "featureTabs";
            this.featureTabs.SelectedIndex = 0;
            this.featureTabs.Size = new System.Drawing.Size(2119, 1278);
            this.featureTabs.TabIndex = 0;
            // 
            // DevicePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.featureTabs);
            this.Name = "DevicePanel";
            this.Size = new System.Drawing.Size(2119, 1278);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TabControl featureTabs;
    }
}
