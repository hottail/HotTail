namespace HotTail
{
    partial class ControlPanel
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
            this.deviceManagerTabs = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // deviceManagerTabs
            // 
            this.deviceManagerTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceManagerTabs.Location = new System.Drawing.Point(0, 0);
            this.deviceManagerTabs.Name = "deviceManagerTabs";
            this.deviceManagerTabs.SelectedIndex = 0;
            this.deviceManagerTabs.Size = new System.Drawing.Size(2119, 1278);
            this.deviceManagerTabs.TabIndex = 0;
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deviceManagerTabs);
            this.Name = "ControlPanel";
            this.Size = new System.Drawing.Size(2119, 1278);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl deviceManagerTabs;
    }
}
