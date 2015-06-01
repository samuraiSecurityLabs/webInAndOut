using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace webAppInAndOutAnalyse
{
    class Analyse
    {

        private string req;//请求

        private Resolve cr;//解析器

        public Resolve Cr
        {
            get { return cr; }
            set { cr = value; }
        }

        private HttpWebResponse rspo;//响应

        private string rspohtml;

        public string Rspohtml
        {
            get { return rspohtml; }
            set { rspohtml = value; }
        }

        private string rspoheader;

        public string Rspoheader
        {
            get { return rspoheader; }
            set { rspoheader = value; }
        }

        public Analyse(string req,Resolve cr)//原始请求和解析类作为参数传递，接下来，交给解析类
        {
            this.req = req;

            this.cr = cr;

            cr.ResolveHttpRequest(this.req);//请求的原文交给解析类处理，解析完毕数据。

            this.rspohtml = "";
            
            Console.WriteLine("Start to analyse!");
        }

        //把整个请求的输入点都分析到。包括cookie的解析等等，参数记得调用htmldecode还原一下

        public Dictionary<string, string> ResponseAnalysis()//分析当前的响应，仅仅针对于显示的能够直接在源码中看到。
        {
            Dictionary<string, string> outcome = new Dictionary<string, string>();
            //直接response搜索输入点

            HttpWebRequest req;
            //如果是发送HTTPS请求  
            if (Cr.Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                
                req = (HttpWebRequest)WebRequest.Create(Cr.Url);
                
                req.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                req = (HttpWebRequest)WebRequest.Create(Cr.Url);
            } 

            req.Method = Cr.Method;

            req.Host = Cr.Host;

            req.UserAgent = Cr.Useragent;

            //req.ContentType = cr.Contenttype;

            req.Referer = Cr.Referer;

            req.AllowAutoRedirect = false;

            req.ContentLength = Convert.ToInt16(Cr.Contentlength);

            CookieContainer cc = new CookieContainer();

            req.CookieContainer = cc;

            foreach (KeyValuePair<string, string> keys in Cr.CookieList)//这里有异常cookie的隐患
            {
                try
                {
                    cc.Add(new Uri(Cr.Url), new Cookie(keys.Key, keys.Value));//这里会new特别多的cookie对象，必须后续优化掉！
                }
                catch (Exception e)
                {
                    //要捕捉下，否则报错。因为COOKIE有限制
                }
            }

            HttpWebResponse rspo = (HttpWebResponse)req.GetResponse();

            this.rspoheader = rspo.StatusCode.ToString() + "\r\n" + rspo.Headers.ToString();//没有对头部做合适解析

            Stream responseStream = rspo.GetResponseStream();

            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);

            string html = streamReader.ReadToEnd();

            this.rspohtml = html;

            foreach (KeyValuePair<string, string> keys in Cr.Headerpars)//查找的效率较低，要优化
            {
                if (html.IndexOf(keys.Value)>0) 
                { 
                    outcome.Add("Header"+keys.Key,keys.Value);
                }
            }

            foreach (KeyValuePair<string, string> keys in Cr.Bodypars)
            {
                if (html.IndexOf(keys.Value) > 0)
                {
                    outcome.Add("Body"+ keys.Key , keys.Value);
                }
            }

            foreach (KeyValuePair<string, string> keys in Cr.CookieList)
            {
                if (html.IndexOf(keys.Value) > 0)
                {
                    outcome.Add("Cookie"+ keys.Key , keys.Value);
                }
            }

            return outcome;
        }

        public void ExternalResourceInResponse(string response)//当前响应中的外部资源，除图片之外的全部抓到
        { 
            //js,iframe,html,动态页等等
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }  


    }
}
