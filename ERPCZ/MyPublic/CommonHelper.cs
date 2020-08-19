using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Xml;

namespace ERPCZ.MyPublic
{
    public class CommonHelper
    {
        public void SelectFK()
        {
            while (true)
            {
                try
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select * from FORMTABLE_MAIN_418 where sqrq >='2020-08-08' and fklsh is null ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int num = dt.Rows.Count;
                    ToolHelper.CloseSql(conn);
                    if (num > 0)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            string oano = dt.Rows[i]["sqdh"].ToString();
                            erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                "<body>" +
                                "<head>" +
                                "<erpSysCode>ERP001</erpSysCode>" +
                                "<custNo>0000070887</custNo>" +
                                "<tradeName>ERP_QUERYTRANSFER</tradeName>" +
                                "</head>" +
                                "<map>" +
                                "<erpReqNo>" + oano + "</erpReqNo>" +
                                "<billCode></billCode>" +
                                "</map>" +
                                "</body>";
                            string xmls = ToolHelper.encrypt(xml);//加密
                            string re = erp.serverErpXml(xmls);//传输 接收
                            string bb = ToolHelper.decrypt(re);//解密
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(bb);
                            XmlNode rootNode = xmlDoc.SelectSingleNode("/body/head/retCode");
                            string a1 = "";
                            string an1 = "";
                            string json = "";
                            foreach (XmlNode xxNode in rootNode.ChildNodes)
                            {
                                a1 = xxNode.InnerText;
                                an1 = xxNode.Name;
                            }
                            if (a1 == "0")
                            {
                                rootNode = xmlDoc.SelectSingleNode("/body/head/retMsg");
                                string a2 = "";
                                string an2 = "";
                                foreach (XmlNode xxNode in rootNode.ChildNodes)
                                {
                                    a2 = xxNode.InnerText;
                                    an2 = xxNode.Name;
                                }
                                rootNode = xmlDoc.SelectSingleNode("/body/map/payState");
                                string a3 = "";
                                string an3 = "";
                                foreach (XmlNode xxNode in rootNode.ChildNodes)
                                {
                                    a3 = xxNode.InnerText;
                                    an3 = xxNode.Name;
                                }
                                rootNode = xmlDoc.SelectSingleNode("/body/map/payMsg");
                                string a4 = "";
                                string an4 = "";
                                foreach (XmlNode xxNode in rootNode.ChildNodes)
                                {
                                    a4 = xxNode.InnerText;
                                    an4 = xxNode.Name;
                                }
                                rootNode = xmlDoc.SelectSingleNode("/body/map/returnMsg");
                                string a5 = "";
                                string an5 = "";
                                foreach (XmlNode xxNode in rootNode.ChildNodes)
                                {
                                    a5 = xxNode.InnerText;
                                    an5 = xxNode.Name;
                                }
                                rootNode = xmlDoc.SelectSingleNode("/body/map/bankSerial");
                                string a6 = "";
                                string an6 = "";
                                foreach (XmlNode xxNode in rootNode.ChildNodes)
                                {
                                    a6 = xxNode.InnerText;
                                    an6 = xxNode.Name;
                                }
                                json = "a1" + a1 + "a2" + a2 + "a3" + a3 + "a4" + a4 + "a5" + a5 + "a6" + a6 + "";
                                conn = ToolHelper.OpenRavoerp("oa");
                                sql = "update FORMTABLE_MAIN_418 set ZTM='" + a1 + "',JKZT='" + a2 + "',FKJGXX='" + a3 + "',YHFHJGXX='" + a4 + "',FKLSH='" + a5 + "',FKJGZT='" + a6 + "'  where sqdh='" + oano + "'";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                            else
                            {
                                json = "a1" + a1 + "a2" + " " + "a3" + " " + "a4" + " " + "a5" + " " + "a6" + " " + "";
                            }
                        }

                    }
                    Thread.Sleep(3600000);

                }
                catch (Exception ex)
                {
                    Thread.Sleep(3600000);
                    throw;
                }
            }

        }
    }
}