namespace StatusRemote
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.BtnToggleServer = new System.Windows.Forms.Button();
            this.BtnOpenImageFolder = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxLog
            // 
            this.textBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLog.Location = new System.Drawing.Point(12, 12);
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(776, 21);
            this.textBoxLog.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 39);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(508, 506);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // BtnToggleServer
            // 
            this.BtnToggleServer.BackColor = System.Drawing.Color.MediumAquamarine;
            this.BtnToggleServer.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnToggleServer.Location = new System.Drawing.Point(-4, -6);
            this.BtnToggleServer.Name = "BtnToggleServer";
            this.BtnToggleServer.Size = new System.Drawing.Size(110, 110);
            this.BtnToggleServer.TabIndex = 2;
            this.BtnToggleServer.Text = "Запустить";
            this.BtnToggleServer.UseVisualStyleBackColor = false;
            this.BtnToggleServer.Click += new System.EventHandler(this.BtnToggleServer_Click);
            this.BtnToggleServer.MouseEnter += new System.EventHandler(this.BtnToggleServer_MouseEnter);
            this.BtnToggleServer.MouseLeave += new System.EventHandler(this.BtnToggleServer_MouseLeave);
            // 
            // BtnOpenImageFolder
            // 
            this.BtnOpenImageFolder.BackColor = System.Drawing.Color.MediumAquamarine;
            this.BtnOpenImageFolder.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnOpenImageFolder.Location = new System.Drawing.Point(-5, -5);
            this.BtnOpenImageFolder.Name = "BtnOpenImageFolder";
            this.BtnOpenImageFolder.Size = new System.Drawing.Size(110, 60);
            this.BtnOpenImageFolder.TabIndex = 3;
            this.BtnOpenImageFolder.Text = "Папка";
            this.BtnOpenImageFolder.UseVisualStyleBackColor = false;
            this.BtnOpenImageFolder.Click += new System.EventHandler(this.BtnOpenImageFolder_Click);
            this.BtnOpenImageFolder.MouseEnter += new System.EventHandler(this.BtnOpenImageFolder_MouseEnter);
            this.BtnOpenImageFolder.MouseLeave += new System.EventHandler(this.BtnOpenImageFolder_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.BtnToggleServer);
            this.panel1.Location = new System.Drawing.Point(688, 445);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 100);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.BtnOpenImageFolder);
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Location = new System.Drawing.Point(688, 389);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(100, 50);
            this.panel2.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(800, 557);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBoxLog);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "Form1";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button BtnToggleServer;
        private System.Windows.Forms.Button BtnOpenImageFolder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

