using Newtonsoft.Json;
using OAService.MyEntity;
using OAService.MyPublic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Threading;
using System.Data.SqlClient;

namespace OAService
{
    /// <summary>
    /// NewOAWebService 的摘要说明 
    /// </summary> 
    [WebService(Namespace = "http://172.16.11.19:1915/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class NewOAWebService : System.Web.Services.WebService
    {

        #region 考勤
        /// <summary>
        /// 获取请假时长
        /// </summary>{"manid":"501485","startTime":"2019-10-06 08:00","endTime":"2019-10-06 20:00","dptID":"52"}
        /// <param name="userID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetLeaveTime(string time)
        {

            string a = KQleave.GetLeaveTime(time);
            return a;
        }
     
        /// <summary>
        /// 判断是否在排班内
        /// </summary>
        /// <param name="manid"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetIsPB(string time)
        {
            string a = KQHelper.GetIsPB(time);
            return a;
        }

        /// <summary>
        /// 存储请假信息
        /// </summary>{"manid":"507514","startTime":"2019-09-06 16:00","endTime":"2019-09-06 20:00","qjlx":"0","oaNo":"R2-QJSQD2019090694","dptID":"52"}
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveLeaveTimes(string time)
        {
            string a = KQleave.SaveLeaveTimes(time);
            return a;
        }
        [WebMethod]
        public string SaveLeaveTime(string time)
        {
            string a = KQleave.SaveLeaveTime(time);
            return a;
        }
        /// <summary>
        /// 存储调休信息
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveTXTime(string time)
        {
            string a = KQtxcc.SaveTXTime(time);
            return a;
        }
        [WebMethod]
        public string SaveTXTimes(string time)
        {
            string a = KQtxcc.SaveTXTimes(time);
            return a;
        }
        /// <summary>
        /// 存储出差信息
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveCCTime(string time)
        {
            string a = KQtxcc.SaveCCTime(time);
            return a;
        }
        [WebMethod]
        public string SaveCCTimes(string time)
        {
            string a = KQtxcc.SaveCCTimes(time);
            return a;
        }
        /// <summary>
        /// 存储加班单
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveOTTime(string time)
        {
            string a = KQHelper.SaveOTTime(time);
            return a;
        }
        #endregion
        #region 办公用品
        /// <summary>
        /// 办公用品请购单
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveQGBG(string time)
        {
            try
            {
                ToolHelper.logger.Debug("message" + time);
                QGInfo qg = new JavaScriptSerializer().Deserialize<QGInfo>(time);
                OracleConnection conn = ToolHelper.OpenRavoerp("23");
                if (conn != null)
                {
                    //先判断是否存在
                    string sql = " select * from PUR_REQBILL where oano='" + qg.oano + "'";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    var num = dt.Rows.Count;
                    ToolHelper.CloseSql(conn);
                    if (num == 0)
                    {
                        //获取fno和fid
                        conn = ToolHelper.OpenRavoerp("23");
                        sql = " select s_public.nextval fid into :ll_id  from dual ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string fid = dt.Rows[0]["FID"].ToString();

                        //conn = ToolHelper.OpenRavoerp("23");
                        sql = " select fno from PUR_REQBILL order by fno desc ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string fnos = dt.Rows[0]["FNO"].ToString();//QG20-0045
                        string a = fnos.Substring(5);//0045
                        int b = Convert.ToInt32(a) + 1;
                        string c = Convert.ToString(b).PadLeft(4, '0');
                        string d = fnos.Substring(0, fnos.Length - 4);
                        string fno = d + c;

                        //conn = ToolHelper.OpenRavoerp("23");
                        sql = " select * from sys_user where fid='" + qg.sqrgh + "' ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        int row = dt.Rows.Count;
                        string user = "0";
                        string dept = "0";
                        if (row > 0)
                        {
                            user = dt.Rows[0]["ID_CODE"].ToString();
                            dept = dt.Rows[0]["department"].ToString();
                        }

                        string department = dept;
                        string name = "系统管理员";
                        string idCode = user;
                        //添加主表 to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss')
                        conn = ToolHelper.OpenRavoerp("23");
                        sql = " INSERT INTO PUR_REQBILL(WRITE_DAY,LIFE_CODE, RE_MARK, SYS_USER ,DEPARTMENT,CHECKMAN,CHECKDAY,FNO,FID,SQ_DD,SAL_NO,BIL_TYPE,EST_DD,CLS_ID,OANO) " +
                            " VALUES(to_date('" + qg.sqrq + "','yyyy-mm-dd hh24:mi:ss'),4,'" + qg.remark + "','" + idCode + "','" + department + "','2770',to_date('" + qg.qrrq + "','yyyy-mm-dd hh24:mi:ss'),'" + fno + "'," +
                            "'" + fid + "',to_date('" + qg.sqrq + "','yyyy-mm-dd hh24:mi:ss'),'" + name + "',9,to_date('" + qg.yjrq + "','yyyy-mm-dd hh24:mi:ss'),'F','" + qg.oano + "') ";
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn);
                        //判断子表是否存在
                        conn = ToolHelper.OpenRavoerp("23");
                        sql = " select * from PUR_REQBILL_DETAIL where oano='" + qg.oano + "' and productid ='" + qg.wlbh + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        ToolHelper.CloseSql(conn);
                        if (num == 0)
                        {
                            //添加子表
                            int xh1 = qg.xh + 1;
                            conn = ToolHelper.OpenRavoerp("23");
                            sql = " INSERT INTO PUR_REQBILL_DETAIL(INDEX_CODE,PRODUCTID,NEEDMANY,LIFE_CODE,RE_MARK,NEEDDAY,FNO,TEMPINQTY,FID,INQTY,OANO) " +
                                " VALUES('" + xh1 + "','" + qg.wlbh + "','" + qg.xqsl + "',4,'" + qg.bz + "',to_date('" + qg.yhrq + "','yyyy-mm-dd hh24:mi:ss'),'" + fno + "',0,'" + fid + "',0,'" + qg.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                            //在oa数据库中添加 采购单号和序号
                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = " select B.* from FORMTABLE_MAIN_330 A left join FORMTABLE_MAIN_330_DT1 B on A.id = B.mainid where qgdh='" + qg.oano + "' and bh='" + qg.wlbh + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string oaid = dt.Rows[0]["ID"].ToString();
                            ToolHelper.CloseSql(conn);

                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = "update FORMTABLE_MAIN_330_DT1 set xh='" + xh1 + "' ,cgdh = '" + fno + "' where id='" + oaid + "'";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                        }
                        return "1";
                    }
                    else
                    {
                        string fno = dt.Rows[0]["FNO"].ToString();
                        string id = dt.Rows[0]["FID"].ToString();
                        //判断子表是否存在 
                        conn = ToolHelper.OpenRavoerp("23");
                        sql = " select * from PUR_REQBILL_DETAIL where oano='" + qg.oano + "' and productid ='" + qg.wlbh + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        ToolHelper.CloseSql(conn);
                        if (num == 0)
                        {
                            //添加子表
                            int xh1 = qg.xh + 1;
                            conn = ToolHelper.OpenRavoerp("23");
                            sql = " INSERT INTO PUR_REQBILL_DETAIL(INDEX_CODE,PRODUCTID,NEEDMANY,LIFE_CODE,RE_MARK,NEEDDAY,FNO,TEMPINQTY,FID,INQTY,OANO) " +
                                " VALUES('" + xh1 + "','" + qg.wlbh + "','" + qg.xqsl + "',4,'" + qg.bz + "',to_date('" + qg.yhrq + "','yyyy-mm-dd hh24:mi:ss'),'" + fno + "',0,'" + id + "',0,'" + qg.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                            //在oa数据库中添加 采购单号和序号
                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = " select B.* from FORMTABLE_MAIN_330 A left join FORMTABLE_MAIN_330_DT1 B on A.id = B.mainid where qgdh='" + qg.oano + "' and bh='" + qg.wlbh + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string oaid = dt.Rows[0]["ID"].ToString();
                            ToolHelper.CloseSql(conn);

                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = "update FORMTABLE_MAIN_330_DT1 set xh='" + xh1 + "' ,cgdh = '" + fno + "' where id='" + oaid + "'";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                        }
                        return "1";
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                return "";
            }
        }
        /// <summary>
        /// 实业办公用品请购单
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveQGBG1(string time)
        {
            try
            {
                QGInfo qg = new JavaScriptSerializer().Deserialize<QGInfo>(time);
                OracleConnection conn = ToolHelper.OpenRavoerp("21");
                if (conn != null)
                {
                    //先判断是否存在
                    string sql = " select * from PUR_REQBILL where oano='" + qg.oano + "'";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    var num = dt.Rows.Count;
                    ToolHelper.CloseSql(conn);
                    if (num == 0)
                    {
                        //获取fno和fid
                        conn = ToolHelper.OpenRavoerp("21");
                        sql = " select s_public.nextval fid into :ll_id  from dual ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string fid = dt.Rows[0]["FID"].ToString();

                        conn = ToolHelper.OpenRavoerp("21");
                        sql = " select fno from PUR_REQBILL order by fno desc ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string fnos = dt.Rows[0]["FNO"].ToString();//QG20-0045
                        string a = fnos.Substring(5);//0045
                        int b = Convert.ToInt32(a) + 1;
                        string c = Convert.ToString(b).PadLeft(4, '0');
                        string d = fnos.Substring(0, fnos.Length - 4);
                        string fno = d + c;
                        ToolHelper.CloseSql(conn);

                        conn = ToolHelper.OpenRavoerp("21");
                        sql = " select * from sys_user where fid='" + qg.sqrgh + "' ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        int row = dt.Rows.Count;
                        string user = "0";
                        string dept = "0";
                        if (row > 0)
                        {
                            user = dt.Rows[0]["ID_CODE"].ToString();
                            dept = dt.Rows[0]["department"].ToString();
                        }
                        string department = dept;
                        string name = "系统管理员";
                        string idCode = user;
                        //添加主表 to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss')
                        conn = ToolHelper.OpenRavoerp("21");
                        sql = " INSERT INTO PUR_REQBILL(WRITE_DAY,LIFE_CODE, RE_MARK, SYS_USER ,DEPARTMENT,CHECKMAN,CHECKDAY,FNO,FID,SQ_DD,SAL_NO,BIL_TYPE,EST_DD,CLS_ID,OANO) " +
                            " VALUES(to_date('" + qg.sqrq + "','yyyy-mm-dd hh24:mi:ss'),4,'" + qg.remark + "','" + idCode + "','" + department + "','2770',to_date('" + qg.qrrq + "','yyyy-mm-dd hh24:mi:ss'),'" + fno + "'," +
                            "'" + fid + "',to_date('" + qg.sqrq + "','yyyy-mm-dd hh24:mi:ss'),'" + name + "',9,to_date('" + qg.yjrq + "','yyyy-mm-dd hh24:mi:ss'),'F','" + qg.oano + "') ";
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn);
                        //判断子表是否存在
                        conn = ToolHelper.OpenRavoerp("21");
                        sql = " select * from PUR_REQBILL_DETAIL where oano='" + qg.oano + "' and productid ='" + qg.wlbh + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        ToolHelper.CloseSql(conn);
                        if (num == 0)
                        {
                            //添加子表
                            int xh1 = qg.xh + 1;
                            conn = ToolHelper.OpenRavoerp("21");
                            sql = " INSERT INTO PUR_REQBILL_DETAIL(INDEX_CODE,PRODUCTID,NEEDMANY,LIFE_CODE,RE_MARK,NEEDDAY,FNO,TEMPINQTY,FID,INQTY,OANO) " +
                                " VALUES('" + xh1 + "','" + qg.wlbh + "','" + qg.xqsl + "',4,'" + qg.bz + "',to_date('" + qg.yhrq + "','yyyy-mm-dd hh24:mi:ss'),'" + fno + "',0,'" + fid + "',0,'" + qg.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                            //在oa数据库中添加 采购单号和序号
                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = " select B.* from FORMTABLE_MAIN_330 A left join FORMTABLE_MAIN_330_DT1 B on A.id = B.mainid where qgdh='" + qg.oano + "' and bh1='" + qg.wlbh + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string oaid = dt.Rows[0]["ID"].ToString();
                            ToolHelper.CloseSql(conn);

                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = "update FORMTABLE_MAIN_330_DT1 set xh='" + xh1 + "' ,cgdh = '" + fno + "' where id='" + oaid + "'";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                        }
                        return "1";
                    }
                    else
                    {
                        string fno = dt.Rows[0]["FNO"].ToString();
                        string id = dt.Rows[0]["FID"].ToString();
                        //判断子表是否存在 
                        conn = ToolHelper.OpenRavoerp("21");
                        sql = " select * from PUR_REQBILL_DETAIL where oano='" + qg.oano + "' and productid ='" + qg.wlbh + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        ToolHelper.CloseSql(conn);
                        if (num == 0)
                        {
                            //添加子表
                            int xh1 = qg.xh + 1;
                            conn = ToolHelper.OpenRavoerp("21");
                            sql = " INSERT INTO PUR_REQBILL_DETAIL(INDEX_CODE,PRODUCTID,NEEDMANY,LIFE_CODE,RE_MARK,NEEDDAY,FNO,TEMPINQTY,FID,INQTY,OANO) " +
                                " VALUES('" + xh1 + "','" + qg.wlbh + "','" + qg.xqsl + "',4,'" + qg.bz + "',to_date('" + qg.yhrq + "','yyyy-mm-dd hh24:mi:ss'),'" + fno + "',0,'" + id + "',0,'" + qg.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                            //在oa数据库中添加 采购单号和序号
                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = " select B.* from FORMTABLE_MAIN_330 A left join FORMTABLE_MAIN_330_DT1 B on A.id = B.mainid where qgdh='" + qg.oano + "' and bh1='" + qg.wlbh + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string oaid = dt.Rows[0]["ID"].ToString();
                            ToolHelper.CloseSql(conn);

                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = "update FORMTABLE_MAIN_330_DT1 set xh='" + xh1 + "' ,cgdh = '" + fno + "' where id='" + oaid + "'";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                        }
                        return "1";
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                return "";
            }
        }
        /// <summary>
        /// 五厂办公用品请购单
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveQGBG5(string time)
        {
            try
            {
                QGInfo qg = new JavaScriptSerializer().Deserialize<QGInfo>(time);
                OracleConnection conn = ToolHelper.OpenRavoerp("22");
                if (conn != null)
                {
                    //先判断是否存在
                    string sql = " select * from PUR_REQBILL where oano='" + qg.oano + "'";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    var num = dt.Rows.Count;
                    ToolHelper.CloseSql(conn);
                    if (num == 0)
                    {
                        //获取fno和fid
                        conn = ToolHelper.OpenRavoerp("22");
                        sql = " select s_public.nextval fid into :ll_id  from dual ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string fid = dt.Rows[0]["FID"].ToString();

                        conn = ToolHelper.OpenRavoerp("22");
                        sql = " select fno from PUR_REQBILL order by fno desc ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string fnos = dt.Rows[0]["FNO"].ToString();//QG20-0045
                        string a = fnos.Substring(5);//0045
                        int b = Convert.ToInt32(a) + 1;
                        string c = Convert.ToString(b).PadLeft(4, '0');
                        string d = fnos.Substring(0, fnos.Length - 4);
                        string fno = d + c;
                        ToolHelper.CloseSql(conn);


                        conn = ToolHelper.OpenRavoerp("22");
                        sql = " select * from sys_user where fid='" + qg.sqrgh + "' ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        int row = dt.Rows.Count;
                        string user = "0";
                        string dept = "0";
                        if (row > 0)
                        {
                            user = dt.Rows[0]["ID_CODE"].ToString();
                            dept = dt.Rows[0]["department"].ToString();
                        }

                        string department = dept;
                        string name = "系统管理员";
                        string idCode = user;
                        //添加主表 to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss')
                        conn = ToolHelper.OpenRavoerp("22");
                        sql = " INSERT INTO PUR_REQBILL(WRITE_DAY,LIFE_CODE, RE_MARK, SYS_USER ,DEPARTMENT,CHECKMAN,CHECKDAY,FNO,FID,SQ_DD,SAL_NO,BIL_TYPE,EST_DD,CLS_ID,OANO) " +
                            " VALUES(to_date('" + qg.sqrq + "','yyyy-mm-dd hh24:mi:ss'),4,'" + qg.remark + "','" + idCode + "','" + department + "','2282',to_date('" + qg.qrrq + "','yyyy-mm-dd hh24:mi:ss'),'" + fno + "'," +
                            "'" + fid + "',to_date('" + qg.sqrq + "','yyyy-mm-dd hh24:mi:ss'),'" + name + "',9,to_date('" + qg.yjrq + "','yyyy-mm-dd hh24:mi:ss'),'F','" + qg.oano + "') ";
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn);
                        //判断子表是否存在
                        conn = ToolHelper.OpenRavoerp("22");
                        sql = " select * from PUR_REQBILL_DETAIL where oano='" + qg.oano + "' and productid ='" + qg.wlbh + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        ToolHelper.CloseSql(conn);
                        if (num == 0)
                        {
                            //添加子表
                            int xh1 = qg.xh + 1;
                            conn = ToolHelper.OpenRavoerp("22");
                            sql = " INSERT INTO PUR_REQBILL_DETAIL(INDEX_CODE,PRODUCTID,NEEDMANY,LIFE_CODE,RE_MARK,NEEDDAY,FNO,TEMPINQTY,FID,INQTY,OANO) " +
                                " VALUES('" + xh1 + "','" + qg.wlbh + "','" + qg.xqsl + "',4,'" + qg.bz + "',to_date('" + qg.yhrq + "','yyyy-mm-dd hh24:mi:ss'),'" + fno + "',0,'" + fid + "',0,'" + qg.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                            //在oa数据库中添加 采购单号和序号
                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = " select B.* from FORMTABLE_MAIN_330 A left join FORMTABLE_MAIN_330_DT1 B on A.id = B.mainid where qgdh='" + qg.oano + "' and bh5='" + qg.wlbh + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string oaid = dt.Rows[0]["ID"].ToString();
                            ToolHelper.CloseSql(conn);

                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = "update FORMTABLE_MAIN_330_DT1 set xh='" + xh1 + "' ,cgdh = '" + fno + "' where id='" + oaid + "'";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                        }
                        return "1";
                    }
                    else
                    {
                        string fno = dt.Rows[0]["FNO"].ToString();
                        string id = dt.Rows[0]["FID"].ToString();
                        //判断子表是否存在 
                        conn = ToolHelper.OpenRavoerp("22");
                        sql = " select * from PUR_REQBILL_DETAIL where oano='" + qg.oano + "' and productid ='" + qg.wlbh + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        ToolHelper.CloseSql(conn);
                        if (num == 0)
                        {
                            //添加子表
                            int xh1 = qg.xh + 1;
                            conn = ToolHelper.OpenRavoerp("22");
                            sql = " INSERT INTO PUR_REQBILL_DETAIL(INDEX_CODE,PRODUCTID,NEEDMANY,LIFE_CODE,RE_MARK,NEEDDAY,FNO,TEMPINQTY,FID,INQTY,OANO) " +
                                " VALUES('" + xh1 + "','" + qg.wlbh + "','" + qg.xqsl + "',4,'" + qg.bz + "',to_date('" + qg.yhrq + "','yyyy-mm-dd hh24:mi:ss'),'" + fno + "',0,'" + id + "',0,'" + qg.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                            //在oa数据库中添加 采购单号和序号
                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = " select B.* from FORMTABLE_MAIN_330 A left join FORMTABLE_MAIN_330_DT1 B on A.id = B.mainid where qgdh='" + qg.oano + "' and bh5='" + qg.wlbh + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            string oaid = dt.Rows[0]["ID"].ToString();
                            ToolHelper.CloseSql(conn);

                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = "update FORMTABLE_MAIN_330_DT1 set xh='" + xh1 + "' ,cgdh = '" + fno + "' where id='" + oaid + "'";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                        }
                        return "1";
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                return "";
            }
        }

        /// <summary>
        /// 办公用品应付单
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveBGYF(string time)
        {
            //ToolHelper.logger.Debug(time);
            string type = "23";
            OracleConnection conn = ToolHelper.OpenRavoerp(type);
            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction transaction;
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
            myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象
            try
            {
                BGYFInfo yf = new JavaScriptSerializer().Deserialize<BGYFInfo>(time);
                if (conn != null)
                {
                    //先判断是否存在
                    string sql = " select * from RCPYHEAD where oano='" + yf.oano + "'";
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
                        sql = " select * from RCPYHEAD where rcpyid like '%YF%' order by rcpyid desc ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string id_code = dt.Rows[0]["RCPYID"].ToString();//YF20-000091
                        string a = id_code.Substring(5);//
                        int b = Convert.ToInt32(a) + 1;
                        string c = Convert.ToString(b).PadLeft(6, '0');
                        string d = id_code.Substring(0, id_code.Length - 6);
                        id_code = d + c;

                        sql = " select * from sys_user where fid='" + yf.gh + "' ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string user = dt.Rows[0]["ID_CODE"].ToString();
                        string department = dt.Rows[0]["DEPARTMENT"].ToString();

                        //添加主表 to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss')0
                        sql = " INSERT INTO RCPYHEAD (RCPYID,BEGINDAY,PLANPAYDAY,LIFECODE,COMPANY,BZ,HL,USERID,TAX,TAXRATE,MANY,RE_MARK,JD,CHECKER,CHECKDAY,INVOICEID," +
                            " COST,SENDDATE,FDEPT,FBILLMANY,FID,FNOTE,FPLANPAYDAY,BILLTYPE,ZHANG_ID,OANO) " +
                            " VALUES ( '" + id_code + "',to_date('" + yf.sqrq + "','yyyy-mm-dd hh24:mi:ss'),to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss'),'4','" + yf.dwid + "'," +
                            " 'RMB','" + yf.hl + "','" + user + "','" + yf.kslb + "','" + yf.sl + "','" + yf.je + "','" + yf.zy + "','-1','1',to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss'),'" + yf.fph + "'," +
                            " '" + yf.qtfy + "',to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss'),'" + department + "','0','" + fid + "','" + yf.hxyq + "',to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss')," +
                            " '98','" + yf.lzfs + "','" + yf.oano + "')";
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        //判断子表是否存在
                        sql = " select * from RCPYDETAIL where oano='" + yf.oano + "' and FPRODUCT ='" + yf.wlbh + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        if (num == 0)
                        {
                            //添加子表
                            int xh1 = yf.xh + 1;
                            sql = " INSERT INTO RCPYDETAIL (FID,FSOURCEID,FSOURCEINDEX,FPRODUCT,FPRICE,FQTY,FREMARK,FPUNIT,RCPYID,PCS,FSOURCE,FINDEX,FFX,TAX_RTO,TAX,AMTN,AMT,OANO) " +
                                " VALUES('" + fid + "','" + yf.rkdh + "','" + yf.ydxh + "','" + yf.wlbh + "','" + yf.dj + "','" + yf.sl1 + "','" + yf.bz + "','" + yf.dwidd + "','" + id_code + "','1','4','" + xh1 + "','1'," +
                                "'" + yf.sl + "','" + yf.se + "','" + yf.wsbwb + "','" + yf.je1 + "','" + yf.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        string fid = dt.Rows[0]["FID"].ToString();
                        string id_code = dt.Rows[0]["RCPYID"].ToString();
                        int xh1 = yf.xh + 1;
                        sql = " select * from RCPYDETAIL where oano='" + yf.oano + "' and FPRODUCT ='" + yf.wlbh + "' and findex='" + xh1 + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        if (num == 0)
                        {
                            //添加子表
                            sql = " INSERT INTO RCPYDETAIL (FID,FSOURCEID,FSOURCEINDEX,FPRODUCT,FPRICE,FQTY,FREMARK,FPUNIT,RCPYID,PCS,FSOURCE,FINDEX,FFX,TAX_RTO,TAX,AMTN,AMT,OANO) " +
                                " VALUES('" + fid + "','" + yf.rkdh + "','" + yf.ydxh + "','" + yf.wlbh + "','" + yf.dj + "','" + yf.sl1 + "','" + yf.bz + "','" + yf.dwidd + "','" + id_code + "','1','4','" + xh1 + "','1'," +
                                "'" + yf.sl + "','" + yf.se + "','" + yf.wsbwb + "','" + yf.je1 + "','" + yf.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                        }
                    }
                }
                transaction.Commit();
                ToolHelper.CloseSql(conn);
                return "";
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                transaction.Rollback();
                ToolHelper.CloseSql(conn);
                return "";
            }
        }
        /// <summary>
        /// 实业办公用品应付单
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveBGYF1(string time)
        {
            string type = "21";
            OracleConnection conn = ToolHelper.OpenRavoerp(type);
            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction transaction;
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
            myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象
            try
            {
                BGYFInfo yf = new JavaScriptSerializer().Deserialize<BGYFInfo>(time);
                if (conn != null)
                {
                    //先判断是否存在
                    string sql = " select * from RCPYHEAD where oano='" + yf.oano + "'";
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
                        sql = " select * from RCPYHEAD where rcpyid like '%YF%' order by rcpyid desc ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string id_code = dt.Rows[0]["RCPYID"].ToString();//YF20-000091
                        string a = id_code.Substring(5);//
                        int b = Convert.ToInt32(a) + 1;
                        string c = Convert.ToString(b).PadLeft(6, '0');
                        string d = id_code.Substring(0, id_code.Length - 6);
                        id_code = d + c;
                        sql = " select * from sys_user where fid='" + yf.gh + "' ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string user = dt.Rows[0]["ID_CODE"].ToString();
                        string department = dt.Rows[0]["DEPARTMENT"].ToString();

                        //添加主表 to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss')
                        sql = " INSERT INTO RCPYHEAD (RCPYID,BEGINDAY,PLANPAYDAY,LIFECODE,COMPANY,BZ,HL,USERID,TAX,TAXRATE,MANY,RE_MARK,JD,CHECKER,CHECKDAY,INVOICEID," +
                            " COST,SENDDATE,FDEPT,FBILLMANY,FID,FNOTE,FPLANPAYDAY,BILLTYPE,ZHANG_ID,OANO) " +
                            " VALUES ( '" + id_code + "',to_date('" + yf.sqrq + "','yyyy-mm-dd hh24:mi:ss'),to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss'),'4','" + yf.dwid + "'," +
                            " 'RMB','" + yf.hl + "','" + user + "','" + yf.kslb + "','" + yf.sl + "','" + yf.je + "','" + yf.zy + "','-1','1',to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss'),'" + yf.fph + "'," +
                            " '" + yf.qtfy + "',to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss'),'" + department + "','0','" + fid + "','" + yf.hxyq + "',to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss')," +
                            " '98','" + yf.lzfs + "','" + yf.oano + "')";
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        //判断子表是否存在
                        sql = " select * from RCPYDETAIL where oano='" + yf.oano + "' and FPRODUCT ='" + yf.wlbh + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        if (num == 0)
                        {
                            //添加子表
                            int xh1 = yf.xh + 1;
                            sql = " INSERT INTO RCPYDETAIL (FID,FSOURCEID,FSOURCEINDEX,FPRODUCT,FPRICE,FQTY,FREMARK,FPUNIT,RCPYID,PCS,FSOURCE,FINDEX,FFX,TAX_RTO,TAX,AMTN,AMT,OANO) " +
                                " VALUES('" + fid + "','" + yf.rkdh + "','" + yf.ydxh + "','" + yf.wlbh + "','" + yf.dj + "','" + yf.sl1 + "','" + yf.bz + "','" + yf.dwidd + "','" + id_code + "','1','4','" + xh1 + "','1'," +
                                "'" + yf.sl + "','" + yf.se + "','" + yf.wsbwb + "','" + yf.je1 + "','" + yf.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();


                        }
                    }
                    else
                    {
                        string fid = dt.Rows[0]["FID"].ToString();
                        string id_code = dt.Rows[0]["RCPYID"].ToString();
                        int xh1 = yf.xh + 1;
                        sql = " select * from RCPYDETAIL where oano='" + yf.oano + "' and FPRODUCT ='" + yf.wlbh + "' and findex='" + xh1 + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        if (num == 0)
                        {
                            //添加子表

                            sql = " INSERT INTO RCPYDETAIL (FID,FSOURCEID,FSOURCEINDEX,FPRODUCT,FPRICE,FQTY,FREMARK,FPUNIT,RCPYID,PCS,FSOURCE,FINDEX,FFX,TAX_RTO,TAX,AMTN,AMT,OANO) " +
                                " VALUES('" + fid + "','" + yf.rkdh + "','" + yf.ydxh + "','" + yf.wlbh + "','" + yf.dj + "','" + yf.sl1 + "','" + yf.bz + "','" + yf.dwidd + "','" + id_code + "','1','4','" + xh1 + "','1'," +
                                "'" + yf.sl + "','" + yf.se + "','" + yf.wsbwb + "','" + yf.je1 + "','" + yf.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();

                        }
                    }
                }
                transaction.Commit();
                ToolHelper.CloseSql(conn);
                return "";
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                transaction.Rollback();
                ToolHelper.CloseSql(conn);
                return "";
            }
        }
        /// <summary>
        /// 五厂办公用品应付单
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveBGYF5(string time)
        {
            string type = "22";
            OracleConnection conn = ToolHelper.OpenRavoerp(type);
            OracleCommand myCommand = conn.CreateCommand();
            OracleTransaction transaction;
            transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
            myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象
            try
            {
                BGYFInfo yf = new JavaScriptSerializer().Deserialize<BGYFInfo>(time);
                if (conn != null)
                {
                    //先判断是否存在
                    string sql = " select * from RCPYHEAD where oano='" + yf.oano + "'";
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
                        sql = " select * from RCPYHEAD where rcpyid like '%YF%' order by rcpyid desc ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string id_code = dt.Rows[0]["RCPYID"].ToString();//YF20-000091
                        string a = id_code.Substring(5);//
                        int b = Convert.ToInt32(a) + 1;
                        string c = Convert.ToString(b).PadLeft(6, '0');
                        string d = id_code.Substring(0, id_code.Length - 6);
                        id_code = d + c;

                        sql = " select * from sys_user where fid='" + yf.gh + "' ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string user = dt.Rows[0]["ID_CODE"].ToString();
                        string department = dt.Rows[0]["DEPARTMENT"].ToString();

                        //添加主表 to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss')
                        sql = " INSERT INTO RCPYHEAD (RCPYID,BEGINDAY,PLANPAYDAY,LIFECODE,COMPANY,BZ,HL,USERID,TAX,TAXRATE,MANY,RE_MARK,JD,CHECKER,CHECKDAY,INVOICEID," +
                            " COST,SENDDATE,FDEPT,FBILLMANY,FID,FNOTE,FPLANPAYDAY,BILLTYPE,ZHANG_ID,OANO) " +
                            " VALUES ( '" + id_code + "',to_date('" + yf.sqrq + "','yyyy-mm-dd hh24:mi:ss'),to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss'),'4','" + yf.dwid + "'," +
                            " 'RMB','" + yf.hl + "','" + user + "','" + yf.kslb + "','" + yf.sl + "','" + yf.je + "','" + yf.zy + "','-1','1',to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss'),'" + yf.fph + "'," +
                            " '" + yf.qtfy + "',to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss'),'" + department + "','0','" + fid + "','" + yf.hxyq + "',to_date('" + yf.fkrq + "','yyyy-mm-dd hh24:mi:ss')," +
                            " '98','" + yf.lzfs + "','" + yf.oano + "')";
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        //判断子表是否存在
                        sql = " select * from RCPYDETAIL where oano='" + yf.oano + "' and FPRODUCT ='" + yf.wlbh + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        if (num == 0)
                        {
                            //添加子表
                            int xh1 = yf.xh + 1;
                            sql = " INSERT INTO RCPYDETAIL (FID,FSOURCEID,FSOURCEINDEX,FPRODUCT,FPRICE,FQTY,FREMARK,FPUNIT,RCPYID,PCS,FSOURCE,FINDEX,FFX,TAX_RTO,TAX,AMTN,AMT,OANO) " +
                                " VALUES('" + fid + "','" + yf.rkdh + "','" + yf.ydxh + "','" + yf.wlbh + "','" + yf.dj + "','" + yf.sl1 + "','" + yf.bz + "','" + yf.dwidd + "','" + id_code + "','1','4','" + xh1 + "','1'," +
                                "'" + yf.sl + "','" + yf.se + "','" + yf.wsbwb + "','" + yf.je1 + "','" + yf.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        string fid = dt.Rows[0]["FID"].ToString();
                        string id_code = dt.Rows[0]["RCPYID"].ToString();
                        int xh1 = yf.xh + 1;
                        sql = " select * from RCPYDETAIL where oano='" + yf.oano + "' and FPRODUCT ='" + yf.wlbh + "' and findex='" + xh1 + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        num = dt.Rows.Count;
                        if (num == 0)
                        {
                            //添加子表

                            sql = " INSERT INTO RCPYDETAIL (FID,FSOURCEID,FSOURCEINDEX,FPRODUCT,FPRICE,FQTY,FREMARK,FPUNIT,RCPYID,PCS,FSOURCE,FINDEX,FFX,TAX_RTO,TAX,AMTN,AMT,OANO) " +
                                " VALUES('" + fid + "','" + yf.rkdh + "','" + yf.ydxh + "','" + yf.wlbh + "','" + yf.dj + "','" + yf.sl1 + "','" + yf.bz + "','" + yf.dwidd + "','" + id_code + "','1','4','" + xh1 + "','1'," +
                                "'" + yf.sl + "','" + yf.se + "','" + yf.wsbwb + "','" + yf.je1 + "','" + yf.oano + "') ";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();

                        }
                    }
                }
                transaction.Commit();
                ToolHelper.CloseSql(conn);
                return "";
            }
            catch (Exception e)
            {
                ToolHelper.logger.Debug("错误" + e.ToString());
                transaction.Rollback();
                ToolHelper.CloseSql(conn);
                return "";
            }
        }
        #endregion
        #region 其他
        /// <summary>
        /// 模具维修
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveMJWX(string time)
        {
            try
            {
                MJWXInfo mj = new JavaScriptSerializer().Deserialize<MJWXInfo>(time);
                OracleConnection conn = ToolHelper.OpenRavoerp("middle");
                if (conn != null)
                {
                    //先判断是否存在
                    string sql = " select * from OA_MJWXD where oaid='" + mj.oaid + "'";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    var num = dt.Rows.Count;
                    ToolHelper.CloseSql(conn);
                    if (num == 0)
                    {
                        conn = ToolHelper.OpenRavoerp("oa");
                        sql = " SELECT * FROM V_MJWX where lcbh='" + mj.oaid + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string mjbh1 = dt.Rows[0]["mjbh1"].ToString();
                        string mjbh2 = dt.Rows[0]["mjbh2"].ToString();
                        string mjbh5 = dt.Rows[0]["mjbh5"].ToString();
                        string mjbh6 = dt.Rows[0]["mjbh6"].ToString();
                        string ly = dt.Rows[0]["LY"].ToString();
                        string cpwt = dt.Rows[0]["cpwt"].ToString();
                        string mjwt = dt.Rows[0]["mjwt"].ToString();
                        string zrhf = dt.Rows[0]["zrhf"].ToString();
                        string xmfa = dt.Rows[0]["xmfa"].ToString();
                        string sqr = dt.Rows[0]["sqr"].ToString();
                        string sqrq = dt.Rows[0]["sqrq"].ToString();
                        string yqwcsj = dt.Rows[0]["yqwcsj"].ToString();
                        string mjysr = dt.Rows[0]["mjysr"].ToString();
                        string mjysrq = dt.Rows[0]["mjysrq"].ToString();
                        string cpysr = dt.Rows[0]["cpysr"].ToString();
                        string cpysrq = dt.Rows[0]["cpysrq"].ToString();
                        ToolHelper.CloseSql(conn);
                        DateTime nowtime = DateTime.Now;
                        string ntime = Convert.ToString(nowtime);
                        if (mj.fb == "23")//工业
                        {
                            conn = ToolHelper.OpenRavoerp("middle");
                            sql = " INSERT INTO OA_MJWXD (OAID,FMOLDID,FCOMPANY,SOURCE,PRODUCT_PROBLEM,DUTY_ALLOCATION,REPAIR_BLUE_PRINT,APPLICANT,APPLICATION_DATE,NEED_TIME," +
                                "MOULD_CHECK_MAN,MOULD_CHECK_DATE,PRODUCT_CHECK_MAN,PRODUCT_CHECK_DATE,RECEIVING_TIME)" +
                                " VALUES ( '" + mj.oaid + "','" + mjbh2 + "','23','" + ly + "','" + cpwt + "','" + zrhf + "','" + xmfa + "','" + sqr + "',to_date('" + sqrq + "','yyyy-mm-dd hh24:mi:ss')," +
                                "to_date('" + yqwcsj + "','yyyy-mm-dd hh24:mi:ss'),'" + mjysr + "',to_date('" + mjysrq + "','yyyy-mm-dd hh24:mi:ss'),'" + cpysr + "',to_date('" + cpysrq + "','yyyy-mm-dd hh24:mi:ss')," +
                                " to_date('" + ntime + "','yyyy-mm-dd hh24:mi:ss'))";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                            return "1";
                        }
                        else if (mj.fb == "21")//实业
                        {
                            conn = ToolHelper.OpenRavoerp("middle");
                            sql = " INSERT INTO OA_MJWXD (OAID,FMOLDID,FCOMPANY,SOURCE,PRODUCT_PROBLEM,DUTY_ALLOCATION,REPAIR_BLUE_PRINT,APPLICANT,APPLICATION_DATE,NEED_TIME," +
                                "MOULD_CHECK_MAN,MOULD_CHECK_DATE,PRODUCT_CHECK_MAN,PRODUCT_CHECK_DATE,RECEIVING_TIME)" +
                                " VALUES ( '" + mj.oaid + "','" + mjbh1 + "','21','" + ly + "','" + cpwt + "','" + zrhf + "','" + xmfa + "','" + sqr + "',to_date('" + sqrq + "','yyyy-mm-dd hh24:mi:ss')," +
                                "to_date('" + yqwcsj + "','yyyy-mm-dd hh24:mi:ss'),'" + mjysr + "',to_date('" + mjysrq + "','yyyy-mm-dd hh24:mi:ss'),'" + cpysr + "',to_date('" + cpysrq + "','yyyy-mm-dd hh24:mi:ss')," +
                                " to_date('" + ntime + "','yyyy-mm-dd hh24:mi:ss'))";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                            return "1";
                        }
                        else if (mj.fb == "22")//隆威
                        {
                            conn = ToolHelper.OpenRavoerp("middle");
                            sql = " INSERT INTO OA_MJWXD (OAID,FMOLDID,FCOMPANY,SOURCE,PRODUCT_PROBLEM,DUTY_ALLOCATION,REPAIR_BLUE_PRINT,APPLICANT,APPLICATION_DATE,NEED_TIME," +
                                "MOULD_CHECK_MAN,MOULD_CHECK_DATE,PRODUCT_CHECK_MAN,PRODUCT_CHECK_DATE,RECEIVING_TIME)" +
                                " VALUES ( '" + mj.oaid + "','" + mjbh5 + "','22','" + ly + "','" + cpwt + "','" + zrhf + "','" + xmfa + "','" + sqr + "',to_date('" + sqrq + "','yyyy-mm-dd hh24:mi:ss')," +
                                "to_date('" + yqwcsj + "','yyyy-mm-dd hh24:mi:ss'),'" + mjysr + "',to_date('" + mjysrq + "','yyyy-mm-dd hh24:mi:ss'),'" + cpysr + "',to_date('" + cpysrq + "','yyyy-mm-dd hh24:mi:ss')," +
                                " to_date('" + ntime + "','yyyy-mm-dd hh24:mi:ss'))";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                            return "1";
                        }
                        else if (mj.fb == "161")//研森
                        {
                            conn = ToolHelper.OpenRavoerp("middle");
                            sql = " INSERT INTO OA_MJWXD (OAID,FMOLDID,FCOMPANY,SOURCE,PRODUCT_PROBLEM,DUTY_ALLOCATION,REPAIR_BLUE_PRINT,APPLICANT,APPLICATION_DATE,NEED_TIME," +
                                "MOULD_CHECK_MAN,MOULD_CHECK_DATE,PRODUCT_CHECK_MAN,PRODUCT_CHECK_DATE,RECEIVING_TIME)" +
                                " VALUES ( '" + mj.oaid + "','" + mjbh6 + "','141','" + ly + "','" + cpwt + "','" + zrhf + "','" + xmfa + "','" + sqr + "',to_date('" + sqrq + "','yyyy-mm-dd hh24:mi:ss')," +
                                "to_date('" + yqwcsj + "','yyyy-mm-dd hh24:mi:ss'),'" + mjysr + "',to_date('" + mjysrq + "','yyyy-mm-dd hh24:mi:ss'),'" + cpysr + "',to_date('" + cpysrq + "','yyyy-mm-dd hh24:mi:ss')," +
                                " to_date('" + ntime + "','yyyy-mm-dd hh24:mi:ss'))";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                            return "1";
                        }
                        else
                        {
                            return "";
                        }
                    }

                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// <summary>
        /// 存储补助申请单
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveBZ(string time)
        {
            try
            {
                BZInfo bz = new JavaScriptSerializer().Deserialize<BZInfo>(time);
                OracleConnection conn = ToolHelper.OpenRavoerp("141");
                if (conn != null)
                {
                    string bzny = bz.xzny.Substring(0, bz.xzny.Length - 3);
                    //先判断是否存在
                    string sql = " select * from MAN_YCXZLR where manid='" + bz.manid + "' and  oano='" + bz.oano + "'";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    var num = dt.Rows.Count;
                    ToolHelper.CloseSql(conn);
                    if (num == 0)
                    {
                        //添加
                        conn = ToolHelper.OpenRavoerp("141");
                        sql = " INSERT INTO MAN_YCXZLR(MANID,XZXM, XZNY, TDRQ ,JS,AMTN,FS_NO, C_USER,E_USER,REM,OANO) VALUES('" + bz.manid + "', 'A12', '" + bzny + "', SYSDATE, '" + bz.wbzje + "', 0, NULL, 1, 1, 'OA同步','" + bz.oano + "') ";
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn);
                        return "1";
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// <summary>
        /// 产品报价单
        /// </summary>type 0-工业 1-实业 2-隆威
        /// <param name="time"></param>
        /// <returns></returns>
        [WebMethod]
        public string SaveCPBJ(string time)
        {
            CPBJInfo cpbj = new JavaScriptSerializer().Deserialize<CPBJInfo>(time);
            string oaid = cpbj.oaid;
            string cpmc = cpbj.cpmc;
            string cplx1 = cpbj.cplx;
            string gyms = cpbj.gyms;
            string bzyq = cpbj.bzyq;
            string tsyq = cpbj.tsyq;
            string many = cpbj.many;
            string sqr1 = cpbj.sqr;
            string type = cpbj.type;
            try
            {
                string cplx = "";
                if (cplx1 == "0")
                {
                    cplx = "新品";
                }
                else
                {
                    cplx = "常规";
                }
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                string sql = " select * from HRMRESOURCE where id='" + sqr1 + "' ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string sqr = dt.Rows[0]["LASTNAME"].ToString();
                ToolHelper.CloseSql(conn);

                //先判断使用哪个数据库
                if (type == "0")
                {
                    conn = ToolHelper.OpenRavoerp("23");
                }
                else if (type == "1")
                {
                    conn = ToolHelper.OpenRavoerp("21");
                }
                else if (type == "3")
                {
                    conn = ToolHelper.OpenRavoerp("141");
                }
                else
                {
                    conn = ToolHelper.OpenRavoerp("22");
                }

                if (conn != null)
                {
                    sql = " select * from BJD_HEAD where oaid='" + oaid + "' and cpmc='" + cpmc + "' and bzyq='" + bzyq + "' ";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    ToolHelper.CloseSql(conn);
                    var num = dt.Rows.Count;
                    if (num == 0)
                    {
                        //conn = ToolHelper.OpenRavoerp("oa");
                        //sql = " select a.ID,A.lcbh,B.cpmc from FORMTABLE_MAIN_279 A left join FORMTABLE_MAIN_279_DT1 B on a.id=b.mainid where a.lcbh='" + oaid + "' and b.cpmc='" + cpmc + "' ";
                        //cmd = new OracleCommand(sql, conn);
                        //da = new OracleDataAdapter(cmd);
                        //dt = new DataTable();
                        //da.Fill(dt);
                        //string id = dt.Rows[0]["ID"].ToString();
                        //ToolHelper.CloseSql(conn);
                        //修改 增加子表id (暂未修改)

                        SqlSaveHelper.SaveCPBJ(type, oaid, cpmc, cplx, gyms, bzyq, tsyq, many, sqr);
                        return "成功";
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                return "失败";
                throw;
            }
        }
        /// <summary>
        /// 企业微信短号维护
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string AAA()
        {
            //开始
            try
            {
                int no = 0;
                string s = "";
                string id = "";
                //获取wxID
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                string sql = " select userid,shortno from WX_SHORTNO where status=0 ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                no = dt.Rows.Count;
                ToolHelper.CloseSql(conn);
                for (int i = 0; i < no; i++)
                {
                    s = dt.Rows[i]["shortno"].ToString();
                    id = dt.Rows[i]["userid"].ToString();
                    //先获取token，再获取职员信息 再修改
                    string corpid = "ww226ac420227ca99f";
                    string corpsecret = "RJImeiGrEG7b6146BB4WWT0oTY1NmjBElRLsiOhUW3M";
                    string accessToken = QYWeixinHelper.GetQYAccessToken(corpid, corpsecret);
                    //再获取职员信息
                    string url = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token=" + accessToken + "&userid=" + id;
                    string res = ToolHelper.GetHttpResponse(url, 6000);
                    UserInfo userIDInfo = new JavaScriptSerializer().Deserialize<UserInfo>(res);
                    if (userIDInfo.errcode == 0)
                    {
                        UpdateUserInfo user = new UpdateUserInfo();
                        user.userid = userIDInfo.userid;
                        user.name = userIDInfo.name;
                        user.alias = userIDInfo.alias;
                        user.mobile = userIDInfo.mobile;
                        user.department = userIDInfo.department;
                        user.order = userIDInfo.order;
                        user.position = userIDInfo.position;
                        user.gender = userIDInfo.gender;
                        user.email = userIDInfo.email;
                        user.telephone = userIDInfo.telephone;
                        user.is_leader_in_dept = userIDInfo.is_leader_in_dept;
                        user.avatar_mediaid = "";
                        user.enable = userIDInfo.enable;
                        List<AttrsItem> aList = new List<AttrsItem>();
                        Text t = new Text();
                        t.value = s;
                        AttrsItem at = new AttrsItem();
                        at.name = "手机短号";
                        at.type = 0;
                        at.text = t;
                        aList.Add(at);
                        Extattr extattr = new Extattr();
                        extattr.attrs = aList;
                        user.extattr = extattr;
                        user.external_profile = null;
                        //user.external_profile.external_corp_name = "";
                        user.external_position = "";
                        user.address = userIDInfo.@address;
                        string strJson = JsonConvert.SerializeObject(user);
                        //post请求
                        url = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token=" + accessToken;
                        string param = strJson;
                        string callback = ToolHelper.Post(url, param);
                        SendMessageInfo mes = new JavaScriptSerializer().Deserialize<SendMessageInfo>(callback);
                        if (Convert.ToString(mes.errcode) == "0")
                        {
                            //修改已成功的状态
                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = "update WX_SHORTNO set status=1 where userid='" + id + "'";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);

                        }
                        else
                        {
                            string aaaa = "";
                        }
                    }


                    string a = "";


                }
                return "1";

            }
            catch (Exception ee)
            {
                string aa = ee.ToString();
                return "0";
                throw;
            }
        }
        #endregion
    }
}
