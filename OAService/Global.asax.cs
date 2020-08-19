using OAService.MyPublic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace OAService
{
    public class Global : System.Web.HttpApplication 
    {
        public static System.Threading.Timer stateTimer = null;
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                //// 在应用程序启动时运行的代码
                //System.Threading.AutoResetEvent autoEvent = new System.Threading.AutoResetEvent(false);
                //System.Threading.TimerCallback timerdelegate = new System.Threading.TimerCallback(timerCall);
                //int IntervalTime = 60000;
                //stateTimer = new System.Threading.Timer(timerdelegate, autoEvent, 0, IntervalTime);

                //推送消息
                //Thread sendMessage = new Thread(new ThreadStart(new CommonHelper().SendMessage));
                //sendMessage.IsBackground = true;
                //sendMessage.Start();

                ////存储采购核价信息
                //Thread saveCGHJMessage = new Thread(new ThreadStart(new CommonHelper().SaveCGHJMessage));
                //saveCGHJMessage.IsBackground = true;
                //saveCGHJMessage.Start();
                ////存储采购订单
                //Thread saveCGDDMessage = new Thread(new ThreadStart(new CommonHelper().SaveCGDDMessage));
                //saveCGDDMessage.IsBackground = true;
                //saveCGDDMessage.Start();

                //添加企业微信部门
                //Thread addDept = new Thread(new ThreadStart(new CommonHelper().AddDept));
                //addDept.IsBackground = true;
                //addDept.Start();


                Thread sendMessages = new Thread(new ThreadStart(new CommonHelper().SendMessages));
                sendMessages.IsBackground = true;
                sendMessages.Start();
                Thread sendMessages3 = new Thread(new ThreadStart(new CommonHelper().SendMessages3));
                sendMessages3.IsBackground = true;
                sendMessages3.Start();
                //获取微信id
                Thread getUserID = new Thread(new ThreadStart(new CommonHelper().GetUserID));
                getUserID.IsBackground = true;
                getUserID.Start();
                //获取部门信息
                Thread getDept = new Thread(new ThreadStart(new CommonHelper().GetDept));
                getDept.IsBackground = true;
                getDept.Start();

                //定时访问接口
                Thread getService = new Thread(new ThreadStart(new CommonHelper().GetService));
                getService.IsBackground = true;
                getService.Start();
                //模具ERP-采购订单 中间库到OA
                Thread getERP = new Thread(new ThreadStart(new CommonHelper().GetERP));
                getERP.IsBackground = true;
                getERP.Start();
                //创建模具ERP-采购订单流程
                Thread getERPWorkflow = new Thread(new ThreadStart(new CommonHelper().getERPWorkflow));
                getERPWorkflow.IsBackground = true;
                getERPWorkflow.Start();
            }
            catch (Exception a)
            {
                ToolHelper.logger.Debug(a.ToString());
            }

        }
        //private void timerCall(object arts)
        //{
        //        string send = "";
        //        string userid = "";
        //        DateTime nowTime1 = DateTime.Now;
        //        //ToolHelper.logger.Debug(nowTime1.ToString());
        //        try
        //        {//先根据手机号查询微信id 存在就发消息 发送成功修改状态 
        //            string type = "oa";
        //            OracleConnection conn = ToolHelper.OpenRavoerp(type);
        //            if (conn != null)
        //            {
        //                string sql = " select * from WX_MESSAGES  where SEND='" + 0 + "'order by CREATETIME";
        //                OracleCommand cmd = new OracleCommand(sql, conn);
        //                OracleDataAdapter da = new OracleDataAdapter(cmd);
        //                DataTable dt = new DataTable();
        //                da.Fill(dt);
        //                ToolHelper.CloseSql(conn);
        //                int num = dt.Rows.Count;
        //                if (num >= 0)
        //                {
        //                    ToolHelper.logger.Info(num.ToString());
        //                    for (int i = 0; i < num; i++)
        //                    {
        //                        string id = dt.Rows[i]["ID"].ToString();
        //                        string phone = dt.Rows[i]["PHONE"].ToString();
        //                        string system = dt.Rows[i]["SYSTEM"].ToString();
        //                        string message = dt.Rows[i]["MESSAGE"].ToString();
        //                        conn = ToolHelper.OpenRavoerp(type);
        //                        sql = " select userid from WX_ID  where mobile='" + phone + "'";
        //                        cmd = new OracleCommand(sql, conn);
        //                        da = new OracleDataAdapter(cmd);
        //                        DataTable dt1 = new DataTable();
        //                        da.Fill(dt1);
        //                        ToolHelper.CloseSql(conn);
        //                        int num1 = dt1.Rows.Count;
        //                        if (num1 > 0)
        //                        {
        //                            userid = dt1.Rows[0]["userid"].ToString();
        //                            send = QYWeixinHelper.SendText(userid, message);
        //                            if (send == "0")
        //                            {
        //                                DateTime nowTime = DateTime.Now;
        //                                conn = ToolHelper.OpenRavoerp(type);
        //                                sql = "update WX_MESSAGES set SEND='1',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),reason='" + nowTime + "SEND=1" + "' where ID='" + id + "'";
        //                                cmd = new OracleCommand(sql, conn);
        //                                //da = new OracleDataAdapter(cmd);
        //                                int result = cmd.ExecuteNonQuery();
        //                                //return result;
        //                                ToolHelper.CloseSql(conn);
        //                            }
        //                            else
        //                            {
        //                                DateTime nowTime = DateTime.Now;
        //                                conn = ToolHelper.OpenRavoerp(type);
        //                                sql = "update WX_MESSAGES set SEND='2',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),reason='" + nowTime + "SEND=2" + "' where ID='" + id + "'";
        //                                cmd = new OracleCommand(sql, conn);
        //                                //da = new OracleDataAdapter(cmd);
        //                                int result = cmd.ExecuteNonQuery();
        //                                //return result;
        //                                ToolHelper.CloseSql(conn);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DateTime nowTime = DateTime.Now;
        //                            conn = ToolHelper.OpenRavoerp(type);
        //                            sql = "update WX_MESSAGES set SEND='3',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),reason='" + nowTime + "SEND=3" + "' where ID='" + id + "'";
        //                            cmd = new OracleCommand(sql, conn);
        //                            //da = new OracleDataAdapter(cmd);
        //                            int result = cmd.ExecuteNonQuery();
        //                            //return result;
        //                            ToolHelper.CloseSql(conn);
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ToolHelper.CloseSql(conn);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ToolHelper.logger.Info("send:" + send + "id:" + userid);
        //            ToolHelper.logger.Debug(ex.ToString());
        //        }
        //}
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}