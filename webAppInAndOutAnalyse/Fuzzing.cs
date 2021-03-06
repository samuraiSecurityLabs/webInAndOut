﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Threading;

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

                fay.GetHttpResponse(fuzzrs);//线程化

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

                //fuzzrs.ResolveHttpRequest(req);//对请求重新定义

                //fay.GetHttpResponse(fuzzrs);//分析特定请求的响应分析

                Thread thread = new Thread(new ParameterizedThreadStart(RequestFuzzy));

                thread.Start((object)req);    

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
                    if (keys.Value.Contains(".") == false)
                    {
                        req = req.Replace(keys.Value, "http://" + fsl.Host + "/" + keys.Value);//这个地方也是有问题，应该可能是https
                    }
                    else
                    {
                        req = req.Replace(keys.Value, "http://"  + keys.Value);//这个地方也是有问题，应该可能是https
                    }

                }


                fuzzrs.ResolveHttpRequest(req);//对请求重新定义

                loghelper.WriteLine(req);

                fuzzay.GetHttpResponse(fuzzrs);

                loghelper.WriteLine(fuzzay.Rspohtml);

            }
            
            return true;//返回结果 fay.i 、  fay.e  输入输出结果
        }


        private static void RequestFuzzy(object req)//请求传递进来，发包
        {
            Resolve fsl = new Resolve();

            Analyse fay = new Analyse((string)req, fsl);
       
            fay.GetHttpResponse(fsl);
        }
    }
}

