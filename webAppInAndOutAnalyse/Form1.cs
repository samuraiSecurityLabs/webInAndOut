using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


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
                
                string tmp = "";
                
                Resolve cr= new Resolve();
                
                cr.ResolveHttpRequest(content);
                
                resourceAnalyse.ForeColor = Color.Red;
                //resourceAnalyse.Text = cr.Url + "\r\n" + cr.Host ;

                resourceAnalyse.Text += "cookie参数：\r\n";

                foreach (KeyValuePair<string, string> keys in cr.CookieList)
                {
                    tmp = tmp + keys.Key + "="+ keys.Value + "\r\n";
                }

                resourceAnalyse.Text += tmp;

                tmp = "";

                resourceAnalyse.Text += "header参数：\r\n";

                foreach (KeyValuePair<string, string> keys in cr.Headerpars)
                {
                    tmp = tmp + keys.Key + "=" + keys.Value + "\r\n";
                }

                resourceAnalyse.Text += tmp;

                tmp = "";

                Dictionary<string, string> tmp1 = cr.Bodypars;


                resourceAnalyse.Text += "body参数：\r\n";

                foreach (KeyValuePair<string, string> keys in tmp1)
                {
                    tmp = tmp + keys.Key + "=" + keys.Value + "\r\n";
                }
                resourceAnalyse.Text += tmp;
            }
        }

        private void clearRequest_Click(object sender, EventArgs e)
        {
            this.httpRequestContent.Text = "";
        }
    }
}
