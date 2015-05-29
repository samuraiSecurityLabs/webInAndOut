using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace webAppInAndOutAnalyse
{
    class Analyse
    {
        public Analyse()
        {
            Console.WriteLine("Start to analyse!");
        }

        public Array[] ParametersInRequest(string request)//把整个请求的输入点都分析到。包括cookie的解析等等
        {
            return null;
        }

        public void ResponseAnalysis(string response, string in)//分析当前的响应，仅仅针对于显示的能够直接在源码中看到。
        { 
            //直接response搜索输入点
        }

        public void ExternalResourceInResponse(string response)//当前响应中的外部资源，除图片之外的全部抓到
        { 
            //js,iframe,html,动态页等等
        }


    }
}
