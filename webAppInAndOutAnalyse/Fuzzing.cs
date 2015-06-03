using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace webAppInAndOutAnalyse
{
    class Fuzzing
    {
        private readonly string Anchor = "<samuraiLabs\\>";//判断字符定位 这里暂时不判定&，避免和URL中的&混。

        public Boolean FuzzingResponse(string req)
        {
            string tmp;

            Resolve fsl = new Resolve();

            Analyse fay = new Analyse(req, fsl);            
            
            foreach (KeyValuePair<string, string> keys in fsl.Headerpars)//存在冗余，效率较低，以下需要改进
            {
                tmp = keys.Value;//原来的值保存

                req = req.Replace(tmp, Anchor);//把该变量替换成锚点，可能会有参数重复的地方，被多次替换

                fsl.ResolveHttpRequest(req);//对请求重新定义

                fay.ResponseAnalysis(fsl);//分析特定请求的响应分析

                req = req.Replace(Anchor,tmp);//请求还原。

            }

            foreach (KeyValuePair<string, string> keys in fsl.CookieList)
            {
                tmp = keys.Value;//原来的值保存

                req = req.Replace(tmp, Anchor);//把该变量替换成锚点，可能会有参数重复的地方，被多次替换

                fsl.ResolveHttpRequest(req);//对请求重新定义

                fay.ResponseAnalysis(fsl);//分析特定请求的响应分析

                req = req.Replace(Anchor, tmp);

            }

            foreach (KeyValuePair<string, string> keys in fsl.Bodypars)
            {
                tmp = keys.Value;//原来的值保存

                req = req.Replace(tmp, Anchor);//把该变量替换成锚点，可能会有参数重复的地方，被多次替换

                fsl.ResolveHttpRequest(req);//对请求重新定义

                fay.ResponseAnalysis(fsl);//分析特定请求的响应分析

                req = req.Replace(Anchor, tmp);

            }


            return true;
        }
    }
}
