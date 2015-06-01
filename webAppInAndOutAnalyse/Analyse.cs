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
        public Analyse(string req,Resolve rs)//原始请求和解析类作为参数传递，接下来，交给解析类
        {
            Console.WriteLine("Start to analyse!");
        }

        public Array[] ParametersInRequest(string request)//把整个请求的输入点都分析到。包括cookie的解析等等
        {//参数记得调用htmldecode还原一下
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

            cc.Add(new Uri(cr.Url), new Cookie("1", "cnzz_a1708446"));

            HttpWebResponse rspo = (HttpWebResponse)req.GetResponse();

            Stream responseStream = rspo.GetResponseStream();

            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);

            string html = streamReader.ReadToEnd();

            MessageBox.Show(html);

            MessageBox.Show(rspo.Headers.ToString());

            MessageBox.Show(rspo.StatusCode.ToString);
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
