using OAService.MyEntity;
using OAService.MyPublic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace OAService
{
    /// <summary>
    /// Test 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://172.16.11.19:1915/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Test : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld(string a)
        {
            if (a == "a")
            {
                ToolHelper.logger.Debug("a");
                return "AAA";
            }
            else
            {
                ToolHelper.logger.Debug("b");
                return "Hello World";
            }
        }
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SaveTest(string time)
        {

            OracleConnection conn = ToolHelper.OpenRavoerp("oa");
            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction transaction;
            //开启本地事务
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            //为挂起的本地事务分配事务对象
            myCommand.Transaction = transaction;

            OracleConnection conn1 = ToolHelper.OpenRavoerp("23");
            OracleCommand myCommand1 = conn1.CreateCommand();
            OracleTransaction transaction1;
            transaction1 = conn1.BeginTransaction(IsolationLevel.ReadCommitted);
            myCommand1.Transaction = transaction1;
            try
            {
                string sql = " select * from AAA where id='" + time + "'";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                var num = dt.Rows.Count;

                sql = " INSERT INTO AAA (ID,NUM) VALUES ( '3','3')";
                cmd = new OracleCommand(sql, conn);
                int result = cmd.ExecuteNonQuery();
                //查询ERP数据库
                sql = " select Count(*) a from A_KQ ";
                cmd = new OracleCommand(sql, conn1);
                da = new OracleDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                num = dt.Rows.Count;
                string aaa = dt.Rows[0]["a"].ToString();
                transaction.Commit();
                transaction1.Commit();
                ToolHelper.CloseSql(conn);
                ToolHelper.CloseSql(conn1);
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                ToolHelper.CloseSql(conn);
                return false;
            }
        }
        /// <summary>
        /// 办公用品领用(出库)单
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SaveBGCKa(string time,string type)
        {
            ToolHelper.logger.Debug(type+"办公用品领用(出库)单测试" + time);
            OracleConnection conn = ToolHelper.OpenRavoerp(type);
            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction transaction;
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
            myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象

            OracleConnection conn1 = ToolHelper.OpenRavoerp("oa");
            OracleCommand myCommand1 = conn1.CreateCommand();
            OracleTransaction transaction1;
            transaction1 = conn1.BeginTransaction(IsolationLevel.ReadCommitted);
            myCommand1.Transaction = transaction1;
            try
            {
                BGCKInfo ck = new JavaScriptSerializer().Deserialize<BGCKInfo>(time);

                string sql = " select * from STORAGE_HEAD where oano='" + ck.oano + "'";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                var num = dt.Rows.Count;
                if (num == 0)
                {
                    //获取id_code和fid
                    sql = " select s_public.nextval fid into :ll_id  from dual ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string fid = dt.Rows[0]["FID"].ToString();
                    sql = " select * from (select * from STORAGE_HEAD where id_code like '%BGCK%' order by id_code desc) where rownum=1 ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string id_code = dt.Rows[0]["ID_CODE"].ToString(); //CGRK20-00880
                    string a = id_code.Substring(7); //00880
                    int b = Convert.ToInt32(a) + 1;
                    string c = Convert.ToString(b).PadLeft(5, '0');
                    string d = id_code.Substring(0, id_code.Length - 5);
                    id_code = d + c;
                    sql = " INSERT INTO STORAGE_HEAD (ID_CODE,WAREHOUSE,USER_CODE,WARE_MAN,INPUT_DAY,INPUT_DEPARTMENT,CHECKED,REMARK,TYPE_CODE,OLD_CODE,LIFE_CODE," +
                        " OK_DAY,PY,PRICEOVER,REALDAY,CERTIFY,CERTIFYDAY,ZZFP,PRICEUNIT,DECIDEMAN,PLANDAY,FBILLTYPE,FID,FROWCOUNT,SENDDAY,FDEPT,SQUE_CODE,TAX_ID,ZHANG_ID,OANO) " +
                        " VALUES ( '" + id_code + "','20','1','',to_date('" + ck.sqrq + "','yyyy-mm-dd hh24:mi:ss'),'00 ','0','" + ck.remark + "','1',null,'1'," +
                        " to_date('" + ck.sqrq + "','yyyy-mm-dd hh24:mi:ss'),'行政部','0',to_date('" + ck.sqrq + "','yyyy-mm-dd hh24:mi:ss'),'1',to_date('" + ck.sqrq + "','yyyy-mm-dd hh24:mi:ss')," +
                        " '0','0','1',to_date('" + ck.sqrq + "','yyyy-mm-dd hh24:mi:ss'),'302','" + fid + "','" + ck.zs + "',to_date('" + ck.sqrq + "','yyyy-mm-dd hh24:mi:ss')," +
                        " '00','NONE','','','" + ck.oano + "')";
                    cmd = new OracleCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();
                    //OA表单
                    sql = " select B.* from FORMTABLE_MAIN_42 a left join FORMTABLE_MAIN_42_DT1 b on a.id=b.mainid where a.lcbh='" + ck.oano + "' order by b.id ";
                    cmd = new OracleCommand(sql, conn1);
                    da = new OracleDataAdapter(cmd);
                    DataTable dta = new DataTable();
                    da.Fill(dta);
                    ToolHelper.logger.Debug("办公用品领用(出库)单SQL" + sql);
                    //string id_code = dt.Rows[0]["ID_CODE"].ToString();//CGRK20-00880
                    string product_code = dta.Rows[0]["WLID"].ToString();
                    ToolHelper.logger.Debug("办公用品领用(出库)单WLID" + product_code);
                    for (int i = 0; i < ck.zs ; i++)
                    {
                        //判断子表是否存在
                        sql = " select * from out_storage_detial where oano='" + ck.oano + "' and product_code ='" + dta.Rows[i]["WLID"].ToString() + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;

                        if (num == 0)
                        {
                            ck.sfsl = "0";
                            //添加子表
                            int xh1 = ck.xh + 1;
                            sql = " INSERT INTO out_storage_detial (ID_CODE,PRODUCT_CODE,INDEX_CODE,PCS,MANY,PRICE,WARE_UNIT,WAREHOUSE,RE_MARK,PUNIT,OUT_MANY,FID,OANO,CKOUT_MANY,STORAGE_DETIALID) " +
                                " VALUES( '" + id_code + "','" + dta.Rows[i]["WLID"].ToString() + "'," + i + 1 + ",1, '"+dta.Rows[i]["XQSL"].ToString()+"',0,'临时','20','" + dta.Rows[i]["BZ"].ToString() + "','" + dta.Rows[i]["DWID"].ToString() + "','" + dta.Rows[i]["XQSL"].ToString() + "'," + fid + ",'" + ck.oano + "',0,'" + dta.Rows[i]["CID"].ToString() + "') ";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    string fid = dt.Rows[0]["FID"].ToString();
                    string id_code = dt.Rows[0]["ID_CODE"].ToString();
                    sql = " select B.* from FORMTABLE_MAIN_42 a left join FORMTABLE_MAIN_42_DT1 b on a.id=b.mainid where a.lcbh='" + ck.oano + "' order by b.id ";
                    cmd = new OracleCommand(sql, conn1);
                    da = new OracleDataAdapter(cmd);
                    DataTable dta = new DataTable();
                    da.Fill(dta);

                    for (int i = 0; i < ck.zs - 1; i++)
                    {
                        //判断子表是否存在
                        sql = " select * from out_storage_detial where oano='" + ck.oano + "' and product_code ='" + dta.Rows[i]["WLID"].ToString() + "'";
                        ToolHelper.logger.Debug("办公用品领用(出库)单SQL4" + sql);
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;

                        if (num == 0)
                        {
                            ck.sfsl = "0";
                            //添加子表
                            int xh1 = ck.xh + 1;
                            sql = " INSERT INTO out_storage_detial (ID_CODE,PRODUCT_CODE,INDEX_CODE,PCS,MANY,PRICE,WARE_UNIT,WAREHOUSE,RE_MARK,PUNIT,OUT_MANY,FID,OANO,CKOUT_MANY,STORAGE_DETIALID) " +
                                " VALUES( '" + id_code + "','" + dta.Rows[i]["WLID"].ToString() + "'," + i + 1 + ",1,0,0,'临时','20','" + dta.Rows[i]["BZ"].ToString() + "','" + dta.Rows[i]["DWID"].ToString() + "','" + dta.Rows[i]["XQSL"].ToString() + "'," + fid + ",'" + ck.oano + "',0,'" + dta.Rows[i]["CID"].ToString() + "') ";
                            ToolHelper.logger.Debug("办公用品领用(出库)单SQL5" + sql);
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();

                        }
                    }
                }
                transaction.Commit();
                transaction1.Commit();
                ToolHelper.CloseSql(conn);
                ToolHelper.CloseSql(conn1);
                return true;
            }
            catch (Exception ex)
            {
                ToolHelper.logger.Debug("办公用品领用(出库)单错误:" + ex.ToString());
                transaction.Rollback();
                transaction1.Rollback();
                ToolHelper.CloseSql(conn);
                ToolHelper.CloseSql(conn1);
                return false;
            }

        }

        [WebMethod]
        public bool SaveBGCKb( string time,string type)
        {
            OracleConnection conn = ToolHelper.OpenRavoerp(type);
            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction transaction;
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
            myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象
            try
            {
                //conn = ToolHelper.OpenRavoerp("23");
                string sql = "update STORAGE_HEAD set life_code='2' where oano='" + time + "'";
                OracleCommand cmd = new OracleCommand(sql, conn);
                int result = cmd.ExecuteNonQuery();
                //ToolHelper.CloseSql(conn);
                transaction.Commit();
                ToolHelper.CloseSql(conn);
                return true;
            }
            catch (Exception ex)
            {
                ToolHelper.logger.Debug("办公用品领用(出库)单错误:" + ex.ToString());
                transaction.Rollback();
                ToolHelper.CloseSql(conn);
                ToolHelper.logger.Debug("false");
                return false;
            }
        }
        /// <summary>
        /// 办公用品入库 制单人 部门 供应商 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SaveBGRK(string time)
        {
            ToolHelper.logger.Debug("办公用品入库单" + time);
            OracleConnection conn = ToolHelper.OpenRavoerp("23");
            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction transaction;
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
            myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象

            OracleConnection conn1 = ToolHelper.OpenRavoerp("oa");
            OracleCommand myCommand1 = conn1.CreateCommand();
            OracleTransaction transaction1;
            transaction1 = conn1.BeginTransaction(IsolationLevel.ReadCommitted);
            myCommand1.Transaction = transaction1;
            try
            {
                BGRKInfo rk = new JavaScriptSerializer().Deserialize<BGRKInfo>(time);
                string sql = " select * from STORAGE_HEAD where oano='" + rk.oano + "' ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                var num = dt.Rows.Count;
                if (num == 0)
                {
                    sql = " select * from sys_user where fid='" + rk.gh + "' ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string user = dt.Rows[0]["ID_CODE"].ToString();
                    string department = dt.Rows[0]["department"].ToString();
                    sql = " select s_public.nextval fid into :ll_id  from dual ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string fid = dt.Rows[0]["FID"].ToString();
                    sql = " select * from STORAGE_HEAD where id_code like '%CGRK%' order by id_code desc ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string id_code = dt.Rows[0]["ID_CODE"].ToString(); //CGRK20-00880
                    string a = id_code.Substring(7); //00880
                    int b = Convert.ToInt32(a) + 1;
                    string c = Convert.ToString(b).PadLeft(5, '0');
                    string d = id_code.Substring(0, id_code.Length - 5);
                    id_code = d + c;
                    //添加主表
                    sql = " INSERT INTO STORAGE_HEAD (ID_CODE,WAREHOUSE,USER_CODE,WARE_MAN,INPUT_DAY,INPUT_DEPARTMENT,CHECKED,REMARK,TYPE_CODE,OLD_CODE,LIFE_CODE," +
                        " OK_DAY,PY,PRICEOVER,REALDAY,CERTIFY,CERTIFYDAY,ZZFP,PRICEUNIT,DECIDEMAN,PLANDAY,FBILLTYPE,FID,FROWCOUNT,SENDDAY,FDEPT,SQUE_CODE,TAX_ID,ZHANG_ID,OANO) " +
                        " VALUES ( '" + id_code + "','20','"+user+"','',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'"+rk.dw+"','0','" + rk.remark + "','1',null,'1'," +
                        " to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'行政部','0',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'1',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss')," +
                        " '" + rk.sl + "','0','1',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'4','" + fid + "','" + rk.zs + "',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss')," +
                        " '"+ department + "','NONE','" + rk.kslb + "','" + rk.lzfs + "','" + rk.oano + "')";
                    cmd = new OracleCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();


                    //查询OA明细表数据
                    sql = " select A.dwid,B.* from formtable_main_349 a left join FORMTABLE_MAIN_349_DT1 b on a.id=b.MAINID where a.lcbh='" + rk.oano + "' ";
                    cmd = new OracleCommand(sql, conn1);
                    da = new OracleDataAdapter(cmd);
                    DataTable dta = new DataTable();
                    da.Fill(dta);
                    int nums = dta.Rows.Count;
                    if (nums > 0)
                    {
                        for (int i = 0; i < nums; i++)
                        {
                            sql = " select * from V_BGYPRK where id='" + dta.Rows[i]["YDH"].ToString() + "'";
                            cmd = new OracleCommand(sql, conn1);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string ydbh = dt.Rows[0]["CGDH"].ToString();
                            int xh1 = i + 1;
                            //判断子表是否存在
                            sql = " select * from in_storage_detial where oano='" + rk.oano + "' and product_code ='" + dta.Rows[i]["WLBH"].ToString() + "' and INDEX_CODE ='"+xh1+"'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            num = dt.Rows.Count;
                            if (num == 0)
                            {
                                
                                sql = " INSERT INTO in_storage_detial (PRODUCT_CODE,INDEX_CODE,ID_CODE,PCS,MANY,PRICE,WARE_UNIT,WAREHOUSE,RE_MARK,PUNIT,IN_MANY,MPSID,MPSINDEX,BILLID," +
                                    " BILLINDEX,FID,FSOURCE,FUSERID,TAX_RTO,TAX,AMTN,OANO,PH) " +
                                    " VALUES( '" + dta.Rows[i]["WLBH"].ToString() + "', '" + xh1 + "','" + id_code + "','1','" + dta.Rows[i]["RKSL"].ToString() + "','" + dta.Rows[i]["DJ"].ToString() + "','临时','20','OA','" + dta.Rows[i]["DWBH"].ToString() + "','" + dta.Rows[i]["RKSL"].ToString() + "','" + ydbh + "','" + dta.Rows[i]["YDXH"].ToString() + "',''," +
                                    " '','" + fid + "','1','"+user+"','" + dta.Rows[i]["SL"].ToString() + "','" + dta.Rows[i]["SE"].ToString() + "','" + dta.Rows[i]["WSBWB"].ToString() + "','" + rk.oano + "','NONE') ";
                                cmd = new OracleCommand(sql, conn);
                                result = cmd.ExecuteNonQuery();

                                sql = " select * from V_BGYPRK where id='" + dta.Rows[i]["YDH"].ToString() + "'";
                                cmd = new OracleCommand(sql, conn1);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                string cgdh = dt.Rows[0]["CGDH"].ToString();

                                sql = " select * from FORMTABLE_MAIN_330_DT1  where cgdh='" + cgdh + "' and bh='" + dta.Rows[i]["WLBH"].ToString() + "'";
                                ToolHelper.logger.Debug("办公用品入库单sql:" + sql);
                                //sql = " select A.lcbh,B.* from formtable_main_349 a left join FORMTABLE_MAIN_349_DT1 b on a.id=b.mainid where A.lcbh='"+rk.oano+"' and B.wlbh='"+rk.wlbh+"'";
                                cmd = new OracleCommand(sql, conn1);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                string id = dt.Rows[0]["ID"].ToString();
                                string ycgsl = dt.Rows[0]["YCGSL"].ToString();
                                ToolHelper.logger.Debug("办公用品入库单ycgsl:" + ycgsl);
                                if (ycgsl == null)
                                {
                                    ycgsl = "0";
                                }
                                int aa = Convert.ToInt32(Convert.ToDouble(dta.Rows[i]["RKSL"].ToString()));
                                ToolHelper.logger.Debug("办公用品入库单aa:" + aa.ToString());
                                int number = aa + Convert.ToInt32(ycgsl);
                                ToolHelper.logger.Debug("办公用品入库单number:" + number.ToString());
                                string n = Convert.ToString(number);
                                sql = "update FORMTABLE_MAIN_330_DT1 set ycgsl='" + n + "'  where id='" + id + "'";
                                ToolHelper.logger.Debug("办公用品入库单SQL:" + sql);
                                //sql = "update FORMTABLE_MAIN_349_DT1 set rkdh='"+ id_code + "',xh='"+ xh1 + "',ycgsl='" + n + "'  where id='" + id + "'";
                                cmd = new OracleCommand(sql, conn1);
                                result = cmd.ExecuteNonQuery();

                                sql = " select B.id from formtable_main_349 a left join FORMTABLE_MAIN_349_DT1 b on a.id=b.mainid where A.lcbh='" + rk.oano + "' and B.wlbh='" + dta.Rows[i]["WLBH"].ToString() + "'   and xqsl='" + dta.Rows[i]["xqsl"].ToString() + "' "; 
                                cmd = new OracleCommand(sql, conn1);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                string ida = dt.Rows[0]["ID"].ToString();
                                sql = "update FORMTABLE_MAIN_349_DT1 set rkdh='" + id_code + "'  where id='" + ida + "'";
                                cmd = new OracleCommand(sql, conn1);
                                result = cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        transaction1.Commit();
                        ToolHelper.CloseSql(conn);
                        ToolHelper.CloseSql(conn1);

                        OracleConnection conn2 = ToolHelper.OpenRavoerp("23");
                        sql = "update STORAGE_HEAD set life_code='2' where fid='" + fid + "'";
                        cmd = new OracleCommand(sql, conn2);
                        result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn2);
                    }

                }


                return true;
            }
            catch (Exception ex)
            {
                ToolHelper.logger.Debug("办公用品入库单错误:" + ex.ToString());
                transaction.Rollback();
                transaction1.Rollback();
                ToolHelper.CloseSql(conn);
                ToolHelper.CloseSql(conn1);
                ToolHelper.logger.Debug("false");
                return false;
            }
        }
        //实业办公用品入库
        [WebMethod]
        public bool SaveBGRK1(string time)
        {
            ToolHelper.logger.Debug("办公用品入库单" + time);
            OracleConnection conn = ToolHelper.OpenRavoerp("21");
            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction transaction;
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
            myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象

            OracleConnection conn1 = ToolHelper.OpenRavoerp("oa");
            OracleCommand myCommand1 = conn1.CreateCommand();
            OracleTransaction transaction1;
            transaction1 = conn1.BeginTransaction(IsolationLevel.ReadCommitted);
            myCommand1.Transaction = transaction1;
            try
            {
                BGRKInfo rk = new JavaScriptSerializer().Deserialize<BGRKInfo>(time);
                string sql = " select * from STORAGE_HEAD where oano='" + rk.oano + "' ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                var num = dt.Rows.Count;
                if (num == 0)
                {
                    sql = " select * from sys_user where fid='" + rk.gh + "' ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string user = dt.Rows[0]["ID_CODE"].ToString();
                    string department = dt.Rows[0]["department"].ToString();
                    sql = " select s_public.nextval fid into :ll_id  from dual ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string fid = dt.Rows[0]["FID"].ToString();
                    sql = " select * from STORAGE_HEAD where id_code like '%CGRK%' order by id_code desc ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string id_code = dt.Rows[0]["ID_CODE"].ToString(); //CGRK20-00880
                    string a = id_code.Substring(7); //00880
                    int b = Convert.ToInt32(a) + 1;
                    string c = Convert.ToString(b).PadLeft(5, '0');
                    string d = id_code.Substring(0, id_code.Length - 5);
                    id_code = d + c;
                    //添加主表
                    sql = " INSERT INTO STORAGE_HEAD (ID_CODE,WAREHOUSE,USER_CODE,WARE_MAN,INPUT_DAY,INPUT_DEPARTMENT,CHECKED,REMARK,TYPE_CODE,OLD_CODE,LIFE_CODE," +
                        " OK_DAY,PY,PRICEOVER,REALDAY,CERTIFY,CERTIFYDAY,ZZFP,PRICEUNIT,DECIDEMAN,PLANDAY,FBILLTYPE,FID,FROWCOUNT,SENDDAY,FDEPT,SQUE_CODE,TAX_ID,ZHANG_ID,OANO) " +
                        " VALUES ( '" + id_code + "','20','" + user + "','',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'" + rk.dw + "','0','" + rk.remark + "','1',null,'1'," +
                        " to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'行政部','0',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'1',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss')," +
                        " '" + rk.sl + "','0','1',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'4','" + fid + "','" + rk.zs + "',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss')," +
                        " '" + department + "','NONE','" + rk.kslb + "','" + rk.lzfs + "','" + rk.oano + "')";
                    cmd = new OracleCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();


                    //查询OA明细表数据
                    sql = " select B.* from formtable_main_349 a left join FORMTABLE_MAIN_349_DT1 b on a.id=b.MAINID where a.lcbh='" + rk.oano + "' ";
                    cmd = new OracleCommand(sql, conn1);
                    da = new OracleDataAdapter(cmd);
                    DataTable dta = new DataTable();
                    da.Fill(dta);
                    int nums = dta.Rows.Count;
                    if (nums > 0)
                    {
                        for (int i = 0; i < nums; i++)
                        {
                            sql = " select * from V_BGYPRK where id='" + dta.Rows[i]["YDH1"].ToString() + "'";
                            cmd = new OracleCommand(sql, conn1);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string ydbh = dt.Rows[0]["CGDH"].ToString();
                            int xh1 = i + 1;
                            //判断子表是否存在
                            sql = " select * from in_storage_detial where oano='" + rk.oano + "' and product_code ='" + dta.Rows[i]["WLBH1"].ToString() + "' and INDEX_CODE ='" + xh1 + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            num = dt.Rows.Count;
                            if (num == 0)
                            {

                                sql = " INSERT INTO in_storage_detial (PRODUCT_CODE,INDEX_CODE,ID_CODE,PCS,MANY,PRICE,WARE_UNIT,WAREHOUSE,RE_MARK,PUNIT,IN_MANY,MPSID,MPSINDEX,BILLID," +
                                    " BILLINDEX,FID,FSOURCE,FUSERID,TAX_RTO,TAX,AMTN,OANO,PH) " +
                                    " VALUES( '" + dta.Rows[i]["WLBH1"].ToString() + "', '" + xh1 + "','" + id_code + "','1','" + dta.Rows[i]["RKSL"].ToString() + "','" + dta.Rows[i]["DJ"].ToString() + "','临时','20','OA','" + dta.Rows[i]["DWBH"].ToString() + "','" + dta.Rows[i]["RKSL"].ToString() + "','" + ydbh + "','" + dta.Rows[i]["YDXH"].ToString() + "',''," +
                                    " '','" + fid + "','1','"+user+"','" + dta.Rows[i]["SL"].ToString() + "','" + dta.Rows[i]["SE"].ToString() + "','" + dta.Rows[i]["WSBWB"].ToString() + "','" + rk.oano + "','NONE') ";
                                cmd = new OracleCommand(sql, conn);
                                result = cmd.ExecuteNonQuery();

                                sql = " select * from V_BGYPRK where id='" + dta.Rows[i]["YDH1"].ToString() + "'";
                                cmd = new OracleCommand(sql, conn1);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                string cgdh = dt.Rows[0]["CGDH"].ToString();

                                sql = " select * from FORMTABLE_MAIN_330_DT1  where cgdh='" + cgdh + "' and bh1='" + dta.Rows[i]["WLBH1"].ToString() + "'";
                                //sql = " select A.lcbh,B.* from formtable_main_349 a left join FORMTABLE_MAIN_349_DT1 b on a.id=b.mainid where A.lcbh='"+rk.oano+"' and B.wlbh='"+rk.wlbh+"'";
                                cmd = new OracleCommand(sql, conn1);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                string id = dt.Rows[0]["ID"].ToString();
                                string ycgsl = dt.Rows[0]["YCGSL"].ToString();
                                if (ycgsl == null)
                                {
                                    ycgsl = "0";
                                }
                                int aa = Convert.ToInt32(Convert.ToDouble(rk.rksl));
                                int number = aa + Convert.ToInt32(ycgsl);
                                string n = Convert.ToString(number);
                                sql = "update FORMTABLE_MAIN_330_DT1 set ycgsl='" + n + "'  where id='" + id + "'";
                                //sql = "update FORMTABLE_MAIN_349_DT1 set rkdh='"+ id_code + "',xh='"+ xh1 + "',ycgsl='" + n + "'  where id='" + id + "'";
                                cmd = new OracleCommand(sql, conn1);
                                result = cmd.ExecuteNonQuery();

                                sql = " select B.id from formtable_main_349 a left join FORMTABLE_MAIN_349_DT1 b on a.id=b.mainid where A.lcbh='" + rk.oano + "' and B.wlbh1='" + dta.Rows[i]["WLBH1"].ToString() + "'   and xqsl='" + dta.Rows[i]["xqsl"].ToString() + "' "; 
                                cmd = new OracleCommand(sql, conn1);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                string ida = dt.Rows[0]["ID"].ToString();
                                sql = "update FORMTABLE_MAIN_349_DT1 set rkdh='" + id_code + "'  where id='" + ida + "'";
                                cmd = new OracleCommand(sql, conn1);
                                result = cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        transaction1.Commit();
                        ToolHelper.CloseSql(conn);
                        ToolHelper.CloseSql(conn1);

                        OracleConnection conn2 = ToolHelper.OpenRavoerp("21");
                        sql = "update STORAGE_HEAD set life_code='2' where fid='" + fid + "'";
                        cmd = new OracleCommand(sql, conn2);
                        result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn2);
                    }

                }


                return true;
            }
            catch (Exception ex)
            {
                ToolHelper.logger.Debug("办公用品入库单错误:" + ex.ToString());
                transaction.Rollback();
                transaction1.Rollback();
                ToolHelper.CloseSql(conn);
                ToolHelper.CloseSql(conn1);
                ToolHelper.logger.Debug("false");
                return false;
            }
        }
        //五厂办公用品入库
        [WebMethod]
        public bool SaveBGRK5(string time)
        {
            ToolHelper.logger.Debug("办公用品入库单" + time);
            OracleConnection conn = ToolHelper.OpenRavoerp("22");
            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction transaction;
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
            myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象

            OracleConnection conn1 = ToolHelper.OpenRavoerp("oa");
            OracleCommand myCommand1 = conn1.CreateCommand();
            OracleTransaction transaction1;
            transaction1 = conn1.BeginTransaction(IsolationLevel.ReadCommitted);
            myCommand1.Transaction = transaction1;
            try
            {
                BGRKInfo rk = new JavaScriptSerializer().Deserialize<BGRKInfo>(time);
                string sql = " select * from STORAGE_HEAD where oano='" + rk.oano + "' ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                var num = dt.Rows.Count;
                if (num == 0)
                {
                    sql = " select * from sys_user where fid='" + rk.gh + "' ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string user = dt.Rows[0]["ID_CODE"].ToString();
                    string department = dt.Rows[0]["department"].ToString();
                    sql = " select s_public.nextval fid into :ll_id  from dual ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string fid = dt.Rows[0]["FID"].ToString();
                    sql = " select * from STORAGE_HEAD where id_code like '%CGRK%' order by id_code desc ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string id_code = dt.Rows[0]["ID_CODE"].ToString(); //CGRK20-00880
                    string a = id_code.Substring(7); //00880
                    int b = Convert.ToInt32(a) + 1;
                    string c = Convert.ToString(b).PadLeft(5, '0');
                    string d = id_code.Substring(0, id_code.Length - 5);
                    id_code = d + c;
                    //添加主表
                    sql = " INSERT INTO STORAGE_HEAD (ID_CODE,WAREHOUSE,USER_CODE,WARE_MAN,INPUT_DAY,INPUT_DEPARTMENT,CHECKED,REMARK,TYPE_CODE,OLD_CODE,LIFE_CODE," +
                        " OK_DAY,PY,PRICEOVER,REALDAY,CERTIFY,CERTIFYDAY,ZZFP,PRICEUNIT,DECIDEMAN,PLANDAY,FBILLTYPE,FID,FROWCOUNT,SENDDAY,FDEPT,SQUE_CODE,TAX_ID,ZHANG_ID,OANO) " +
                        " VALUES ( '" + id_code + "','20','" + user + "','',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'" + rk.dw + "','0','" + rk.remark + "','1',null,'1'," +
                        " to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'行政部','0',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'1',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss')," +
                        " '" + rk.sl + "','0','1',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss'),'4','" + fid + "','" + rk.zs + "',to_date('" + rk.zdrq + "','yyyy-mm-dd hh24:mi:ss')," +
                        " '" + department + "','NONE','" + rk.kslb + "','" + rk.lzfs + "','" + rk.oano + "')";
                    cmd = new OracleCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();


                    //查询OA明细表数据
                    sql = " select B.* from formtable_main_349 a left join FORMTABLE_MAIN_349_DT1 b on a.id=b.MAINID where a.lcbh='" + rk.oano + "' ";
                    cmd = new OracleCommand(sql, conn1);
                    da = new OracleDataAdapter(cmd);
                    DataTable dta = new DataTable();
                    da.Fill(dta);
                    int nums = dta.Rows.Count;
                    if (nums > 0)
                    {
                        for (int i = 0; i < nums; i++)
                        {
                            sql = " select * from V_BGYPRK where id='" + dta.Rows[i]["YDH5"].ToString() + "'";
                            cmd = new OracleCommand(sql, conn1);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string ydbh = dt.Rows[0]["CGDH"].ToString();
                            int xh1 = i + 1;
                            //判断子表是否存在
                            sql = " select * from in_storage_detial where oano='" + rk.oano + "' and product_code ='" + dta.Rows[i]["WLBH5"].ToString() + "' and INDEX_CODE ='" + xh1 + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            num = dt.Rows.Count;
                            if (num == 0)
                            {

                                sql = " INSERT INTO in_storage_detial (PRODUCT_CODE,INDEX_CODE,ID_CODE,PCS,MANY,PRICE,WARE_UNIT,WAREHOUSE,RE_MARK,PUNIT,IN_MANY,MPSID,MPSINDEX,BILLID," +
                                    " BILLINDEX,FID,FSOURCE,FUSERID,TAX_RTO,TAX,AMTN,OANO,PH) " +
                                    " VALUES( '" + dta.Rows[i]["WLBH5"].ToString() + "', '" + xh1 + "','" + id_code + "','1','" + dta.Rows[i]["RKSL"].ToString() + "','" + dta.Rows[i]["DJ"].ToString() + "','临时','20','OA','" + dta.Rows[i]["DWBH"].ToString() + "','" + dta.Rows[i]["RKSL"].ToString() + "','" + ydbh + "','" + dta.Rows[i]["YDXH"].ToString() + "',''," +
                                    " '','" + fid + "','1','"+user+"','" + dta.Rows[i]["SL"].ToString() + "','" + dta.Rows[i]["SE"].ToString() + "','" + dta.Rows[i]["WSBWB"].ToString() + "','" + rk.oano + "','NONE') ";
                                cmd = new OracleCommand(sql, conn);
                                result = cmd.ExecuteNonQuery();

                                sql = " select * from V_BGYPRK where id='" + dta.Rows[i]["YDH5"].ToString() + "'";
                                cmd = new OracleCommand(sql, conn1);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                string cgdh = dt.Rows[0]["CGDH"].ToString();

                                sql = " select * from FORMTABLE_MAIN_330_DT1  where cgdh='" + cgdh + "' and bh5='" + dta.Rows[i]["WLBH5"].ToString() + "'  and xqsl='" + dta.Rows[i]["xqsl"].ToString() + "' ";
                                //sql = " select A.lcbh,B.* from formtable_main_349 a left join FORMTABLE_MAIN_349_DT1 b on a.id=b.mainid where A.lcbh='"+rk.oano+"' and B.wlbh='"+rk.wlbh+"'";
                                cmd = new OracleCommand(sql, conn1);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                string id = dt.Rows[0]["ID"].ToString();
                                string ycgsl = dt.Rows[0]["YCGSL"].ToString();
                                if (ycgsl == null)
                                {
                                    ycgsl = "0";
                                }
                                int aa = Convert.ToInt32(Convert.ToDouble(rk.rksl));
                                int number = aa + Convert.ToInt32(ycgsl);
                                string n = Convert.ToString(number);
                                sql = "update FORMTABLE_MAIN_330_DT1 set ycgsl='" + n + "'  where id='" + id + "'";
                                //sql = "update FORMTABLE_MAIN_349_DT1 set rkdh='"+ id_code + "',xh='"+ xh1 + "',ycgsl='" + n + "'  where id='" + id + "'";
                                cmd = new OracleCommand(sql, conn1);
                                result = cmd.ExecuteNonQuery();

                                sql = " select B.id from formtable_main_349 a left join FORMTABLE_MAIN_349_DT1 b on a.id=b.mainid where A.lcbh='" + rk.oano + "' and B.wlbh5='" + dta.Rows[i]["WLBH5"].ToString() + "'";
                                cmd = new OracleCommand(sql, conn1);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                string ida = dt.Rows[0]["ID"].ToString();
                                sql = "update FORMTABLE_MAIN_349_DT1 set rkdh='" + id_code + "'  where id='" + ida + "'";
                                cmd = new OracleCommand(sql, conn1);
                                result = cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        transaction1.Commit();
                        ToolHelper.CloseSql(conn);
                        ToolHelper.CloseSql(conn1);

                        OracleConnection conn2 = ToolHelper.OpenRavoerp("22");
                        sql = "update STORAGE_HEAD set life_code='2' where fid='" + fid + "'";
                        cmd = new OracleCommand(sql, conn2);
                        result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn2);
                    }

                }


                return true;
            }
            catch (Exception ex)
            {
                ToolHelper.logger.Debug("办公用品入库单错误:" + ex.ToString());
                transaction.Rollback();
                transaction1.Rollback();
                ToolHelper.CloseSql(conn);
                ToolHelper.CloseSql(conn1);
                ToolHelper.logger.Debug("false");
                return false;
            }
        }
        //办公用品领用保存
        [WebMethod]
        public int SaveLY(string lcbh,string type)
        {
            try
            {
                ToolHelper.logger.Debug("办公用品保存:" + lcbh+";"+type);
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                string sql = "update formtable_main_42 set bc='"+type+"' where lcbh='"+lcbh+"'";
                OracleCommand cmd = new OracleCommand(sql, conn);
                int result = cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                ToolHelper.logger.Debug("办公用品保存:" + ex.ToString());
                return 0;
            }
        }

        [WebMethod]
        public bool SaveBGCKaa(string time, string type)
        {
            ToolHelper.logger.Debug(type + "办公用品领用(出库)单测试" + time);
            OracleConnection conn = ToolHelper.OpenRavoerp(type);
            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction transaction;
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
            myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象

            OracleConnection conn1 = ToolHelper.OpenRavoerp("oa");
            OracleCommand myCommand1 = conn1.CreateCommand();
            OracleTransaction transaction1;
            transaction1 = conn1.BeginTransaction(IsolationLevel.ReadCommitted);
            myCommand1.Transaction = transaction1;
            try
            {
                //BGCKInfo ck = new JavaScriptSerializer().Deserialize<BGCKInfo>(time);
                BGCKInfoa cka = new JavaScriptSerializer().Deserialize<BGCKInfoa>(time);
                for (int i = 0; i < cka.BGCKInfoc.Count; i++)
                {
                    if (cka.BGCKInfoc[i].cid != null || cka.BGCKInfoc[i].cid != "")
                    {
                        string sql = " select * from STORAGE_HEAD where oano='" + cka.BGCKInfoc[i].oano + "'";
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        var num = dt.Rows.Count;
                        if (num == 0)
                        {
                            //获取id_code和fid
                            sql = " select s_public.nextval fid into :ll_id  from dual ";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string fid = dt.Rows[0]["FID"].ToString();
                            sql = " select * from (select * from STORAGE_HEAD where id_code like '%BGCK%' order by id_code desc) where rownum=1 ";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string id_code = dt.Rows[0]["ID_CODE"].ToString(); //CGRK20-00880
                            string a = id_code.Substring(7); //00880
                            int b = Convert.ToInt32(a) + 1;
                            string c = Convert.ToString(b).PadLeft(5, '0');
                            string d = id_code.Substring(0, id_code.Length - 5);
                            id_code = d + c;
                            sql = " INSERT INTO STORAGE_HEAD (ID_CODE,WAREHOUSE,USER_CODE,WARE_MAN,INPUT_DAY,INPUT_DEPARTMENT,CHECKED,REMARK,TYPE_CODE,OLD_CODE,LIFE_CODE," +
                                " OK_DAY,PY,PRICEOVER,REALDAY,CERTIFY,CERTIFYDAY,ZZFP,PRICEUNIT,DECIDEMAN,PLANDAY,FBILLTYPE,FID,FROWCOUNT,SENDDAY,FDEPT,SQUE_CODE,TAX_ID,ZHANG_ID,OANO) " +
                                " VALUES ( '" + id_code + "','20','1','',to_date('" + cka.BGCKInfoc[i].sqrq + "','yyyy-mm-dd hh24:mi:ss'),'00 ','0',null,'1',null,'1'," +
                                " to_date('" + cka.BGCKInfoc[i].sqrq + "','yyyy-mm-dd hh24:mi:ss'),'行政部','0',to_date('" + cka.BGCKInfoc[i].sqrq + "','yyyy-mm-dd hh24:mi:ss'),'1',to_date('" + cka.BGCKInfoc[i].sqrq + "','yyyy-mm-dd hh24:mi:ss')," +
                                " '0','0','1',to_date('" + cka.BGCKInfoc[i].sqrq + "','yyyy-mm-dd hh24:mi:ss'),'302','" + fid + "','" + cka.BGCKInfoc[i].zs + "',to_date('" + cka.BGCKInfoc[i].sqrq + "','yyyy-mm-dd hh24:mi:ss')," +
                                " '00','NONE','','','" + cka.BGCKInfoc[i].oano + "')";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            //OA表单

                            //判断子表是否存在
                            sql = " select * from out_storage_detial where oano='" + cka.BGCKInfoc[i].oano + "' and product_code ='" + cka.BGCKInfoc[i].wlid + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            num = dt.Rows.Count;

                            if (num == 0)
                            {
                                //添加子表
                                int xh1 = cka.BGCKInfoc[i].xh + 1;
                                sql = " INSERT INTO out_storage_detial (ID_CODE,PRODUCT_CODE,INDEX_CODE,PCS,MANY,PRICE,WARE_UNIT,WAREHOUSE,RE_MARK,PUNIT,OUT_MANY,FID,OANO,CKOUT_MANY,STORAGE_DETIALID) " +
                                    " VALUES( '" + id_code + "','" + cka.BGCKInfoc[i].wlid + "'," + i + 1 + ",1, '" + cka.BGCKInfoc[i].xqsl + "',0,'临时','20',null,'" + cka.BGCKInfoc[i].dwid + "','" + cka.BGCKInfoc[i].xqsl + "'," + fid + ",'" + cka.BGCKInfoc[i].oano + "',0,'" + cka.BGCKInfoc[i].cid + "') ";
                                cmd = new OracleCommand(sql, conn);
                                result = cmd.ExecuteNonQuery();

                            }
                        }
                        else
                        {
                            string fid = dt.Rows[0]["FID"].ToString();
                            string id_code = dt.Rows[0]["ID_CODE"].ToString();
                            //判断子表是否存在
                            sql = " select * from out_storage_detial where oano='" + cka.BGCKInfoc[i].oano + "' and product_code ='" + cka.BGCKInfoc[i].wlid + "'";
                            ToolHelper.logger.Debug("办公用品领用(出库)单SQL4" + sql);
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            num = dt.Rows.Count;

                            if (num == 0)
                            {
                                //添加子表
                                int xh1 = cka.BGCKInfoc[i].xh + 1;
                                sql = " INSERT INTO out_storage_detial (ID_CODE,PRODUCT_CODE,INDEX_CODE,PCS,MANY,PRICE,WARE_UNIT,WAREHOUSE,RE_MARK,PUNIT,OUT_MANY,FID,OANO,CKOUT_MANY,STORAGE_DETIALID) " +
                                    " VALUES( '" + id_code + "','" + cka.BGCKInfoc[i].wlid + "'," + i + 1 + ",1, '" + cka.BGCKInfoc[i].xqsl + "',0,'临时','20',null,'" + cka.BGCKInfoc[i].dwid + "','" + cka.BGCKInfoc[i].xqsl + "'," + fid + ",'" + cka.BGCKInfoc[i].oano + "',0,'" + cka.BGCKInfoc[i].cid + "') ";
                                ToolHelper.logger.Debug("办公用品领用(出库)单SQL5" + sql);
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    
                }

                transaction.Commit();
                transaction1.Commit();
                ToolHelper.CloseSql(conn);
                ToolHelper.CloseSql(conn1);
                return true;
            }
            catch (Exception ex)
            {
                ToolHelper.logger.Debug("办公用品领用(出库)单错误:" + ex.ToString());
                transaction.Rollback();
                transaction1.Rollback();
                ToolHelper.CloseSql(conn);
                ToolHelper.CloseSql(conn1);
                return false; 
            }
            


        }

        [WebMethod]
        public string GetNo()
        {
            try
            {
                string no = "IT" + Convert.ToString(DateTime.Now.Year) + DateTime.Now.ToString("MM");

                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                string sql = " select * from FORMTABLE_MAIN_446_DT1 where  zcbh like '%" + no + "%' order by zcbh desc ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int num = dt.Rows.Count;
                string bh = "";
                string n = "";
                string re = "";
                if (num != 0)
                {
                    bh = dt.Rows[0]["zcbh"].ToString();
                    n = bh.Substring(bh.Length - 3);
                    re = no + n;
                }
                else
                {
                    re = no + "000";
                }
                ToolHelper.CloseSql(conn);
                return re;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        [WebMethod]
        public string GetXmNo()
        {
            try
            {
                string year = Convert.ToString(DateTime.Now.Year);
                string y= year.Substring(year.Length - 2, 2);
                string no = "IT-XM" + y;

                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                string sql = " select * from FORMTABLE_MAIN_473 order by id desc ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int num = dt.Rows.Count;
                string bh = "";
                string n = "";
                string re = "";
                if (num != 0)
                {
                    bh = dt.Rows[0]["xmbh"].ToString();
                    n = bh.Substring(bh.Length - 3);
                    int n1 = Convert.ToInt32(n)+1;
                    string n2 = Convert.ToString(n1);
                    string n3 = n2.PadLeft(3, '0');
                    re = no + n3;
                }
                else
                {
                    re = no + "000";
                }
                ToolHelper.CloseSql(conn);
                return re;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        [WebMethod]
        public string GetGZSP(string gh,string fb)
        {
            try
            {
                ToolHelper.logger.Debug(gh+";"+fb);
                OracleConnection conn = ToolHelper.OpenRavoerp(fb);
                OracleCommand myCommand = conn.CreateCommand();
                string sql = " select * from tf_yg where  YG_NO='" + gh + "' and SZ_NO='A01' ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string a01 = dt.Rows[0]["amtn"].ToString();

                sql = " select * from tf_yg where  YG_NO='" + gh + "' and SZ_NO='A05' ";
                cmd = new OracleCommand(sql, conn);
                da = new OracleDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                string a05 = dt.Rows[0]["amtn"].ToString();

                sql = " select * from tf_yg where  YG_NO='" + gh + "' and SZ_NO='A02' ";
                cmd = new OracleCommand(sql, conn);
                da = new OracleDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                string a02 = dt.Rows[0]["amtn"].ToString();

                sql = " select * from tf_yg where  YG_NO='" + gh + "' and SZ_NO='A04' ";
                cmd = new OracleCommand(sql, conn);
                da = new OracleDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                string a04 = dt.Rows[0]["amtn"].ToString();
                return "a01"+a01+"a05"+a05+"a02"+a02+"a04"+a04;

            }
            catch (Exception ex)
            {
                return "a01" + "" + "a05" + "" + "a02" + "" + "a04" + "";
            }
        }

        [WebMethod]
        public string SaveMemo(string oano)
        {
            try
            {
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                string sql = " select B.* from FORMTABLE_MAIN_458 a left join FORMTABLE_MAIN_458_DT1 b on a.ID=b.MAINID where lcbh='" + oano + "' ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int num = dt.Rows.Count;
                if (num > 0)
                {
                    for (int i = 0; i < num; i++)
                    {
                        string cid = dt.Rows[i]["cid"].ToString();
                        string memo = dt.Rows[i]["memo"].ToString();
                        string companyid= dt.Rows[i]["companyid"].ToString();
                        string orderid = dt.Rows[i]["orderid"].ToString();
                        string ITEMNO = dt.Rows[i]["ITEMNO"].ToString();
                        conn = ToolHelper.OpenRavoerp("middle");
                        sql = " update STORAGE_DELAY set memo='" + memo + "' where companyid='"+ companyid + "' and orderid='"+ orderid + "' and ITEMNO='"+ ITEMNO + "' ";
                        cmd = new OracleCommand(sql, conn);
                        int resultt = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 转正单
        /// </summary>
        /// <param name="manid"></param>
        /// <param name="fb"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveZZ(string manid,string fb,string date)
        {
            try
            {
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                string sql = " select a.workcode,a.lastname,b.departmentname,c.jobtitlename from HRMRESOURCE a " +
                    " left join hrmdepartment b on a.DEPARTMENTID=b.id" +
                    " left join hrmjobtitles c on a.jobtitle=c.id" +
                    " where a.id='" + manid + "' ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int num = dt.Rows.Count;
                ToolHelper.CloseSql(conn);
                if (num > 0)
                {
                    string workcode = dt.Rows[0]["WORKCODE"].ToString();
                    string departmentname = dt.Rows[0]["departmentname"].ToString();
                    string jobtitlename = dt.Rows[0]["jobtitlename"].ToString();

                    conn = ToolHelper.OpenRavoerp("24");
                    myCommand = conn.CreateCommand();
                    sql = " select post_id from post_tb where post_name='" + jobtitlename + "' ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string post_id = "";
                    int num1 = dt.Rows.Count;
                    if (num1>0)
                    {
                        post_id= dt.Rows[0]["post_id"].ToString();
                    }
                    string companyid = "";
                    string company = "";
                    if (fb=="21")
                    {
                        companyid = "RAVO1";
                        company = "3";
                    }else if (fb == "22")
                    {
                        companyid = "RAVO5";
                        company = "1";
                    }
                    else if (fb == "23")
                    {
                        companyid = "RAVO2";
                        company = "2";
                    }
                    else if (fb == "141")
                    {
                        companyid = "RAVO3";
                        company = "";
                    }
                    else if (fb == "4")
                    {
                        companyid = "RAVO6";
                        company = "5";
                    }
                    sql = " select id_code from department where cn_name='"+ departmentname + "' and companyid='"+ companyid+"' ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    string id_code = "";
                    int num2 = dt.Rows.Count;
                    if (num2 > 0)
                    {
                        id_code = dt.Rows[0]["id_code"].ToString();
                    }
                    sql = " select * from MAN_TB_DPCHANGE where man_id='"+workcode+"' order by itm desc ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    int num3 = dt.Rows.Count;
                    string itm = "1";
                    if (num3>0)
                    {
                        string itms = dt.Rows[0]["itm"].ToString();
                        itm = Convert.ToString(Convert.ToInt32(itms) + 1);
                    }
                    sql = " INSERT INTO MAN_TB_DPCHANGE (MAN_ID,ITM,TYPE,COMPANY,DEP,POST,REASON,DP_DD,COMPANY_NEW,DEP_NEW,POST_NEW) " +
                        " VALUES ( '"+workcode+"','"+itm+"','4','"+company+"','"+ id_code + "','"+ post_id + "','转正',to_date('" + date+" 00:00:00" + "','yyyy-mm-dd hh24:mi:ss'),'" + company+"','"+ departmentname+"','"+ jobtitlename + "') ";
                    cmd = new OracleCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();
                    sql = "update MAN_TB set EDITDAY=to_date('" + Convert.ToString(DateTime.Now) + "','yyyy-mm-dd hh24:mi:ss')  where MAN_ID='" + workcode + "'";
                    cmd = new OracleCommand(sql, conn);
                    result = cmd.ExecuteNonQuery();
                    ToolHelper.CloseSql(conn);
                }
                return "成功";
            }
            catch (Exception ex)
            {
                ToolHelper.logger.Debug(ex.ToString());
                return "失败";
            }
        }

        [WebMethod]
        public string GetTX(string gh, string lch)
        {
            try
            {
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                string sql = " select * from FORMTABLE_MAIN_160 where TXRGH='"+gh+"' and jbsqdlc='"+lch+ "' and lcbh is not null ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int num = dt.Rows.Count;
                ToolHelper.CloseSql(conn);
                if (num==0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "0";
                throw;
            }
        }
        /// <summary>
        /// 人员档案属性变更申请单
        /// </summary>
        /// <param name="oano"></param>
        /// <returns></returns>
        [WebMethod]
        public string SendRYDA(string oano)
        {
            try
            {
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                string sql = " select * from formtable_main_474 a left join formtable_main_474_DT1 b on a.id=b.mainid where lcbh='"+oano+"' ";
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
                        string id_code = dt.Rows[i]["yggh"].ToString();
                        string xxzgs = dt.Rows[i]["xxzgs"].ToString();
                        string xz = ToolHelper.GetCompany(xxzgs);

                        string xbzgs = dt.Rows[i]["xbzgs"].ToString();
                        string bz= ToolHelper.GetCompany(xbzgs);

                        string xkqgs = dt.Rows[i]["xkqgs"].ToString();
                        string kq = ToolHelper.GetCompany(xkqgs);

                        string xjtbbfl = dt.Rows[i]["xjtbbfl"].ToString();
                        string xfyft = dt.Rows[i]["xfyft"].ToString();
                        string sxrq= dt.Rows[i]["sxrq"].ToString();//生效日期


                        conn = ToolHelper.OpenRavoerp("24");
                        myCommand = conn.CreateCommand();
                        
                        sql = "update man_tb set gs_bz='"+bz+"',gs_xz='"+xz+"',gs_kq='"+kq+"',jt_type='"+ xjtbbfl + "',is_ft='"+ xfyft + "'" +
                            " where man_id='" + id_code + "'";
                        ToolHelper.logger.Debug(sql);
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn);

                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                ToolHelper.logger.Debug(ex.ToString());
                return null;
            }
        }

    }
}
