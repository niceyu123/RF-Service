using Newtonsoft.Json;
using OAService.MyEntity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

namespace OAService.MyPublic
{
    public class CommonHelper
    {
        //发送信息
        public void SendMessage()
        {
            while (true)
            {
                try
                {//先根据手机号查询微信id 存在就发消息 发送成功修改状态 
                    string type = "oa";
                    OracleConnection conn = ToolHelper.OpenRavoerp(type);
                    if (conn != null)
                    {
                        string sql = " select * from WX_MESSAGE  where SEND='" + 0 + "'order by CREATETIME";
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ToolHelper.CloseSql(conn);
                        int num = dt.Rows.Count;
                        if (num >= 0)
                        {
                            for (int i = 0; i < num; i++)
                            {
                                string id = dt.Rows[i]["ID"].ToString();
                                string phone = dt.Rows[i]["PHONE"].ToString();
                                string system = dt.Rows[i]["SYSTEM"].ToString();
                                string message = dt.Rows[i]["MESSAGE"].ToString();
                                conn = ToolHelper.OpenRavoerp(type);
                                sql = " select userid from WX_ID  where mobile='" + phone + "'";
                                cmd = new OracleCommand(sql, conn);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                ToolHelper.CloseSql(conn);
                                num = dt.Rows.Count;
                                if (num > 0)
                                {
                                    string userid = dt.Rows[0]["userid"].ToString();
                                    string send = QYWeixinHelper.SendText(userid, message);
                                    if (send == "0")
                                    {
                                        DateTime nowTime = DateTime.Now;
                                        conn = ToolHelper.OpenRavoerp(type);
                                        sql = "update WX_MESSAGE set SEND='1',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss') where ID='" + id + "'";
                                        cmd = new OracleCommand(sql, conn);
                                        //da = new OracleDataAdapter(cmd);
                                        int result = cmd.ExecuteNonQuery();
                                        //return result;
                                        ToolHelper.CloseSql(conn);
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(1000);
                }
            }
        }
        public void SendMessages()
        {
            while (true)
            {
                string send = "";
                string userid = "";
                //DateTime nowTime1 = DateTime.Now;
                //ToolHelper.logger.Info(nowTime1.ToString());
                try
                {//先根据手机号查询微信id 存在就发消息 发送成功修改状态 
                    string type = "oa";
                    OracleConnection conn = ToolHelper.OpenRavoerp(type);
                    if (conn != null)
                    {
                        string sql = " select * from WX_MESSAGES  where SEND='" + 0 + "'order by CREATETIME";
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ToolHelper.CloseSql(conn);
                        int num = dt.Rows.Count;
                        if (num >= 0)
                        {
                            for (int i = 0; i < num; i++)
                            {
                                string id = dt.Rows[i]["ID"].ToString();
                                string phone = dt.Rows[i]["PHONE"].ToString();
                                string system = dt.Rows[i]["SYSTEM"].ToString();
                                string message = dt.Rows[i]["MESSAGE"].ToString();
                                conn = ToolHelper.OpenRavoerp(type);
                                sql = " select userid from WX_ID  where mobile='" + phone + "'";
                                cmd = new OracleCommand(sql, conn);
                                da = new OracleDataAdapter(cmd);
                                DataTable dt1 = new DataTable();
                                da.Fill(dt1);
                                ToolHelper.CloseSql(conn);
                                int num1 = dt1.Rows.Count;
                                if (num1 > 0)
                                {
                                    userid = dt1.Rows[0]["userid"].ToString();
                                    send = QYWeixinHelper.SendText(userid, message);
                                    if (send == "0")
                                    {
                                        DateTime nowTime = DateTime.Now;
                                        conn = ToolHelper.OpenRavoerp(type);
                                        sql = "update WX_MESSAGES set SEND='1',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),reason='" + nowTime + "SEND=1" + "' where ID='" + id + "'";
                                        cmd = new OracleCommand(sql, conn);
                                        //da = new OracleDataAdapter(cmd);
                                        int result = cmd.ExecuteNonQuery();
                                        //return result;
                                        ToolHelper.CloseSql(conn);
                                    }
                                    else
                                    {
                                        //发送错误信息
                                        //userid = "IT03";
                                        //string mes = "企业微信发送失败" + phone + message;
                                        //send = QYWeixinHelper.SendText(userid, mes);
                                        //userid = "RAVOIT05";
                                        //send = QYWeixinHelper.SendText(userid, mes);

                                        DateTime nowTime = DateTime.Now;
                                        conn = ToolHelper.OpenRavoerp(type);
                                        sql = "update WX_MESSAGES set SEND='2',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),reason='" + nowTime + "SEND=" + send + "' where ID='" + id + "'";
                                        cmd = new OracleCommand(sql, conn);
                                        //da = new OracleDataAdapter(cmd);
                                        int result = cmd.ExecuteNonQuery();
                                        //return result;
                                        ToolHelper.CloseSql(conn);
                                    }
                                }
                                else
                                {
                                    //发送错误信息
                                    //userid = "IT03";
                                    //string mes = "企业微信找不到人发送失败" + phone + message;
                                    //send = QYWeixinHelper.SendText(userid, mes);
                                    //userid = "RAVOIT05";
                                    //send = QYWeixinHelper.SendText(userid, mes);

                                    DateTime nowTime = DateTime.Now;
                                    conn = ToolHelper.OpenRavoerp(type);
                                    sql = "update WX_MESSAGES set SEND='3',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),reason='" + nowTime + "SEND=3" + "' where ID='" + id + "'";
                                    cmd = new OracleCommand(sql, conn);
                                    //da = new OracleDataAdapter(cmd);
                                    int result = cmd.ExecuteNonQuery();
                                    //return result;
                                    ToolHelper.CloseSql(conn);
                                }
                            }
                        }
                    }
                    else
                    {
                        ToolHelper.CloseSql(conn);
                    }
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Info("send:" + send + "id:" + userid);
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(1000);
                }
            }
        }
        public void SendMessages3()
        {
            while (true)
            {
                string send = "";
                string userid = "";
                try
                {//先根据手机号查询微信id 存在就发消息 发送成功修改状态 
                    string type = "oa";
                    OracleConnection conn = ToolHelper.OpenRavoerp(type);

                    if (conn != null)
                    {
                        string sql = " select * from WX_MESSAGES  where SEND='" + 3 + "' or SEND='" + 2 + "'order by CREATETIME";
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ToolHelper.CloseSql(conn);
                        int num = dt.Rows.Count;
                        if (num >= 0)
                        {
                            for (int i = 0; i < num; i++)
                            {
                                string id = dt.Rows[i]["ID"].ToString();
                                string phone = dt.Rows[i]["PHONE"].ToString();
                                string system = dt.Rows[i]["SYSTEM"].ToString();
                                string message = dt.Rows[i]["MESSAGE"].ToString();
                                string qywxid = dt.Rows[i]["QYWXID"].ToString();

                                //有企业微信ID的
                                if (qywxid != null && qywxid != string.Empty)
                                {
                                    userid = qywxid;
                                    send = QYWeixinHelper.SendText(userid, message);
                                    if (send == "0")
                                    {
                                        DateTime nowTime = DateTime.Now;
                                        conn = ToolHelper.OpenRavoerp(type);
                                        sql = "update WX_MESSAGES set SEND='1',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss') where ID='" + id + "'";
                                        cmd = new OracleCommand(sql, conn);
                                        //da = new OracleDataAdapter(cmd);
                                        int result = cmd.ExecuteNonQuery();
                                        //return result;
                                        ToolHelper.CloseSql(conn);
                                    }
                                    else if (send == "40008")
                                    {
                                        DateTime nowTime = DateTime.Now;
                                        conn = ToolHelper.OpenRavoerp(type);
                                        sql = "update WX_MESSAGES set SEND='40008',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss') where ID='" + id + "'";
                                        cmd = new OracleCommand(sql, conn);
                                        //da = new OracleDataAdapter(cmd);
                                        int result = cmd.ExecuteNonQuery();
                                        //return result;
                                        ToolHelper.CloseSql(conn);
                                    }
                                    else
                                    {
                                        DateTime nowTime = DateTime.Now;
                                        conn = ToolHelper.OpenRavoerp(type);
                                        sql = "update WX_MESSAGES set SEND='2',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss') where ID='" + id + "'";
                                        cmd = new OracleCommand(sql, conn);
                                        //da = new OracleDataAdapter(cmd);
                                        int result = cmd.ExecuteNonQuery();
                                        //return result;
                                        ToolHelper.CloseSql(conn);
                                    }
                                }
                                else
                                {
                                    conn = ToolHelper.OpenRavoerp(type);
                                    sql = " select userid from WX_ID  where mobile='" + phone + "'";
                                    cmd = new OracleCommand(sql, conn);
                                    da = new OracleDataAdapter(cmd);
                                    DataTable dt1 = new DataTable();
                                    da.Fill(dt1);
                                    ToolHelper.CloseSql(conn);
                                    int num1 = dt1.Rows.Count;
                                    if (num1 > 0)
                                    {
                                        userid = dt1.Rows[0]["userid"].ToString();
                                        send = QYWeixinHelper.SendText(userid, message);
                                        if (send == "0")
                                        {
                                            DateTime nowTime = DateTime.Now;
                                            conn = ToolHelper.OpenRavoerp(type);
                                            sql = "update WX_MESSAGES set SEND='1',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss') where ID='" + id + "'";
                                            cmd = new OracleCommand(sql, conn);
                                            //da = new OracleDataAdapter(cmd);
                                            int result = cmd.ExecuteNonQuery();
                                            //return result;
                                            ToolHelper.CloseSql(conn);
                                        }
                                        else if (send == "40008")
                                        {
                                            DateTime nowTime = DateTime.Now;
                                            conn = ToolHelper.OpenRavoerp(type);
                                            sql = "update WX_MESSAGES set SEND='40008',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss') where ID='" + id + "'";
                                            cmd = new OracleCommand(sql, conn);
                                            //da = new OracleDataAdapter(cmd);
                                            int result = cmd.ExecuteNonQuery();
                                            //return result;
                                            ToolHelper.CloseSql(conn);
                                        }
                                        else
                                        {
                                            DateTime nowTime = DateTime.Now;
                                            conn = ToolHelper.OpenRavoerp(type);
                                            sql = "update WX_MESSAGES set SEND='2',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss') where ID='" + id + "'";
                                            cmd = new OracleCommand(sql, conn);
                                            //da = new OracleDataAdapter(cmd);
                                            int result = cmd.ExecuteNonQuery();
                                            //return result;
                                            ToolHelper.CloseSql(conn);
                                        }
                                    }
                                    else
                                    {
                                        DateTime nowTime = DateTime.Now;
                                        conn = ToolHelper.OpenRavoerp(type);
                                        sql = "update WX_MESSAGES set SEND='3',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss') where ID='" + id + "'";
                                        cmd = new OracleCommand(sql, conn);
                                        //da = new OracleDataAdapter(cmd);
                                        int result = cmd.ExecuteNonQuery();
                                        //return result;
                                        ToolHelper.CloseSql(conn);
                                    }
                                }




                            }
                        }
                    }
                    else
                    {
                        ToolHelper.CloseSql(conn);
                    }
                    Thread.Sleep(3600000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Info("send:" + send + "id:" + userid);
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(3600000);
                }
            }
        }
        //获取职员信息
        public void GetUserID()
        {
            while (true)
            {
                try
                {
                    string type = "oa";
                    string corpid = "ww226ac420227ca99f";
                    string corpsecret = "ofSnN2DGGfTnwe7VE9W7_pbqv3i5wzj1bH6OXJ1-YKo";
                    //先获取token 
                    string accessToken = QYWeixinHelper.GetQYAccessToken(corpid, corpsecret);
                    string url = "https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token=" + accessToken + "&department_id=1&fetch_child=1&status=0";
                    string res = ToolHelper.GetHttpResponse(url, 6000);
                    UserIDInfo userIDInfo = new JavaScriptSerializer().Deserialize<UserIDInfo>(res);
                    if (userIDInfo.errcode == 0)
                    {
                        for (int i = 0; i < userIDInfo.userlist.Count; i++)
                        {
                            string userid = userIDInfo.userlist[i].userid;
                            string mobile = userIDInfo.userlist[i].mobile;
                            string name = userIDInfo.userlist[i].name;
                            List<int> dpt = userIDInfo.userlist[i].department;
                            string department = dpt[0].ToString();
                            //先判断是否存在 存在就修改 不存在就添加 
                            OracleConnection conn = ToolHelper.OpenRavoerp(type);
                            string sql = " select * from WX_ID  where userid='" + userid + "'";
                            OracleCommand cmd = new OracleCommand(sql, conn);
                            OracleDataAdapter da = new OracleDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            ToolHelper.CloseSql(conn);
                            int num = dt.Rows.Count;
                            if (dt.Rows.Count > 0)//存在 修改
                            {
                                conn = ToolHelper.OpenRavoerp(type);
                                sql = " update WX_ID set MOBILE='" + mobile + "',NAME='" + name + "' ,DEPARTMENT='" + department + "' where USERID='" + userid + "'";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                            else//不存在 添加
                            {
                                conn = ToolHelper.OpenRavoerp(type);
                                sql = " insert into WX_ID (USERID,MOBILE,NAME,DEPARTMENT) values('" + userid + "','" + mobile + "','" + name + "','" + department + "')";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }
                    }
                    Thread.Sleep(3600000);
                }
                catch (ThreadAbortException ex)
                {
                    //不进行操作
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(600000);
                }
            }
        }
        //获取部门信息
        public void GetDept()
        {
            while (true)
            {
                try
                {
                    string type = "oa";
                    string corpid = "ww226ac420227ca99f";
                    string corpsecret = "RJImeiGrEG7b6146BB4WWT0oTY1NmjBElRLsiOhUW3M";
                    //先获取token 
                    string accessToken = QYWeixinHelper.GetQYAccessToken(corpid, corpsecret);
                    string url = "https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token=" + accessToken + "&id=";
                    string res = ToolHelper.GetHttpResponse(url, 6000);
                    DeptInfo deptInfo = new JavaScriptSerializer().Deserialize<DeptInfo>(res);
                    if (deptInfo.errcode == 0)
                    {
                        for (int i = 0; i < deptInfo.department.Count; i++)
                        {
                            //判断在数据库中是否存在
                            string id = deptInfo.department[i].id;
                            string name = deptInfo.department[i].name;
                            string parentid = deptInfo.department[i].parentid;
                            string order = deptInfo.department[i].order;
                            OracleConnection conn = ToolHelper.OpenRavoerp(type);
                            string sql = " select * from WX_DEPT  where ID='" + id + "'";
                            OracleCommand cmd = new OracleCommand(sql, conn);
                            OracleDataAdapter da = new OracleDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            ToolHelper.CloseSql(conn);
                            int num = dt.Rows.Count;
                            if (dt.Rows.Count > 0)//存在 修改
                            {
                                conn = ToolHelper.OpenRavoerp(type);
                                sql = " update WX_DEPT set NAME='" + name + "',PARENTID='" + parentid + "' ,ORDE='" + order + "' where ID='" + id + "'";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                            else//添加
                            {
                                conn = ToolHelper.OpenRavoerp(type);
                                sql = " insert into WX_DEPT (ID,NAME,PARENTID,ORDE) values('" + id + "','" + name + "','" + parentid + "','" + order + "')";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }
                    }
                    Thread.Sleep(3600000);
                }
                catch (ThreadAbortException ex)
                {
                    //不进行操作
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(600000);
                }
            }
        }
        //存储采购核价单消息
        public void SaveCGHJMessage()
        {
            while (true)
            {
                try
                {
                    string type = "oa";
                    //先查询
                    OracleConnection conn = ToolHelper.OpenRavoerp(type);
                    string sql = " select * from V_WX_CGHJ ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ToolHelper.CloseSql(conn);
                    int num = dt.Rows.Count;
                    for (int i = 0; i < num; i++)
                    {
                        //不存在就添加
                        string id = dt.Rows[i]["id"].ToString();
                        string requestid = dt.Rows[i]["requestid"].ToString();
                        conn = ToolHelper.OpenRavoerp(type);
                        //取分部名称和提交人姓名
                        sql = " select H.lastname,Y.shortname,R.userid from WORKFLOW_CURRENTOPERATOR R left join HRMRESOURCE H on R.USERID=H.ID left join HRMSUBCOMPANY Y on Y.id=H.subcompanyid1 left join V_WX_CGHJ V on V.REQUESTID=R.REQUESTID where R.GROUPID='0' and R.REQUESTID='" + requestid + "' ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        DataTable dt0 = new DataTable();
                        da.Fill(dt0);
                        ToolHelper.CloseSql(conn);
                        int num0 = dt0.Rows.Count;
                        string lastname = dt0.Rows[0]["lastname"].ToString();
                        string fenbu = dt0.Rows[0]["shortname"].ToString();
                        string userid = dt0.Rows[0]["userid"].ToString();
                        //判断是否是归档
                        conn = ToolHelper.OpenRavoerp(type);
                        sql = " select * from (select * from WORKFLOW_CURRENTOPERATOR where requestid='" + requestid + "' ORDER BY groupid desc) where rownum=1 ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        DataTable dt2 = new DataTable();
                        da.Fill(dt2);
                        ToolHelper.CloseSql(conn);
                        int num2 = dt2.Rows.Count;
                        string userid1 = dt2.Rows[0]["userid"].ToString();
                        if (userid != userid1)
                        {

                            conn = ToolHelper.OpenRavoerp(type);
                            sql = " select * from WX_MESSAGE where id='" + id + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            DataTable dt1 = new DataTable();
                            da.Fill(dt1);
                            ToolHelper.CloseSql(conn);
                            int num1 = dt1.Rows.Count;
                            if (num1 == 0)
                            {
                                string system = "OA";
                                string wxid = dt.Rows[i]["wxid"].ToString();
                                string phone = dt.Rows[i]["mobile"].ToString();
                                string workflowcode = dt.Rows[i]["workflowcode"].ToString();
                                string message = fenbu + " " + lastname + " 给您递交了一份采购核价单，单号:" + workflowcode + "，请及时处理！（" + system + "系统）";
                                DateTime nowTime = DateTime.Now;
                                conn = ToolHelper.OpenRavoerp(type);
                                sql = " insert into WX_MESSAGE (ID,PHONE,MESSAGE,CREATETIME,SYSTEM) values('" + id + "','" + phone + "','" + message + "',to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),'" + system + "')";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }

                    }
                    Thread.Sleep(300000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(30000);
                }
            }
        }
        //存储采购订单
        public void SaveCGDDMessage()
        {
            while (true)
            {
                try
                {
                    string type = "oa";
                    //先查询
                    OracleConnection conn = ToolHelper.OpenRavoerp(type);
                    string sql = " select * from V_WX_CGDD ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ToolHelper.CloseSql(conn);
                    int num = dt.Rows.Count;
                    for (int i = 0; i < num; i++)
                    {
                        //不存在就添加
                        string id = dt.Rows[i]["id"].ToString();
                        string requestid = dt.Rows[i]["requestid"].ToString();
                        conn = ToolHelper.OpenRavoerp(type);
                        //取分部名称和提交人姓名
                        sql = " select H.lastname,Y.shortname,R.userid from WORKFLOW_CURRENTOPERATOR R left join HRMRESOURCE H on R.USERID=H.ID left join HRMSUBCOMPANY Y on Y.id=H.subcompanyid1 left join V_WX_CGHJ V on V.REQUESTID=R.REQUESTID where R.GROUPID='0' and R.REQUESTID='" + requestid + "' ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        DataTable dt0 = new DataTable();
                        da.Fill(dt0);
                        ToolHelper.CloseSql(conn);
                        int num0 = dt0.Rows.Count;
                        string lastname = dt0.Rows[0]["lastname"].ToString();
                        string fenbu = dt0.Rows[0]["shortname"].ToString();
                        string userid = dt0.Rows[0]["userid"].ToString();
                        //判断是否是归档
                        conn = ToolHelper.OpenRavoerp(type);
                        sql = " select * from (select * from WORKFLOW_CURRENTOPERATOR where requestid='" + requestid + "' ORDER BY groupid desc) where rownum=1 ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        DataTable dt2 = new DataTable();
                        da.Fill(dt2);
                        ToolHelper.CloseSql(conn);
                        int num2 = dt2.Rows.Count;
                        string userid1 = dt2.Rows[0]["userid"].ToString();
                        if (userid != userid1)
                        {

                            conn = ToolHelper.OpenRavoerp(type);
                            sql = " select * from WX_MESSAGE where id='" + id + "'";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            DataTable dt1 = new DataTable();
                            da.Fill(dt1);
                            ToolHelper.CloseSql(conn);
                            int num1 = dt1.Rows.Count;
                            if (num1 == 0)
                            {
                                string system = "OA";
                                string wxid = dt.Rows[i]["wxid"].ToString();
                                string phone = dt.Rows[i]["mobile"].ToString();
                                string workflowcode = dt.Rows[i]["workflowcode"].ToString();
                                string message = fenbu + " " + lastname + " 给您递交了一份采购订单，单号:" + workflowcode + "，请及时处理！（" + system + "系统）";
                                DateTime nowTime = DateTime.Now;
                                conn = ToolHelper.OpenRavoerp(type);
                                sql = " insert into WX_MESSAGE (ID,PHONE,MESSAGE,CREATETIME,SYSTEM) values('" + id + "','" + phone + "','" + message + "',to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),'" + system + "')";
                                cmd = new OracleCommand(sql, conn);
                                int result = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }

                    }
                    Thread.Sleep(300000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(30000);
                }
            }
        }
        //定时访问接口
        public void GetService()
        {
            while (true)
            {
                try
                {
                    WebClient client = new WebClient();
                    //client.Encoding = Encoding.UTF8;
                    var address = "http://172.16.11.19:1915/OAWebService.asmx?op=GetOrderDetailList";
                    string content = client.DownloadString(address);
                    Thread.Sleep(600000);
                }
                catch (Exception e)
                {
                    ToolHelper.logger.Debug(e.ToString());
                    Thread.Sleep(60000);
                }
            }
        }

        //企业微信添加修改删除部门
        public void AddDept()
        {
            while (true)
            {
                try
                {//先查询状态=0 成功后修改状态=1 
                    string type = "oa";
                    OracleConnection conn = ToolHelper.OpenRavoerp(type);
                    if (conn != null)
                    {
                        string sql = " select * from OA_WX_DEPT where status != '1' order by createtime ";
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ToolHelper.CloseSql(conn);
                        int num = dt.Rows.Count;
                        if (num >= 0)
                        {
                            for (int i = 0; i < num; i++)
                            {
                                string status = dt.Rows[i]["STATUS"].ToString();
                                string id = dt.Rows[i]["ID"].ToString();
                                string departmentname = dt.Rows[i]["DEPARTMENTNAME"].ToString();
                                string subcompanyid1 = dt.Rows[i]["SUBCOMPANYID1"].ToString();
                                string supdepid = dt.Rows[i]["SUPDEPID"].ToString();
                                string wxid = dt.Rows[i]["WXID"].ToString();
                                string name = dt.Rows[i]["NAME"].ToString();
                                string parentid = dt.Rows[i]["PARENTID"].ToString();
                                conn = ToolHelper.OpenRavoerp(type);
                                //先判断状态类型 状态(0-新增 1-已同步 2-修改 3-删除)
                                if (status == "0")//新增
                                {
                                    QYWeixinHelper.AddDept(id, departmentname, subcompanyid1, supdepid);
                                }
                                else if (status == "2")//修改
                                {
                                    QYWeixinHelper.UpdateDept(id, departmentname, subcompanyid1, supdepid, wxid, name, parentid);
                                }
                                else if (status == "3")//删除
                                {
                                    QYWeixinHelper.DeleteDept(id, departmentname, subcompanyid1, supdepid);
                                }
                                else
                                {

                                }

                            }
                        }
                    }
                    Thread.Sleep(100000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(100000);
                }
            }
        }
        //模具ERP-采购订单 中间库到OA
        public void GetERP()
        {
            while (true)
            {
                OracleConnection conn = ToolHelper.OpenRavoerp("middle");
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
                    string sql = " select * from OA_FT_POS_POA where FLC_STATA = '" + 0 + "' ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int num = dt.Rows.Count;
                    if (num != 0)
                    {
                        //添加进OA系统
                        for (int i = 0; i < num; i++)
                        {
                            string id = dt.Rows[i]["ID"].ToString();
                            sql = " select * from OA_FT_POS_POB where PO_ID = '" + id + "' ";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            DataTable dta = new DataTable();
                            da.Fill(dta);
                            int nums = dta.Rows.Count;
                            for (int j = 0; j < nums; j++)
                            {
                                sql = " INSERT INTO OA_FT_POS_POB (ID,PO_ID,LINE_NO,FROM_TAG_NAME,GDINA_ID,GDCODE,GDNAME,MOLD_ID,MOLD_CODE,MOLD_NAME," +
                                    "PIECE_NO,GSTAND,P_UNIT,P_QUAN,C_VALUE,C_PRICE,S_C_FEE,S_PRICE,LC_TOT,SMATER_NAME,P_WEIGHT,NETWEIGHT,SHAPEIFNAME," +
                                    "LENGTH,GWIDTH,HEIGHT,DIAMETER,SAREA,WST_NO,WST_NAME,CRAFTS_EXPLAIN,R_DATE,NOTE) " +
                            " VALUES ( '" + dta.Rows[j]["ID"].ToString() + "','" + dta.Rows[j]["PO_ID"].ToString() + "','" + dta.Rows[j]["LINE_NO"].ToString() + "'," +
                            "'" + dta.Rows[j]["FROM_TAG_NAME"].ToString() + "','" + dta.Rows[j]["GDINA_ID"].ToString() + "','" + dta.Rows[j]["GDCODE"].ToString() + "'," +
                            "'" + dta.Rows[j]["GDNAME"].ToString() + "','" + dta.Rows[j]["MOLD_ID"].ToString() + "','" + dta.Rows[j]["MOLD_CODE"].ToString() + "'," +
                            "'" + dta.Rows[j]["MOLD_NAME"].ToString() + "','" + dta.Rows[j]["PIECE_NO"].ToString() + "','" + dta.Rows[j]["GSTAND"].ToString() + "'," +
                            "'" + dta.Rows[j]["P_UNIT"].ToString() + "','" + dta.Rows[j]["P_QUAN"].ToString() + "','" + dta.Rows[j]["C_VALUE"].ToString() + "'," +
                            "'" + dta.Rows[j]["C_PRICE"].ToString() + "','" + dta.Rows[j]["S_C_FEE"].ToString() + "','" + dta.Rows[j]["S_PRICE"].ToString() + "'," +
                            "'" + dta.Rows[j]["LC_TOT"].ToString() + "','" + dta.Rows[j]["SMATER_NAME"].ToString() + "','" + dta.Rows[j]["P_WEIGHT"].ToString() + "'," +
                            "'" + dta.Rows[j]["NETWEIGHT"].ToString() + "','" + dta.Rows[j]["SHAPEIFNAME"].ToString() + "','" + dta.Rows[j]["LENGTH"].ToString() + "'," +
                            "'" + dta.Rows[j]["GWIDTH"].ToString() + "','" + dta.Rows[j]["HEIGHT"].ToString() + "','" + dta.Rows[j]["DIAMETER"].ToString() + "'," +
                            "'" + dta.Rows[j]["SAREA"].ToString() + "','" + dta.Rows[j]["WST_NO"].ToString() + "','" + dta.Rows[j]["WST_NAME"].ToString() + "'," +
                            "'" + dta.Rows[j]["CRAFTS_EXPLAIN"].ToString() + "','" + dta.Rows[j]["R_DATE"].ToString() + "','" + dta.Rows[j]["NOTE"].ToString() + "')";
                                cmd = new OracleCommand(sql, conn1);
                                int result1 = cmd.ExecuteNonQuery();
                            }
                            sql = " INSERT INTO OA_FT_POS_POA (ID,CATEGORY_ID,CATEGORYNAME,PO_NO,CUS_NO,CUS_NAME,R_DATE,CURY_NAME,MVAR_1,SETTLE_CAPTION," +
                                "TAX_TYPE_NAME,TAX_RATE,STATE_NAME,NOTE,NICKNAME,CA_USER_NO1,CA_USER_NO2,CA_USER_NO3,CA_USER_NO4,CA_USER_NO5,FLC_STATA,CR_DATE) " +
                            " VALUES ( '" + dt.Rows[i]["ID"].ToString() + "', '" + dt.Rows[i]["CATEGORY_ID"].ToString() + "','" + dt.Rows[i]["CATEGORYNAME"].ToString() + "'," +
                            "'" + dt.Rows[i]["PO_NO"].ToString() + "','" + dt.Rows[i]["CUS_NO"].ToString() + "','" + dt.Rows[i]["CUS_NAME"].ToString() + "'," +
                            "'" + dt.Rows[i]["R_DATE"].ToString() + "','" + dt.Rows[i]["CURY_NAME"].ToString() + "','" + dt.Rows[i]["MVAR_1"].ToString() + "'," +
                            "'" + dt.Rows[i]["SETTLE_CAPTION"].ToString() + "','" + dt.Rows[i]["TAX_TYPE_NAME"].ToString() + "','" + dt.Rows[i]["TAX_RATE"].ToString() + "'," +
                            "'" + dt.Rows[i]["STATE_NAME"].ToString() + "','" + dt.Rows[i]["NOTE"].ToString() + "','" + dt.Rows[i]["NICKNAME"].ToString() + "'," +
                            "'" + dt.Rows[i]["CA_USER_NO1"].ToString() + "','" + dt.Rows[i]["CA_USER_NO2"].ToString() + "','" + dt.Rows[i]["CA_USER_NO3"].ToString() + "'," +
                            "'" + dt.Rows[i]["CA_USER_NO4"].ToString() + "','" + dt.Rows[i]["CA_USER_NO5"].ToString() + "','0'," +
                            "'" + DateTime.Now + "')";
                            cmd = new OracleCommand(sql, conn1);
                            int result2 = cmd.ExecuteNonQuery();

                            sql = " UPDATE OA_FT_POS_POA SET FLC_STATA = '" + 1 + "' WHERE ID =  '" + id + "' ";
                            cmd = new OracleCommand(sql, conn);
                            int result3 = cmd.ExecuteNonQuery();


                        }

                    }

                    transaction.Commit();
                    transaction1.Commit();
                    ToolHelper.CloseSql(conn);
                    ToolHelper.CloseSql(conn1);
                    Thread.Sleep(120000);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    transaction1.Rollback();
                    ToolHelper.CloseSql(conn);
                    ToolHelper.CloseSql(conn1);
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(120000);
                }
            }
        }
        public void getERPWorkflow()
        {
            while (true)
            {
                try
                {

                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    string sql = " select * from OA_FT_POS_POA where FLC_STATA='0' ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    int num1 = dt1.Rows.Count;
                    ToolHelper.CloseSql(conn);
                    for (int j = 0; j < num1; j++)
                    {


                        conn = ToolHelper.OpenRavoerp("oa");
                        sql = " select a.*,x.*,b.id name1,c.id name2,d.id name3,e.id name4,f.id name5,g.id name from OA_FT_POS_POA a" +
                           " left join HRMRESOURCE b on a.CA_USER_NO1=b.workcode " +
                           " left join HRMRESOURCE c on a.CA_USER_NO2=c.workcode " +
                           " left join HRMRESOURCE d on a.CA_USER_NO3=d.workcode " +
                           " left join HRMRESOURCE e on a.CA_USER_NO4=e.workcode " +
                           " left join HRMRESOURCE f on a.CA_USER_NO5=f.workcode " +
                           " left join HRMRESOURCE g on a.nickname=g.workcode " +
                           " left join OA_FT_POS_POB x on a.id=x.PO_ID where a.FLC_STATA='0' and a.id='" + dt1.Rows[j]["ID"].ToString() + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        int num = dt.Rows.Count;
                        ToolHelper.CloseSql(conn);
                        string name = dt.Rows[0]["name"].ToString();
                        string name1 = dt.Rows[0]["name1"].ToString();
                        string name2 = dt.Rows[0]["name2"].ToString();
                        string name3 = dt.Rows[0]["name3"].ToString();
                        string name4 = dt.Rows[0]["name4"].ToString();
                        string name5 = dt.Rows[0]["name5"].ToString();
                        if (name2 == "")
                        {
                            name2 = name1;
                            name3 = name1;
                            name4 = name1;
                            name5 = name1;
                        }
                        else if (name3 == "")
                        {
                            name3 = name2;
                            name4 = name2;
                            name5 = name2;
                        }
                        else if (name4 == "")
                        {
                            name4 = name3;
                            name5 = name3;
                        }
                        else if (name5 == "")
                        {
                            name5 = name4;
                        }

                        WorkflowServiceXml.WorkflowServiceXml wsx = new WorkflowServiceXml.WorkflowServiceXml();
                        string xml0 = "<WorkflowRequestInfo> " +
          "<requestName>R0-模具ERP-采购订单</requestName> " +
         " <requestLevel>0</requestLevel> " +
          "<workflowBaseInfo> " +
         " <workflowId>6722</workflowId> " +
         " </workflowBaseInfo> " +
         //创建人
         " <creatorId>" + name + "</creatorId> " +
         " <workflowMainTableInfo> " +
         "   <requestRecords> " +
          //主表
          "    <weaver.workflow.webservices.WorkflowRequestTableRecord> " +
           "     <recordOrder>0</recordOrder> " +
           "     <workflowRequestTableFields> " +

           "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CATEGORY_ID</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["CATEGORY_ID"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

          "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CATEGORYNAME</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["CATEGORYNAME"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

          "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>PO_NO</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["PO_NO"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

          "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CUS_NO</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["CUS_NO"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CUS_NAME</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["CUS_NAME"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>R_DATE</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["R_DATE"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CURY_NAME</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["CURY_NAME"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>MVAR_1</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["MVAR_1"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>SETTLE_CAPTION</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["SETTLE_CAPTION"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>TAX_TYPE_NAME</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["TAX_TYPE_NAME"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>TAX_RATE</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["TAX_RATE"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>STATE_NAME</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["STATE_NAME"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>NOTE</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["NOTE"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                              "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CA_USER_NO1</fieldName> " +
           "         <fieldValue>" + name1 + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                              "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CA_USER_NO2</fieldName> " +
           "         <fieldValue>" + name2 + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                              "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CA_USER_NO3</fieldName> " +
           "         <fieldValue>" + name3 + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                              "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CA_USER_NO4</fieldName> " +
           "         <fieldValue>" + name4 + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                              "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CA_USER_NO5</fieldName> " +
           "         <fieldValue>" + name5 + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                                                  "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
                              "          <fieldName>NICKNAME</fieldName> " +
                               "         <fieldValue>" + name + "</fieldValue> " +
                                "       <isView>true</isView> " +
                                  "      <isEdit>true</isEdit> " +
                                      "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                              "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
          "          <fieldName>CR_DATE</fieldName> " +
           "         <fieldValue>" + dt.Rows[0]["CR_DATE"].ToString() + "</fieldValue> " +
            "       <isView>true</isView> " +
              "      <isEdit>true</isEdit> " +
                  "</weaver.workflow.webservices.WorkflowRequestTableField> " +

               " </workflowRequestTableFields> " +
             " </weaver.workflow.webservices.WorkflowRequestTableRecord> " +
           " </requestRecords> " +
         " </workflowMainTableInfo> " +
          //主表结束

          " <workflowDetailTableInfos> " +
         "   <weaver.workflow.webservices.WorkflowDetailTableInfo> " +
          "    <workflowRequestTableRecords> ";

                        string xml1 = "";
                        for (int i = 0; i < num; i++)
                        {
                            xml1 = xml1 + "  <weaver.workflow.webservices.WorkflowRequestTableRecord> " +
             "     <recordOrder>0</recordOrder> " +
              "    <workflowRequestTableFields> " +
                                " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>FROM_TAG_NAME</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["FROM_TAG_NAME"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>GDCODE</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["GDCODE"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>GDNAME</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["GDNAME"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>MOLD_ID</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["MOLD_ID"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>MOLD_CODE</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["MOLD_CODE"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>MOLD_NAME</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["MOLD_NAME"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>PIECE_NO</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["PIECE_NO"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>GSTAND</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["GSTAND"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>P_UNIT</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["P_UNIT"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>P_QUAN</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["P_QUAN"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>C_VALUE</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["C_VALUE"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>C_PRICE</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["C_PRICE"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>S_C_FEE</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["S_C_FEE"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>S_PRICE</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["S_PRICE"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>LC_TOT</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["LC_TOT"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>SMATER_NAME</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["SMATER_NAME"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>P_WEIGHT</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["P_WEIGHT"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>NETWEIGHT</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["NETWEIGHT"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>SHAPEIFNAME</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["SHAPEIFNAME"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>LENGTH</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["LENGTH"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>GWIDTH</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["GWIDTH"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>HEIGHT</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["HEIGHT"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>DIAMETER</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["DIAMETER"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>SAREA</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["SAREA"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>WST_NO</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["WST_NO"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>WST_NAME</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["WST_NAME"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>CRAFTS_EXPLAIN</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["CRAFTS_EXPLAIN"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                    " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                "      <fieldName>R_DATE</fieldName> " +
                 "     <fieldValue>" + dt.Rows[i]["R_DATE"].ToString() + "</fieldValue> " +
                  "    <isView>true</isView> " +
                   "   <isEdit>true</isEdit> " +
                    "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                     "  </workflowRequestTableFields> " +
                "  </weaver.workflow.webservices.WorkflowRequestTableRecord> ";
                        }
                        string xml2 = "   </workflowRequestTableRecords> " +
          "  </weaver.workflow.webservices.WorkflowDetailTableInfo> " +
         " </workflowDetailTableInfos> " +

         "</WorkflowRequestInfo> ";
                        string xml = xml0 + xml1 + xml2;
                        int b = Convert.ToInt32(name);//创建人
                        string ab = wsx.doCreateWorkflowRequest(xml, b);
                        int re = Convert.ToInt32(ab);
                        if (re > 0)//成功
                        {
                            conn = ToolHelper.OpenRavoerp("oa");
                            sql = " Delete OA_FT_POS_POA where id='" + dt1.Rows[j]["ID"].ToString() + "' ";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                            sql = " Delete OA_FT_POS_POB where PO_ID='" + dt1.Rows[j]["ID"].ToString() + "' ";
                            cmd = new OracleCommand(sql, conn);
                            result = cmd.ExecuteNonQuery();
                            ToolHelper.CloseSql(conn);
                        }
                        else//失败
                        {

                        }
                    }
                    Thread.Sleep(120000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(120000);
                }
            }
        }

        public void getZCYCWorkflow()
       {
            while (true)
            {
                try
                {
                    string time = DateTime.Now.ToString("d");
                    OracleConnection conn = ToolHelper.OpenRavoerp("middle");
                    string sql = " select * from STORAGE_DELAY_MAIN where STATUS='0'  ";
                    //string sql = " select * from STORAGE_DELAY_MAIN where STATUS='0' and createdate > to_date('20200819','yyyy-mm-dd') ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ToolHelper.CloseSql(conn);
                    int num = dt.Rows.Count;
                    if (num > 0)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            WorkflowServiceXml.WorkflowServiceXml wsx = new WorkflowServiceXml.WorkflowServiceXml();
                            string xml0 = "<WorkflowRequestInfo> " +
              "<requestName>R0-自产完工15天未出订单明细</requestName> " +
             " <requestLevel>0</requestLevel> " +
              "<workflowBaseInfo> " +
             " <workflowId>8721</workflowId> " +
             " </workflowBaseInfo> " +
             //创建人
             " <creatorId>6175</creatorId> " +
             " <workflowMainTableInfo> " +
             "   <requestRecords> " +
              //主表
              "    <weaver.workflow.webservices.WorkflowRequestTableRecord> " +
               "     <recordOrder>0</recordOrder> " +
               "     <workflowRequestTableFields> " +

               "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
              "          <fieldName>COMPANYNAME</fieldName> " +
               "         <fieldValue>" + dt.Rows[i]["COMPANYNAME"].ToString() + "</fieldValue> " +
                "       <isView>true</isView> " +
                  "      <isEdit>true</isEdit> " +
                      "</weaver.workflow.webservices.WorkflowRequestTableField> " +

              "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
              "          <fieldName>oaid</fieldName> " +
               "         <fieldValue>" + dt.Rows[i]["oaid"].ToString() + "</fieldValue> " +
                "       <isView>true</isView> " +
                  "      <isEdit>true</isEdit> " +
                      "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                       "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
              "          <fieldName>xfywbm</fieldName> " +
               "         <fieldValue>" + dt.Rows[i]["departmentid"].ToString() + "</fieldValue> " +
                "       <isView>true</isView> " +
                  "      <isEdit>true</isEdit> " +
                      "</weaver.workflow.webservices.WorkflowRequestTableField> " +

              "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
              "          <fieldName>CREATEDATE</fieldName> " +
               "         <fieldValue>" + dt.Rows[i]["CREATEDATE"].ToString() + "</fieldValue> " +
                "       <isView>true</isView> " +
                  "      <isEdit>true</isEdit> " +
                      "</weaver.workflow.webservices.WorkflowRequestTableField> " +

              "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
              "          <fieldName>GDRY</fieldName> " +
               "         <fieldValue>" + dt.Rows[i]["GDRY"].ToString() + "</fieldValue> " +
                "       <isView>true</isView> " +
                  "      <isEdit>true</isEdit> " +
                      "</weaver.workflow.webservices.WorkflowRequestTableField> " +

              "       <weaver.workflow.webservices.WorkflowRequestTableField> " +
              "          <fieldName>AUDIT_DEPTID</fieldName> " +
               "         <fieldValue>" + dt.Rows[i]["AUDIT_DEPTID"].ToString() + "</fieldValue> " +
                "       <isView>true</isView> " +
                  "      <isEdit>true</isEdit> " +
                      "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                   " </workflowRequestTableFields> " +
                 " </weaver.workflow.webservices.WorkflowRequestTableRecord> " +
               " </requestRecords> " +
             " </workflowMainTableInfo> " +
              //主表结束

              " <workflowDetailTableInfos> " +
             "   <weaver.workflow.webservices.WorkflowDetailTableInfo> " +
              "    <workflowRequestTableRecords> ";

                            string oaid = dt.Rows[i]["OAID"].ToString();
                            string createDate= dt.Rows[i]["CREATEDATE"].ToString();
                            string companyname = dt.Rows[i]["COMPANYNAME"].ToString();
                            string mfid = dt.Rows[i]["MFID"].ToString();
                            conn = ToolHelper.OpenRavoerp("middle");
                            sql = " select * from STORAGE_DELAY where oaid='" + oaid + "' and mfid='"+mfid+"' and companyname='"+ companyname + "' ";
                            //sql = " select * from STORAGE_DELAY where oaid='" + oaid + "' and creatdate > to_date('2020-08-19','yyyy-mm-dd') ";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            DataTable dts = new DataTable();
                            da.Fill(dts);
                            ToolHelper.CloseSql(conn);
                            int nums = dts.Rows.Count;
                            string xml1 = "";
                            if (nums > 0)
                            {
                                for (int j = 0; j < nums; j++)
                                {
                                    string plandoday = dts.Rows[j]["plandoday"].ToString();
                                    string plan = plandoday.Substring(0, plandoday.Length - 8);
                                    string squeday = dts.Rows[j]["squeday"].ToString();
                                    string squ = squeday.Substring(0, squeday.Length - 8);
                                    xml1 = xml1 + "  <weaver.workflow.webservices.WorkflowRequestTableRecord> " +
                     "     <recordOrder>0</recordOrder> " +
                      "    <workflowRequestTableFields> " +
                                        " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>orderid</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["orderid"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>HF_CUST_NO</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["HF_CUST_NO"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>COMPANYID</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["COMPANYID"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>ISPARTS</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["ISPARTS"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>ITEMNO</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["ITEMNO"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>PARITEMNO</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["PARITEMNO"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>PRODUCT_NAME</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["PRODUCT_NAME"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>cust_prdno</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["cust_prdno"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>INDEX_CODE</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["INDEX_CODE"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>in_many</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["in_many"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>OUT_MANY</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["OUT_MANY"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>plandoday</fieldName> " +
                         "     <fieldValue>" + plan + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>squeday</fieldName> " +
                         "     <fieldValue>" + squ + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                            " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>overdate</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["overdate"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                        " <weaver.workflow.webservices.WorkflowRequestTableField> " +
                        "      <fieldName>CID</fieldName> " +
                         "     <fieldValue>" + dts.Rows[j]["CID"].ToString() + "</fieldValue> " +
                          "    <isView>true</isView> " +
                           "   <isEdit>true</isEdit> " +
                            "</weaver.workflow.webservices.WorkflowRequestTableField> " +

                             "  </workflowRequestTableFields> " +
                        "  </weaver.workflow.webservices.WorkflowRequestTableRecord> ";
                                }

                            }
                            string xml2 = "   </workflowRequestTableRecords> " +
                                              "  </weaver.workflow.webservices.WorkflowDetailTableInfo> " +
                                             " </workflowDetailTableInfos> " +

                                             "</WorkflowRequestInfo> ";
                            string xml = xml0 + xml1 + xml2;
                            int b = 6175;//创建人
                            string ab = wsx.doCreateWorkflowRequest(xml, b);
                            int re = Convert.ToInt32(ab);
                            if (re > 0)//成功
                            {
                                DateTime nowTime = DateTime.Now;
                                string id = dt.Rows[i]["ID"].ToString();
                                conn = ToolHelper.OpenRavoerp("middle");
                                sql = " update STORAGE_DELAY_MAIN set status='1',SYNTIME=to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss') where id='"+id+"' ";
                                cmd = new OracleCommand(sql, conn);
                                int resultt = cmd.ExecuteNonQuery();
                                ToolHelper.CloseSql(conn);
                            }
                        }
                    }
                    Thread.Sleep(120000);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(120000);
                }
            }
        }
        /// <summary>
        /// 根据生效日期同步人员档案归属
        /// </summary>
        public void getRYDAWorkflow()
        {
            while (true)
            {
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                OracleTransaction transaction;
                transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
                myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象

                OracleConnection conn1 = ToolHelper.OpenRavoerp("24");
                OracleCommand myCommand1 = conn1.CreateCommand();
                OracleTransaction transaction1;
                transaction1 = conn1.BeginTransaction(IsolationLevel.ReadCommitted);
                myCommand1.Transaction = transaction1;
                try
                {
                    DateTime nowTime = DateTime.Now;
                    string nt = nowTime.ToString("yyyy-MM-dd");
                    //取出已归档 状态未同步 生效日期小于当前日期 的流程id
                    string sql = " select * from workflow_requestbase where requestid in (select requestid from formtable_main_474 where zt='0' and sxrq<'"+nt+"') and currentnodetype='3' ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int num = dt.Rows.Count;
                    if (num != 0)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            string requestid = dt.Rows[i]["requestid"].ToString();
                            sql = "select * from formtable_main_474 a left join formtable_main_474_DT1 b on a.id=b.mainid where a.requestid='"+ requestid + "' ";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            DataTable dta = new DataTable();
                            da.Fill(dta);
                            int nums = dta.Rows.Count;
                            for (int j = 0; j < nums; j++)
                            {
                                string id_code = dta.Rows[j]["yggh"].ToString();
                                string xxzgs = dta.Rows[j]["xxzgs"].ToString();
                                string xz = ToolHelper.GetCompany(xxzgs);

                                string xbzgs = dta.Rows[j]["xbzgs"].ToString();
                                string bz = ToolHelper.GetCompany(xbzgs);

                                string xkqgs = dta.Rows[j]["xkqgs"].ToString();
                                string kq = ToolHelper.GetCompany(xkqgs);

                                string xjtbbfl = dta.Rows[j]["xjtbbfl"].ToString();
                                string xfyft = dta.Rows[j]["xfyft"].ToString();

                                sql = "update man_tb set gs_bz='" + bz + "',gs_xz='" + xz + "',gs_kq='" + kq + "',jt_type='" + xjtbbfl + "',is_ft='" + xfyft + "'" +
                            " where man_id='" + id_code + "'";
                                cmd = new OracleCommand(sql, conn1);
                                int result1 = cmd.ExecuteNonQuery();

                            }
                            sql = "update formtable_main_474 set zt='1' where requestid='" + requestid + "' ";
                            cmd = new OracleCommand(sql, conn);
                            int result2 = cmd.ExecuteNonQuery();
                        }

                    }

                    transaction.Commit();
                    transaction1.Commit();
                    ToolHelper.CloseSql(conn);
                    ToolHelper.CloseSql(conn1);
                    Thread.Sleep(120000);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    transaction1.Rollback();
                    ToolHelper.CloseSql(conn);
                    ToolHelper.CloseSql(conn1);
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(120000);
                }
            }
        }
        /// <summary>
        /// 同步车辆信息
        /// </summary>
        public void getCLWorkflow()
        {
            while (true)
            {
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                OracleTransaction transaction;
                transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);//开启本地事务
                myCommand.Transaction = transaction;//为挂起的本地事务分配事务对象

           
                try
                {
                    DateTime nowTime = DateTime.Now;
                    string nt = nowTime.ToString("yyyy-MM-dd");
                    //取出已归档 状态未同步 的流程id
                    string sql = " select * from workflow_requestbase where requestid in (select requestid from formtable_main_476 where tbbz='0') and currentnodetype='3' ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int num = dt.Rows.Count;
                    if (num != 0)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            string requestid = dt.Rows[i]["requestid"].ToString();
                            sql = "select * from formtable_main_476 where requestid='" + requestid + "' ";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            DataTable dta = new DataTable();
                            da.Fill(dta);
                            int nums = dta.Rows.Count;
                            string sqr = dta.Rows[i]["sqr"].ToString();
                            string clhp = dta.Rows[i]["clhp"].ToString();
                            string clhp2 = dta.Rows[i]["clhp2"].ToString();
                            string cph = "";
                            if (clhp2=="" || clhp2==null)
                            {
                                cph = clhp;
                            }
                            else
                            {
                                cph = clhp + ";" + clhp2;
                            }
                            //先查询自定义表是否有车牌信息
                            sql = "select id,Field158 from Cus_Fielddata where scopeid =-1 and id='"+sqr+"' ";
                            cmd = new OracleCommand(sql, conn);
                            da = new OracleDataAdapter(cmd);
                            DataTable dts = new DataTable();
                            da.Fill(dts);
                            string cl= dts.Rows[i]["Field158"].ToString();
                            string cphs = "";
                            if(cl=="" || cl == null)
                            {
                                cphs = cph;
                            }
                            else
                            {
                                cphs = cl + ";" + cph;
                            }
                            sql = "update Cus_Fielddata set Field158='"+cphs+ "' where scopeid =-1 and id='" + sqr + "' ";
                            cmd = new OracleCommand(sql, conn);
                            int result2 = cmd.ExecuteNonQuery();

                            sql = "update formtable_main_476 set tbbz='1' where requestid='" + requestid + "' ";
                            cmd = new OracleCommand(sql, conn);
                            int result3 = cmd.ExecuteNonQuery();

                        }

                    }

                    transaction.Commit();
                    ToolHelper.CloseSql(conn);
                    Thread.Sleep(120000);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ToolHelper.CloseSql(conn);
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(120000);
                }
            }
        }



    }
}