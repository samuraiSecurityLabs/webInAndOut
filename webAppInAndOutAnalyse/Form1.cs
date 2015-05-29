using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace webAppInAndOutAnalyse
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.httpRequestContent.Text!="" && this.httpRequestContent.Text!="粘贴觉得可疑的HTTP请求至此" )
            {
                string content = httpRequestContent.Text;
                Resolve cr= new Resolve();
                cr.ResolveHttpRequest(content);
                resourceAnalyse.ForeColor = Color.Red;
                resourceAnalyse.Text = cr.Url + "\r\n" + cr.Host;

            }
        }

        private void clearRequest_Click(object sender, EventArgs e)
        {
            this.httpRequestContent.Text = "";
        }
    }
}
