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
using System.Text.RegularExpressions;

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

        private string rspoheader;//比对头部知道参数变化后请求的差异性

        public string Rspoheader
        {
            get { return rspoheader; }

            set { rspoheader = value; }
        }

        public Analyse(string req,Resolve cr)//原始请求和解析类作为参数传递，接下来，交给解析类
        {
            this.req = req;

            this.cr = cr;

            cr.ResolveHttpRequest(this.req);//请求的原文交给解析类处理，解析完毕数据完成第一次初始化。

            this.rspohtml = "";
            
            Console.WriteLine("Start to analyse!");
        }

        public void ExternalResourceInResponse(string response)//当前响应中的外部资源，除图片之外的全部抓到
        { 
            //js,iframe,html,动态页等等
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        public HttpWebResponse GetHttpResponse(Resolve Cr)//解析器把URL请求解析得清楚
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

            req.Referer = Cr.Referer;

            req.AllowAutoRedirect = false;

            //req.TransferEncoding = "gzip, deflate";

            req.Headers.Add("Cookie:" + Cr.Cookie); //不加这句，POST请求没有COOKIE字段 

            if (Cr.Method.Equals("POST"))
            { 
                 req.ContentType = Cr.Contenttype;

                 req.Timeout = 20000;
 
                 byte[] btBodys = Encoding.UTF8.GetBytes(Cr.Body);

                 req.ContentLength = Convert.ToInt16(Cr.Body.Length);//每次变换参数后都要获得BODY长度
                 
                 req.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            }

            HttpWebResponse rspo;

            try
            {
                rspo = (HttpWebResponse)req.GetResponse();//响应的编码
            }
            catch (WebException)
            {
                return null;
            }

            this.rspoheader = rspo.StatusCode.ToString() + "\r\n" + rspo.Headers.ToString();

            Stream responseStream = rspo.GetResponseStream();

            string html;

            if (rspo.Headers["Content-Type"].Contains("="))
            {
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding(rspo.Headers["Content-Type"].Split('=')[1]));//Encoding.UTF8

                html = streamReader.ReadToEnd();
            }
            else
            {
                MemoryStream ms = new MemoryStream();

                byte[] buffer = new byte[1024];

                while (true)
                {
                    int sz = responseStream.Read(buffer, 0, 1024);

                    if (sz == 0) break;

                    ms.Write(buffer, 0, sz);

                }
                //默认编码读取
            
                ms.Position = 0;//指针置于流开头

                StreamReader streamReader = new StreamReader(ms, Encoding.UTF8);//Encoding.UTF8

                html = streamReader.ReadToEnd();

                Match charSetMatch = Regex.Match(html, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);

                string webCharSet = charSetMatch.Groups[2].Value;

                if (!String.IsNullOrEmpty(webCharSet))
                {
                    ms.Position = 0;

                    streamReader = new StreamReader(ms, Encoding.GetEncoding(webCharSet));

                    html = streamReader.ReadToEnd();
                }
            }

            this.rspohtml = html;

            HtmlDocument htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(this.rspohtml);

            foreach (HtmlNode script in htmlDoc.DocumentNode.Descendants("script"))//script
            {
                if (script.Attributes["src"] != null)
                {
                    string a = script.Attributes["src"].Value;

                    if (Cr.Innersources.ContainsKey(script.Line) == false)
                    {
                        Cr.Innersources.Add(script.Line, a);

                    }
                }
            }

            foreach (HtmlNode script in htmlDoc.DocumentNode.Descendants("link"))//script
            {
                if (script.Attributes["href"] != null)
                {
                    string a = script.Attributes["href"].Value;
                    if (Cr.Innersources.ContainsKey(script.Line) == false)
                    {
                        Cr.Innersources.Add(script.Line, a);
                    }
                }
            }

            foreach (HtmlNode script in htmlDoc.DocumentNode.Descendants("embed"))//script
            {
                if (script.Attributes["src"] != null)
                {
                    string a = script.Attributes["src"].Value;
                    if (Cr.Innersources.ContainsKey(script.Line) == false)
                    {
                        Cr.Innersources.Add(script.Line, a);
                    }
                }

            }

            //<meta name="DCS.dcsuri" content="/zh-cn/library/5t9y35bd(d=default,l=zh-cn,v=vs.110).aspx" />

            foreach (HtmlNode script in htmlDoc.DocumentNode.Descendants("meta"))//script
            {
                if (script.Attributes["content"] != null)
                {
                    string a = script.Attributes["content"].Value;
                    if (Cr.Innersources.ContainsKey(script.Line) == false && a.Contains("http") == true)
                    {
                        Cr.Innersources.Add(script.Line, a);
                    }
                }

            }
            
            return rspo;
        }

    }
}
