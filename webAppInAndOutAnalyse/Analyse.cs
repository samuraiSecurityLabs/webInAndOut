using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using HtmlAgilityPack;

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

        private Dictionary<int, string> implicitelements;

        public Dictionary<int, string> Implicitelements
        {
            get { return implicitelements; }
            set { implicitelements = value; }
        }



        public Analyse(string req,Resolve cr)//原始请求和解析类作为参数传递，接下来，交给解析类
        {
            this.req = req;

            this.cr = cr;

            cr.ResolveHttpRequest(this.req);//请求的原文交给解析类处理，解析完毕数据。

            this.rspohtml = "";
            
            Console.WriteLine("Start to analyse!");

            implicitelements = new Dictionary<int, string>();
        }

        //把整个请求的输入点都分析到。包括cookie的解析等等，参数记得调用htmldecode还原一下

        public Dictionary<string, string> ResponseAnalysis()//分析当前的响应，仅仅针对于显示的能够直接在源码中看到。
        {
            Dictionary<string, string> outcome = new Dictionary<string, string>();

            GetHttpResponse(Cr);//初始化了rspohtml
            
            foreach (KeyValuePair<string, string> keys in Cr.Headerpars)//查找的效率较低，要优化
            {
                if (!keys.Value.Equals(String.Empty) && this.rspohtml.IndexOf(keys.Value) >= 0)
                {
                    outcome.Add("Header" + keys.Key, keys.Value);
                }
            }

            foreach (KeyValuePair<string, string> keys in Cr.Bodypars)
            {
                if (!keys.Value.Equals(String.Empty) && this.rspohtml.IndexOf(keys.Value) >= 0)
                {
                    outcome.Add("Body" + keys.Key, keys.Value);
                }
            }

            foreach (KeyValuePair<string, string> keys in Cr.CookieList)
            {
                //if (!keys.Value.Equals(String.Empty) && this.rspohtml.IndexOf(keys.Value) >= 0)
                //{
                //    outcome.Add("Cookie" + keys.Key, keys.Value);
                //}
            }

            HtmlDocument htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(this.rspohtml);

            int i = 0;

            foreach (var script in htmlDoc.DocumentNode.Descendants("script").ToArray())//script
            {
                implicitelements.Add(i, script.OuterHtml);

                i++;
            }

            foreach (var script in htmlDoc.DocumentNode.Descendants("link").ToArray())//css
            {
                implicitelements.Add(i, script.OuterHtml);

                i++;
            }

            foreach (var script in htmlDoc.DocumentNode.Descendants("embed").ToArray())//swf
            {
                implicitelements.Add(i, script.OuterHtml);//隐式结果

                i++;
            }

            return outcome;//显式结果
        }

        public void ExternalResourceInResponse(string response)//当前响应中的外部资源，除图片之外的全部抓到
        { 
            //js,iframe,html,动态页等等
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        private HttpWebResponse GetHttpResponse(Resolve Cr)
        {
            //直接response搜索输入点

            HttpWebRequest req;

            //如果是发送HTTPS请求  

            if (Cr.Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                req = (HttpWebRequest)WebRequest.Create(Cr.Url);

                req.ProtocolVersion = HttpVersion.Version10;

                req.Host = Cr.Host;

            }
            else
            {
                req = (HttpWebRequest)WebRequest.Create(Cr.Url);

                req.Host = Cr.Host;

            }

            //Encoding encoding = Encoding.UTF8;

            req.UserAgent = Cr.Useragent;

            req.Method = Cr.Method;

            req.Accept = "text/html, application/xhtml+xml, */*";

            //req.TransferEncoding = "gzip, deflate";

            if (Cr.Method.Equals("POST"))
            { 
                 req.ContentType = Cr.Contenttype;

                 req.Timeout = 20000;
 
                 byte[] btBodys = Encoding.UTF8.GetBytes(Cr.Body);
                 
                 req.ContentLength = Convert.ToInt16(Cr.Contentlength);
                 
                 req.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            }



            //req.ContentType = cr.Contenttype;

            req.Referer = Cr.Referer;

            req.AllowAutoRedirect = false;

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

            return rspo;
        }

    }
}
