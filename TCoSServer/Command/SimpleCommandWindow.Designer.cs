namespace TCoSServer.Command
{
    partial class SimpleCommandWindow
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
            this.commandBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // commandBox
            // 
            this.commandBox.Location = new System.Drawing.Point(12, 21);
            this.commandBox.Multiline = true;
            this.commandBox.Name = "commandBox";
            this.commandBox.Size = new System.Drawing.Size(546, 346);
            this.commandBox.TabIndex = 0;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(412, 437);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // SimpleCommandWindow
            // 
            this.ClientSize = new System.Drawing.Size(570, 510);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.commandBox);
            this.Name = "SimpleCommandWindow";
            this.Load += new System.EventHandler(this.SimpleCommandWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox commandBox;
        private System.Windows.Forms.Button sendButton;
    }
}