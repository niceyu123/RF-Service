using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using Nbcb.Cobp.SDK;
using NewERPCZ.MyEntity;
using Oracle.ManagedDataAccess.Client;

namespace NewERPCZ.MyPublic
{
    public class CommonHelper
    {
        string path = HttpRuntime.AppDomainAppPath.ToString();
        string configName = "/config.ini";
        string privateKey = "MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCFa0iDhkd4wo/UxU5pUIPFLmKCZa5TiVWMuOU6ffNej+zmHB4u25eXCtr6XLiOgWlNw1KWQQ+nMI8CdM57jABSPFD4ra1nziuWg0AAYR0KCGhiCzSGU1vLQ0R1mDVe8IC1b8MwwDztr/xfPy6s69KSnKbLwzIxHEnpN515SEfeAE66vfbOH6+HvjOg6nOuhL56YL2IrkOzO6Nu4LPvYPik3s+D93V7rddZOCPKHaOScwb57isnWFKLGFK07dyIu1Y0MZLS30NmrjXHI1LqmhUQ6Gjfeb0Z1+PmGYUyNU1z0pLsKmGlbg8jaTjhY19+I3SNfSVNajZendbu+G7lqVABAgMBAAECggEAUlVJU3j7BCe00M3NvKnmFzmvqt6KvJxkgcncE8OD+xgATmSNr8btflVBmvy7G5362PUvMvAFc9xAdHiWr6FO1XDJWxz6hLOzLFfkmBdV70oO+GoHyNkKLZ5eUd9TGDp8gvrsTlpjfx56NGDuMeH5eWZYfCgCAlJ9vgEHGcAkMXe+w/mRItEwFyz4fqPYcqLU1/PSLbC46Od5fUdorOa+Yx3WgT3iMyINNMGLDZCkFg9dcHKfcRDBNX4aTzDTgGf/6M3ClGLdfZi/71JD7CzPWbSrdRwtismlKREHFVLIMZpbkvV9355WqIrpMiUTAJ/rRIyYsXaXTao1OCngFWLFEQKBgQDpcp6zwrhOL+de/1BeMVn9dyD/p9yb2mw7bTlRhEf92jzDgWrXUEa0G1IoFOkLQZ5cz/ak8Z0yaRegkB5Au0i0efhcjV9u2rGRdNfnEMMHCKDp0HwNUWM+yi/yPfg+kTop4r6ahXExC/SqN9PEtOLzrCWFU2E2vbJHmPFu9TTq5QKBgQCSTtyBkAAdainxuDdpt8LW3wZuFI3IX04tz5qfqHt1Vta6CYw1vsFEVFIJKH5o/S9C59AQe2wABQqdtyHKpgwd+M2M+jvq5q4cHULwDEwfd8r+GDqE548/9Yh1tsuI1aDT8/zQ2NNiz7IslItC6prM0sjNSsPNNZfxVUlA00zS7QKBgBiz91VAWq5zZUFpNQDyqfonXAeRpMedQmy7byBQJioXqOxrSnoEVacDaRsys0JsrCxYGVp08tR9yHFGLt1ctCHc8kog76NUYwvoWFxsKqcY46Y6WJY0MZNYY+B3bEh6p7P8+XxyeHrfMAG/LJqZJZbxdXr5SsU3J6Fp7sp2CiZ9AoGAeL8a3tbAMYZ3fWViXh5pb7n6bYkLBm4ZcFdgrhl3Yny7lCfjDkwS5tiMJ8DCqtUhVx9HqQKjPFTs0QLdoYhugaHfylSOdKvSz6MaplAP1vyfjBrk2ODeaZOy/itRSOm95I79fEMmGet9iatCT4SdIyNm0367n7V2Y5bWcOiyA3UCgYBFFh6Y0YCQjdBGpcE7zGWTOUQAUHPo+TT+9fA/l/ptpMwj2W+aOnI44js2EUIbxE529FATqNnHeQyfATIdbHZ+ECIGs5MzJYzyFA/bXilFZ6BjUfeTqH80ETIP59Rl34SsdkAAeQN7xBT0ppv0BWGB7EM8eQG1CXsRZPXUiLzi1w==";
        string custId = "0000070887";

        //先单笔查证 再回单查询 最后获取下载地址
        public void SelectFK()
        {
            while (true)
            {
                try
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select * from FORMTABLE_MAIN_418 where sqrq >='2020-12-22' and fkcg is null or sqrq >='2020-12-22' and fkcg != '0' ";
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
                            bool pass = NbcbSDK.init(path, configName, privateKey);
                            string res = "";
                            if (pass)
                            {
                                string dataJson = "{\"Data\":{\"custId\":\"" + custId + "\",\"serialNo\":\"" + oano + "\"}}";
                                res = NbcbSDK.send("", "singleTransfer", "querySingleTransferResult", dataJson);
                            }
                            TransferBack bk = new JavaScriptSerializer().Deserialize<TransferBack>(res);
                            string code = bk.Data.transferDtl.status;
                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = "update FORMTABLE_MAIN_418 set FKCG='" + code + "' where sqdh='" + oano + "'";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);

                        }
                    }
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug("错误" + ex.ToString());
                    Thread.Sleep(60000);
                    throw;
                }
            }
        }

        public void SelectDownloadNo()
        {
            while (true)
            {
                try
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select * from FORMTABLE_MAIN_418 where sqrq >='2020-12-22' and fkcg = '0' ";
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
                            bool pass = NbcbSDK.init(path, configName, privateKey);
                            string res = "";
                            if (pass)
                            {
                                string dataJson = "{\"Data\":{\"custId\":\"" + custId + "\",\"queryFlag\":\"3\",\"serialNo\":\"" + oano + "\",\"accountSet\":[]}}";
                                res = NbcbSDK.send("", "accInfo ", "queryReceipt", dataJson);
                            }
                            Back bk = new JavaScriptSerializer().Deserialize<Back>(res);
                            string code = bk.Data.retCode;
                            if (code == "0000")
                            {
                                string downloadNo = bk.Data.downloadNo;
                                conn = ToolHelper.OpenRavoerp("oa");
                                sql = "update FORMTABLE_MAIN_418 set xzbh='" + downloadNo + "' where sqdh='" + oano + "'";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }
                    }
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug("错误" + ex.ToString());
                    Thread.Sleep(60000);
                    throw;
                }
            }
        }

        public void SelectDownloadUrl()
        {
            while (true)
            {
                try
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select * from FORMTABLE_MAIN_418 where sqrq >='2020-12-22' and xzbh is not null ";
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
                            string downloadNo= dt.Rows[i]["xzbh"].ToString();
                            bool pass = NbcbSDK.init(path, configName, privateKey);
                            string res = "";
                            if (pass)
                            {
                                string dataJson = "{\"Data\":{\"custId\":\"" + custId + "\",\"tradeType\":\"RECEIPT\",\"downloadNo\":\"" + downloadNo + "\"}}";
                                res = NbcbSDK.send("", "downLoad", "getGeneralDownloadUrl", dataJson);
                            }
                            Back bk = new JavaScriptSerializer().Deserialize<Back>(res);
                            string downloadUrl = bk.Data.downloadUrl;
                            if (downloadUrl != null)
                            {
                                conn = ToolHelper.OpenRavoerp("oa");
                                sql = "update FORMTABLE_MAIN_418 set sdxzdz='" + downloadUrl + "' where sqdh='" + oano + "'";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }
                    }
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug("错误" + ex.ToString());
                    Thread.Sleep(60000);
                    throw;
                }
            }
        }

        public void SelectDownloadUrl1()
        {
            while (true)
            {
                try
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select * from FORMTABLE_MAIN_418 where sqrq >='2020-12-22' and sdxzdz is not null ";
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
                            string downloadUrl = dt.Rows[i]["sdxzdz"].ToString();
                            string path = ToolHelper.HttpDownloadFile(downloadUrl,oano);
                            if (path != null)
                            {
                                conn = ToolHelper.OpenRavoerp("oa");
                                sql = "update FORMTABLE_MAIN_418 set nwxzdz='" + path + "' where sqdh='" + oano + "'";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }
                    }
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug("错误" + ex.ToString());
                    Thread.Sleep(60000);
                    throw;
                }
            }
        }
        //批量查证
        public void SelectFKS()
        {
            while (true)
            {
                try
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select a.requestid,b.* from FORMTABLE_MAIN_480 a left join FORMTABLE_MAIN_480_DT1 b on a.id=b.mainid where b.fkcg is null or b.fkcg != '0' order by b.id ";//未添加 查证状态不为0的
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
                            string requestid = dt.Rows[i]["requestid"].ToString();
                            string mid = dt.Rows[i]["id"].ToString();
                            string ano = "HF_GYSFK"+ requestid;//主
                            string bno = "GYSFK"+ requestid+mid;//子
                            bool pass = NbcbSDK.init(path, configName, privateKey);
                            string res = "";
                            if (pass)
                            {
                                string dataJson = "{\"Data\":{\"batchSerialNo\":\"" + ano + "\",\"serialNo\":[" + bno + "],\"custId\":\"" + custId + "\"}}";
                                res = NbcbSDK.send("", "batchTransfer", "queryBatchTransferResult", dataJson);
                            }
                            Back bk = new JavaScriptSerializer().Deserialize<Back>(res);
                            string successNum = bk.Data.successNum;
                            if (successNum=="1")
                            {
                                conn = ToolHelper.OpenRavoerp("oa");
                                sql = "update FORMTABLE_MAIN_480_DT1 set FKCG='0' where id='" + mid + "'";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }

                        }
                    }
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug("错误" + ex.ToString());
                    Thread.Sleep(60000);
                    throw;
                }
            }
        }
        //批量付款回单查询
        public void SelectDownloadNoS()
        {
            while (true)
            {
                try
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select a.requestid,b.* from FORMTABLE_MAIN_480 a left join FORMTABLE_MAIN_480_DT1 b on a.id=b.mainid where b.fkcg = '0' order by b.id  "; 
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
                            string requestid = dt.Rows[i]["requestid"].ToString();
                            string mid = dt.Rows[i]["id"].ToString();
                            string ano = "HF_GYSFK" + requestid;//主
                            string bno = "GYSFK" + requestid + mid;//子

                            bool pass = NbcbSDK.init(path, configName, privateKey);
                            string res = "";
                            if (pass)
                            {
                                string dataJson = "{\"Data\":{\"custId\":\"" + custId + "\",\"queryFlag\":\"3\",\"serialNo\":\"" + bno + "\",\"accountSet\":[]}}";
                                res = NbcbSDK.send("", "accInfo ", "queryReceipt", dataJson);
                            }
                            Back bk = new JavaScriptSerializer().Deserialize<Back>(res);
                            string code = bk.Data.retCode;
                            if (code == "0000")
                            {
                                string downloadNo = bk.Data.downloadNo;
                                conn = ToolHelper.OpenRavoerp("oa");
                                sql = "update FORMTABLE_MAIN_480_DT1 set xzbh='" + downloadNo + "' where id='" + mid + "'";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }
                    }
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug("错误" + ex.ToString());
                    Thread.Sleep(60000);
                    throw;
                }
            }
        }

        //批量付款获取下载地址
        public void SelectDownloadUrlS()
        {
            while (true)
            {
                try
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select * from FORMTABLE_MAIN_480_DT1 where xzbh is not null ";
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
                            string id = dt.Rows[i]["ID"].ToString();
                            string downloadNo = dt.Rows[i]["xzbh"].ToString();
                            bool pass = NbcbSDK.init(path, configName, privateKey);
                            string res = "";
                            if (pass)
                            {
                                string dataJson = "{\"Data\":{\"custId\":\"" + custId + "\",\"tradeType\":\"RECEIPT\",\"downloadNo\":\"" + downloadNo + "\"}}";
                                res = NbcbSDK.send("", "downLoad", "getGeneralDownloadUrl", dataJson);
                            }
                            Back bk = new JavaScriptSerializer().Deserialize<Back>(res);
                            string downloadUrl = bk.Data.downloadUrl;
                            if (downloadUrl != null)
                            {
                                conn = ToolHelper.OpenRavoerp("oa");
                                sql = "update FORMTABLE_MAIN_480_DT1 set sdxzdz='" + downloadUrl + "' where id='" + id + "'";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }
                    }
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug("错误" + ex.ToString());
                    Thread.Sleep(60000);
                    throw;
                }
            }
        }

        //
        public void SelectDownloadUrl1S()
        {
            while (true)
            {
                try
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select a.requestid,b.* from FORMTABLE_MAIN_480 a left join FORMTABLE_MAIN_480_DT1 b on a.id=b.mainid where sdxzdz is not null ";
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
                            string requestid = dt.Rows[i]["requestid"].ToString();
                            string mid = dt.Rows[i]["id"].ToString();
                            string ano = "HF_GYSFK" + requestid;//主
                            string bno = "GYSFK" + requestid + mid;//子
                            string downloadUrl = dt.Rows[i]["sdxzdz"].ToString();
                            string path = ToolHelper.HttpDownloadFile(downloadUrl, bno);
                            if (path != null)
                            {
                                conn = ToolHelper.OpenRavoerp("oa");
                                sql = "update FORMTABLE_MAIN_480_DT1 set nwxzdz='" + path + "' where id='" + mid + "'";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }
                    }
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug("错误" + ex.ToString());
                    Thread.Sleep(60000);
                    throw;
                }
            }
        }
        //未完成 同步孚盟数据库
        public void SvaeURL()
        {
            while (true)
            {
                try
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    OracleCommand myCommand = conn.CreateCommand();
                    string sql = " select * from FORMTABLE_MAIN_480_DT1  where tbsd is null and nwxzdz is not null ";
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
                            string fid = dt.Rows[i]["SOURCEFORMFID"].ToString();
                            string pid = dt.Rows[i]["PROVFID"].ToString();
                            string dz = dt.Rows[i]["nwxzdz"].ToString();
                            string id = dt.Rows[i]["ID"].ToString();

                            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                            scsb.DataSource = "172.16.11.6,1800";
                            scsb.InitialCatalog = "FumaCRM8";
                            scsb.UserID = "sa";
                            scsb.Password = "abc_123";
                            //创建连接 参数为连接字符串
                            SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                            sqlConn.Open();
                            string sqlStr = " update FumaCRM8.dbo.faPRWriteOFFdtltot set nwxzdz='" + dz+"' where ProvFID='"+pid+"' and FID='"+fid+"' ";
                            SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                            int result = comm.ExecuteNonQuery();
                            sqlConn.Close();//关闭连接


                            if (result>0)
                            {
                                conn = ToolHelper.OpenRavoerp("oa");
                                sql = "update FORMTABLE_MAIN_480_DT1 set TBSD='0' where id='" + id + "'";
                                cmd = new OracleCommand(sql, conn);
                                int results = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }
                    }
                    Thread.Sleep(60000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug("错误" + ex.ToString());
                    Thread.Sleep(60000);
                    throw;
                }
            }
        }
    }
}