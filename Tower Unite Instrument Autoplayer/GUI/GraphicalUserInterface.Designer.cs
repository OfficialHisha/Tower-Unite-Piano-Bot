namespace Tower_Unite_Instrument_Autoplayer.GUI
{
    partial class GraphicalUserInterface
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.MainTabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClearNotesButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.NoteTextBox = new System.Windows.Forms.TextBox();
            this.SettingsTabPage = new System.Windows.Forms.TabPage();
            this.CustomizeTabButton = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RemoveDelayButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomDelayTimeBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.CustomDelayCharacterBox = new System.Windows.Forms.TextBox();
            this.AddDelayButton = new System.Windows.Forms.Button();
            this.DelayListBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.MainTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.CustomizeTabButton.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomDelayTimeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.MainTabPage);
            this.tabControl1.Controls.Add(this.SettingsTabPage);
            this.tabControl1.Controls.Add(this.CustomizeTabButton);
            this.tabControl1.Location = new System.Drawing.Point(-5, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(643, 400);
            this.tabControl1.TabIndex = 0;
            // 
            // MainTabPage
            // 
            this.MainTabPage.Controls.Add(this.groupBox1);
            this.MainTabPage.Location = new System.Drawing.Point(4, 22);
            this.MainTabPage.Name = "MainTabPage";
            this.MainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.MainTabPage.Size = new System.Drawing.Size(635, 374);
            this.MainTabPage.TabIndex = 0;
            this.MainTabPage.Text = "Main";
            this.MainTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ClearNotesButton);
            this.groupBox1.Controls.Add(this.VersionLabel);
            this.groupBox1.Controls.Add(this.SaveButton);
            this.groupBox1.Controls.Add(this.LoadButton);
            this.groupBox1.Controls.Add(this.StopButton);
            this.groupBox1.Controls.Add(this.PlayButton);
            this.groupBox1.Controls.Add(this.NoteTextBox);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 374);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Notes";
            // 
            // ClearNotesButton
            // 
            this.ClearNotesButton.Location = new System.Drawing.Point(547, 19);
            this.ClearNotesButton.Name = "ClearNotesButton";
            this.ClearNotesButton.Size = new System.Drawing.Size(75, 23);
            this.ClearNotesButton.TabIndex = 6;
            this.ClearNotesButton.Text = "Clear Notes";
            this.ClearNotesButton.UseVisualStyleBackColor = true;
            this.ClearNotesButton.Click += new System.EventHandler(this.ClearNotesButton_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(544, 358);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(69, 13);
            this.VersionLabel.TabIndex = 5;
            this.VersionLabel.Text = "Version: x.x.x";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(547, 219);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 51);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save Notes";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(547, 162);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 51);
            this.LoadButton.TabIndex = 3;
            this.LoadButton.Text = "Load Notes";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(547, 105);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 51);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(547, 48);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(75, 51);
            this.PlayButton.TabIndex = 1;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // NoteTextBox
            // 
            this.NoteTextBox.Location = new System.Drawing.Point(0, 19);
            this.NoteTextBox.Multiline = true;
            this.NoteTextBox.Name = "NoteTextBox";
            this.NoteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NoteTextBox.Size = new System.Drawing.Size(541, 355);
            this.NoteTextBox.TabIndex = 0;
            this.NoteTextBox.Text = "Nothing to see here yet..";
            // 
            // SettingsTabPage
            // 
            this.SettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.SettingsTabPage.Name = "SettingsTabPage";
            this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTabPage.Size = new System.Drawing.Size(635, 374);
            this.SettingsTabPage.TabIndex = 1;
            this.SettingsTabPage.Text = "Settings";
            this.SettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // CustomizeTabButton
            // 
            this.CustomizeTabButton.Controls.Add(this.groupBox3);
            this.CustomizeTabButton.Controls.Add(this.groupBox2);
            this.CustomizeTabButton.Location = new System.Drawing.Point(4, 22);
            this.CustomizeTabButton.Name = "CustomizeTabButton";
            this.CustomizeTabButton.Size = new System.Drawing.Size(635, 374);
            this.CustomizeTabButton.TabIndex = 2;
            this.CustomizeTabButton.Text = "Customize";
            this.CustomizeTabButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(317, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(318, 374);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Custom Notes (Coming Soon)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RemoveDelayButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.CustomDelayTimeBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.CustomDelayCharacterBox);
            this.groupBox2.Controls.Add(this.AddDelayButton);
            this.groupBox2.Controls.Add(this.DelayListBox);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 374);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Custom Delays";
            // 
            // RemoveDelayButton
            // 
            this.RemoveDelayButton.Location = new System.Drawing.Point(176, 94);
            this.RemoveDelayButton.Name = "RemoveDelayButton";
            this.RemoveDelayButton.Size = new System.Drawing.Size(86, 23);
            this.RemoveDelayButton.TabIndex = 6;
            this.RemoveDelayButton.Text = "Remove Delay";
            this.RemoveDelayButton.UseVisualStyleBackColor = true;
            this.RemoveDelayButton.Click += new System.EventHandler(this.RemoveDelayButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Delay (ms):";
            // 
            // CustomDelayTimeBox
            // 
            this.CustomDelayTimeBox.Location = new System.Drawing.Point(238, 39);
            this.CustomDelayTimeBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.CustomDelayTimeBox.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.CustomDelayTimeBox.Name = "CustomDelayTimeBox";
            this.CustomDelayTimeBox.Size = new System.Drawing.Size(67, 20);
            this.CustomDelayTimeBox.TabIndex = 4;
            this.CustomDelayTimeBox.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Character:";
            // 
            // CustomDelayCharacterBox
            // 
            this.CustomDelayCharacterBox.Location = new System.Drawing.Point(238, 16);
            this.CustomDelayCharacterBox.MaxLength = 1;
            this.CustomDelayCharacterBox.Name = "CustomDelayCharacterBox";
            this.CustomDelayCharacterBox.Size = new System.Drawing.Size(14, 20);
            this.CustomDelayCharacterBox.TabIndex = 2;
            this.CustomDelayCharacterBox.Text = ".";
            // 
            // AddDelayButton
            // 
            this.AddDelayButton.Location = new System.Drawing.Point(176, 65);
            this.AddDelayButton.Name = "AddDelayButton";
            this.AddDelayButton.Size = new System.Drawing.Size(75, 23);
            this.AddDelayButton.TabIndex = 1;
            this.AddDelayButton.Text = "Add Delay";
            this.AddDelayButton.UseVisualStyleBackColor = true;
            this.AddDelayButton.Click += new System.EventHandler(this.AddDelayButton_Click);
            // 
            // DelayListBox
            // 
            this.DelayListBox.Location = new System.Drawing.Point(6, 19);
            this.DelayListBox.Multiline = true;
            this.DelayListBox.Name = "DelayListBox";
            this.DelayListBox.ReadOnly = true;
            this.DelayListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DelayListBox.Size = new System.Drawing.Size(164, 349);
            this.DelayListBox.TabIndex = 0;
            // 
            // GraphicalUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 395);
            this.Controls.Add(this.tabControl1);
            this.Name = "GraphicalUserInterface";
            this.Text = "Tower Unite Instrument Autoplayer";
            this.tabControl1.ResumeLayout(false);
            this.MainTabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.CustomizeTabButton.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomDelayTimeBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage MainTabPage;
        private System.Windows.Forms.TabPage SettingsTabPage;
        private System.Windows.Forms.TabPage CustomizeTabButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.TextBox NoteTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown CustomDelayTimeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CustomDelayCharacterBox;
        private System.Windows.Forms.Button AddDelayButton;
        private System.Windows.Forms.TextBox DelayListBox;
        private System.Windows.Forms.Button RemoveDelayButton;
        private System.Windows.Forms.Button ClearNotesButton;
    }
}

