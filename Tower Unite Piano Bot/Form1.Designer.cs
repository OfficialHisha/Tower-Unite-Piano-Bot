namespace Tower_Unite_Piano_Bot
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SongTextBox = new System.Windows.Forms.TextBox();
            this.PlayButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NormalDelayBox = new System.Windows.Forms.NumericUpDown();
            this.FastDelayBox = new System.Windows.Forms.NumericUpDown();
            this.StopButton = new System.Windows.Forms.Button();
            this.MainGroup = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ProcessChooserBox = new System.Windows.Forms.ComboBox();
            this.LoopCheckBox = new System.Windows.Forms.CheckBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.DelaySettingsGroup = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.NormalDelayBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FastDelayBox)).BeginInit();
            this.MainGroup.SuspendLayout();
            this.DelaySettingsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // SongTextBox
            // 
            this.SongTextBox.Location = new System.Drawing.Point(6, 19);
            this.SongTextBox.Multiline = true;
            this.SongTextBox.Name = "SongTextBox";
            this.SongTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SongTextBox.Size = new System.Drawing.Size(323, 391);
            this.SongTextBox.TabIndex = 0;
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(335, 19);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(92, 54);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Delay for normal:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Delay for fast:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Milliseconds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Milliseconds";
            // 
            // NormalDelayBox
            // 
            this.NormalDelayBox.Location = new System.Drawing.Point(98, 24);
            this.NormalDelayBox.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.NormalDelayBox.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.NormalDelayBox.Name = "NormalDelayBox";
            this.NormalDelayBox.Size = new System.Drawing.Size(49, 20);
            this.NormalDelayBox.TabIndex = 9;
            this.NormalDelayBox.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // FastDelayBox
            // 
            this.FastDelayBox.Location = new System.Drawing.Point(98, 49);
            this.FastDelayBox.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.FastDelayBox.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.FastDelayBox.Name = "FastDelayBox";
            this.FastDelayBox.Size = new System.Drawing.Size(49, 20);
            this.FastDelayBox.TabIndex = 10;
            this.FastDelayBox.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(335, 79);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(92, 54);
            this.StopButton.TabIndex = 11;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // MainGroup
            // 
            this.MainGroup.Controls.Add(this.label5);
            this.MainGroup.Controls.Add(this.ProcessChooserBox);
            this.MainGroup.Controls.Add(this.LoopCheckBox);
            this.MainGroup.Controls.Add(this.VersionLabel);
            this.MainGroup.Controls.Add(this.SaveButton);
            this.MainGroup.Controls.Add(this.LoadButton);
            this.MainGroup.Controls.Add(this.SongTextBox);
            this.MainGroup.Controls.Add(this.StopButton);
            this.MainGroup.Controls.Add(this.PlayButton);
            this.MainGroup.Location = new System.Drawing.Point(12, 12);
            this.MainGroup.Name = "MainGroup";
            this.MainGroup.Size = new System.Drawing.Size(434, 416);
            this.MainGroup.TabIndex = 12;
            this.MainGroup.TabStop = false;
            this.MainGroup.Text = "Notes:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(335, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Target:";
            // 
            // ProcessChooserBox
            // 
            this.ProcessChooserBox.FormattingEnabled = true;
            this.ProcessChooserBox.Location = new System.Drawing.Point(338, 300);
            this.ProcessChooserBox.Name = "ProcessChooserBox";
            this.ProcessChooserBox.Size = new System.Drawing.Size(89, 21);
            this.ProcessChooserBox.TabIndex = 16;
            this.ProcessChooserBox.Text = "TU";
            this.ProcessChooserBox.SelectedIndexChanged += new System.EventHandler(this.ProcessChooserBox_SelectedIndexChanged);
            // 
            // LoopCheckBox
            // 
            this.LoopCheckBox.AutoSize = true;
            this.LoopCheckBox.Location = new System.Drawing.Point(338, 259);
            this.LoopCheckBox.Name = "LoopCheckBox";
            this.LoopCheckBox.Size = new System.Drawing.Size(50, 17);
            this.LoopCheckBox.TabIndex = 15;
            this.LoopCheckBox.Text = "Loop";
            this.LoopCheckBox.UseVisualStyleBackColor = true;
            this.LoopCheckBox.CheckedChanged += new System.EventHandler(this.LoopCheckBox_CheckedChanged);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(335, 397);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(63, 13);
            this.VersionLabel.TabIndex = 14;
            this.VersionLabel.Text = "Version: 1.2";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(336, 199);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(92, 54);
            this.SaveButton.TabIndex = 13;
            this.SaveButton.Text = "Save Song";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(336, 139);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(92, 54);
            this.LoadButton.TabIndex = 12;
            this.LoadButton.Text = "Load Song";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // DelaySettingsGroup
            // 
            this.DelaySettingsGroup.Controls.Add(this.label3);
            this.DelaySettingsGroup.Controls.Add(this.label4);
            this.DelaySettingsGroup.Controls.Add(this.label2);
            this.DelaySettingsGroup.Controls.Add(this.FastDelayBox);
            this.DelaySettingsGroup.Controls.Add(this.label1);
            this.DelaySettingsGroup.Controls.Add(this.NormalDelayBox);
            this.DelaySettingsGroup.Location = new System.Drawing.Point(15, 434);
            this.DelaySettingsGroup.Name = "DelaySettingsGroup";
            this.DelaySettingsGroup.Size = new System.Drawing.Size(222, 77);
            this.DelaySettingsGroup.TabIndex = 13;
            this.DelaySettingsGroup.TabStop = false;
            this.DelaySettingsGroup.Text = "Delay Settings";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 523);
            this.Controls.Add(this.DelaySettingsGroup);
            this.Controls.Add(this.MainGroup);
            this.Name = "Form1";
            this.Text = "Tower Unite Piano Bot";
            ((System.ComponentModel.ISupportInitialize)(this.NormalDelayBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FastDelayBox)).EndInit();
            this.MainGroup.ResumeLayout(false);
            this.MainGroup.PerformLayout();
            this.DelaySettingsGroup.ResumeLayout(false);
            this.DelaySettingsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox SongTextBox;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown NormalDelayBox;
        private System.Windows.Forms.NumericUpDown FastDelayBox;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.GroupBox MainGroup;
        private System.Windows.Forms.GroupBox DelaySettingsGroup;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.CheckBox LoopCheckBox;
        private System.Windows.Forms.ComboBox ProcessChooserBox;
        private System.Windows.Forms.Label label5;
    }
}

