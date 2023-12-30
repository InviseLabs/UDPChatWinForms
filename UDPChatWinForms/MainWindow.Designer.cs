namespace UDPChatWinForms
{
    partial class MainWindow
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
            this.chatField = new System.Windows.Forms.RichTextBox();
            this.MsgBox = new System.Windows.Forms.TextBox();
            this.SendBtn = new System.Windows.Forms.Button();
            this.nameTBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.chatField.Location = new System.Drawing.Point(13, 54);
            this.chatField.Margin = new System.Windows.Forms.Padding(6);
            this.chatField.Name = "richTextBox1";
            this.chatField.Size = new System.Drawing.Size(961, 475);
            this.chatField.TabIndex = 0;
            this.chatField.Text = "";
            // 
            // MsgBox
            // 
            this.MsgBox.Location = new System.Drawing.Point(13, 543);
            this.MsgBox.Margin = new System.Windows.Forms.Padding(6);
            this.MsgBox.Multiline = true;
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.Size = new System.Drawing.Size(746, 85);
            this.MsgBox.TabIndex = 1;
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(774, 572);
            this.SendBtn.Margin = new System.Windows.Forms.Padding(6);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(204, 59);
            this.SendBtn.TabIndex = 2;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.nameTBox.Location = new System.Drawing.Point(13, 6);
            this.nameTBox.Margin = new System.Windows.Forms.Padding(6);
            this.nameTBox.Name = "textBox2";
            this.nameTBox.Size = new System.Drawing.Size(961, 29);
            this.nameTBox.TabIndex = 3;
            this.nameTBox.Text = "Username1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 642);
            this.Controls.Add(this.nameTBox);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.MsgBox);
            this.Controls.Add(this.chatField);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Main";
            this.Text = "UDP Chat Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox chatField;
        private System.Windows.Forms.TextBox MsgBox;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.TextBox nameTBox;
    }
}

