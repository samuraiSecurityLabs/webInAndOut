using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace webAppInAndOutAnalyse
{
    class Analyse
    {

        private string req;//请求

        private Resolve cr;//解析器

        private string rspo;//响应

        public Analyse(string req,Resolve cr)//原始请求和解析类作为参数传递，接下来，交给解析类
        {
            this.req = req;

            this.cr = cr;
            
            Console.WriteLine("Start to analyse!");
        }

        public Array[] ParametersInRequest()//把整个请求的输入点都分析到。包括cookie的解析等等
        {//参数记得调用htmldecode还原一下
            
            cr.ResolveHttpRequest(this.req);//请求的原文交给解析类

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(cr.Url);

            req.Method = cr.Method;

            req.Host = cr.Host;

            req.UserAgent = cr.Useragent;

            //req.ContentType = cr.Contenttype;

            req.Referer = cr.Referer;

            req.AllowAutoRedirect = false;

            req.ContentLength = Convert.ToInt16(cr.Contentlength);

            CookieContainer cc = new CookieContainer();

            req.CookieContainer = cc;

            foreach (KeyValuePair<string, string> keys in cr.CookieList)//这里有异常cookie的隐患
            {
                cc.Add(new Uri(cr.Url), new Cookie(keys.Key, keys.Value));//这里会new特别多的cookie对象，必须后续优化掉！
            }
       
            HttpWebResponse rspo = (HttpWebResponse)req.GetResponse();

            Stream responseStream = rspo.GetResponseStream();

            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);

            string html = streamReader.ReadToEnd();

            return null;
        }

        public void ResponseAnalysis(string response, string ins)//分析当前的响应，仅仅针对于显示的能够直接在源码中看到。
        { 
            //直接response搜索输入点
        }

        public void ExternalResourceInResponse(string response)//当前响应中的外部资源，除图片之外的全部抓到
        { 
            //js,iframe,html,动态页等等
        }


    }
}
