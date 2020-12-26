using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace NewERPCZ.MyPublic
{
    public class ToolHelper
    {
        /// <summary>
        /// 写日志对象
        /// </summary>
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 下载文件到服务器
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        //string url = "https://e.nbcb.com.cn/file/unionbankcode/unionbankcode.zip";
        //string path1 = "d:\\caizi\\1.zip";D:\oadownload
        public static string HttpDownloadFile(string url, string name)
        {
            string path = "D:\\oadownload\\" + name + ".zip";
            // 创建HttpWebRequest对象
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //获取WebResponse对象
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            //关键：获取Stream对象 (http请求的文件流对象)
            Stream responseStream = response.GetResponseStream();
            //创建本地文件写入流
            Stream stream = new FileStream(path, FileMode.Create);
            //分段写入本地文件 
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
            string webpath = "http://172.16.11.19:1916/download.aspx?name="+name+".zip";
            return webpath;
        }

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
            else if (type == "24")//汇隆
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

    }
}