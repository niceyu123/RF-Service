using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAWEB
{
    public class ToolHelper
    {
        /// <summary>
        /// 获取Post或Get参数
        /// </summary>
        /// <param name="strParameter">待获取字符串</param>
        /// <param name="strIsNull">待获取字符串为空时返回值</param>
        /// <returns>获取字符串</returns>
        public static string GetPostOrGetPar(string strParameter, string strIsNull)
        {
            if (!string.IsNullOrEmpty(strParameter))
            {
                return string.IsNullOrEmpty(strParameter.Trim()) ? strIsNull : strParameter.Trim();
            }
            return strIsNull;
        }
    }
}