namespace webAppInAndOutAnalyse
{
    partial class mainForm
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
            this.httpRequestContent = new System.Windows.Forms.TextBox();
            this.sendRequest = new System.Windows.Forms.Button();
            this.page = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.resourceAnalyse = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.savaRequest = new System.Windows.Forms.Button();
            this.clearRequest = new System.Windows.Forms.Button();
            this.page.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // httpRequestContent
            // 
            this.httpRequestContent.Location = new System.Drawing.Point(13, 12);
            this.httpRequestContent.Multiline = true;
            this.httpRequestContent.Name = "httpRequestContent";
            this.httpRequestContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.httpRequestContent.Size = new System.Drawing.Size(501, 189);
            this.httpRequestContent.TabIndex = 0;
            this.httpRequestContent.Text = "粘贴觉得可疑的HTTP请求至此";
            this.httpRequestContent.WordWrap = false;
            // 
            // sendRequest
            // 
            this.sendRequest.Location = new System.Drawing.Point(12, 217);
            this.sendRequest.Name = "sendRequest";
            this.sendRequest.Size = new System.Drawing.Size(75, 23);
            this.sendRequest.TabIndex = 1;
            this.sendRequest.Text = "发送请求";
            this.sendRequest.UseVisualStyleBackColor = true;
            this.sendRequest.Click += new System.EventHandler(this.button1_Click);
            // 
            // page
            // 
            this.page.Controls.Add(this.tabPage1);
            this.page.Controls.Add(this.tabPage2);
            this.page.Controls.Add(this.tabPage3);
            this.page.Location = new System.Drawing.Point(12, 291);
            this.page.Name = "page";
            this.page.SelectedIndex = 0;
            this.page.Size = new System.Drawing.Size(501, 202);
            this.page.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.resourceAnalyse);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(493, 176);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "资源分析";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // resourceAnalyse
            // 
            this.resourceAnalyse.Location = new System.Drawing.Point(6, 6);
            this.resourceAnalyse.Multiline = true;
            this.resourceAnalyse.Name = "resourceAnalyse";
            this.resourceAnalyse.ReadOnly = true;
            this.resourceAnalyse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resourceAnalyse.Size = new System.Drawing.Size(481, 157);
            this.resourceAnalyse.TabIndex = 0;
            this.resourceAnalyse.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(493, 176);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "输入输出点";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(493, 176);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "响应内容";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // savaRequest
            // 
            this.savaRequest.Location = new System.Drawing.Point(94, 217);
            this.savaRequest.Name = "savaRequest";
            this.savaRequest.Size = new System.Drawing.Size(75, 23);
            this.savaRequest.TabIndex = 3;
            this.savaRequest.Text = "保存请求";
            this.savaRequest.UseVisualStyleBackColor = true;
            // 
            // clearRequest
            // 
            this.clearRequest.Location = new System.Drawing.Point(175, 217);
            this.clearRequest.Name = "clearRequest";
            this.clearRequest.Size = new System.Drawing.Size(75, 23);
            this.clearRequest.TabIndex = 4;
            this.clearRequest.Text = "清空";
            this.clearRequest.UseVisualStyleBackColor = true;
            this.clearRequest.Click += new System.EventHandler(this.clearRequest_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 505);
            this.Controls.Add(this.clearRequest);
            this.Controls.Add(this.savaRequest);
            this.Controls.Add(this.page);
            this.Controls.Add(this.sendRequest);
            this.Controls.Add(this.httpRequestContent);
            this.Name = "mainForm";
            this.Text = "跨站辅助 samuraiSecurityLabs";
            this.page.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox httpRequestContent;
        private System.Windows.Forms.Button sendRequest;
        private System.Windows.Forms.TabControl page;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox resourceAnalyse;
        private System.Windows.Forms.Button savaRequest;
        private System.Windows.Forms.Button clearRequest;
    }
}

