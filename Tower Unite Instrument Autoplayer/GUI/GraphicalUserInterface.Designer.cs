﻿namespace Tower_Unite_Instrument_Autoplayer.GUI
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
            this.LoopCheckBox = new System.Windows.Forms.CheckBox();
            this.ClearNotesButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.NoteTextBox = new System.Windows.Forms.TextBox();
            this.SettingsTabPage = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FastDelayBox = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.NormalDelayBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.StopKeyTextBox = new System.Windows.Forms.TextBox();
            this.StartKeyTextBox = new System.Windows.Forms.TextBox();
            this.StopKeyLabel = new System.Windows.Forms.Label();
            this.StartKeyLabel = new System.Windows.Forms.Label();
            this.CustomizeTabButton = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CustomNoteNewCharacterBox = new System.Windows.Forms.TextBox();
            this.RemoveAllNotesButton = new System.Windows.Forms.Button();
            this.CustomNoteListBox = new System.Windows.Forms.TextBox();
            this.RemoveNoteButton = new System.Windows.Forms.Button();
            this.AddNoteButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.CustomNoteCharacterBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RemoveAllDelayButton = new System.Windows.Forms.Button();
            this.RemoveDelayButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomDelayTimeBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.CustomDelayCharacterBox = new System.Windows.Forms.TextBox();
            this.AddDelayButton = new System.Windows.Forms.Button();
            this.DelayListBox = new System.Windows.Forms.TextBox();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ErrorTextBox = new System.Windows.Forms.RichTextBox();
            this.AutoplayerToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlayMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoplayerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MusicPiecesTabButton = new System.Windows.Forms.TabPage();
            this.PlayTogetherTabButton = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.MainTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SettingsTabPage.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FastDelayBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NormalDelayBox)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.CustomizeTabButton.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomDelayTimeBox)).BeginInit();
            this.MenuStrip.SuspendLayout();
            this.PlayTogetherTabButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.MainTabPage);
            this.tabControl1.Controls.Add(this.SettingsTabPage);
            this.tabControl1.Controls.Add(this.CustomizeTabButton);
            this.tabControl1.Controls.Add(this.MusicPiecesTabButton);
            this.tabControl1.Controls.Add(this.PlayTogetherTabButton);
            this.tabControl1.Location = new System.Drawing.Point(-5, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(643, 380);
            this.tabControl1.TabIndex = 0;
            // 
            // MainTabPage
            // 
            this.MainTabPage.Controls.Add(this.groupBox1);
            this.MainTabPage.Location = new System.Drawing.Point(4, 22);
            this.MainTabPage.Name = "MainTabPage";
            this.MainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.MainTabPage.Size = new System.Drawing.Size(635, 354);
            this.MainTabPage.TabIndex = 0;
            this.MainTabPage.Text = "Main";
            this.MainTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ErrorTextBox);
            this.groupBox1.Controls.Add(this.LoopCheckBox);
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
            // LoopCheckBox
            // 
            this.LoopCheckBox.AutoSize = true;
            this.LoopCheckBox.Location = new System.Drawing.Point(548, 277);
            this.LoopCheckBox.Name = "LoopCheckBox";
            this.LoopCheckBox.Size = new System.Drawing.Size(50, 17);
            this.LoopCheckBox.TabIndex = 7;
            this.LoopCheckBox.Text = "Loop";
            this.LoopCheckBox.UseVisualStyleBackColor = true;
            this.LoopCheckBox.CheckedChanged += new System.EventHandler(this.LoopCheckBox_CheckedChanged);
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
            this.VersionLabel.Location = new System.Drawing.Point(553, 297);
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
            this.StopButton.Text = "Stop (F3)";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(547, 48);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(75, 51);
            this.PlayButton.TabIndex = 1;
            this.PlayButton.Text = "Play (F2)";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // NoteTextBox
            // 
            this.NoteTextBox.Location = new System.Drawing.Point(0, 19);
            this.NoteTextBox.Multiline = true;
            this.NoteTextBox.Name = "NoteTextBox";
            this.NoteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NoteTextBox.Size = new System.Drawing.Size(541, 275);
            this.NoteTextBox.TabIndex = 0;
            this.NoteTextBox.Text = "Load a song or input notes here..";
            this.NoteTextBox.TextChanged += new System.EventHandler(this.NoteTextBox_TextChanged);
            // 
            // SettingsTabPage
            // 
            this.SettingsTabPage.Controls.Add(this.groupBox5);
            this.SettingsTabPage.Controls.Add(this.groupBox4);
            this.SettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.SettingsTabPage.Name = "SettingsTabPage";
            this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTabPage.Size = new System.Drawing.Size(635, 354);
            this.SettingsTabPage.TabIndex = 1;
            this.SettingsTabPage.Text = "Settings";
            this.SettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.FastDelayBox);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.NormalDelayBox);
            this.groupBox5.Location = new System.Drawing.Point(0, 108);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(263, 91);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Default delays";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Milliseconds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Milliseconds";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Delay for fast speed:";
            // 
            // FastDelayBox
            // 
            this.FastDelayBox.Location = new System.Drawing.Point(131, 54);
            this.FastDelayBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.FastDelayBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FastDelayBox.Name = "FastDelayBox";
            this.FastDelayBox.Size = new System.Drawing.Size(49, 20);
            this.FastDelayBox.TabIndex = 16;
            this.FastDelayBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.FastDelayBox.ValueChanged += new System.EventHandler(this.FastDelayBox_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Delay for normal speed:";
            // 
            // NormalDelayBox
            // 
            this.NormalDelayBox.Location = new System.Drawing.Point(131, 28);
            this.NormalDelayBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.NormalDelayBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NormalDelayBox.Name = "NormalDelayBox";
            this.NormalDelayBox.Size = new System.Drawing.Size(49, 20);
            this.NormalDelayBox.TabIndex = 15;
            this.NormalDelayBox.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.NormalDelayBox.ValueChanged += new System.EventHandler(this.NormalDelayBox_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.StopKeyTextBox);
            this.groupBox4.Controls.Add(this.StartKeyTextBox);
            this.groupBox4.Controls.Add(this.StopKeyLabel);
            this.groupBox4.Controls.Add(this.StartKeyLabel);
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(187, 102);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Key bindings";
            // 
            // StopKeyTextBox
            // 
            this.StopKeyTextBox.Location = new System.Drawing.Point(41, 58);
            this.StopKeyTextBox.Name = "StopKeyTextBox";
            this.StopKeyTextBox.Size = new System.Drawing.Size(100, 20);
            this.StopKeyTextBox.TabIndex = 7;
            this.StopKeyTextBox.Text = "F3";
            this.StopKeyTextBox.TextChanged += new System.EventHandler(this.StopKeyTextBox_TextChanged);
            // 
            // StartKeyTextBox
            // 
            this.StartKeyTextBox.Location = new System.Drawing.Point(41, 32);
            this.StartKeyTextBox.Name = "StartKeyTextBox";
            this.StartKeyTextBox.Size = new System.Drawing.Size(100, 20);
            this.StartKeyTextBox.TabIndex = 6;
            this.StartKeyTextBox.Text = "F2";
            this.StartKeyTextBox.TextChanged += new System.EventHandler(this.StartKeyTextBox_TextChanged);
            // 
            // StopKeyLabel
            // 
            this.StopKeyLabel.AutoSize = true;
            this.StopKeyLabel.Location = new System.Drawing.Point(6, 61);
            this.StopKeyLabel.Name = "StopKeyLabel";
            this.StopKeyLabel.Size = new System.Drawing.Size(29, 13);
            this.StopKeyLabel.TabIndex = 5;
            this.StopKeyLabel.Text = "Stop";
            // 
            // StartKeyLabel
            // 
            this.StartKeyLabel.AutoSize = true;
            this.StartKeyLabel.Location = new System.Drawing.Point(6, 35);
            this.StartKeyLabel.Name = "StartKeyLabel";
            this.StartKeyLabel.Size = new System.Drawing.Size(29, 13);
            this.StartKeyLabel.TabIndex = 4;
            this.StartKeyLabel.Text = "Start";
            // 
            // CustomizeTabButton
            // 
            this.CustomizeTabButton.Controls.Add(this.groupBox3);
            this.CustomizeTabButton.Controls.Add(this.groupBox2);
            this.CustomizeTabButton.Location = new System.Drawing.Point(4, 22);
            this.CustomizeTabButton.Name = "CustomizeTabButton";
            this.CustomizeTabButton.Size = new System.Drawing.Size(635, 354);
            this.CustomizeTabButton.TabIndex = 2;
            this.CustomizeTabButton.Text = "Customize";
            this.CustomizeTabButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CustomNoteNewCharacterBox);
            this.groupBox3.Controls.Add(this.RemoveAllNotesButton);
            this.groupBox3.Controls.Add(this.CustomNoteListBox);
            this.groupBox3.Controls.Add(this.RemoveNoteButton);
            this.groupBox3.Controls.Add(this.AddNoteButton);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.CustomNoteCharacterBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(317, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(318, 358);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Custom Notes";
            // 
            // CustomNoteNewCharacterBox
            // 
            this.CustomNoteNewCharacterBox.Location = new System.Drawing.Point(121, 38);
            this.CustomNoteNewCharacterBox.MaxLength = 1;
            this.CustomNoteNewCharacterBox.Name = "CustomNoteNewCharacterBox";
            this.CustomNoteNewCharacterBox.Size = new System.Drawing.Size(14, 20);
            this.CustomNoteNewCharacterBox.TabIndex = 15;
            this.CustomNoteNewCharacterBox.Text = "B";
            // 
            // RemoveAllNotesButton
            // 
            this.RemoveAllNotesButton.Location = new System.Drawing.Point(49, 123);
            this.RemoveAllNotesButton.Name = "RemoveAllNotesButton";
            this.RemoveAllNotesButton.Size = new System.Drawing.Size(86, 23);
            this.RemoveAllNotesButton.TabIndex = 14;
            this.RemoveAllNotesButton.Text = "Remove All";
            this.RemoveAllNotesButton.UseVisualStyleBackColor = true;
            this.RemoveAllNotesButton.Click += new System.EventHandler(this.RemoveAllNotesButton_Click);
            // 
            // CustomNoteListBox
            // 
            this.CustomNoteListBox.Location = new System.Drawing.Point(141, 19);
            this.CustomNoteListBox.Multiline = true;
            this.CustomNoteListBox.Name = "CustomNoteListBox";
            this.CustomNoteListBox.ReadOnly = true;
            this.CustomNoteListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CustomNoteListBox.Size = new System.Drawing.Size(164, 332);
            this.CustomNoteListBox.TabIndex = 8;
            // 
            // RemoveNoteButton
            // 
            this.RemoveNoteButton.Location = new System.Drawing.Point(49, 94);
            this.RemoveNoteButton.Name = "RemoveNoteButton";
            this.RemoveNoteButton.Size = new System.Drawing.Size(86, 23);
            this.RemoveNoteButton.TabIndex = 13;
            this.RemoveNoteButton.Text = "Remove Note";
            this.RemoveNoteButton.UseVisualStyleBackColor = true;
            this.RemoveNoteButton.Click += new System.EventHandler(this.RemoveNoteButton_Click);
            // 
            // AddNoteButton
            // 
            this.AddNoteButton.Location = new System.Drawing.Point(49, 65);
            this.AddNoteButton.Name = "AddNoteButton";
            this.AddNoteButton.Size = new System.Drawing.Size(86, 23);
            this.AddNoteButton.TabIndex = 8;
            this.AddNoteButton.Text = "Add Note";
            this.AddNoteButton.UseVisualStyleBackColor = true;
            this.AddNoteButton.Click += new System.EventHandler(this.AddNoteButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "New character:";
            // 
            // CustomNoteCharacterBox
            // 
            this.CustomNoteCharacterBox.Location = new System.Drawing.Point(121, 16);
            this.CustomNoteCharacterBox.MaxLength = 1;
            this.CustomNoteCharacterBox.Name = "CustomNoteCharacterBox";
            this.CustomNoteCharacterBox.Size = new System.Drawing.Size(14, 20);
            this.CustomNoteCharacterBox.TabIndex = 9;
            this.CustomNoteCharacterBox.Text = "A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(59, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Character:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RemoveAllDelayButton);
            this.groupBox2.Controls.Add(this.RemoveDelayButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.CustomDelayTimeBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.CustomDelayCharacterBox);
            this.groupBox2.Controls.Add(this.AddDelayButton);
            this.groupBox2.Controls.Add(this.DelayListBox);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 358);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Custom Delays";
            // 
            // RemoveAllDelayButton
            // 
            this.RemoveAllDelayButton.Location = new System.Drawing.Point(176, 123);
            this.RemoveAllDelayButton.Name = "RemoveAllDelayButton";
            this.RemoveAllDelayButton.Size = new System.Drawing.Size(86, 23);
            this.RemoveAllDelayButton.TabIndex = 7;
            this.RemoveAllDelayButton.Text = "Remove All";
            this.RemoveAllDelayButton.UseVisualStyleBackColor = true;
            this.RemoveAllDelayButton.Click += new System.EventHandler(this.RemoveAllDelayButton_Click);
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
            1,
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
            this.AddDelayButton.Size = new System.Drawing.Size(86, 23);
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
            this.DelayListBox.Size = new System.Drawing.Size(164, 332);
            this.DelayListBox.TabIndex = 0;
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStrip,
            this.AutoplayerToolStrip,
            this.HelpToolStrip});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(633, 24);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileToolStrip
            // 
            this.FileToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveMenuItem,
            this.LoadMenuItem,
            this.ExportMenuItem,
            this.ImportMenuItem,
            this.ExitMenuItem});
            this.FileToolStrip.Name = "FileToolStrip";
            this.FileToolStrip.Size = new System.Drawing.Size(37, 20);
            this.FileToolStrip.Text = "File";
            // 
            // SaveMenuItem
            // 
            this.SaveMenuItem.Name = "SaveMenuItem";
            this.SaveMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SaveMenuItem.Text = "Save";
            // 
            // LoadMenuItem
            // 
            this.LoadMenuItem.Name = "LoadMenuItem";
            this.LoadMenuItem.Size = new System.Drawing.Size(152, 22);
            this.LoadMenuItem.Text = "Load";
            // 
            // ExportMenuItem
            // 
            this.ExportMenuItem.Name = "ExportMenuItem";
            this.ExportMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExportMenuItem.Text = "Export";
            this.ExportMenuItem.Click += new System.EventHandler(this.ExportMenuItem_Click);
            // 
            // ImportMenuItem
            // 
            this.ImportMenuItem.Name = "ImportMenuItem";
            this.ImportMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ImportMenuItem.Text = "Import";
            this.ImportMenuItem.Click += new System.EventHandler(this.ImportMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExitMenuItem.Text = "Exit";
            // 
            // ErrorTextBox
            // 
            this.ErrorTextBox.ForeColor = System.Drawing.Color.Red;
            this.ErrorTextBox.Location = new System.Drawing.Point(0, 297);
            this.ErrorTextBox.Name = "ErrorTextBox";
            this.ErrorTextBox.ReadOnly = true;
            this.ErrorTextBox.Size = new System.Drawing.Size(541, 51);
            this.ErrorTextBox.TabIndex = 9;
            this.ErrorTextBox.Text = "ERROR: Something went wrong!";
            // 
            // AutoplayerToolStrip
            // 
            this.AutoplayerToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearMenuItem,
            this.PlayMenuItem,
            this.StopMenuItem,
            this.LoopMenuItem});
            this.AutoplayerToolStrip.Name = "AutoplayerToolStrip";
            this.AutoplayerToolStrip.Size = new System.Drawing.Size(77, 20);
            this.AutoplayerToolStrip.Text = "Autoplayer";
            // 
            // ClearMenuItem
            // 
            this.ClearMenuItem.Name = "ClearMenuItem";
            this.ClearMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ClearMenuItem.Text = "Clear Notes";
            // 
            // PlayMenuItem
            // 
            this.PlayMenuItem.Name = "PlayMenuItem";
            this.PlayMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PlayMenuItem.Text = "Play Song (F2)";
            // 
            // StopMenuItem
            // 
            this.StopMenuItem.Name = "StopMenuItem";
            this.StopMenuItem.Size = new System.Drawing.Size(152, 22);
            this.StopMenuItem.Text = "Stop Song (F3)";
            // 
            // LoopMenuItem
            // 
            this.LoopMenuItem.Name = "LoopMenuItem";
            this.LoopMenuItem.Size = new System.Drawing.Size(152, 22);
            this.LoopMenuItem.Text = "Loop Song";
            // 
            // HelpToolStrip
            // 
            this.HelpToolStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpMenuItem,
            this.AutoplayerMenuItem});
            this.HelpToolStrip.Name = "HelpToolStrip";
            this.HelpToolStrip.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStrip.Text = "Help";
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(188, 22);
            this.HelpMenuItem.Text = "Documentation";
            // 
            // AutoplayerMenuItem
            // 
            this.AutoplayerMenuItem.Name = "AutoplayerMenuItem";
            this.AutoplayerMenuItem.Size = new System.Drawing.Size(188, 22);
            this.AutoplayerMenuItem.Text = "About the Autoplayer";
            // 
            // MusicPiecesTabButton
            // 
            this.MusicPiecesTabButton.Location = new System.Drawing.Point(4, 22);
            this.MusicPiecesTabButton.Name = "MusicPiecesTabButton";
            this.MusicPiecesTabButton.Size = new System.Drawing.Size(635, 354);
            this.MusicPiecesTabButton.TabIndex = 3;
            this.MusicPiecesTabButton.Text = "Music Pieces (W.I.P)";
            this.MusicPiecesTabButton.UseVisualStyleBackColor = true;
            // 
            // PlayTogetherTabButton
            // 
            this.PlayTogetherTabButton.Controls.Add(this.label9);
            this.PlayTogetherTabButton.Location = new System.Drawing.Point(4, 22);
            this.PlayTogetherTabButton.Name = "PlayTogetherTabButton";
            this.PlayTogetherTabButton.Size = new System.Drawing.Size(635, 354);
            this.PlayTogetherTabButton.TabIndex = 4;
            this.PlayTogetherTabButton.Text = "Doesn\'t look like anything to me...";
            this.PlayTogetherTabButton.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label9.Location = new System.Drawing.Point(27, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(577, 25);
            this.label9.TabIndex = 1;
            this.label9.Text = "Seriously, there\'s nothing here.. Why did you even click this tab?..";
            // 
            // GraphicalUserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 395);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.MenuStrip);
            this.Name = "GraphicalUserInterface";
            this.Text = "Tower Unite Instrument Autoplayer";
            this.tabControl1.ResumeLayout(false);
            this.MainTabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.SettingsTabPage.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FastDelayBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NormalDelayBox)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.CustomizeTabButton.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomDelayTimeBox)).EndInit();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.PlayTogetherTabButton.ResumeLayout(false);
            this.PlayTogetherTabButton.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.CheckBox LoopCheckBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox StopKeyTextBox;
        private System.Windows.Forms.TextBox StartKeyTextBox;
        private System.Windows.Forms.Label StopKeyLabel;
        private System.Windows.Forms.Label StartKeyLabel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown FastDelayBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown NormalDelayBox;
        private System.Windows.Forms.Button RemoveAllDelayButton;
        private System.Windows.Forms.TextBox CustomNoteNewCharacterBox;
        private System.Windows.Forms.Button RemoveAllNotesButton;
        private System.Windows.Forms.TextBox CustomNoteListBox;
        private System.Windows.Forms.Button RemoveNoteButton;
        private System.Windows.Forms.Button AddNoteButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox CustomNoteCharacterBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox ErrorTextBox;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStrip;
        private System.Windows.Forms.ToolStripMenuItem SaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.TabPage MusicPiecesTabButton;
        private System.Windows.Forms.TabPage PlayTogetherTabButton;
        private System.Windows.Forms.ToolStripMenuItem AutoplayerToolStrip;
        private System.Windows.Forms.ToolStripMenuItem ClearMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlayMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoopMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStrip;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoplayerMenuItem;
        private System.Windows.Forms.Label label9;
    }
}

