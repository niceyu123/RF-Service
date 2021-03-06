﻿using OAService.MyPublic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace SendMessageService
{
    public partial class MainService : ServiceBase
    {
        private System.Timers.Timer timer = new System.Timers.Timer();
        public MainService()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            try
            {
                timer.Elapsed += SendMessage;
                timer.AutoReset = true;
                timer.Enabled = true;
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        protected override void OnStop()
        {
            timer.Elapsed -= SendMessage;
            timer.Enabled = false;
        }
        private void SendMessage(object source, System.Timers.ElapsedEventArgs e)
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
                        //string sql = " select * from WX_MESSAGES  where SEND='" + 0 + "'order by CREATETIME";
                        string sql = " update WX_MESSAGES set phone ='15257881618' where id='1880' ";
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ToolHelper.CloseSql(conn);
                        int num = dt.Rows.Count;
                        //if (num >= 0)
                        //{
                        //    for (int i = 0; i < num; i++)
                        //    {
                        //        string id = dt.Rows[i]["ID"].ToString();
                        //        string phone = dt.Rows[i]["PHONE"].ToString();
                        //        string system = dt.Rows[i]["SYSTEM"].ToString();
                        //        string message = dt.Rows[i]["MESSAGE"].ToString();
                        //        conn = ToolHelper.OpenRavoerp(type);
                        //        sql = " select userid from WX_ID  where mobile='" + phone + "'";
                        //        cmd = new OracleCommand(sql, conn);
                        //        da = new OracleDataAdapter(cmd);
                        //        DataTable dt1 = new DataTable();
                        //        da.Fill(dt1);
                        //        ToolHelper.CloseSql(conn);
                        //        int num1 = dt1.Rows.Count;
                        //        if (num1 > 0)
                        //        {
                        //            userid = dt1.Rows[0]["userid"].ToString();
                        //            send = QYWeixinHelper.SendText(userid, message);
                        //            if (send == "0")
                        //            {
                        //                DateTime nowTime = DateTime.Now;
                        //                conn = ToolHelper.OpenRavoerp(type);
                        //                sql = "update WX_MESSAGES set SEND='1',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),reason='" + nowTime + "SEND=1" + "' where ID='" + id + "'";
                        //                cmd = new OracleCommand(sql, conn);
                        //                //da = new OracleDataAdapter(cmd);
                        //                int result = cmd.ExecuteNonQuery();
                        //                //return result;
                        //                ToolHelper.CloseSql(conn);
                        //            }
                        //            else
                        //            {
                        //                DateTime nowTime = DateTime.Now;
                        //                conn = ToolHelper.OpenRavoerp(type);
                        //                sql = "update WX_MESSAGES set SEND='2',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),reason='" + nowTime + "SEND=2" + "' where ID='" + id + "'";
                        //                cmd = new OracleCommand(sql, conn);
                        //                //da = new OracleDataAdapter(cmd);
                        //                int result = cmd.ExecuteNonQuery();
                        //                //return result;
                        //                ToolHelper.CloseSql(conn);
                        //            }
                        //        }
                        //        else
                        //        {
                        //            DateTime nowTime = DateTime.Now;
                        //            conn = ToolHelper.OpenRavoerp(type);
                        //            sql = "update WX_MESSAGES set SEND='3',SENDTIME= to_date('" + nowTime + "','yyyy-mm-dd hh24:mi:ss'),reason='" + nowTime + "SEND=3" + "' where ID='" + id + "'";
                        //            cmd = new OracleCommand(sql, conn);
                        //            //da = new OracleDataAdapter(cmd);
                        //            int result = cmd.ExecuteNonQuery();
                        //            //return result;
                        //            ToolHelper.CloseSql(conn);
                        //        }
                        //    }
                        //}
                    }
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    //ToolHelper.logger.Info("send:" + send + "id:" + userid);
                    //ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
