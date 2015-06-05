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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.httpRequestContent = new System.Windows.Forms.TextBox();
            this.sendRequest = new System.Windows.Forms.Button();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.resourceAnalyse = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.findinandout = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.httpresponse = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.httpresponseheaders = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.savaRequest = new System.Windows.Forms.Button();
            this.clearRequest = new System.Windows.Forms.Button();
            this.tab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // httpRequestContent
            // 
            this.httpRequestContent.Location = new System.Drawing.Point(13, 12);
            this.httpRequestContent.Multiline = true;
            this.httpRequestContent.Name = "httpRequestContent";
            this.httpRequestContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
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
            // tab
            // 
            this.tab.Controls.Add(this.tabPage1);
            this.tab.Controls.Add(this.tabPage2);
            this.tab.Controls.Add(this.tabPage3);
            this.tab.Controls.Add(this.tabPage4);
            this.tab.Controls.Add(this.tabPage5);
            this.tab.Location = new System.Drawing.Point(12, 246);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(501, 247);
            this.tab.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.resourceAnalyse);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(493, 221);
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
            this.resourceAnalyse.Size = new System.Drawing.Size(481, 209);
            this.resourceAnalyse.TabIndex = 0;
            this.resourceAnalyse.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.findinandout);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(493, 221);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "输入输出点";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // findinandout
            // 
            this.findinandout.ForeColor = System.Drawing.Color.Red;
            this.findinandout.Location = new System.Drawing.Point(7, 7);
            this.findinandout.Multiline = true;
            this.findinandout.Name = "findinandout";
            this.findinandout.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.findinandout.Size = new System.Drawing.Size(480, 208);
            this.findinandout.TabIndex = 0;
            this.findinandout.WordWrap = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.httpresponse);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(493, 221);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "响应内容";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // httpresponse
            // 
            this.httpresponse.Location = new System.Drawing.Point(4, 7);
            this.httpresponse.Multiline = true;
            this.httpresponse.Name = "httpresponse";
            this.httpresponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.httpresponse.Size = new System.Drawing.Size(483, 208);
            this.httpresponse.TabIndex = 0;
            this.httpresponse.WordWrap = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.httpresponseheaders);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(493, 221);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "响应头部";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // httpresponseheaders
            // 
            this.httpresponseheaders.Location = new System.Drawing.Point(7, 7);
            this.httpresponseheaders.Multiline = true;
            this.httpresponseheaders.Name = "httpresponseheaders";
            this.httpresponseheaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.httpresponseheaders.Size = new System.Drawing.Size(480, 208);
            this.httpresponseheaders.TabIndex = 0;
            this.httpresponseheaders.WordWrap = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.richTextBox1);
            this.tabPage5.Controls.Add(this.label1);
            this.tabPage5.Controls.Add(this.button3);
            this.tabPage5.Controls.Add(this.textBox1);
            this.tabPage5.Controls.Add(this.button2);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(493, 221);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "PayLoad";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 75);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(484, 140);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "现有Payload";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(440, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(47, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "保存";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(427, 48);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(440, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "删除";
            this.button2.UseVisualStyleBackColor = true;
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
            this.Controls.Add(this.tab);
            this.Controls.Add(this.sendRequest);
            this.Controls.Add(this.httpRequestContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.Text = "跨站辅助 SamuraiSecurityLabs";
            this.tab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox httpRequestContent;
        private System.Windows.Forms.Button sendRequest;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox resourceAnalyse;
        private System.Windows.Forms.Button savaRequest;
        private System.Windows.Forms.Button clearRequest;
        private System.Windows.Forms.TextBox findinandout;
        private System.Windows.Forms.TextBox httpresponse;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox httpresponseheaders;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
    }
}

