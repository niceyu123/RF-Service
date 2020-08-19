using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace KKMWLN.MyPublic
{
    public class ToolHelper
    {
        /// <summary>
        /// 写日志对象
        /// </summary>
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        //md5加密
        public static string EncryptString(string str)
        {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }
        //url转码
        public static string UrlEncode(string str)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in str)
            {
                if (HttpUtility.UrlEncode(c.ToString()).Length > 1)
                {
                    builder.Append(HttpUtility.UrlEncode(c.ToString()).ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
        //秒时间戳
        public static string time()
        {
            long aa = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string t = Convert.ToString(aa);
            return t;
        }
        //毫秒时间戳
        public static string times(DateTime dt)
        {
            long aa = (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            string t = Convert.ToString(aa);
            return t;
        }
        //时间戳转日期
        public static DateTime StampToDatetime(long TimeStamp, bool isMinSeconds = true)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));//当地时区
            //返回转换后的日期
            if (isMinSeconds)
                return startTime.AddMilliseconds(TimeStamp);
            else
                return startTime.AddSeconds(TimeStamp);
        }
    }
}