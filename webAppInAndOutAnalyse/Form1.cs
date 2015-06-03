using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;


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
            this.resourceAnalyse.Text = "";

            if (this.httpRequestContent.Text!="" && this.httpRequestContent.Text!="粘贴觉得可疑的HTTP请求至此" )
            {
                //输入输出点分析

                findinandout.Text = "";

                string tmp = "";

                string content = httpRequestContent.Text;

                Fuzzing fuz = new Fuzzing();

                Analyse ay = fuz.FuzzingResponse(content);
  
                //资源分析

                resourceAnalyse.ForeColor = Color.Red;
                //resourceAnalyse.Text =ay.Cr.Url + "\r\n" + ay.Cr.Host ;

                resourceAnalyse.Text += "cookie参数:(" + ay.Cr.CookieList.Count + ")\r\n";

                foreach (KeyValuePair<string, string> keys in ay.Cr.CookieList)
                {
                    tmp = tmp + keys.Key + "="+ keys.Value + "\r\n";
                }

                resourceAnalyse.Text += tmp;

                tmp = "";

                resourceAnalyse.Text += "header参数:(" + ay.Cr.Headerpars.Count + ")\r\n";

                foreach (KeyValuePair<string, string> keys in ay.Cr.Headerpars)
                {
                    tmp = tmp + keys.Key + "=" + keys.Value + "\r\n";
                }

                resourceAnalyse.Text += tmp;

                tmp = "";

                Dictionary<string, string> tmp1 = ay.Cr.Bodypars;

                resourceAnalyse.Text += "body参数:("+ ay.Cr.Bodypars.Count +")\r\n";

                foreach (KeyValuePair<string, string> keys in tmp1)
                {
                    tmp = tmp + keys.Key + "=" + keys.Value + "\r\n";
                }

                resourceAnalyse.Text += tmp;

                tmp = "";

                findinandout.Text += "显式参数：\r\n";

                foreach (KeyValuePair<string, string> keys in ay.Extrinsicelements)
                {
                    tmp = tmp + keys.Key + "=" +keys.Value + "\r\n";
                }

                findinandout.Text += tmp;

                findinandout.Text += "隐式参数：\r\n";

                foreach (KeyValuePair<int, string> keys in ay.Implicitelements)
                {
                    tmp = tmp + keys.Key + "=" + keys.Value + "\r\n";
                }

                findinandout.Text += tmp;

                //响应内容

                httpresponse.Text = "";

                httpresponse.Text = ay.Rspohtml;

                //响应头部

                httpresponseheaders.Text = "";

                httpresponseheaders.Text = ay.Rspoheader;

            }
        }

        private void clearRequest_Click(object sender, EventArgs e)
        {
            this.httpRequestContent.Text = "";
        }
    }
}
