namespace BankBotWithGpt
{
    partial class BankBotMainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelReq = new System.Windows.Forms.Label();
            this.labelAnsw = new System.Windows.Forms.Label();
            this.textBoxReq = new System.Windows.Forms.TextBox();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.buttonGet = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelReq
            // 
            this.labelReq.AutoSize = true;
            this.labelReq.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.labelReq.Location = new System.Drawing.Point(28, 10);
            this.labelReq.Name = "labelReq";
            this.labelReq.Size = new System.Drawing.Size(80, 28);
            this.labelReq.TabIndex = 0;
            this.labelReq.Text = "Вопрос:";
            this.labelReq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAnsw
            // 
            this.labelAnsw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAnsw.AutoSize = true;
            this.labelAnsw.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.labelAnsw.Location = new System.Drawing.Point(28, 96);
            this.labelAnsw.Name = "labelAnsw";
            this.labelAnsw.Size = new System.Drawing.Size(141, 28);
            this.labelAnsw.TabIndex = 1;
            this.labelAnsw.Text = "Ответ Бота:";
            this.labelAnsw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxReq
            // 
            this.textBoxReq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReq.Location = new System.Drawing.Point(28, 41);
            this.textBoxReq.Name = "textBoxReq";
            this.textBoxReq.Size = new System.Drawing.Size(743, 31);
            this.textBoxReq.TabIndex = 2;
            this.textBoxReq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxReq_KeyDown);
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAnswer.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxAnswer.Location = new System.Drawing.Point(28, 127);
            this.textBoxAnswer.Multiline = true;
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.ReadOnly = true;
            this.textBoxAnswer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxAnswer.Size = new System.Drawing.Size(743, 221);
            this.textBoxAnswer.TabIndex = 3;
            // 
            // buttonGet
            // 
            this.buttonGet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGet.Location = new System.Drawing.Point(438, 366);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(333, 53);
            this.buttonGet.TabIndex = 4;
            this.buttonGet.Text = "Задать вопрос";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClear.Location = new System.Drawing.Point(28, 366);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(333, 53);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // BankBotMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonGet);
            this.Controls.Add(this.textBoxAnswer);
            this.Controls.Add(this.textBoxReq);
            this.Controls.Add(this.labelAnsw);
            this.Controls.Add(this.labelReq);
            this.MinimumSize = new System.Drawing.Size(750, 400);
            this.Name = "BankBotMainForm";
            this.Text = "Приложение NGBank";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelReq;
        private Label labelAnsw;
        private TextBox textBoxReq;
        private TextBox textBoxAnswer;
        private Button buttonGet;
        private Button buttonClear;
    }
}