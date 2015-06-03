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

        private string rspoheader;//比对头部知道参数变化后请求的差异性

        public string Rspoheader
        {
            get { return rspoheader; }
            set { rspoheader = value; }
        }

        private Dictionary<int, string> implicitelements;//用来保存每次FUZZ的结果（隐式）

        public Dictionary<int, string> Implicitelements
        {
            get { return implicitelements; }
            set { implicitelements = value; }
        }

        private Dictionary<string, string> extrinsicelements;//用来保存每次FUZZ的结果（显示）

        public Dictionary<string, string> Extrinsicelements
        {
            get { return extrinsicelements; }
            set { extrinsicelements = value; }
        }
        
        public Analyse(string req,Resolve cr)//原始请求和解析类作为参数传递，接下来，交给解析类
        {
            this.req = req;

            this.cr = cr;

            cr.ResolveHttpRequest(this.req);//请求的原文交给解析类处理，解析完毕数据完成第一次初始化。

            this.rspohtml = "";
            
            Console.WriteLine("Start to analyse!");

            this.implicitelements = new Dictionary<int, string>();

            this.extrinsicelements = new Dictionary<string, string>();

        }

 
        //把整个请求的输入点都分析到。包括cookie的解析等等，参数记得调用htmldecode还原一下

        public void ResponseAnalysis(Resolve Cr, KeyValuePair<string, string> keys,string position)//分析当前Cr的响应体。
        {
            GetHttpResponse(Cr);//初始化了rspohtml
            
            //foreach (KeyValuePair<string, string> keys in Cr.Headerpars)//查找的效率较低，要优化
            //{
            //    if (!keys.Value.Equals(String.Empty) && (this.rspohtml.IndexOf(keys.Value) >= 0 || this.rspohtml.IndexOf("samuraiLabs") >= 0))//考虑到被过滤<>\情形
            //    {
            //        if (extrinsicelements.ContainsKey("[Header]" + keys.Key) ==  false)//如果key已经存在了，则不再添加。
            //        {
            //            extrinsicelements.Add("[Header]" + keys.Key, keys.Value);
            //        }
            //    }
            //}

            if (this.rspohtml.IndexOf("samuraiLabs") >= 0)//考虑到被过滤<>\情形
            {
                if (extrinsicelements.ContainsKey("[Header]" + keys.Key) == false)//如果key已经存在了，则不再添加。
                {
                    extrinsicelements.Add("[Header]" + keys.Key, keys.Value);
                }
            }

            //foreach (KeyValuePair<string, string> keys in Cr.Bodypars)
            //{
            //    if (!keys.Value.Equals(String.Empty) && (this.rspohtml.IndexOf(keys.Value) >= 0 || this.rspohtml.IndexOf("samuraiLabs") >= 0))
            //    {
            //        if (extrinsicelements.ContainsKey("[Body]" + keys.Key) ==  false)
            //        {
            //            extrinsicelements.Add("[Body]" + keys.Key, keys.Value);
            //        }
            //    }
            //}

            //foreach (KeyValuePair<string, string> keys in Cr.CookieList)
            //{
            //    if (!keys.Value.Equals(String.Empty) && (this.rspohtml.IndexOf(keys.Value) >= 0 || this.rspohtml.IndexOf("samuraiLabs") >= 0))
            //    {
            //        if (extrinsicelements.ContainsKey("[Cookie]" + keys.Key)==false)
            //        {
            //            extrinsicelements.Add("[Cookie]" + keys.Key, keys.Value);
            //        }
            //    }
            //}

            HtmlDocument htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(this.rspohtml);

            //int i = 0;

            //foreach (var script in htmlDoc.DocumentNode.Descendants("script").ToArray())//script
            //{
            //    if (implicitelements.ContainsKey(keys.Key))
            //    {
            //        implicitelements.Add(i, script.OuterHtml);//隐式结果
            //    }
            //    i++;
            //}

            //foreach (var script in htmlDoc.DocumentNode.Descendants("link").ToArray())//css
            //{
            //    implicitelements.Add(i, script.OuterHtml);//隐式结果

            //    i++;
            //}

            //foreach (var script in htmlDoc.DocumentNode.Descendants("embed").ToArray())//swf
            //{
            //    implicitelements.Add(i, script.OuterHtml);//隐式结果

            //    i++;
            //}

            //if (htmlDoc.DocumentNode.SelectNodes("//comment()") != null)
            //{
            //    foreach (var comment in htmlDoc.DocumentNode.SelectNodes("//comment()").ToArray())//注释
            //    {
            //        implicitelements.Add(i, comment.OuterHtml);//隐式结果

            //        i++;
            //    }
            //}

        }

        public void ExternalResourceInResponse(string response)//当前响应中的外部资源，除图片之外的全部抓到
        { 
            //js,iframe,html,动态页等等
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        private HttpWebResponse GetHttpResponse(Resolve Cr)//解析器把URL请求解析得清楚
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

            //req.ContentType = cr.Contenttype;

            //CookieContainer cc = new CookieContainer();
            
            //foreach (KeyValuePair<string, string> keys in Cr.CookieList)
            //{
            //    try
            //    {
            //        cc.Add(new Uri(Cr.Url), new Cookie(keys.Key, keys.Value));//这里会new特别多的cookie对象，必须后续优化掉！
            //    }
            //    catch (Exception e)
            //    {
            //        //要捕捉下，否则报错。因为COOKIE有限制
            //    }
            //}

            //req.CookieContainer = cc;//接受返回的cookie         
   
            //cookiestr =  request.CookieContainer.GetCookieHeader(request.RequestUri);  

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
