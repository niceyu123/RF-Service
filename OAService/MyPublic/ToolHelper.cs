using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using NLog;
using Oracle.ManagedDataAccess.Client;

namespace OAService.MyPublic
{
    public class ToolHelper
    {
        /// <summary>
        /// 写日志对象
        /// </summary>
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 开启连接ravo2数据库
        /// </summary>
        /// <returns></returns>
        public static OracleConnection OpenRavoerp(string type)
        {
            string connString = "";
            if (type == "23")//工业
            {
                connString = "User ID=ravo2;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            }
            else if (type == "141")//汇隆
            {
                connString = "User ID=ravo3;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            }
            else if (type == "22")//隆威
            {
                connString = "User ID=ravo5;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            }
            else if (type == "21")//实业
            {
                connString = "User ID=ravo1;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.10.6)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = syravoerp)))";
            }
            else if (type == "161")//研森
            {
                connString = "User ID=ravo6;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            }
            else if (type == "oa")//oa
            {
                connString = "User ID=ecology;Password=ecology;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.16)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ecology)))";
            }
            else if (type == "middle")//中台数据库
            {
                connString = "User ID=ravo;Password=Ravo2020;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.64)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = RAVO_SJJC)))";
            }
            else if (type == "24")//
            {
                connString = "User ID=ravo0;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            }
            else
            {
                connString = "User ID=ravo2;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            }

            OracleConnection conn = new OracleConnection(connString);
            try
            {//成功
                conn.Open();
                return conn;
            }
            catch (System.Exception ex)
            {//失败
                ToolHelper.logger.Debug(ex.ToString());
                return conn;
            }
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public static void CloseSql(OracleConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        /// <summary>
        /// 运行cmd命令
        /// </summary>
        /// <param name="strCmd"></param>
        public static void RunCmd(string strCmd)
        {
            try
            {
                using (Process proc = new Process())
                {
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardError = false;
                    proc.StartInfo.RedirectStandardInput = true;
                    proc.StartInfo.RedirectStandardOutput = false;
                    proc.Start();
                    proc.StandardInput.WriteLine(strCmd);
                    //return true;
                }
            }
            catch
            {
                //return false;
            }
        }

        ///
        /// Get请求
        /// 
        /// 
        /// 字符串
        public static string GetHttpResponse(string url, int Timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = Timeout;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        /// 创建POST方式的HTTP请求  
        public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int timeout, string userAgent, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            //设置代理UserAgent和超时
            //request.UserAgent = userAgent;
            //request.Timeout = timeout; 

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //发送POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        i++;
                    }
                }
                byte[] data = Encoding.ASCII.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            string[] values = request.Headers.GetValues("Content-Type");
            return request.GetResponse() as HttpWebResponse;
        }
        /// <summary>
        /// 获取请求的数据
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();

            }
        }
        public static string Post(string url, string param)
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            string paraUrlCoded = param;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }

        public static double LeaveTimeTotal(double x)
        {
            double tt = x;
            double total = 0;
            if (tt % 1 == 0)
            {
                total = tt;
            }
            else
            {
                double b = Math.Floor(tt);
                double c = tt - b;
                if (c > 0.5)
                {
                    total = b + 1;
                }
                else
                {
                    total = b + 0.5;
                }
            }
            return total;
        }

        /// <summary>
        /// string转byte
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] stringToByte(string data)
        {
            try
            {
                byte[] bt = System.Text.Encoding.UTF8.GetBytes(data);
                return bt;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// byte转string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string byteToSting(byte[] data)
        {
            try
            {
                string str = System.Text.Encoding.UTF8.GetString(data);
                return str;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string GetCompany(string comid)
        {
            try
            {
                string id = "";
                if (comid == "24")
                {
                    id = "RAVO0";
                }
                else if (comid == "21")
                {
                    id = "RAVO1";
                }
                else if (comid == "23")
                {
                    id = "RAVO2";
                }
                else if (comid == "141")
                {
                    id = "RAVO3";
                }
                else if (comid == "22")
                {
                    id = "RAVO5";
                }
                else if (comid == "161")
                {
                    id = "RAVO6";
                }
                return id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}