using log4net;
using Nbcb.Cobp.SDK;
using NewERPCZ.MyEntity;
using NewERPCZ.MyPublic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace NewERPCZ
{
    /// <summary>
    /// NewCZAPI 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://172.16.11.19:2022/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class NewCZAPI : System.Web.Services.WebService
    {
        private static ILog log = LogManager.GetLogger("Program");
        string path = HttpRuntime.AppDomainAppPath.ToString();
        string configName = "/config.ini";
        string privateKey = "MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCFa0iDhkd4wo/UxU5pUIPFLmKCZa5TiVWMuOU6ffNej+zmHB4u25eXCtr6XLiOgWlNw1KWQQ+nMI8CdM57jABSPFD4ra1nziuWg0AAYR0KCGhiCzSGU1vLQ0R1mDVe8IC1b8MwwDztr/xfPy6s69KSnKbLwzIxHEnpN515SEfeAE66vfbOH6+HvjOg6nOuhL56YL2IrkOzO6Nu4LPvYPik3s+D93V7rddZOCPKHaOScwb57isnWFKLGFK07dyIu1Y0MZLS30NmrjXHI1LqmhUQ6Gjfeb0Z1+PmGYUyNU1z0pLsKmGlbg8jaTjhY19+I3SNfSVNajZendbu+G7lqVABAgMBAAECggEAUlVJU3j7BCe00M3NvKnmFzmvqt6KvJxkgcncE8OD+xgATmSNr8btflVBmvy7G5362PUvMvAFc9xAdHiWr6FO1XDJWxz6hLOzLFfkmBdV70oO+GoHyNkKLZ5eUd9TGDp8gvrsTlpjfx56NGDuMeH5eWZYfCgCAlJ9vgEHGcAkMXe+w/mRItEwFyz4fqPYcqLU1/PSLbC46Od5fUdorOa+Yx3WgT3iMyINNMGLDZCkFg9dcHKfcRDBNX4aTzDTgGf/6M3ClGLdfZi/71JD7CzPWbSrdRwtismlKREHFVLIMZpbkvV9355WqIrpMiUTAJ/rRIyYsXaXTao1OCngFWLFEQKBgQDpcp6zwrhOL+de/1BeMVn9dyD/p9yb2mw7bTlRhEf92jzDgWrXUEa0G1IoFOkLQZ5cz/ak8Z0yaRegkB5Au0i0efhcjV9u2rGRdNfnEMMHCKDp0HwNUWM+yi/yPfg+kTop4r6ahXExC/SqN9PEtOLzrCWFU2E2vbJHmPFu9TTq5QKBgQCSTtyBkAAdainxuDdpt8LW3wZuFI3IX04tz5qfqHt1Vta6CYw1vsFEVFIJKH5o/S9C59AQe2wABQqdtyHKpgwd+M2M+jvq5q4cHULwDEwfd8r+GDqE548/9Yh1tsuI1aDT8/zQ2NNiz7IslItC6prM0sjNSsPNNZfxVUlA00zS7QKBgBiz91VAWq5zZUFpNQDyqfonXAeRpMedQmy7byBQJioXqOxrSnoEVacDaRsys0JsrCxYGVp08tR9yHFGLt1ctCHc8kog76NUYwvoWFxsKqcY46Y6WJY0MZNYY+B3bEh6p7P8+XxyeHrfMAG/LJqZJZbxdXr5SsU3J6Fp7sp2CiZ9AoGAeL8a3tbAMYZ3fWViXh5pb7n6bYkLBm4ZcFdgrhl3Yny7lCfjDkwS5tiMJ8DCqtUhVx9HqQKjPFTs0QLdoYhugaHfylSOdKvSz6MaplAP1vyfjBrk2ODeaZOy/itRSOm95I79fEMmGet9iatCT4SdIyNm0367n7V2Y5bWcOiyA3UCgYBFFh6Y0YCQjdBGpcE7zGWTOUQAUHPo+TT+9fA/l/ptpMwj2W+aOnI44js2EUIbxE529FATqNnHeQyfATIdbHZ+ECIGs5MzJYzyFA/bXilFZ6BjUfeTqH80ETIP59Rl34SsdkAAeQN7xBT0ppv0BWGB7EM8eQG1CXsRZPXUiLzi1w==";
        string custId = "0000070887";

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 账户信息查询
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string QueryAccount()
        {
            try
            {
                bool pass = NbcbSDK.init(path, configName, privateKey);
                string res = "";
                if (pass)
                {
                    string dataJson = "{\"Data\":{\"custId\":\""+ custId + "\",\"pageSize\":\"10\",\"currentPage\":\"1\",\"bankAccList\":[],\"beginDate\":\"2020-12-08\",\"endDate\":\"2020-12-18\"}}";
                    res = NbcbSDK.send("", "accInfo", "queryAccount", dataJson);
                }
                return res;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 交易明细查询
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string QueryAccDetail()
        {
            try
            {
                bool pass = NbcbSDK.init(path, configName, privateKey);
                string res = "";
                if (pass)
                {
                    string dataJson = "{\"Data\":{\"custId\":\"" + custId + "\",\"bankAccList\":[\"77010122000031318\"],\"currentPage\":\"1\",\"pageSize\":\"10\",\"queryType\":\"0\",\"beginAmt\":\"0.01\",\"endAmt\":\"1000000\"}}";
                     res = NbcbSDK.send("", "accInfo ", "queryAccDetail", dataJson);
                }
                return res;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    
        /// <summary>
        /// 回单查询
        /// </summary>"downloadNo":"CZ2020122344293546"
        /// <returns></returns>
        [WebMethod]
        public string QueryReceipt(string oano)
        {
            try
            {
                bool pass = NbcbSDK.init(path, configName, privateKey);
                string res = "";
                if (pass)
                {
                    string dataJson = "{\"Data\":{\"custId\":\"" + custId + "\",\"queryFlag\":\"3\",\"serialNo\":\""+oano+"\",\"accountSet\":[]}}";
                    res = NbcbSDK.send("", "accInfo ", "queryReceipt", dataJson);
                }
                return res;
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 获取下载地址
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetGeneralDownloadUrl(string downloadNo)
        {
            try
            {
                bool pass = NbcbSDK.init(path, configName, privateKey);
                string res = "";
                if (pass)
                {
                    string dataJson = "{\"Data\":{\"custId\":\"" + custId + "\",\"tradeType\":\"RECEIPT\",\"downloadNo\":\""+ downloadNo + "\"}}";
                    res = NbcbSDK.send("", "downLoad", "getGeneralDownloadUrl", dataJson);

                }
                return res;
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 单笔转账
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string SingleTransfer(string data)
        {
            try
            {
                ToolHelper.logger.Debug("message" + data);
                FKInfo fk = new JavaScriptSerializer().Deserialize<FKInfo>(data);
                string payAcc = "";
                string corpCode = "";
                if (fk.dw != null)
                {
                    if (fk.dw == "gy")
                    {
                        corpCode = "1000";
                        payAcc = "30010122000464108";
                    }
                    else if (fk.dw == "lw")
                    {
                        corpCode = "1001";
                        payAcc = "32010122000076927";
                    }
                    else if (fk.dw == "sy")
                    {
                        corpCode = "1002";
                        payAcc = "30010122001118454";
                    }
                    else if (fk.dw == "hl")
                    {
                        corpCode = "1003";
                        payAcc = "30010122001158585";
                    }
                    else if (fk.dw == "ys")
                    {
                        corpCode = "1004";
                        payAcc = "30010122001162958";
                    }
                }
                bool pass = NbcbSDK.init(path, configName, privateKey);
                string res = "";
                if (pass)
                {
                    string dataJson = "{\"Data\":{\"serialNo\":\""+ fk.oano+ "\",\"corpCode\":\""+ corpCode + "\",\"rcvAcc\":\""+fk.skzh+"\",\"payAcc\":\""+ payAcc + "\",\"rcvBankName\":\""+fk.skyh+"\",\"rcvBankNo\":\"\",\"rcvName\":\""+fk.skdw+"\",\"purpose\":\"转账\",\"remark\":\""+ fk.oano + "\",\"difBank\":\"0\",\"areaSign\":\"1\",\"amt\":\""+fk.fkje+"\",\"isForIndividual\":\"0\",\"custId\":\"" + custId + "\"}}";
                    res = NbcbSDK.send("", "singleTransfer  ", "singleTransfer", dataJson);
                }
                Back bk = new JavaScriptSerializer().Deserialize<Back>(res);
                string code = bk.Data.retCode;
                if (code == "0000")
                {

                }
                else
                {

                }
                return res;
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 单笔查证
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string QuerySingleTransferResult(string oano)
        {
            try
            {
                bool pass = NbcbSDK.init(path, configName, privateKey);
                string res = "";
                if (pass)
                {
                    string dataJson = "{\"Data\":{\"custId\":\"" + custId + "\",\"serialNo\":\""+oano+"\"}}";
                    res = NbcbSDK.send("", "singleTransfer", "querySingleTransferResult", dataJson);
                }

                return res;
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                return null;
            }
        }
        ///// <summary>
        ///// 退汇查询
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public string QueryRefund()
        //{
        //    try
        //    {
        //        bool pass = NbcbSDK.init(path, configName, privateKey);
        //        string res = "";
        //        if (pass)
        //        {
        //            string dataJson = "";
        //            res = NbcbSDK.send("", "queryRefund", "queryRefund", dataJson);
        //        }
        //        return res;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 批量转账
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string BatchTransfer(string no)
        {
            try
            {
                bool pass = NbcbSDK.init(path, configName, privateKey);
                string res = "";
                if (pass)
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select * from FORMTABLE_MAIN_480 where lcbh='" + no + "' ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int num = dt.Rows.Count;
                    ToolHelper.CloseSql(conn);
                    if (num > 0)
                    {
                        string companyname = dt.Rows[0]["CompanyName"].ToString();
                        string payAcc = "";
                        string corpCode = "";
                        if (companyname.IndexOf("瑞孚工业") > 0)
                        {
                            corpCode = "1000";
                            payAcc = "30010122000464108";
                        } else if (companyname.IndexOf("隆威婴儿") > 0)
                        {
                            corpCode = "1001";
                            payAcc = "32010122000076927";
                        }
                        else if (companyname.IndexOf("汇隆") > 0)
                        {
                            corpCode = "1003";
                            payAcc = "30010122001158585";
                        }
                        string id = dt.Rows[0]["id"].ToString();
                        string requestid = dt.Rows[0]["requestid"].ToString();
                        string bno = "HF-GYSFK" + requestid;
                        conn = ToolHelper.OpenRavoerp("oa");
                        myCommand = conn.CreateCommand();
                        sql = "  SELECT a.*,b.* FROM FORMTABLE_MAIN_480_DT1 a," +
                            " (SELECT sum(realcheckedamt) total FROM FORMTABLE_MAIN_480_DT1 where mainid = '" + id + "') b where mainid = '" + id + "'";
                        cmd = new OracleCommand(sql, conn);
                        OracleDataAdapter das = new OracleDataAdapter(cmd);
                        DataTable dts = new DataTable();
                        das.Fill(dts);
                        int nums = dts.Rows.Count;
                        ToolHelper.CloseSql(conn);
                        string total = dts.Rows[0]["total"].ToString();
                        string daa = "{\"Data\":{\"batchSerialNo\":\"" + bno + "\",\"businessCode\":\"nis_smart_expense\",\"custId\":\"" + custId + "\",\"totalNumber\":\"" + nums + "\",\"showFlag\":\"1\",\"totalAmt\":\"" + total + "\",\"transferDtls\":[";
                        string data = "";
                        for (int i = 0; i < nums; i++)
                        {
                            string dtlSerialNo= "GYSFK"+requestid +dts.Rows[i]["id"].ToString();
                            string amt= dts.Rows[i]["realcheckedamt"].ToString();
                            string rcvAcc= dts.Rows[i]["accountno"].ToString();
                            string rcvBank = dts.Rows[i]["bankfullname"].ToString();
                            string rcvName = dts.Rows[i]["realprovname"].ToString();
                            data += "{\"dtlSerialNo\":\""+ dtlSerialNo + "\",\"payAcc\":\""+ payAcc + "\",\"rcvAcc\":\""+rcvAcc+"\",\"corpCode\":\""+ corpCode + "\",\"rcvBank\":\""+ rcvBank + "\",\"rcvBankCode\":\"\",\"rcvName\":\""+ rcvName + "\",\"isTellRcv\":\"0\",\"amt\":\"" + amt+"\",\"difBank\":\"0\",\"areaSign\":\"1\",\"purpose\":\"转账\",\"remark\":\"\",\"isForIndividual\":\"0\",\"showFlag\":\"1\"},";

                        }
                        data = data.Substring(0, data.Length - 1);//去除最后一位
                        string ta = "]}}";
                        string dataJson = daa + data + ta;
                        res = NbcbSDK.send("", "batchTransfer", "batchTransfer", dataJson);
                    }
                    //res = "";
                }
                ToolHelper.logger.Debug(res);
                return res;
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 批量查证
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string QueryBatchTransferResult(string no)
        {
            try
            {
                bool pass = NbcbSDK.init(path, configName, privateKey);
                string res = "";
                if (pass)
                {
                    string dataJson = "{\"Data\":{\"batchSerialNo\":\""+no+"\",\"serialNo\":[],\"custId\":\""+custId+"\"}}";
                    res = NbcbSDK.send("", "batchTransfer", "queryBatchTransferResult", dataJson);
                }
                return res;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        ///// <summary>
        ///// 明细对账数据查询
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public string CustAccountDayTransList()
        //{
        //    try
        //    {
        //        bool pass = NbcbSDK.init(path, configName, privateKey);
        //        string res = "";
        //        if (pass)
        //        {
        //            string dataJson = "";
        //            res = NbcbSDK.send("", "custAccountDayTransList", "custAccountDayTransList", dataJson);
        //        }
        //        return res;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}
        ///// <summary>
        ///// 对账单流水号查询
        ///// </summary>
        ///// <returns></returns>
        //[WebMethod]
        //public string QueryBillDownloadId()
        //{
        //    try
        //    {
        //        bool pass = NbcbSDK.init(path, configName, privateKey);
        //        string res = "";
        //        if (pass)
        //        {
        //            string dataJson = "";
        //            res = NbcbSDK.send("", "queryBillDownloadId", "queryBillDownloadId", dataJson);
        //        }
        //        return res;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

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
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                string sql = " select * from FORMTABLE_MAIN_418 where sqdh ='"+oano+"' ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int num = dt.Rows.Count;
                string a1= dt.Rows[0]["fkcg"].ToString();
                string a2 = dt.Rows[0]["xzbh"].ToString();
                string a3 = dt.Rows[0]["nwxzdz"].ToString();

                string json = "";
                json = "a1" + a1 + "a2" + a2 + "a3" + a3 + "";

                return json;
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("失败" + e.ToString());
                return null;
            }
        }

    }
}
