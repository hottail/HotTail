namespace HotTail.manipulators
{
    partial class CritDirectHitManipulatorSettings
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
            System.Windows.Forms.TableLayoutPanel layout;
            System.Windows.Forms.Label decayTimeLabel;
            System.Windows.Forms.Label critIncreaseLabel;
            System.Windows.Forms.Label directhitIncreaseLabel;
            this.fullDecayTimeSetting = new System.Windows.Forms.NumericUpDown();
            this.critIncreaseSetting = new System.Windows.Forms.TrackBar();
            this.dhIncreaseSetting = new System.Windows.Forms.TrackBar();
            layout = new System.Windows.Forms.TableLayoutPanel();
            decayTimeLabel = new System.Windows.Forms.Label();
            critIncreaseLabel = new System.Windows.Forms.Label();
            directhitIncreaseLabel = new System.Windows.Forms.Label();
            layout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fullDecayTimeSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.critIncreaseSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dhIncreaseSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // layout
            // 
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.Controls.Add(decayTimeLabel, 0, 0);
            layout.Controls.Add(critIncreaseLabel, 0, 1);
            layout.Controls.Add(directhitIncreaseLabel, 0, 2);
            layout.Controls.Add(this.fullDecayTimeSetting, 1, 0);
            layout.Controls.Add(this.critIncreaseSetting, 1, 1);
            layout.Controls.Add(this.dhIncreaseSetting, 1, 2);
            layout.Dock = System.Windows.Forms.DockStyle.Fill;
            layout.Location = new System.Drawing.Point(0, 0);
            layout.Name = "layout";
            layout.RowCount = 3;
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.Size = new System.Drawing.Size(300, 150);
            layout.TabIndex = 0;
            // 
            // decayTimeLabel
            // 
            decayTimeLabel.AutoSize = true;
            decayTimeLabel.Location = new System.Drawing.Point(3, 0);
            decayTimeLabel.Name = "decayTimeLabel";
            decayTimeLabel.Size = new System.Drawing.Size(109, 13);
            decayTimeLabel.TabIndex = 0;
            decayTimeLabel.Text = "Full Decay Time (sec)";
            // 
            // critIncreaseLabel
            // 
            critIncreaseLabel.AutoSize = true;
            critIncreaseLabel.Location = new System.Drawing.Point(3, 26);
            critIncreaseLabel.Name = "critIncreaseLabel";
            critIncreaseLabel.Size = new System.Drawing.Size(142, 13);
            critIncreaseLabel.TabIndex = 1;
            critIncreaseLabel.Text = "Critical Hit Vibration Increase";
            // 
            // directhitIncreaseLabel
            // 
            directhitIncreaseLabel.AutoSize = true;
            directhitIncreaseLabel.Location = new System.Drawing.Point(3, 77);
            directhitIncreaseLabel.Name = "directhitIncreaseLabel";
            directhitIncreaseLabel.Size = new System.Drawing.Size(139, 13);
            directhitIncreaseLabel.TabIndex = 2;
            directhitIncreaseLabel.Text = "Direct Hit Vibration Increase";
            // 
            // fullDecayTimeSetting
            // 
            this.fullDecayTimeSetting.DecimalPlaces = 1;
            this.fullDecayTimeSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.fullDecayTimeSetting.Location = new System.Drawing.Point(151, 3);
            this.fullDecayTimeSetting.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fullDecayTimeSetting.Name = "fullDecayTimeSetting";
            this.fullDecayTimeSetting.Size = new System.Drawing.Size(80, 20);
            this.fullDecayTimeSetting.TabIndex = 1;
            this.fullDecayTimeSetting.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // critIncreaseSetting
            // 
            this.critIncreaseSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.critIncreaseSetting.Location = new System.Drawing.Point(151, 29);
            this.critIncreaseSetting.Maximum = 100;
            this.critIncreaseSetting.Name = "critIncreaseSetting";
            this.critIncreaseSetting.Size = new System.Drawing.Size(146, 45);
            this.critIncreaseSetting.TabIndex = 2;
            this.critIncreaseSetting.TickFrequency = 5;
            this.critIncreaseSetting.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // dhIncreaseSetting
            // 
            this.dhIncreaseSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.dhIncreaseSetting.Location = new System.Drawing.Point(151, 80);
            this.dhIncreaseSetting.Maximum = 100;
            this.dhIncreaseSetting.Name = "dhIncreaseSetting";
            this.dhIncreaseSetting.Size = new System.Drawing.Size(146, 45);
            this.dhIncreaseSetting.TabIndex = 3;
            this.dhIncreaseSetting.TickFrequency = 5;
            this.dhIncreaseSetting.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // CritDirectHitManipulatorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(layout);
            this.Name = "CritDirectHitManipulatorSettings";
            this.Size = new System.Drawing.Size(300, 150);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fullDecayTimeSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.critIncreaseSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dhIncreaseSetting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.NumericUpDown fullDecayTimeSetting;
        public System.Windows.Forms.TrackBar critIncreaseSetting;
        public System.Windows.Forms.TrackBar dhIncreaseSetting;
    }
}
