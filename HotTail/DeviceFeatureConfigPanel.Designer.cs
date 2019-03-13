namespace HotTail
{
    partial class DeviceFeatureConfigPanel
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
            this.availableManipulators = new System.Windows.Forms.ListBox();
            this.levelDisplay = new System.Windows.Forms.TrackBar();
            this.manipulatorSettings = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.levelDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // availableManipulators
            // 
            this.availableManipulators.Dock = System.Windows.Forms.DockStyle.Left;
            this.availableManipulators.FormattingEnabled = true;
            this.availableManipulators.Location = new System.Drawing.Point(10, 10);
            this.availableManipulators.Name = "availableManipulators";
            this.availableManipulators.Size = new System.Drawing.Size(250, 380);
            this.availableManipulators.TabIndex = 0;
            // 
            // levelDisplay
            // 
            this.levelDisplay.Dock = System.Windows.Forms.DockStyle.Left;
            this.levelDisplay.Enabled = false;
            this.levelDisplay.Location = new System.Drawing.Point(260, 10);
            this.levelDisplay.Maximum = 100;
            this.levelDisplay.Name = "levelDisplay";
            this.levelDisplay.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.levelDisplay.Size = new System.Drawing.Size(45, 380);
            this.levelDisplay.TabIndex = 1;
            this.levelDisplay.TabStop = false;
            this.levelDisplay.TickFrequency = 5;
            this.levelDisplay.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // manipulatorSettings
            // 
            this.manipulatorSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.manipulatorSettings.Location = new System.Drawing.Point(305, 10);
            this.manipulatorSettings.Margin = new System.Windows.Forms.Padding(10);
            this.manipulatorSettings.Name = "manipulatorSettings";
            this.manipulatorSettings.Padding = new System.Windows.Forms.Padding(10);
            this.manipulatorSettings.Size = new System.Drawing.Size(432, 380);
            this.manipulatorSettings.TabIndex = 2;
            this.manipulatorSettings.TabStop = false;
            this.manipulatorSettings.Text = "Settings";
            // 
            // DeviceFeatureConfigPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.manipulatorSettings);
            this.Controls.Add(this.levelDisplay);
            this.Controls.Add(this.availableManipulators);
            this.Name = "DeviceFeatureConfigPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(748, 400);
            ((System.ComponentModel.ISupportInitialize)(this.levelDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TrackBar levelDisplay;
        public System.Windows.Forms.ListBox availableManipulators;
        public System.Windows.Forms.GroupBox manipulatorSettings;
    }
}
