using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;



namespace webAppInAndOutAnalyse
{
    class Resolve
    {

        private readonly string[] conststring = { 
                                                    "callback", 
                                                    "redirect", 
                                                    "jump" };//参数里面的关键字，预先定义一下。

        private string host;//avatar.csdn.net

        public string Host
        {
            get { return host; }
            set { host = value; }
        }
        private string method;//get or post or trace

        public string Method
        {
            get { return method; }
            set { method = value; }
        }


        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private string protocal;//http/1.1

        public string Protocal
        {
            get { return protocal; }
            set { protocal = value; }
        }
        private string connection;//

        public string Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        private string useragent;

        public string Useragent
        {
            get { return useragent; }
            set { useragent = value; }
        }
        private string referer;

        public string Referer
        {
            get { return referer; }
            set { referer = value; }
        }
        private string cookie;

        public string Cookie
        {
            get { return cookie; }
            set { cookie = value; }
        }
        private string otherheaders;

        public string Otherheaders
        {
            get { return otherheaders; }
            set { otherheaders = value; }
        }

        private string contentlength;

        public string Contentlength
        {
            get { return contentlength; }
            set { contentlength = value; }
        }


        private string body;

        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        private string contenttype;

        public string Contenttype
        {
            get { return contenttype; }
            set { contenttype = value; }
        }



        private Dictionary<string, string> cookieList;

        public Dictionary<string, string> CookieList
        {
            get
            {
                return this.cookieList;
            }
            set { cookieList = value; }
        }

        private Dictionary<string, string> headerpars;

        public Dictionary<string, string> Headerpars
        {
            get
            {
                return this.headerpars;
            }
            set { headerpars = value; }
        }

        private Dictionary<string, string> bodypars;

        public Dictionary<string, string> Bodypars
        {
            get
            {
                return this.bodypars;
            }
            set { bodypars = value; }
        }

        public Resolve()
        {
            this.otherheaders = "";

            this.body = "";

            Console.WriteLine("Constructor");
        }

        static int SubstringCount(string str, string substring)
        {
            if (str.Contains(substring))
            {
                string strReplaced = str.Replace(substring, "");

                return (str.Length - strReplaced.Length) / substring.Length;
            }

            return 0;
        }
        
        public void ResolveHttpRequest(string httprequest)//解析请求方法解析请求后，对http等属性进行赋值。
        {
            /*
            GET http://avatar.csdn.net/A/A/2/3_zhangzhennan1989.jpg HTTP/1.1
            Host: avatar.csdn.net
            Connection: keep-alive
            Accept: image/webp,**;q=0.8
            User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36
            Referer: http://blog.csdn.net/zhoufoxcn/article/details/6404236
            Accept-Encoding: gzip, deflate, sdch
            Accept-Language: zh-CN,zh;q=0.8,en;q=0.6
            Cookie: CloudGuest=U0ibguNE7DP9khMOyBt3I486oh1cz4kaFNY7byGH50A2VSmWXxDkmbOQAFUOWyyt73vzZ85/IbAXsCqW7fkEtoYBgCZ2XHxXt3qqgsxpoO4QtZx0bmTfo4/J8tz9VowFC2L8HdIADLMZOemVietcKjxQ2tHh+tA+0akCV1cFHDoXZ6xkYS9phRH1f5r7iSjd; uuid_tt_dd=-5728114581253503220_20150512; __gads=ID=92bf8cb62cc36be5:T=1431422228:S=ALNI_Ma9riYEUPDWLlhtxe6ip1TA9M5nag; __qca=P0-1870644198-1432092710731; _JQCMT_browser=18e39eecb67da78defe5ebf1f4d79383; __message_district_code=000000; _JQCMT_ifcookie=1; sid=00lx3une2jnjtidpdovzsini; lzstat_uv=35731125342580460042|3432968@2831342; lzstat_ss=2146225042_0_1432906774_2831342; dc_tos=np3jpy; __utmt=1; __utma=17226283.1130300211.1431422081.1432867432.1432877976.45; __utmb=17226283.1.10.1432877976; __utmc=17226283; __utmz=17226283.1432877976.45.44.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); dc_session_id=1432877974318; __message_sys_msg_id=0; __message_gu_msg_id=0; __message_cnel_msg_id=0; __message_in_school=0
            */
            Boolean tag;

            tag = false;

            String[] line = Regex.Split(httprequest, "\r\n", RegexOptions.IgnoreCase);

            this.method=line[0].Split(' ')[0];//可能是connect , trace,post等等

            this.url=line[0].Split(' ')[1];

            this.protocal=line[0].Split(' ')[2];//解析请求第一行

            this.cookieList = new Dictionary<string, string>();

            this.headerpars = new Dictionary<string, string>();

            this.bodypars = new Dictionary<string, string>();


            if (this.method.Contains("POST"))//如果是POST请求
            {

                this.body = Regex.Split(httprequest, "\r\n\r\n", RegexOptions.IgnoreCase)[1];//body part!!!
                line = Regex.Split(Regex.Split(httprequest, "\r\n\r\n", RegexOptions.IgnoreCase)[0], "\r\n", RegexOptions.IgnoreCase);//header part
                for (int i = 1; i < line.Length; ++i)
                {

                    if (line[i].Contains("Host:")) { this.host = line[i].Split(':')[1].Trim(); tag = true; }
                    if (line[i].Contains("Connection:")) { this.connection = line[i].Split(':')[1].Trim(); tag = true; }
                    if (line[i].Contains("User-Agent:"))
                    {
                        this.useragent = Regex.Split(line[i], "User-Agent:", RegexOptions.IgnoreCase)[1].Trim();
                        tag = true;
                    }
                    if (line[i].Contains("Referer:")) 
                    {
                        this.referer = Regex.Split(line[i], "Referer:", RegexOptions.IgnoreCase)[1].Trim(); 
                        
                        tag = true;
                    }
                    if (line[i].Contains("Cookie:")) 
                    { 

                        this.cookie = Regex.Split(line[i], "Cookie:", RegexOptions.IgnoreCase)[1].Trim(); 
                        
                        tag = true; 
                    }
                    if (line[i].Contains("Content-Length:")) { this.contentlength = line[i].Split(':')[1].Trim(); tag = true; }
                    if (line[i].Contains("Content-type:")) { this.Contenttype = line[i].Split(':')[1].Trim(); tag = true; }
                    if (tag.Equals(false))
                    {
                        this.otherheaders = this.otherheaders + line[i] + "\r\n";
                    }
                    tag = false;
                }
            }
            else
            {
                for (int i = 1; i < line.Length; ++i)
                {

                    if (line[i].Contains("Host:")) { this.host = line[i].Split(':')[1].Trim(); tag = true; }
                    if (line[i].Contains("Connection:")) { this.connection = line[i].Split(':')[1].Trim(); tag = true; }
                    if (line[i].Contains("User-Agent:")) 
                    { 
                        this.useragent = Regex.Split(line[i], "User-Agent:", RegexOptions.IgnoreCase)[1].Trim();
                        tag = true;
                    }
                    if (line[i].Contains("Referer:"))
                    {
                        this.referer = Regex.Split(line[i], "Referer:", RegexOptions.IgnoreCase)[1].Trim();

                        tag = true;
                    }
                    if (line[i].Contains("Cookie:"))
                    {

                        this.cookie = Regex.Split(line[i], "Cookie:", RegexOptions.IgnoreCase)[1].Trim();

                        tag = true;
                    }
                    if (line[i].Contains("Content-Length:")) { this.contentlength = line[i].Split(':')[1].Trim(); tag = true; }
                    if (tag.Equals(false))
                    {
                        this.otherheaders = this.otherheaders + line[i] + "\r\n"; 
                    }
                    tag = false;
                }
            }


            string[] pars;
            //resolve header
            if (this.url.Split('?').Length > 1)
            {
                pars = this.url.Split('?')[1].Split('&');

                foreach (string par in pars)
                {
                    //GET s?ie=utf-8&mod=1&isbd=1&isid=bdf6c84b000e5dbb&ie=utf-8&f=3&rsv_bp=1&rsv_idx=1&tn=baidu&wd=httpwebrequest%20post&rsv_pq=bdf6c84b000e5dbb&rsv_t=5e2fUXjJ0d1BrkdEfSjzse%2FaERaq2XZKz77aNIWX7oGJO4Kd7FqJYbzP2T8&rsv_enter=0&oq=httpwebre&inputT=5434&sug=httpwebrequest&rsv_sug3=40&rsv_sug1=24&rsp=0&rsv_sug4=1425&bs=httpwebrequest%20post&rsv_sid=14351_1423_7477_14511_12824_10212_12868_10562_14502_12722_14547_14244_11604_14370_8498&_ss=1&clist=&hsug=&f4s=1&csor=19&_cr1=37520 
                    //有些请求中包含了重复参数，如ie=utf-8 2次，导致添加不了，所以要加判断。
                    if (this.headerpars.ContainsKey(par) == false)
                    {
                        this.headerpars.Add(par.Split('=')[0].Trim(), par.Split('=')[1].Trim());
                    }
                }
            }

            //resolve body
            if (this.body.Equals(String.Empty) == false)
            {
                pars = this.body.Split('&');

                foreach (string par in pars)
                {
                    if(this.bodypars.ContainsKey(par) == false)
                    {
                    this.bodypars.Add(par.Split('=')[0].Trim(), par.Split('=')[1].Trim());
                    }
                }
            }

            //resolve cookie
            if (this.cookie != null)
            {
                String[] t = this.cookie.Split(';');

                foreach (string tt in t)
                {
                    if (!string.IsNullOrEmpty(tt))
                    {
                        if (tt.IndexOf('=') > 1)
                        {
                            this.cookieList.Add(tt.Split('=')[0].Trim(), tt.Substring(tt.IndexOf('='), tt.Length-tt.IndexOf('=')).Substring(1));
                        }
                    }
                    else
                    {

                        throw new ArgumentException("参数不合法");
                    }
        
                }

            }

        }

    }
}
