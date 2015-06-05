using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace webAppInAndOutAnalyse
{
    class Fuzzing
    {
        private readonly string Anchor = "samuraiLabs";//判断字符定位 这里暂时不判定&，避免和URL中的&混。

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

        public Fuzzing()
        {
            this.implicitelements = new Dictionary<int, string>();

            this.extrinsicelements = new Dictionary<string, string>();
        }

        public Boolean FuzzingResponse(string req)
        {
            string tmp;

            Resolve fsl = new Resolve();

            Analyse fay = new Analyse(req, fsl);

            fay.GetHttpResponse(fsl);//拿到了innersource

            Resolve fuzzrs = new Resolve();

            Analyse fuzzay = new Analyse(req, fuzzrs);

            LogHelper loghelper = new LogHelper();
     
            //Fuzzing Headers

            foreach (KeyValuePair<string, string> keys in fsl.Headerpars)//存在冗余，效率较低，以下需要改进
            {
                tmp = keys.Value;//原来的值保存

                req = req.Replace(keys.Key + "=" + tmp, keys.Key + "=" + Anchor);//把该变量替换成锚点，可能会有参数重复的地方，被多次替换

                loghelper.WriteLine(req);

                fuzzrs.ResolveHttpRequest(req);//对请求重新定义

                fay.GetHttpResponse(fuzzrs);

                loghelper.WriteLine(fay.Rspohtml);

                req = req.Replace(keys.Key + "=" + Anchor, keys.Key + "=" + tmp);//请求还原。

            }

            //Fuzzing Cookies

            foreach (KeyValuePair<string, string> keys in fsl.CookieList)
            {
                tmp = keys.Value;//原来的值保存

                req = req.Replace(keys.Key + "=" + tmp, keys.Key + "=" + Anchor);//把该变量替换成锚点，可能会有参数重复的地方，被多次替换

                loghelper.WriteLine(req);

                fuzzrs.ResolveHttpRequest(req);//对请求重新定义

                fay.GetHttpResponse(fuzzrs);//分析特定请求的响应分析

                loghelper.WriteLine(fay.Rspohtml);

                req = req.Replace(keys.Key + "=" + Anchor, keys.Key + "=" + tmp);//请求还原。

            }

            //Fuzzing Body

            foreach (KeyValuePair<string, string> keys in fsl.Bodypars)
            {
                tmp = keys.Value;//原来的值保存

                req = req.Replace(keys.Key + "=" + tmp, keys.Key + "=" + Anchor);//把该变量替换成锚点，可能会有参数重复的地方，被多次替换

                loghelper.WriteLine(req);

                fuzzrs.ResolveHttpRequest(req);//对请求重新定义

                fay.GetHttpResponse(fuzzrs); ;//分析特定请求的响应分析

                loghelper.WriteLine(fay.Rspohtml);

                req = req.Replace(keys.Key + "=" + Anchor, keys.Key + "=" + tmp);//请求还原。
            }

            //HtmlDocument htmlDoc = new HtmlDocument();

            //Fuzzing InnerSource

            foreach (KeyValuePair<int, string> keys in fsl.Innersources)
            {
                tmp = keys.Value;//原来的值保存

                req = "GET " + keys.Value + " HTTP/1.0\r\n"//避免POST的情况下，还得去删除BODY什么的。
                             + "Host: " + fsl.Host + "\r\n"
                             + "User-Agent:" + fsl.Useragent + "\r\n"
                             + "Accept: text/html, application/xhtml+xml, */*\r\n"
                             + "Cookie: " + fsl.Cookie + "\r\n"
                             + "Referer:" + fsl.Referer + "\r\n"
                             + "Connection: Keep-Alive\r\n";

                if (keys.Value.Contains("http://"))
                {
                    string pattern = @"(?<=http://)[\w\.]+[^/]";　//C#正则表达式提取匹配URL的模式  

                    MatchCollection mc = Regex.Matches(keys.Value, pattern);//满足pattern的匹配集合

                    string domain = "";

                    foreach (Match match in mc)
                    {
                        domain = match.ToString();
                    }

                    req = req.Replace(fsl.Host, domain);

                }

                else
                {
                    req = req.Replace(keys.Value, "http://" + fsl.Host + "/" + keys.Value);
                }


                fuzzrs.ResolveHttpRequest(req);//对请求重新定义

                fuzzay.GetHttpResponse(fuzzrs);

                loghelper.WriteLine(fuzzay.Rspohtml);

            }



            return true;//返回结果 fay.i 、  fay.e  输入输出结果
        }
    }
}



            //if (this.rspohtml.IndexOf("samuraiLabs") >= 0)//考虑到被过滤<>\情形
            //{
            //    if (position.Equals("Header"))
            //    {
            //        if (extrinsicelements.ContainsKey("[Header]" + keys.Key) == false)//如果key已经存在了，则不再添加。
            //        {
            //            extrinsicelements.Add("[Header]" + keys.Key, keys.Value);
            //        }
            //    }
            //    if (position.Equals("Body"))
            //    {
            //        if (extrinsicelements.ContainsKey("[Body]" + keys.Key) == false)
            //        {
            //            extrinsicelements.Add("[Body]" + keys.Key, keys.Value);
            //        }
            //    }
            //    if (position.Equals("Cookie"))
            //    {
            //        if (extrinsicelements.ContainsKey("[Cookie]" + keys.Key) == false)
            //        {
            //            extrinsicelements.Add("[Cookie]" + keys.Key, keys.Value);
            //        }
            //    }
            //}

            //HtmlDocument htmlDoc = new HtmlDocument();

            //htmlDoc.LoadHtml(this.rspohtml);

            //foreach (HtmlNode script in htmlDoc.DocumentNode.Descendants("script"))//script
            //{
            //    if (script.Attributes["src"] != null)
            //    {
            //        string a = script.Attributes["src"].Value;

            //        if (implicitelements.ContainsKey(script.Line) == false)
            //        {
            //            implicitelements.Add(script.Line, a);

            //            Resolve rs = new Resolve();

            //            string req = "GET " + Cr.Url + " HTTP/1.0\r\n"//避免POST的情况下，还得去删除BODY什么的。
            //                         + "Host: " + Cr.Host + "\r\n"
            //                         + "User-Agent:" + cr.Useragent + "\r\n"
            //                         + "Accept: text/html, application/xhtml+xml, */*\r\n"
            //                         + "Cookie: " + Cr.Cookie + "\r\n"
            //                         + "Referer:" + Cr.Referer + "\r\n"
            //                         + "Connection: Keep-Alive\r\n";

            //            if (a.Contains("http://"))
            //            {
            //                string pattern = @"(?<=http://)[\w\.]+[^/]";　//C#正则表达式提取匹配URL的模式  

            //                MatchCollection mc = Regex.Matches(a, pattern);//满足pattern的匹配集合

            //                string domain = "";

            //                foreach (Match match in mc)
            //                {
            //                    domain = match.ToString();
            //                }

            //                req = req.Replace(Cr.Url, a);

            //                req = req.Replace(Cr.Host,domain);

            //            }

            //            else
            //            {
            //                req = req.Replace(Cr.Url, Cr.Host+a);
            //            }

            //            Analyse ays = new Analyse(req,rs);

            //            a = ays.rspohtml; 

            //        }
            //    }
            //}

            //foreach (HtmlNode script in htmlDoc.DocumentNode.Descendants("link"))//script
            //{
            //    if (script.Attributes["href"] != null)
            //    {
            //        string a = script.Attributes["href"].Value;
            //        if (implicitelements.ContainsKey(script.Line) == false)
            //        {
            //            implicitelements.Add(script.Line, a);
            //        }
            //    }
            //}

            //foreach (HtmlNode script in htmlDoc.DocumentNode.Descendants("embed"))//script
            //{
            //    if (script.Attributes["src"] != null)
            //    {
            //        string a = script.Attributes["src"].Value;
            //        if (implicitelements.ContainsKey(script.Line) == false)
            //        {
            //            implicitelements.Add(script.Line, a);
            //        }
            //    }

            //}

            ////<meta name="DCS.dcsuri" content="/zh-cn/library/5t9y35bd(d=default,l=zh-cn,v=vs.110).aspx" />

            //foreach (HtmlNode script in htmlDoc.DocumentNode.Descendants("meta"))//script
            //{
            //    if (script.Attributes["content"] != null)
            //    {
            //        string a = script.Attributes["content"].Value;
            //        if (implicitelements.ContainsKey(script.Line) == false && a.Contains("http")==true)
            //        {
            //            implicitelements.Add(script.Line, a);
            //        }
            //    }

            //}