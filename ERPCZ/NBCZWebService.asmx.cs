using com;
using ERPCZ.MyEntity;
using ERPCZ.MyPublic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Xml;

namespace ERPCZ
{
    /// <summary>
    /// NBCZWebService 的摘要说明
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "http://172.16.11.19:1917/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class NBCZWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld(string a)
        {
            ToolHelper.logger.Debug("111");
            try
            {
                if (a == "a")
                {
                    return "AAA";
                }
                else
                {
                    return "Hello World";
                }
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug(e.ToString());
                return "1";
            }
            

        }
        /// <summary>
        /// 账号信息列表查询
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        [WebMethod]
        public string QUERYACCLIST()
        {
            try
            {
                ToolHelper.logger.Debug("message");
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<body>"
                + "<head>"
                + "<erpSysCode>ERP001</erpSysCode>"
                + "<custNo>0000070887</custNo>"
                + "<tradeName>ERP_QUERYACCLIST</tradeName>"
                + "</head>"
                + "<map>"
                + "<queryCustNo>0000070887</queryCustNo>"
                + "</map>"
                + "</body>";
                string xmls = ToolHelper.encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = ToolHelper.decrypt(re);//解密
                return bb;
            }
            catch (Exception e)
            {
                return "123";
            }
        }
        /// <summary>
        /// 付款指令
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        [WebMethod]
        public string TRANSFER(string data)
        {
            try
            {
                ToolHelper.logger.Debug("message" + data);
                FKInfo fk = new JavaScriptSerializer().Deserialize<FKInfo>(data);
                string payerAccNo = "";
                string payerCorpCode = "";
                if (fk.dw != null)
                {
                    if (fk.dw == "gy")
                    {
                        payerCorpCode = "1000";
                        payerAccNo = "30010122000464108";
                    }
                    else if (fk.dw == "lw")
                    {
                        payerCorpCode = "1001";
                        payerAccNo = "32010122000076927";
                    }
                    else if (fk.dw == "sy")
                    {
                        payerCorpCode = "1002";
                        payerAccNo = "30010122001118454";
                    }
                    else if (fk.dw == "hl")
                    {
                        payerCorpCode = "1003";
                        payerAccNo = "32010122000204640";
                    }
                    else if (fk.dw == "ys")
                    {
                        payerCorpCode = "1004";
                        payerAccNo = "30010122001162958";
                    }
                }
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    "<body>" +
                    "<head>" +
                    "<erpSysCode>ERP001</erpSysCode>" +
                    "<custNo>0000070887</custNo>" +
                    "<tradeName>ERP_TRANSFER</tradeName>" +
                    "</head>" +
                    "<map>" +
                    "<payerAccNo>"+ payerAccNo + "</payerAccNo>" +
                    "<payerCorpCode>"+ payerCorpCode + "</payerCorpCode>" +
                    "<payerCorpName></payerCorpName>" +
                    "<erpPayerCorpCode></erpPayerCorpCode>" +
                    "<payeeAccNo>" + fk.skzh + "</payeeAccNo>" +
                    "<payeeAccName>" + fk.skdw + "</payeeAccName>" +
                    "<payeeBankName>" + fk.skyh + "</payeeBankName>" +
                    "<payeeBankCode></payeeBankCode>" +
                    "<payeeProv></payeeProv>" +
                    "<payeeCity></payeeCity>" +
                    "<payMoney>" + fk.fkje + "</payMoney>" +
                    "<areaSign>1</areaSign>" +
                    "<difSign>1</difSign>" +
                    "<payPurpose>OA付款申请</payPurpose>" +
                    "<erpReqNo>" + fk.sqdh + "</erpReqNo>" +
                    "<erpReqUser></erpReqUser>" +
                    "<allowEditPayeeAcc>0</allowEditPayeeAcc>" +
                    "<allowEditPayMoney>0</allowEditPayMoney>" +
                    "<allowEditPayerAcc>1</allowEditPayerAcc>" +
                    "<reverse1></reverse1>" +
                    "<reverse2></reverse2>" +
                    "<reverse3></reverse3>" +
                    "<reverse4></reverse4>" +
                    "<reverse5></reverse5>" +
                    "</map>" +
                    "</body>";

                string xmls = ToolHelper.encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = ToolHelper.decrypt(re);//解密
                ToolHelper.logger.Debug("messages" + bb);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(bb);
                XmlNode rootNode = xmlDoc.SelectSingleNode("/body/head/retCode");
                string zt = "";
                string ztName = "";
                foreach (XmlNode xxNode in rootNode.ChildNodes)
                {
                    zt = xxNode.InnerText;
                    ztName = xxNode.Name;
                }
                ToolHelper.logger.Debug("zt" + zt);
                rootNode = xmlDoc.SelectSingleNode("/body/head/retMsg");
                string msg = "";
                string msgName = "";
                foreach (XmlNode xxNode in rootNode.ChildNodes)
                {
                    msg = xxNode.InnerText;
                    msgName = xxNode.Name;
                }
                if (zt == "0")
                {
                    rootNode = xmlDoc.SelectSingleNode("/body/map/billCode");
                    string dsf = "";
                    string sdf = "";
                    foreach (XmlNode xxNode in rootNode.ChildNodes)
                    {
                        dsf = xxNode.InnerText;
                        sdf = xxNode.Name;
                    }
                    ToolHelper.logger.Debug("dsf" + dsf);
                    if (dsf != "" || dsf != null)
                    {
                        ToolHelper.logger.Debug("123");
                        OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                        OracleCommand myCommand = conn.CreateCommand();
                        OracleTransaction transaction;
                        transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
                        myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象

                        string sql = "update formtable_main_418 set BILLCODE='" + dsf + "' where sqdh='" + fk.sqdh + "'";
                        ToolHelper.logger.Debug("sql " + sql);
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        ToolHelper.logger.Debug("int" + result.ToString());
                        transaction.Commit();
                        ToolHelper.CloseSql(conn);
                    }
                }
                return msg;
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("失败" + e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 同行付款
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string TRANSFERT(string data)
        {
            try
            {
                ToolHelper.logger.Debug("message" + data);
                FKInfo fk = new JavaScriptSerializer().Deserialize<FKInfo>(data);
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    "<body>" +
                    "<head>" +
                    "<erpSysCode>ERP001</erpSysCode>" +
                    "<custNo>0000070887</custNo>" +
                    "<tradeName>ERP_TRANSFER</tradeName>" +
                    "</head>" +
                    "<map>" +
                    "<payerAccNo>30010122000533641</payerAccNo>" +
                    "<payerCorpCode>1000</payerCorpCode>" +
                    "<payerCorpName></payerCorpName>" +
                    "<erpPayerCorpCode></erpPayerCorpCode>" +
                    "<payeeAccNo>" + fk.skzh + "</payeeAccNo>" +
                    "<payeeAccName>" + fk.skdw + "</payeeAccName>" +
                    "<payeeBankName>" + fk.skyh + "</payeeBankName>" +
                    "<payeeBankCode></payeeBankCode>" +
                    "<payeeProv></payeeProv>" +
                    "<payeeCity></payeeCity>" +
                    "<payMoney>" + fk.fkje + "</payMoney>" +
                    "<areaSign>1</areaSign>" +
                    "<difSign>0</difSign>" +
                    "<payPurpose>OA付款申请"+fk.oano+"</payPurpose>" +
                    "<erpReqNo>" + fk.sqdh + "</erpReqNo>" +
                    "<erpReqUser></erpReqUser>" +
                    "<allowEditPayeeAcc>0</allowEditPayeeAcc>" +
                    "<allowEditPayMoney>0</allowEditPayMoney>" +
                    "<allowEditPayerAcc>1</allowEditPayerAcc>" +
                    "<reverse1></reverse1>" +
                    "<reverse2></reverse2>" +
                    "<reverse3></reverse3>" +
                    "<reverse4></reverse4>" +
                    "<reverse5></reverse5>" +
                    "</map>" +
                    "</body>";

                string xmls = ToolHelper.encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = ToolHelper.decrypt(re);//解密
                ToolHelper.logger.Debug("messages" + bb);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(bb);
                XmlNode rootNode = xmlDoc.SelectSingleNode("/body/head/retCode");
                string zt = "";
                string ztName = "";
                foreach (XmlNode xxNode in rootNode.ChildNodes)
                {
                    zt = xxNode.InnerText;
                    ztName = xxNode.Name;
                }
                ToolHelper.logger.Debug("zt" + zt);
                rootNode = xmlDoc.SelectSingleNode("/body/head/retMsg");
                string msg = "";
                string msgName = "";
                foreach (XmlNode xxNode in rootNode.ChildNodes)
                {
                    msg = xxNode.InnerText;
                    msgName = xxNode.Name;
                }
                if (zt == "0")
                {
                    rootNode = xmlDoc.SelectSingleNode("/body/map/billCode");
                    string dsf = "";
                    string sdf = "";
                    foreach (XmlNode xxNode in rootNode.ChildNodes)
                    {
                        dsf = xxNode.InnerText;
                        sdf = xxNode.Name;
                    }
                    ToolHelper.logger.Debug("dsf" + dsf);
                    if (dsf != "" || dsf != null)
                    {
                        ToolHelper.logger.Debug("123");
                        OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                        OracleCommand myCommand = conn.CreateCommand();
                        OracleTransaction transaction;
                        transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
                        myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象

                        string sql = "update formtable_main_418 set BILLCODE='" + dsf + "' where sqdh='" + fk.sqdh + "'";
                        ToolHelper.logger.Debug("sql " + sql);
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        ToolHelper.logger.Debug("int" + result.ToString());
                        transaction.Commit();
                        ToolHelper.CloseSql(conn);
                    }
                }
                return msg;
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("失败" + e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 财资单号 付款结果查询
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        [WebMethod]
        public string CZQUERYTRANSFER(string billCode)
        {
            try
            {
                ToolHelper.logger.Debug("开始" + billCode);
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    "<body>" +
                    "<head>" +
                    "<erpSysCode>ERP001</erpSysCode>" +
                    "<custNo>0000070887</custNo>" +
                    "<tradeName>ERP_QUERYTRANSFER</tradeName>" +
                    "</head>" +
                    "<map>" +
                    "<erpReqNo></erpReqNo>" +
                    "<billCode>" + billCode + "</billCode>" +
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
                foreach (XmlNode xxNode in rootNode.ChildNodes)
                {
                    a1 = xxNode.InnerText;
                    an1 = xxNode.Name;
                }
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
                //string json = "{\"retCode\":\""+a1+ "\",\"retMsg\":\"" + a2 + "\",\"payState\":\"" + a3 + "\",\"payMsg\":\"" + a4 + "\",\"returnMsg\":\"" + a5 + "\",\"bankSerial\":\"" + a6 + "\"}";
                string json = "a1" + a1 + "a2" + a2 + "a3" + a3 + "a4" + a4 + "a5" + a5 + "a6" + a6 + "";
                return json;
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("失败" + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// OA单号 付款结果查询
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        [WebMethod]
        public string OAQUERYTRANSFER(string oano)
        {
            try
            {
                ToolHelper.logger.Debug("开始" + oano);
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
                }
                else
                {
                    json = "a1" + a1 + "a2" + " " + "a3" + " " + "a4" + " " + "a5" + " " + "a6" + " " + "";
                }


                //string json = "{\"retCode\":\""+a1+ "\",\"retMsg\":\"" + a2 + "\",\"payState\":\"" + a3 + "\",\"payMsg\":\"" + a4 + "\",\"returnMsg\":\"" + a5 + "\",\"bankSerial\":\"" + a6 + "\"}";
               
                return json;
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("失败" + e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 当日明细查询
        /// </summary>
        /// <param name="bankNo"></param>30010122000533641
        /// <returns></returns>0000070887
        [WebMethod]
        public string QUERYCURDTL(string bankNo)
        {
            try
            {
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<body>"
                + "<head>"
                + "<erpSysCode>ERP001</erpSysCode>"
                + "<custNo>0000070887</custNo>"
                + "<tradeName>ERP_QUERYCURDTL</tradeName>"
                + "</head>"
                + "<map>"
                + "<bankAcc>"+bankNo+"</bankAcc>"//待查询账号
                + "<queryAmtBegin></queryAmtBegin>"
                + "<queryAmtEnd></queryAmtEnd>"
                + "<queryOppAccName></queryOppAccName>"
                + "</map>"
                + "</body>";
                string xmls = ToolHelper.encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = ToolHelper.decrypt(re);//解密
                return bb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 历史明细查询
        /// </summary>
        /// <param name="bankNo"></param>30010122000533641
        /// <param name="startDay"></param>2020-07-01
        /// <param name="endDay"></param>2020-07-20
        /// <returns></returns>
        [WebMethod]
        public string QUERYHISDTL(string bankNo, string startDay, string endDay)
        {
            try
            {
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<body>"
                + "<head>"
                + "<erpSysCode>ERP001</erpSysCode>"
                + "<custNo>0000070887</custNo>"
                + "<tradeName>ERP_QUERYHISDTL</tradeName>"
                + "</head>"
                + "<map>"
                + "<bankAcc>" + bankNo + "</bankAcc>"//待查询账号
                + "<queryDateBegin>" + startDay + "</queryDateBegin>"//开始时间
                + "<queryDateEnd>" + endDay + "</queryDateEnd>"//结束时间
                + "<queryAmtBegin>0.01</queryAmtBegin>"
                + "<queryAmtEnd>999999.00</queryAmtEnd>"
                + "<queryOppAccName></queryOppAccName>"
                + "</map>"
                + "</body>";
                string xmls = ToolHelper.encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = ToolHelper.decrypt(re);//解密
                return bb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 指令发送
        /// </summary>
        /// <param name="bifCode"></param>BONB
        /// <param name="tradeType"></param>3
        /// <param name="startDate"></param>2020-07-01
        /// <param name="endDate"></param>2020-07-25
        /// <param name="cmdDes"></param>test
        /// <param name="bankAcc"></param>30010122000533641
        /// <returns></returns>
        [WebMethod]
        public string BISCMDSEND(string bifCode,string tradeType,string startDate,string endDate,string cmdDes,string bankAcc)
        {
            try
            {
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<body>"
                + "<head>"
                + "<erpSysCode>ERP001</erpSysCode>"
                + "<custNo>0000070887</custNo>"
                + "<tradeName>ERP_BISCMDSEND</tradeName>"
                + "</head>"
                + "<map>"
                + "<bifCode>"+ bifCode + "</bifCode >"
                + "<tradeType>"+ tradeType + "</tradeType>"
                + "<startDate>"+ startDate + "</startDate>"
                + "<endDate>"+ endDate + "</endDate>"
                + "<cmdDes>"+ cmdDes + "</cmdDes>"
                + "<bankAcc>"+ bankAcc + "</bankAcc>"
                + "</map>"
                + "</body>";
                string xmls = ToolHelper.encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = ToolHelper.decrypt(re);//解密
                return bb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 指令结果查询
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string QUERYBISCMD(string bankAccount,string startDate,string endDate)
        {
            try
            {
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<body>"
                + "<head>"
                + "<erpSysCode>ERP001</erpSysCode>"
                + "<custNo>0000070887</custNo>"
                + "<tradeName>ERP_QUERYBISCMD</tradeName>"
                + "</head>"
                + "<map>"
                + "<bifCode>BONB</bifCode >"
                + "<tradeType>1</tradeType>"
                + "<bankAccount>"+ bankAccount + "</bankAccount>"
                + "<bisStateCode></bisStateCode>"
                + "<startDate>"+ startDate + "</startDate>"
                + "<endDate>"+ endDate + "</endDate>" +
                "<currentPage></currentPage>" +
                "<pageSize></pageSize>"
                + "</map>"
                + "</body>";
                string xmls = ToolHelper.encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = ToolHelper.decrypt(re);//解密
                return bb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 批量付款指令申请
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string BATCHTRANSFER()
        {
            try
            {
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<body>"
                + "<head>"
                + "<erpSysCode>ERP001</erpSysCode>"
                + "<custNo>0000070887</custNo>"
                + "<tradeName>ERP_BATCHTRANSFER</tradeName>"
                + "</head>"
                + "<map>"
                + "<totalNum>1</totalNum>"
                + "<list>"

                + "<row>"
                + "<payerAccNo>30010122000533641</payerAccNo>"
                + "<payerCorpCode>1000</payerCorpCode>"
                + "<payerCorpName>75625430X</payerCorpName>"
                + "<erpPayerCorpCode>11651000</erpPayerCorpCode>"
                + "<payeeAccNo>75200122000010790</payeeAccNo>"
                + "<payeeAccName>794590849</payeeAccName>"
                + "<payeeBankName>宁波银行股份有限公司昆山高新技术开发区支行</payeeBankName>"
                + "<payeeBankCode>313305216723</payeeBankCode>"
                + "<payeeProv></payeeProv>"
                + "<payeeCity></payeeCity>"
                + "<payMoney>9999.00</payMoney>"
                + "<areaSign>1</areaSign>"
                + "<difSign>0</difSign>"
                + "<payPurpose>ERP付款</payPurpose>"
                + "<erpReqNo>CS019</erpReqNo>"
                + "<erpReqUser></erpReqUser>"
                + "<allowEditPayeeAcc>0</allowEditPayeeAcc>"
                + "<allowEditPayMoney>0</allowEditPayMoney>"
                + "<allowEditPayerAcc>1</allowEditPayerAcc>"
                + "<reverse1></reverse1>"
                + "<reverse2></reverse2>"
                + "<reverse3></reverse3>"
                + "<reverse4></reverse4>"
                + "<reverse5>1</reverse5>"
                + ""
                + ""
                + ""
                + ""
                + "</row>"

                + "</list>"
                + "</map>"
                + "</body>";
                string xmls = ToolHelper.encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = ToolHelper.decrypt(re);//解密
                return bb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 批量付款结果查询
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string BATCHQUERYTRANSFER()
        {
            try
            {
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<body>"
                + "<head>"
                + "<erpSysCode>ERP001</erpSysCode>"
                + "<custNo>0000070887</custNo>"
                + "<tradeName>ERP_BATCHQUERYTRANSFER</tradeName>"
                + "</head>"
                + "<map>"
                + "<totalNum>1</totalNum>"
                + "<list>"
                + "<row>"
                + "<erpReqNo>CS019</erpReqNo>"
                + "<billCode></billCode>"
                + "</row>" 
                +"</list>"
                + "</map>"
                + "</body>";
                string xmls = ToolHelper.encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = ToolHelper.decrypt(re);//解密
                return bb;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [WebMethod]
        public string ERP_THESAMEYEARRECEIPTQUERY()
        {
            try
            {
                erpPlatform.erpPlatform erp = new erpPlatform.erpPlatform();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    "<body>" +
                    "<head>" +
                    "<erpSysCode>ERP001</erpSysCode>" +
                    "<custNo>0000082767</custNo>" +
                    "<tradeName> ERP_THESAMEYEARRECEIPTQUERY</tradeName>" +
                    "</head>" +
                    "<map>" +
                    "<bankAcc>总笔数</bankAcc>" +
                    "<beginDate>2020-03-01</beginDate>" +
                    "<endDate>2020-09-27</endDate >" +
                    "<certCode></certCode>" +
                    "<critType></critType>" +
                    "</map>" +
                    "</body>";
                string xmls = ToolHelper.encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = ToolHelper.decrypt(re);//解密
                return bb;
            }
            catch (Exception e)
            {
                return null;
            }


        }

    }
}
