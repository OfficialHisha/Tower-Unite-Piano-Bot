namespace Tower_Unite_Instrument_Autoplayer.GUI
{
    partial class DocumentationViewer
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
            this.TopicListBox = new System.Windows.Forms.ListBox();
            this.InfoTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // TopicListBox
            // 
            this.TopicListBox.FormattingEnabled = true;
            this.TopicListBox.Location = new System.Drawing.Point(1, 0);
            this.TopicListBox.Name = "TopicListBox";
            this.TopicListBox.Size = new System.Drawing.Size(177, 589);
            this.TopicListBox.TabIndex = 0;
            this.TopicListBox.SelectedIndexChanged += new System.EventHandler(this.TopicListBox_SelectedIndexChanged);
            // 
            // InfoTextBox
            // 
            this.InfoTextBox.Location = new System.Drawing.Point(184, 0);
            this.InfoTextBox.Name = "InfoTextBox";
            this.InfoTextBox.Size = new System.Drawing.Size(543, 589);
            this.InfoTextBox.TabIndex = 1;
            this.InfoTextBox.Text = "";
            // 
            // DocumentationViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 589);
            this.Controls.Add(this.InfoTextBox);
            this.Controls.Add(this.TopicListBox);
            this.Name = "DocumentationViewer";
            this.Text = "DocumentationViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox TopicListBox;
        private System.Windows.Forms.RichTextBox InfoTextBox;
    }
}