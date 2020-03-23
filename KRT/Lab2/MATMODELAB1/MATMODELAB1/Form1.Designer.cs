namespace MATMODELAB1
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
            this.ResultBox = new System.Windows.Forms.ListBox();
            this.AnswerTextBox = new System.Windows.Forms.TextBox();
            this.NameLabke = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // ResultBox
            // 
            this.ResultBox.FormattingEnabled = true;
            this.ResultBox.ItemHeight = 16;
            this.ResultBox.Location = new System.Drawing.Point(88, 33);
            this.ResultBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.Size = new System.Drawing.Size(262, 244);
            this.ResultBox.TabIndex = 0;
            this.ResultBox.SelectedIndexChanged += new System.EventHandler(this.ResultBox_SelectedIndexChanged);
            // 
            // AnswerTextBox
            // 
            this.AnswerTextBox.Location = new System.Drawing.Point(458, 30);
            this.AnswerTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AnswerTextBox.Multiline = true;
            this.AnswerTextBox.Name = "AnswerTextBox";
            this.AnswerTextBox.Size = new System.Drawing.Size(225, 241);
            this.AnswerTextBox.TabIndex = 1;
            // 
            // NameLabke
            // 
            this.NameLabke.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.NameLabke.Location = new System.Drawing.Point(9, -4);
            this.NameLabke.Name = "NameLabke";
            this.NameLabke.Size = new System.Drawing.Size(443, 35);
            this.NameLabke.TabIndex = 2;
            this.NameLabke.Text = "МКМ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 302);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 45);
            this.button1.TabIndex = 3;
            this.button1.Text = "MMM/MKM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.NameLabke);
            this.Controls.Add(this.AnswerTextBox);
            this.Controls.Add(this.ResultBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ResultBox;
        private System.Windows.Forms.TextBox AnswerTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label NameLabke;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}