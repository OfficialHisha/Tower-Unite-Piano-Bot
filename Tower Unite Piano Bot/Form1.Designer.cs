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
            this.NotesLabel = new System.Windows.Forms.Label();
            this.PlayButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NormalDelayBox = new System.Windows.Forms.NumericUpDown();
            this.FastDelayBox = new System.Windows.Forms.NumericUpDown();
            this.StopButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NormalDelayBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FastDelayBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SongTextBox
            // 
            this.SongTextBox.Location = new System.Drawing.Point(12, 24);
            this.SongTextBox.Multiline = true;
            this.SongTextBox.Name = "SongTextBox";
            this.SongTextBox.Size = new System.Drawing.Size(410, 266);
            this.SongTextBox.TabIndex = 0;
            // 
            // NotesLabel
            // 
            this.NotesLabel.AutoSize = true;
            this.NotesLabel.Location = new System.Drawing.Point(12, 9);
            this.NotesLabel.Name = "NotesLabel";
            this.NotesLabel.Size = new System.Drawing.Size(38, 13);
            this.NotesLabel.TabIndex = 1;
            this.NotesLabel.Text = "Notes:";
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(330, 295);
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
            this.label1.Location = new System.Drawing.Point(9, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Delay for normal:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Delay for fast:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 299);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Milliseconds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 322);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Milliseconds";
            // 
            // NormalDelayBox
            // 
            this.NormalDelayBox.Location = new System.Drawing.Point(101, 295);
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
            this.FastDelayBox.Location = new System.Drawing.Point(101, 320);
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
            this.StopButton.Location = new System.Drawing.Point(232, 295);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(92, 54);
            this.StopButton.TabIndex = 11;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 361);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.FastDelayBox);
            this.Controls.Add(this.NormalDelayBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.NotesLabel);
            this.Controls.Add(this.SongTextBox);
            this.Name = "Form1";
            this.Text = "Tower Unite Piano Bot";
            ((System.ComponentModel.ISupportInitialize)(this.NormalDelayBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FastDelayBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SongTextBox;
        private System.Windows.Forms.Label NotesLabel;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown NormalDelayBox;
        private System.Windows.Forms.NumericUpDown FastDelayBox;
        private System.Windows.Forms.Button StopButton;
    }
}

