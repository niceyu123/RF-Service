using com;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPCZ.MyPublic
{
    public class ToolHelper
    {
        /// <summary>
        /// 写日志对象
        /// </summary>
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string encrypt(string data)
        {
            try
            {
                byte[] bt = stringToByte(data);
                byte[] encryptBt = AesCode.encrypt(bt);
                string encryptStr = AesCode.bytesToString(encryptBt);
                return encryptStr;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string decrypt(string data)
        {
            try
            {
                string decryptGBKStr = AesCode.decrypt2GBK(data);
                //string decryptUTF8Str = AesCode.decrypt2UTF8(data);
                return decryptGBKStr;

            }
            catch (Exception e)
            {
                return null;
            }
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