//using Dapper;
using com;
using OAService.MyEntity;
using OAService.MyPublic;
using Oracle.ManagedDataAccess.Client;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WindowsFormsApp1.MyPublic;
using zkemkeeper;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //public static OracleConnection conn = null;
        private void Button1_Click(object sender, EventArgs e)
        {
            string time = "{\"manid\":\"2049605\",\"startTime\":\"2019-06-11 08:00\",\"endTime\":\"2019-06-11 16:30\"}";
            LeaveTime leaveTime = new JavaScriptSerializer().Deserialize<LeaveTime>(time);
            string a = "";
            //ServiceReference1.NewOAWebServiceSoapClient c = new ServiceReference1.NewOAWebServiceSoapClient();



            //OracleConnection conn = ToolHelper.OpenRavoerp();
            //if (conn != null)
            //{
            //    string dptID = "143";
            //    string sql = " select subcompanyid1 from hrmdepartment@ECOLOGY where id='" + dptID + "'";
            //    OracleCommand cmd = new OracleCommand(sql, conn);
            //    OracleDataAdapter da = new OracleDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    string companyID = dt.Rows[0]["subcompanyid1"].ToString();
            //    if (companyID != null)
            //    {

            //    }
            //    ToolHelper.CloseSql(conn);
            //}
            //string startTime = "2019-06-17 08:00";
            //string endTime = "2019-06-17 18:00";
            //string rest = "1.5";
            //DateTime start1 = Convert.ToDateTime(startTime);
            //DateTime end = Convert.ToDateTime(endTime);
            ////未判定跨天
            //TimeSpan ts = end - start1;
            //double d = Math.Round((double)(ts.Hours * 60 + ts.Minutes) / 60, 2);
            //double t = d - Convert.ToDouble(rest);
            //string times = Convert.ToString(t);

            //try
            //{
            //    string time = "{\"ccr\":\"3931\",\"txr\":\"677,186\",\"startTime\":\"2019-07-04 00:00:00\",\"endTime\":\"2019-07-04 23:59:59\",\"oaNo\":\"HF-CCSQD2019070548\"}";
            //    SaveCCTime ccTime = new JavaScriptSerializer().Deserialize<SaveCCTime>(time);
            //    string[] strArray = ccTime.txr.Split(','); //字符串转数组
            //    List<string> person = new List<string>();
            //    if (ccTime.txr != string.Empty)
            //    {
            //        if (strArray.Contains(ccTime.ccr))
            //        {
            //            person = strArray.ToList();
            //        }
            //        else
            //        {
            //            person = strArray.ToList();
            //            person.Add(ccTime.ccr);
            //        }
            //    }
            //    else
            //    {
            //        person = new List<string>();
            //        person.Add(ccTime.ccr);
            //    }
            //    for (int i = 0; i < person.Count; i++)
            //    {
            //        conn = ToolHelper.OpenRavoerp();
            //        if (conn != null)
            //        {
            //            string sql = " select workcode from hrmresource@ecology where id='" + person[i] + "' ";
            //            OracleCommand cmd = new OracleCommand(sql, conn);
            //            OracleDataAdapter da = new OracleDataAdapter(cmd);
            //            DataTable dt = new DataTable();
            //            da.Fill(dt);
            //            string rid = dt.Rows[0]["workcode"].ToString();
            //            ToolHelper.CloseSql(conn);
            //        }
            //        else
            //        {
            //            string a = "";
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}




            //DateTime start = DateTime.Now;
            //string star12t = start.ToString("yyyy-MM-dd");//"2019-07-02"
            //DateTime startDate = Convert.ToDateTime(start.ToString("yyyy-MM-dd"));//{2019/7/2 0:00:00}

            //OracleManager.Instance.OpenDb();
            //OracleManager.Instance.CallProc("9654321", "1", "2019-06-10 08:33:00", "2019-06-10 08:39:00", "2", "0", "ttrr");
            //OracleManager.Instance.CloseDb();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //leaveInfo l = new leaveInfo();
            //l.manid = "2000036";
            //l.startTime = "2019-05-01 16:30";
            //l.endTime = "2019-05-03 05:00";
            //string aa = new JavaScriptSerializer().Serialize(l);
            //string aaa = aa.ToString();
            string connString = "User ID=ravo2;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            OracleConnection conn = new OracleConnection(connString);
            try
            {
                string manid = "2049605";
                string startTime = "2019-06-27 08:00";
                string endTime = "2019-06-27 18:00";
                DateTime start = Convert.ToDateTime(startTime);
                DateTime end = Convert.ToDateTime(endTime);
                DateTime ss = Convert.ToDateTime(start.ToString("yyyy-MM-dd"));
                DateTime ee = Convert.ToDateTime(end.ToString("yyyy-MM-dd"));
                TimeSpan ts = ee - ss;
                //var day = Math.Ceiling((decimal)ts.TotalDays);
                var day = Math.Ceiling((decimal)ts.TotalDays);
                //这里加个判断是否是相同日期
                double x = 0;
                for (int i = 0; i < day + 1; i++)
                {
                    string startday = start.AddDays(i).ToString("yyyyMMdd");
                    conn.Open();
                    string sql = " select * from t_kqbc_dept where manid='" + manid + "' and dayid='" + startday + "' and state='4' ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    var num = dt.Rows.Count;
                    if (num == 0)
                    {
                        conn.Close();
                        x = x + 0;
                    }
                    else
                    {
                        string bcid = dt.Rows[0]["BCID"].ToString();
                        //string bcid = "18";
                        sql = " select * from VH_LIST_KQBC where bcid='" + bcid + "' order by indexs ";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        int n = dt.Rows.Count;
                        if (n > 1)
                        {
                            string bTime1 = dt.Rows[0]["BTIME"].ToString();
                            string eTime1 = dt.Rows[0]["ETIME"].ToString();
                            string times1 = dt.Rows[0]["TIMES"].ToString();
                            string bTime2 = dt.Rows[1]["BTIME"].ToString();
                            string eTime2 = dt.Rows[1]["ETIME"].ToString();
                            string times2 = dt.Rows[1]["TIMES"].ToString();
                            string bTime3 = dt.Rows[2]["BTIME"].ToString();
                            string eTime3 = dt.Rows[2]["ETIME"].ToString();
                            string times3 = dt.Rows[2]["TIMES"].ToString();
                            conn.Close();
                            DateTime bt1 = Convert.ToDateTime(bTime1);
                            DateTime et1 = Convert.ToDateTime(eTime1);
                            DateTime bt2 = Convert.ToDateTime(bTime2);
                            DateTime et2 = Convert.ToDateTime(eTime2);
                            DateTime bt3 = Convert.ToDateTime(bTime3);
                            DateTime et3 = Convert.ToDateTime(eTime3);
                            double t1 = Convert.ToDouble(times1);
                            double t2 = Convert.ToDouble(times2);
                            double t3 = Convert.ToDouble(times3);
                            DateTime ks = Convert.ToDateTime(start.Hour + ":" + start.Minute);//请假开始时间
                            DateTime js = Convert.ToDateTime(end.Hour + ":" + end.Minute);
                            if (i == 0)//第一天
                            {
                                if (day == 0)//只有一天
                                {
                                    if (ks <= bt1)
                                    {
                                        if (js <= et1)
                                        {
                                            TimeSpan ts1 = js - bt1;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1;
                                        }
                                        else if (js <= bt2)
                                        {
                                            x = x + t1;
                                        }
                                        else if (js < et2)
                                        {
                                            TimeSpan ts1 = js - bt2;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1 + t1;

                                        }
                                        else if (js < bt3)
                                        {
                                            x = x + t1 + t2;
                                        }
                                        else
                                        {
                                            x = x + t1 + t2 + t3;
                                        }
                                    }
                                    else if (ks <= et1)
                                    {
                                        if (js <= et1)
                                        {
                                            TimeSpan ts1 = js - ks;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1;
                                        }
                                        else if (js <= bt2)
                                        {
                                            x = x + t1;
                                        }
                                        else if (js < et2)
                                        {
                                            TimeSpan ts1 = js - bt2;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1 + t1;

                                        }
                                        else if (js < bt3)
                                        {
                                            x = x + t1 + t2;
                                        }
                                        else
                                        {
                                            x = x + t1 + t2 + t3;
                                        }

                                    }
                                    else if (ks <= bt2)
                                    {
                                        if (js <= et2)
                                        {
                                            TimeSpan ts1 = js - bt2;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1;
                                        }
                                        else if (js <= bt3)
                                        {
                                            x = x + t2;
                                        }
                                        else if (js <= et3)
                                        {
                                            TimeSpan ts1 = js - bt3;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1 + t2;
                                        }
                                        else
                                        {
                                            x = x + t2 + t3;
                                        }
                                    }
                                    else if (ks <= et2)
                                    {
                                        if (js < et2)
                                        {
                                            TimeSpan ts1 = js - bt2;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1;

                                        }
                                        else if (js < bt3)
                                        {
                                            x = x + t2;
                                        }
                                        else
                                        {
                                            x = x + t2 + t3;
                                        }
                                    }
                                    else if (ks <= bt3)
                                    {
                                        if (js <= bt3)
                                        {
                                            x = x + 0;
                                        }
                                        else if (js <= et3)
                                        {
                                            TimeSpan ts1 = js - bt3;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1;
                                        }
                                        else
                                        {
                                            x = x + t3;
                                        }
                                    }
                                    else if (ks <= et3)
                                    {
                                        TimeSpan ts1 = js - bt3;
                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                        x = x + d1;
                                    }
                                    else
                                    {
                                        x = x + 0;
                                    }
                                }
                                else if (i != day)//其中的第一天
                                {
                                    if (ks <= bt1)
                                    {
                                        x = x + t1 + t2 + t3;
                                    }
                                    else if (ks <= et1)
                                    {
                                        TimeSpan ts1 = et1 - ks;
                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                        x = x + d1 + t2 + t3;
                                    }
                                    else if (ks <= bt2)
                                    {
                                        x = x + t2 + t3;
                                    }
                                    else if (ks <= et2)
                                    {
                                        TimeSpan ts1 = et2 - ks;
                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                        x = x + d1 + t3;
                                    }
                                    else if (ks <= bt3)
                                    {
                                        x = x + t3;
                                    }
                                    else if (ks <= et3)
                                    {
                                        TimeSpan ts1 = et3 - ks;
                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                        x = x + d1;
                                    }
                                    else
                                    {
                                        x = x + 0;
                                    }
                                }
                            }
                            else if (i == day)//最后一天
                            {
                                if (js >= et3)
                                {
                                    x = x + t1 + t2 + t3;
                                }
                                else if (js >= bt3)
                                {
                                    TimeSpan ts1 = js - bt3;
                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                    x = x + d1 + t2 + t1;
                                }
                                else if (js >= et2)
                                {
                                    x = x + t2 + t1;
                                }
                                else if (js >= bt2)
                                {
                                    TimeSpan ts1 = js - bt2;
                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                    x = x + d1 + t1;
                                }
                                else if (js >= et1)
                                {
                                    x = x + t1;
                                }
                                else if (js > bt1)
                                {
                                    TimeSpan ts1 = js - bt1;
                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                    x = x + d1;
                                }
                                else
                                {
                                    x = x + 0;
                                }
                            }
                            else//当中几天
                            {
                                x = x + t1 + t2 + t3;
                            }
                        }
                        else if (n == 1)
                        {
                            string bTime = dt.Rows[0]["BTIME"].ToString();
                            string eTime = dt.Rows[0]["ETIME"].ToString();
                            string times = dt.Rows[0]["TIMES"].ToString();
                            string rests = dt.Rows[0]["RESTS"].ToString();
                            string xbTime = dt.Rows[0]["XBTIME"].ToString();
                            string xeTime = dt.Rows[0]["XETIME"].ToString();
                            conn.Close();
                            DateTime bt = Convert.ToDateTime(bTime);
                            DateTime et = Convert.ToDateTime(eTime);
                            DateTime xb = Convert.ToDateTime(xbTime);
                            DateTime xe = Convert.ToDateTime(xeTime);
                            double t = Convert.ToDouble(times);
                            double r = Convert.ToDouble(rests);
                            //先判断是否是第一天还是最后一天 先不考虑分段排班情况
                            if (i == 0)
                            {
                                DateTime st1 = Convert.ToDateTime(start.Hour + ":" + start.Minute);//请假开始时间
                                DateTime et1 = Convert.ToDateTime(end.Hour + ":" + end.Minute);
                                if (bt < et)//不跨天
                                {

                                    if (day == 0)//请假开始结束都在同一天
                                    {
                                        if (st1 >= bt && et1 <= et)
                                        {
                                            if (st1 <= xb && et1 >= xe)
                                            {
                                                TimeSpan ts1 = xb - st1;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                TimeSpan ts2 = et1 - xe;
                                                double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                                x = x + d1 + d2;
                                            }
                                            else if (st1 <= xb && et1 <= xe)
                                            {
                                                TimeSpan ts1 = et1 - st1;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                x = x + d1;
                                            }
                                            else if (st1 >= xb && et1 >= xe)
                                            {
                                                TimeSpan ts2 = et1 - xe;
                                                double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                                x = x + d2;
                                            }
                                            else
                                            {
                                                x = x + 0;
                                            }
                                        }
                                        else if (st1 >= bt && et1 >= et)
                                        {
                                            if (st1 <= xb && et1 >= xe)
                                            {
                                                TimeSpan ts1 = xb - bt;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                TimeSpan ts2 = et - xe;
                                                double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                                x = x + d1 + d2;
                                            }
                                            else if (st1 <= xb && et1 <= xe)
                                            {
                                                TimeSpan ts1 = xb - bt;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                x = x + d1;
                                            }
                                            else if (st1 >= xb && et1 >= xe)
                                            {
                                                TimeSpan ts2 = et - xe;
                                                double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                                x = x + d2;
                                            }
                                            else
                                            {
                                                x = x + 0;
                                            }
                                        }
                                        else if (st1 <= bt && et1 <= et)
                                        {
                                            if (st1 <= xb && et1 >= xe)
                                            {
                                                TimeSpan ts1 = xb - st1;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                TimeSpan ts2 = et1 - xe;
                                                double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                                x = x + d1 + d2;
                                            }
                                            else if (st1 <= xb && et1 <= xe)
                                            {
                                                TimeSpan ts1 = xb - st1;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                x = x + d1;
                                            }
                                            else if (st1 >= xb && et1 >= xe)
                                            {
                                                TimeSpan ts2 = et1 - xe;
                                                double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                                x = x + d2;
                                            }
                                            else
                                            {
                                                x = x + 0;
                                            }
                                        }

                                    }
                                    else if (i != day)//请假有n天,只是其中的第一天
                                    {
                                        if (st1 >= bt)
                                        {
                                            if (st1 >= et)
                                            {
                                                x = x + 0;
                                            }
                                            else
                                            {
                                                if (st1 <= xb)
                                                {
                                                    TimeSpan ts1 = xb - st1;
                                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                    TimeSpan ts2 = et - xe;
                                                    double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                                    x = x + d1 + d2;
                                                }
                                                else if (st1 >= xb && st1 >= xe)
                                                {
                                                    TimeSpan ts1 = et - st1;
                                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                    x = x + d1;
                                                }
                                                else
                                                {
                                                    x = x + 0;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (st1 <= xb)
                                            {
                                                TimeSpan ts1 = xb - bt;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                TimeSpan ts2 = et - xe;
                                                double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                                x = x + d1 + d2;
                                            }
                                            else if (st1 >= xb && st1 >= xe)
                                            {
                                                TimeSpan ts1 = xe - bt;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                x = x + d1;
                                            }
                                            else
                                            {
                                                x = x + 0;
                                            }

                                        }

                                    }
                                }
                                else if (bt > et)//跨天
                                {
                                    if (day == 0)//只有一天
                                    {
                                        if (st1 >= bt && et1 <= xb)
                                        {
                                            TimeSpan ts1 = et1 - st1;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1;
                                        }
                                        else if (st1 >= bt && et1 >= xb)
                                        {
                                            TimeSpan ts1 = xb - st1;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1;
                                        }
                                        else if (st1 <= bt && et1 >= xb)
                                        {
                                            TimeSpan ts1 = et1 - bt;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1;
                                        }
                                        else if (st1 <= bt && et1 <= xb)
                                        {
                                            TimeSpan ts1 = et1 - bt;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            x = x + d1;
                                        }
                                    }
                                    else if (i != day)
                                    {
                                        if (st1 >= bt)
                                        {
                                            TimeSpan ts1 = xb - st1;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            TimeSpan ts2 = et - xe;
                                            double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                            x = x + d1 + d2;
                                        }
                                        else
                                        {
                                            TimeSpan ts1 = xb - bt;
                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                            TimeSpan ts2 = et - xe;
                                            double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                            x = x + d1 + d2;
                                        }
                                    }
                                }
                                else
                                {
                                    x = x + 0;
                                }
                            }
                            else if (i == day)//最后一天
                            {
                                DateTime et1 = Convert.ToDateTime(end.Hour + ":" + end.Minute);
                                if (bt < et)//不跨天
                                {
                                    if (et1 >= et)
                                    {
                                        TimeSpan ts1 = et1 - xe;
                                        double d1 = Math.Round((double)(t - r));
                                        x = x + d1;
                                    }
                                    else
                                    {
                                        if (et1 <= bt)
                                        {
                                            x = x + 0;
                                        }
                                        else
                                        {
                                            if (et1 >= xb && et1 >= xe)
                                            {
                                                TimeSpan ts1 = xb - bt;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                TimeSpan ts2 = et1 - xe;
                                                double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                                x = x + d1 + d2;
                                            }
                                            else if (et1 >= xb && et1 <= xe)
                                            {
                                                TimeSpan ts1 = xb - bt;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                x = x + d1;
                                            }
                                            else if (et1 <= xb && et1 <= xe)
                                            {
                                                TimeSpan ts1 = et1 - bt;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                x = x + d1;
                                            }
                                            else if (et1 <= xb && et1 >= xe)
                                            {
                                                TimeSpan ts1 = et1 - bt;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                x = x + d1;
                                            }
                                        }
                                    }
                                }
                                else if (bt > et)//跨天 weiwancheng
                                {
                                    if (et1 <= et)
                                    {
                                        TimeSpan ts1 = et - et1;
                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                        double s = (x - d1);
                                        x = s;
                                    }
                                    else
                                    {
                                        if (et1 >= et && et1 <= bt)
                                        {
                                            x = x + 0;
                                        }
                                        else
                                        {
                                            if (et1 <= xb && et1 >= xe)
                                            {
                                                if (et1 >= bt)
                                                {
                                                    TimeSpan ts1 = et1 - bt;
                                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                    x = x + d1;
                                                }
                                                else
                                                {
                                                    //et1 = et1.AddHours(24);
                                                    TimeSpan ts1 = xb - bt;
                                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                    TimeSpan ts2 = et1 - xe;
                                                    double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
                                                    x = x + d1 + d2;
                                                }

                                            }
                                            else if (et1 >= xb && et1 >= xe)
                                            {
                                                TimeSpan ts1 = xb - bt;
                                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                                x = x + d1;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    x = x + 0;
                                }
                            }
                            else//n天当中的几天
                            {
                                if (bt < et)//不跨天
                                {
                                    x = x + t - r;
                                }
                                else//跨天
                                {
                                    x = x + t - r;
                                    //TimeSpan ts1 = xb - bt;
                                    //double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
                                    //x = x + d1;
                                }

                            }
                        }

                    }
                }
                double total = x;

            }
            catch (Exception ex)
            {
                //ShowErrorMessage(ex.Message.ToString());
            }
        }
        //调试
        private void Button3_Click(object sender, EventArgs e)
        {
            string time1 = "{\"manid\":\"2052545\",\"startTime\":\"2019-07-25 08:00\",\"endTime\":\"2019-07-25 19:30\",\"dptID\":\"85\"}";
            LeaveTime leaveTime = new JavaScriptSerializer().Deserialize<LeaveTime>(time1);
            string manid = leaveTime.manid;
            string startTime = leaveTime.startTime;
            string endTime = leaveTime.endTime;
            string dptID = leaveTime.dptID;
            double x = 0;


            //ServiceReference1.NewOAWebServiceSoapClient c = new ServiceReference1.NewOAWebServiceSoapClient();
            //SaveLeaveTime l = new SaveLeaveTime();
            //l.manid = "2049605";
            //l.startTime = "2019-06-25 08:00";
            //l.endTime = "2019-06-27 18:00";
            //l.qjlx = "2";
            //l.oaNo = "R2-JBSQD2019070224";
            //l.dptID = "148";
            ////string aba = "{\"ccr\":\"3931\",\"txr\":\"677,186\",\"startTime\":\"2019-06-04 00:00:00\",\"endTime\":\"2019-06-04 23:59:59\",\"oaNo\":\"HF-CCSQD2019070548\"}";
            //string aa = new JavaScriptSerializer().Serialize(l);
            //string time = c.SaveTXTime(aa);


            ////string aa1 = "";
            ////string time = "{\"manid\":\"2049605\",\"startTime\":\"2019-06-27 08:00\",\"endTime\":\"2019-06-27 18:00\"},{\"manid\":\"2049605\",\"startTime\":\"2019-06-27 08:00\",\"endTime\":\"2019-06-27 18:00\"}";
            ////List<leaveInfo> leaveTime = new JavaScriptSerializer().Deserialize<List<leaveInfo>>(time);




            ////List<leaveInfo> a = new List<leaveInfo>();
            ////a.Add("manid", "2049605");

            ////List<leaveInfo> s = new List<leaveInfo>();
            ////s.Add(new List<leaveInfo>() { manid="1111" });
            ////s.Add("2049606");
            ////s.Add("2049607");



            //List<leaveInfo> nums = new List<leaveInfo>();
            //nums.Add(new leaveInfo()
            //{
            //    manid = "1",
            //    startTime = "2019-06-27",
            //    endTime = "2019-06-28"
            //});
            //nums.Add(new leaveInfo()
            //{
            //    manid = "2",
            //    startTime = "2019-06-29",
            //    endTime = "2019-06-30"
            //});
            //string aaa = new JavaScriptSerializer().Serialize(nums);

            ////a[0].manid = "2049605";
            ////a[0].startTime = "2019-06-27 08:00";
            ////a[0].endTime = "2019-06-27 18:00";
            //////a[1].manid = "2049606";
            //////a[1].startTime = "2019-06-28 08:00";
            //////a[1].endTime = "2019-06-28 18:00";
            ////string bb = new JavaScriptSerializer().Serialize(a);
            ////string aa11 = "";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string startTime = "2019-05-01 16:30";
            string manid = "2000036";
            int z = 0;
            string connString = "User ID=ravo2;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            OracleConnection conn = new OracleConnection(connString);
            try
            {
                conn.Open();
                DateTime time = Convert.ToDateTime(startTime);
                string start = time.ToString("yyyyMMdd");
                string t = time.ToString("HH:mm");
                string sql = " select count(*) x from T_KQBC_DEPT where MANID='" + manid + "' and DAYID='" + start + "'";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                string x = dt.Rows[0]["x"].ToString();
                conn.Close();
                int y = Convert.ToInt32(x);
                if (y == 0)
                {
                    //return 0;
                }
                else if (y == 1)
                {
                    conn.Open();
                    sql = " select bcid from T_KQBC_DEPT where MANID='" + manid + "' and DAYID='" + start + "'";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    var num = dt.Rows.Count;
                    if (num > 0)
                    {
                        conn.Close();
                        string bcid = dt.Rows[0]["bcid"].ToString();
                        conn.Open();
                        sql = " select btime,etime from VH_LIST_KQBC where bcid='" + bcid + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        string btime = dt.Rows[0]["btime"].ToString();
                        string etime = dt.Rows[0]["etime"].ToString();
                        conn.Close();
                        if (Convert.ToDateTime(btime) < Convert.ToDateTime(etime) && Convert.ToDateTime(t) < Convert.ToDateTime(etime))
                        {
                            z = 1;
                        }
                        else if (Convert.ToDateTime(btime) > Convert.ToDateTime(etime) && Convert.ToDateTime(t) > Convert.ToDateTime(btime) || Convert.ToDateTime(btime) > Convert.ToDateTime(etime) && Convert.ToDateTime(t) < Convert.ToDateTime(etime))
                        {
                            z = 1;
                        }
                        else
                        {
                            z = 0;
                        }
                    }
                    else
                    {
                        conn.Close();
                    }

                }
                else
                {
                    //return 0;
                }
            }
            catch (Exception ex)
            {
                //return "0";
                throw;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            //LeaveTime leaveTime = new JavaScriptSerializer().Deserialize<LeaveTime>(time);
            //string manid = leaveTime.manid;
            //string startTime = leaveTime.startTime;
            //string endTime = leaveTime.endTime;
            //SaveLeaveTime leaveTime = new JavaScriptSerializer().Deserialize<SaveLeaveTime>(time);
            string manid = "2049605";
            string startTime = "2019-06-23 10:00:00";
            string endTime = "2019-06-25 17:00:00";
            string qjlx = "1";
            string oaNo = "lcgl166661";
            //try
            //{
            //    DateTime start = Convert.ToDateTime(startTime);
            //    DateTime end = Convert.ToDateTime(endTime);
            //    DateTime ss = Convert.ToDateTime(start.ToString("yyyy-MM-dd"));
            //    DateTime ee = Convert.ToDateTime(end.ToString("yyyy-MM-dd"));
            //    TimeSpan ts = ee - ss;
            //    var day = Math.Ceiling((decimal)ts.TotalDays);
            //    double x = 0;
            //    for (int i = 0; i < day + 1; i++)
            //    {
            //        OracleConnection conn = ToolHelper.OpenRavoerp();
            //        string startday = start.AddDays(i).ToString("yyyyMMdd");
            //        if (conn != null)
            //        {
            //            string sql = " select * from t_kqbc_dept where manid='" + manid + "' and dayid='" + startday + "' and state='4' ";
            //            OracleCommand cmd = new OracleCommand(sql, conn);
            //            OracleDataAdapter da = new OracleDataAdapter(cmd);
            //            DataTable dt = new DataTable();
            //            da.Fill(dt);
            //            var num = dt.Rows.Count;
            //            if (num == 0)
            //            {
            //                ToolHelper.CloseSql(conn);
            //                x = x + 0;
            //            }
            //            else
            //            {
            //                string bcid = dt.Rows[0]["BCID"].ToString();
            //                //string bcid = "18";
            //                sql = " select * from VH_LIST_KQBC where bcid='" + bcid + "' order by indexs ";
            //                cmd = new OracleCommand(sql, conn);
            //                da = new OracleDataAdapter(cmd);
            //                dt = new DataTable();
            //                da.Fill(dt);
            //                int n = dt.Rows.Count;
            //                if (n > 1)
            //                {
            //                    string bTime1 = dt.Rows[0]["BTIME"].ToString();
            //                    string eTime1 = dt.Rows[0]["ETIME"].ToString();
            //                    string times1 = dt.Rows[0]["TIMES"].ToString();
            //                    string bTime2 = dt.Rows[1]["BTIME"].ToString();
            //                    string eTime2 = dt.Rows[1]["ETIME"].ToString();
            //                    string times2 = dt.Rows[1]["TIMES"].ToString();
            //                    string bTime3 = dt.Rows[2]["BTIME"].ToString();
            //                    string eTime3 = dt.Rows[2]["ETIME"].ToString();
            //                    string times3 = dt.Rows[2]["TIMES"].ToString();
            //                    ToolHelper.CloseSql(conn);
            //                    DateTime bt1 = Convert.ToDateTime(bTime1);
            //                    DateTime et1 = Convert.ToDateTime(eTime1);
            //                    DateTime bt2 = Convert.ToDateTime(bTime2);
            //                    DateTime et2 = Convert.ToDateTime(eTime2);
            //                    DateTime bt3 = Convert.ToDateTime(bTime3);
            //                    DateTime et3 = Convert.ToDateTime(eTime3);
            //                    double t1 = Convert.ToDouble(times1);
            //                    double t2 = Convert.ToDouble(times2);
            //                    double t3 = Convert.ToDouble(times3);
            //                    DateTime ks = Convert.ToDateTime(start.Hour + ":" + start.Minute);//请假开始时间
            //                    DateTime js = Convert.ToDateTime(end.Hour + ":" + end.Minute);
            //                    if (i == 0)//第一天
            //                    {
            //                        if (day == 0)//只有一天
            //                        {
            //                            if (ks <= bt1)
            //                            {
            //                                if (js <= et1)
            //                                {
            //                                    TimeSpan ts1 = js - bt1;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else if (js <= bt2)
            //                                {
            //                                    x = x + t1;
            //                                }
            //                                else if (js < et2)
            //                                {
            //                                    TimeSpan ts1 = js - bt2;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1 + t1;

            //                                }
            //                                else if (js < bt3)
            //                                {
            //                                    x = x + t1 + t2;
            //                                }
            //                                else
            //                                {
            //                                    x = x + t1 + t2 + t3;
            //                                }
            //                            }
            //                            else if (ks <= et1)
            //                            {
            //                                if (js <= et1)
            //                                {
            //                                    TimeSpan ts1 = js - ks;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else if (js <= bt2)
            //                                {
            //                                    x = x + t1;
            //                                }
            //                                else if (js < et2)
            //                                {
            //                                    TimeSpan ts1 = js - bt2;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1 + t1;

            //                                }
            //                                else if (js < bt3)
            //                                {
            //                                    x = x + t1 + t2;
            //                                }
            //                                else
            //                                {
            //                                    x = x + t1 + t2 + t3;
            //                                }

            //                            }
            //                            else if (ks <= bt2)
            //                            {
            //                                if (js <= et2)
            //                                {
            //                                    TimeSpan ts1 = js - bt2;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else if (js <= bt3)
            //                                {
            //                                    x = x + t2;
            //                                }
            //                                else if (js <= et3)
            //                                {
            //                                    TimeSpan ts1 = js - bt3;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1 + t2;
            //                                }
            //                                else
            //                                {
            //                                    x = x + t2 + t3;
            //                                }
            //                            }
            //                            else if (ks <= et2)
            //                            {
            //                                if (js < et2)
            //                                {
            //                                    TimeSpan ts1 = js - bt2;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;

            //                                }
            //                                else if (js < bt3)
            //                                {
            //                                    x = x + t2;
            //                                }
            //                                else
            //                                {
            //                                    x = x + t2 + t3;
            //                                }
            //                            }
            //                            else if (ks <= bt3)
            //                            {
            //                                if (js <= bt3)
            //                                {
            //                                    x = x + 0;
            //                                }
            //                                else if (js <= et3)
            //                                {
            //                                    TimeSpan ts1 = js - bt3;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else
            //                                {
            //                                    x = x + t3;
            //                                }
            //                            }
            //                            else if (ks <= et3)
            //                            {
            //                                TimeSpan ts1 = js - bt3;
            //                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                x = x + d1;
            //                            }
            //                            else
            //                            {
            //                                x = x + 0;
            //                            }
            //                        }
            //                        else if (i != day)//其中的第一天
            //                        {
            //                            if (ks <= bt1)
            //                            {
            //                                x = x + t1 + t2 + t3;
            //                            }
            //                            else if (ks <= et1)
            //                            {
            //                                TimeSpan ts1 = et1 - ks;
            //                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                x = x + d1 + t2 + t3;
            //                            }
            //                            else if (ks <= bt2)
            //                            {
            //                                x = x + t2 + t3;
            //                            }
            //                            else if (ks <= et2)
            //                            {
            //                                TimeSpan ts1 = et2 - ks;
            //                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                x = x + d1 + t3;
            //                            }
            //                            else if (ks <= bt3)
            //                            {
            //                                x = x + t3;
            //                            }
            //                            else if (ks <= et3)
            //                            {
            //                                TimeSpan ts1 = et3 - ks;
            //                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                x = x + d1;
            //                            }
            //                            else
            //                            {
            //                                x = x + 0;
            //                            }
            //                        }
            //                    }
            //                    else if (i == day)//最后一天
            //                    {
            //                        if (js >= et3)
            //                        {
            //                            x = x + t1 + t2 + t3;
            //                        }
            //                        else if (js >= bt3)
            //                        {
            //                            TimeSpan ts1 = js - bt3;
            //                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                            x = x + d1 + t2 + t1;
            //                        }
            //                        else if (js >= et2)
            //                        {
            //                            x = x + t2 + t1;
            //                        }
            //                        else if (js >= bt2)
            //                        {
            //                            TimeSpan ts1 = js - bt2;
            //                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                            x = x + d1 + t1;
            //                        }
            //                        else if (js >= et1)
            //                        {
            //                            x = x + t1;
            //                        }
            //                        else if (js > bt1)
            //                        {
            //                            TimeSpan ts1 = js - bt1;
            //                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                            x = x + d1;
            //                        }
            //                        else
            //                        {
            //                            x = x + 0;
            //                        }
            //                    }
            //                    else//当中几天
            //                    {
            //                        x = x + t1 + t2 + t3;

            //                    }
            //                }
            //                //一个排班分多个段
            //                else if (n == 1)
            //                {
            //                    string bTime = dt.Rows[0]["BTIME"].ToString();
            //                    string eTime = dt.Rows[0]["ETIME"].ToString();
            //                    string times = dt.Rows[0]["TIMES"].ToString();
            //                    string rests = dt.Rows[0]["RESTS"].ToString();
            //                    string xbTime = dt.Rows[0]["XBTIME"].ToString();
            //                    string xeTime = dt.Rows[0]["XETIME"].ToString();
            //                    ToolHelper.CloseSql(conn);
            //                    DateTime bt = Convert.ToDateTime(bTime);
            //                    DateTime et = Convert.ToDateTime(eTime);
            //                    DateTime xb = Convert.ToDateTime(xbTime);
            //                    DateTime xe = Convert.ToDateTime(xeTime);
            //                    double t = Convert.ToDouble(times);
            //                    double r = Convert.ToDouble(rests);
            //                    DateTime st1 = Convert.ToDateTime(start.Hour + ":" + start.Minute);//请假开始时间
            //                    DateTime et1 = Convert.ToDateTime(end.Hour + ":" + end.Minute);
            //                    string strDate = start.ToString("yyyy-MM-dd");
            //                    DateTime startDate = Convert.ToDateTime(strDate);
            //                    //先判断是否是第一天还是最后一天 先不考虑分段排班情况
            //                    if (i == 0)
            //                    {

            //                        if (bt < et)//不跨天
            //                        {
            //                            if (day == 0)//请假开始结束都在同一天
            //                            {
            //                                if (st1 >= bt && et1 <= et)
            //                                {
            //                                    if (st1 <= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - st1;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        TimeSpan ts2 = et1 - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d1 + d2;
            //                                        //先判断是否要存储
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 <= xb && et1 <= xe)
            //                                    {
            //                                        TimeSpan ts1 = et1 - st1;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        x = x + d1;
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 >= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts2 = et1 - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d2;
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d2), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        x = x + 0;
            //                                    }
            //                                }
            //                                else if (st1 >= bt && et1 >= et)
            //                                {
            //                                    if (st1 <= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - bt;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        TimeSpan ts2 = et - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d1 + d2;
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 <= xb && et1 <= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - bt;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        x = x + d1;
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), strDate + " " + Convert.ToString(xb.Hour) + ":" + Convert.ToString(xb.Minute), Convert.ToString(d1), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 >= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts2 = et - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d2;
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d2), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        x = x + 0;
            //                                    }
            //                                }
            //                                else if (st1 <= bt && et1 <= et)
            //                                {
            //                                    if (st1 <= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - st1;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        TimeSpan ts2 = et1 - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d1 + d2;
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(xe.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 <= xb && et1 <= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - st1;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        x = x + d1;
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(xb.Hour) + ":" + Convert.ToString(xb.Minute), Convert.ToString(d1), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 >= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts2 = et1 - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d2;
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d2), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        x = x + 0;
            //                                    }
            //                                }
            //                            }
            //                            else if (i != day)//请假有n天,只是其中的第一天
            //                            {
            //                                if (st1 >= bt)
            //                                {
            //                                    if (st1 >= et)
            //                                    {
            //                                        x = x + 0;
            //                                    }
            //                                    else
            //                                    {
            //                                        if (st1 <= xb)
            //                                        {
            //                                            TimeSpan ts1 = xb - st1;
            //                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                            TimeSpan ts2 = et - xe;
            //                                            double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                            x = x + d1 + d2;
            //                                            string dayy = strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute);
            //                                            sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') ";
            //                                            conn = ToolHelper.OpenRavoerp();
            //                                            cmd = new OracleCommand(sql, conn);
            //                                            da = new OracleDataAdapter(cmd);
            //                                            dt = new DataTable();
            //                                            da.Fill(dt);
            //                                            num = dt.Rows.Count;
            //                                            ToolHelper.CloseSql(conn);
            //                                            if (num == 0)
            //                                            {
            //                                                OracleManager.Instance.OpenDb();
            //                                                OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                                OracleManager.Instance.CloseDb();
            //                                            }
            //                                        }
            //                                        else if (st1 >= xb && st1 >= xe)
            //                                        {
            //                                            TimeSpan ts1 = et - st1;
            //                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                            x = x + d1;
            //                                            string dayy = strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute);
            //                                            sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') ";
            //                                            conn = ToolHelper.OpenRavoerp();
            //                                            cmd = new OracleCommand(sql, conn);
            //                                            da = new OracleDataAdapter(cmd);
            //                                            dt = new DataTable();
            //                                            da.Fill(dt);
            //                                            num = dt.Rows.Count;
            //                                            ToolHelper.CloseSql(conn);
            //                                            if (num == 0)
            //                                            {
            //                                                OracleManager.Instance.OpenDb();
            //                                                OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1), "0", oaNo);
            //                                                OracleManager.Instance.CloseDb();
            //                                            }
            //                                        }
            //                                        else
            //                                        {
            //                                            x = x + 0;
            //                                        }
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    if (st1 <= xb)
            //                                    {
            //                                        TimeSpan ts1 = xb - bt;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        TimeSpan ts2 = et - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d1 + d2;
            //                                        string dayy = strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 >= xb && st1 >= xe)
            //                                    {
            //                                        TimeSpan ts1 = et - st1;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        x = x + d1;
            //                                        string dayy = strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProc(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        x = x + 0;
            //                                    }

            //                                }

            //                            }
            //                        }
            //                        else if (bt > et)//跨天
            //                        {
            //                            if (day == 0)//只有一天
            //                            {
            //                                if (st1 >= bt && et1 <= xb)
            //                                {
            //                                    TimeSpan ts1 = et1 - st1;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;

            //                                }
            //                                else if (st1 >= bt && et1 >= xb)
            //                                {
            //                                    TimeSpan ts1 = xb - st1;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else if (st1 <= bt && et1 >= xb)
            //                                {
            //                                    TimeSpan ts1 = et1 - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else if (st1 <= bt && et1 <= xb)
            //                                {
            //                                    TimeSpan ts1 = et1 - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                            }
            //                            else if (i != day)
            //                            {
            //                                if (st1 >= bt)
            //                                {
            //                                    TimeSpan ts1 = xb - st1;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    TimeSpan ts2 = et - xe;
            //                                    double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                    x = x + d1 + d2;
            //                                }
            //                                else
            //                                {
            //                                    TimeSpan ts1 = xb - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    TimeSpan ts2 = et - xe;
            //                                    double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                    x = x + d1 + d2;
            //                                }
            //                            }
            //                        }
            //                        else
            //                        {
            //                            x = x + 0;
            //                        }
            //                    }
            //                    else if (i == day)//最后一天
            //                    {
            //                        if (bt < et)//不跨天
            //                        {
            //                            //if (et1 >= et)
            //                            //{
            //                            //    TimeSpan ts1 = et - xe;
            //                            //    double d1 = Math.Round((double)(t - r));
            //                            //    x = x + d1;
            //                            //    string kssj = Convert.ToString(Convert.ToDateTime(strDate).AddDays(i));
            //                            //    OracleManager.Instance.OpenDb();
            //                            //    OracleManager.Instance.CallProc(manid, qjlx, kssj + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1), "0", oaNo);
            //                            //    OracleManager.Instance.CloseDb();
            //                            //}
            //                            //else
            //                            //{
            //                            if (et1 <= bt)
            //                            {
            //                                x = x + 0;
            //                            }
            //                            else
            //                            {
            //                                if (et1 >= xb && et1 >= xe)
            //                                {
            //                                    TimeSpan ts1 = xb - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    TimeSpan ts2 = et1 - xe;
            //                                    double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                    x = x + d1 + d2;
            //                                    DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                                    string kssj = aaa.ToString("yyyy-MM-dd");
            //                                    string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                    sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') ";
            //                                    conn = ToolHelper.OpenRavoerp();
            //                                    cmd = new OracleCommand(sql, conn);
            //                                    da = new OracleDataAdapter(cmd);
            //                                    dt = new DataTable();
            //                                    da.Fill(dt);
            //                                    num = dt.Rows.Count;
            //                                    ToolHelper.CloseSql(conn);
            //                                    if (num == 0)
            //                                    {
            //                                        OracleManager.Instance.OpenDb();
            //                                        OracleManager.Instance.CallProc(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                        OracleManager.Instance.CloseDb();
            //                                    }
            //                                }
            //                                else if (et1 >= xb && et1 <= xe)
            //                                {
            //                                    TimeSpan ts1 = xb - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                    DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                                    string kssj = aaa.ToString("yyyy-MM-dd");
            //                                    string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                    sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') ";
            //                                    conn = ToolHelper.OpenRavoerp();
            //                                    cmd = new OracleCommand(sql, conn);
            //                                    da = new OracleDataAdapter(cmd);
            //                                    dt = new DataTable();
            //                                    da.Fill(dt);
            //                                    num = dt.Rows.Count;
            //                                    ToolHelper.CloseSql(conn);
            //                                    if (num == 0)
            //                                    {
            //                                        OracleManager.Instance.OpenDb();
            //                                        OracleManager.Instance.CallProc(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(xb.Hour) + ":" + Convert.ToString(xb.Minute), Convert.ToString(d1), "0", oaNo);
            //                                        OracleManager.Instance.CloseDb();
            //                                    }
            //                                }
            //                                else if (et1 <= xb && et1 <= xe)
            //                                {
            //                                    TimeSpan ts1 = et1 - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                    DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                                    string kssj = aaa.ToString("yyyy-MM-dd");
            //                                    string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                    sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') ";
            //                                    conn = ToolHelper.OpenRavoerp();
            //                                    cmd = new OracleCommand(sql, conn);
            //                                    da = new OracleDataAdapter(cmd);
            //                                    dt = new DataTable();
            //                                    da.Fill(dt);
            //                                    num = dt.Rows.Count;
            //                                    ToolHelper.CloseSql(conn);
            //                                    if (num == 0)
            //                                    {
            //                                        OracleManager.Instance.OpenDb();
            //                                        OracleManager.Instance.CallProc(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1), "0", oaNo);
            //                                        OracleManager.Instance.CloseDb();
            //                                    }
            //                                }
            //                                else if (et1 <= xb && et1 >= xe)
            //                                {
            //                                    TimeSpan ts1 = et1 - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                    DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                                    string kssj = aaa.ToString("yyyy-MM-dd");
            //                                    string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                    sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') ";
            //                                    conn = ToolHelper.OpenRavoerp();
            //                                    cmd = new OracleCommand(sql, conn);
            //                                    da = new OracleDataAdapter(cmd);
            //                                    dt = new DataTable();
            //                                    da.Fill(dt);
            //                                    num = dt.Rows.Count;
            //                                    ToolHelper.CloseSql(conn);
            //                                    if (num == 0)
            //                                    {
            //                                        OracleManager.Instance.OpenDb();
            //                                        OracleManager.Instance.CallProc(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1), "0", oaNo);
            //                                        OracleManager.Instance.CloseDb();
            //                                    }
            //                                }
            //                            }
            //                            //}
            //                        }
            //                        else if (bt > et)//跨天 weiwancheng
            //                        {
            //                            if (et1 <= et)
            //                            {
            //                                TimeSpan ts1 = et - et1;
            //                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                double s = (x - d1);
            //                                x = s;
            //                            }
            //                            else
            //                            {
            //                                if (et1 >= et && et1 <= bt)
            //                                {
            //                                    x = x + 0;
            //                                }
            //                                else
            //                                {
            //                                    if (et1 <= xb && et1 >= xe)
            //                                    {
            //                                        if (et1 >= bt)
            //                                        {
            //                                            TimeSpan ts1 = et1 - bt;
            //                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                            x = x + d1;
            //                                        }
            //                                        else
            //                                        {
            //                                            //et1 = et1.AddHours(24);
            //                                            TimeSpan ts1 = xb - bt;
            //                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                            TimeSpan ts2 = et1 - xe;
            //                                            double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                            x = x + d1 + d2;
            //                                        }

            //                                    }
            //                                    else if (et1 >= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - bt;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        x = x + d1;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                        else
            //                        {
            //                            x = x + 0;
            //                        }
            //                    }
            //                    else//n天当中的几天
            //                    {
            //                        if (bt < et)//不跨天
            //                        {
            //                            x = x + t - r;
            //                            DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                            string kssj = aaa.ToString("yyyy-MM-dd");
            //                            string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                            sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') ";
            //                            conn = ToolHelper.OpenRavoerp();
            //                            cmd = new OracleCommand(sql, conn);
            //                            da = new OracleDataAdapter(cmd);
            //                            dt = new DataTable();
            //                            da.Fill(dt);
            //                            num = dt.Rows.Count;
            //                            ToolHelper.CloseSql(conn);
            //                            if (num == 0)
            //                            {
            //                                OracleManager.Instance.OpenDb();
            //                                OracleManager.Instance.CallProc(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(t - r), rests, oaNo);
            //                                OracleManager.Instance.CloseDb();
            //                            }
            //                        }
            //                        else//跨天
            //                        {
            //                            x = x + t - r;
            //                            //TimeSpan ts1 = xb - bt;
            //                            //double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                            //x = x + d1;
            //                        }

            //                    }
            //                }

            //            }
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }
            //    double total = x;
            //    string z = Convert.ToString(total);
            //}
            //catch (Exception ex)
            //{
            //}
        }
        //出差
        private void Button6_Click(object sender, EventArgs e)
        {
            string time = "{\"ccr\":\"186\",\"startTime\":\"2019-06-04 13:00:00\",\"endTime\":\"2019-06-06 13:00:00\",\"oaNo\":\"HF-CCSQD2019070570\"}";
            string qjlx = "1";
            string manid = "";
            SaveCCTime ccTime = new JavaScriptSerializer().Deserialize<SaveCCTime>(time);
            string ccr = ccTime.ccr;
            string startTime = ccTime.startTime;
            string endTime = ccTime.endTime;
            string oaNo = ccTime.oaNo;
            double total = 0;
            //try
            //{
            //    OracleConnection conn1 = ToolHelper.OpenRavoerp();
            //    if (conn1 != null)
            //    {
            //        string sql = " select workcode from hrmresource@ecology where id='" + ccr + "' ";
            //        OracleCommand cmd1 = new OracleCommand(sql, conn1);
            //        OracleDataAdapter da1 = new OracleDataAdapter(cmd1);
            //        DataTable dt1 = new DataTable();
            //        da1.Fill(dt1);
            //        manid = dt1.Rows[0]["workcode"].ToString();
            //        ToolHelper.CloseSql(conn1);
            //    }
            //    DateTime start = Convert.ToDateTime(startTime);
            //    DateTime end = Convert.ToDateTime(endTime);
            //    DateTime ss = Convert.ToDateTime(start.ToString("yyyy-MM-dd"));
            //    DateTime ee = Convert.ToDateTime(end.ToString("yyyy-MM-dd"));
            //    TimeSpan ts = ee - ss;
            //    var day = Math.Ceiling((decimal)ts.TotalDays);
            //    double x = 0;
            //    for (int i = 0; i < day + 1; i++)
            //    {
            //        OracleConnection conn = ToolHelper.OpenRavoerp();
            //        string startday = start.AddDays(i).ToString("yyyyMMdd");
            //        if (conn != null)
            //        {
            //            string sql = " select * from t_kqbc_dept where manid='" + manid + "' and dayid='" + startday + "' and state='4' ";
            //            OracleCommand cmd = new OracleCommand(sql, conn);
            //            OracleDataAdapter da = new OracleDataAdapter(cmd);
            //            DataTable dt = new DataTable();
            //            da.Fill(dt);
            //            var num = dt.Rows.Count;
            //            if (num == 0)
            //            {
            //                ToolHelper.CloseSql(conn);
            //                x = x + 0;
            //            }
            //            else
            //            {
            //                string bcid = dt.Rows[0]["BCID"].ToString();
            //                //string bcid = "18";
            //                sql = " select * from VH_LIST_KQBC where bcid='" + bcid + "' order by indexs ";
            //                cmd = new OracleCommand(sql, conn);
            //                da = new OracleDataAdapter(cmd);
            //                dt = new DataTable();
            //                da.Fill(dt);
            //                int n = dt.Rows.Count;
            //                if (n > 1)
            //                {
            //                    string bTime1 = dt.Rows[0]["BTIME"].ToString();
            //                    string eTime1 = dt.Rows[0]["ETIME"].ToString();
            //                    string times1 = dt.Rows[0]["TIMES"].ToString();
            //                    string bTime2 = dt.Rows[1]["BTIME"].ToString();
            //                    string eTime2 = dt.Rows[1]["ETIME"].ToString();
            //                    string times2 = dt.Rows[1]["TIMES"].ToString();
            //                    string bTime3 = dt.Rows[2]["BTIME"].ToString();
            //                    string eTime3 = dt.Rows[2]["ETIME"].ToString();
            //                    string times3 = dt.Rows[2]["TIMES"].ToString();
            //                    ToolHelper.CloseSql(conn);
            //                    DateTime bt1 = Convert.ToDateTime(bTime1);
            //                    DateTime et1 = Convert.ToDateTime(eTime1);
            //                    DateTime bt2 = Convert.ToDateTime(bTime2);
            //                    DateTime et2 = Convert.ToDateTime(eTime2);
            //                    DateTime bt3 = Convert.ToDateTime(bTime3);
            //                    DateTime et3 = Convert.ToDateTime(eTime3);
            //                    double t1 = Convert.ToDouble(times1);
            //                    double t2 = Convert.ToDouble(times2);
            //                    double t3 = Convert.ToDouble(times3);
            //                    DateTime ks = Convert.ToDateTime(start.Hour + ":" + start.Minute);//请假开始时间
            //                    DateTime js = Convert.ToDateTime(end.Hour + ":" + end.Minute);
            //                    if (i == 0)//第一天
            //                    {
            //                        if (day == 0)//只有一天
            //                        {
            //                            if (ks <= bt1)
            //                            {
            //                                if (js <= et1)
            //                                {
            //                                    TimeSpan ts1 = js - bt1;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else if (js <= bt2)
            //                                {
            //                                    x = x + t1;
            //                                }
            //                                else if (js < et2)
            //                                {
            //                                    TimeSpan ts1 = js - bt2;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1 + t1;

            //                                }
            //                                else if (js < bt3)
            //                                {
            //                                    x = x + t1 + t2;
            //                                }
            //                                else
            //                                {
            //                                    x = x + t1 + t2 + t3;
            //                                }
            //                            }
            //                            else if (ks <= et1)
            //                            {
            //                                if (js <= et1)
            //                                {
            //                                    TimeSpan ts1 = js - ks;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else if (js <= bt2)
            //                                {
            //                                    x = x + t1;
            //                                }
            //                                else if (js < et2)
            //                                {
            //                                    TimeSpan ts1 = js - bt2;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1 + t1;

            //                                }
            //                                else if (js < bt3)
            //                                {
            //                                    x = x + t1 + t2;
            //                                }
            //                                else
            //                                {
            //                                    x = x + t1 + t2 + t3;
            //                                }

            //                            }
            //                            else if (ks <= bt2)
            //                            {
            //                                if (js <= et2)
            //                                {
            //                                    TimeSpan ts1 = js - bt2;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else if (js <= bt3)
            //                                {
            //                                    x = x + t2;
            //                                }
            //                                else if (js <= et3)
            //                                {
            //                                    TimeSpan ts1 = js - bt3;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1 + t2;
            //                                }
            //                                else
            //                                {
            //                                    x = x + t2 + t3;
            //                                }
            //                            }
            //                            else if (ks <= et2)
            //                            {
            //                                if (js < et2)
            //                                {
            //                                    TimeSpan ts1 = js - bt2;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;

            //                                }
            //                                else if (js < bt3)
            //                                {
            //                                    x = x + t2;
            //                                }
            //                                else
            //                                {
            //                                    x = x + t2 + t3;
            //                                }
            //                            }
            //                            else if (ks <= bt3)
            //                            {
            //                                if (js <= bt3)
            //                                {
            //                                    x = x + 0;
            //                                }
            //                                else if (js <= et3)
            //                                {
            //                                    TimeSpan ts1 = js - bt3;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else
            //                                {
            //                                    x = x + t3;
            //                                }
            //                            }
            //                            else if (ks <= et3)
            //                            {
            //                                TimeSpan ts1 = js - bt3;
            //                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                x = x + d1;
            //                            }
            //                            else
            //                            {
            //                                x = x + 0;
            //                            }
            //                        }
            //                        else if (i != day)//其中的第一天
            //                        {
            //                            if (ks <= bt1)
            //                            {
            //                                x = x + t1 + t2 + t3;
            //                            }
            //                            else if (ks <= et1)
            //                            {
            //                                TimeSpan ts1 = et1 - ks;
            //                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                x = x + d1 + t2 + t3;
            //                            }
            //                            else if (ks <= bt2)
            //                            {
            //                                x = x + t2 + t3;
            //                            }
            //                            else if (ks <= et2)
            //                            {
            //                                TimeSpan ts1 = et2 - ks;
            //                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                x = x + d1 + t3;
            //                            }
            //                            else if (ks <= bt3)
            //                            {
            //                                x = x + t3;
            //                            }
            //                            else if (ks <= et3)
            //                            {
            //                                TimeSpan ts1 = et3 - ks;
            //                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                x = x + d1;
            //                            }
            //                            else
            //                            {
            //                                x = x + 0;
            //                            }
            //                        }
            //                    }
            //                    else if (i == day)//最后一天
            //                    {
            //                        if (js >= et3)
            //                        {
            //                            x = x + t1 + t2 + t3;
            //                        }
            //                        else if (js >= bt3)
            //                        {
            //                            TimeSpan ts1 = js - bt3;
            //                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                            x = x + d1 + t2 + t1;
            //                        }
            //                        else if (js >= et2)
            //                        {
            //                            x = x + t2 + t1;
            //                        }
            //                        else if (js >= bt2)
            //                        {
            //                            TimeSpan ts1 = js - bt2;
            //                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                            x = x + d1 + t1;
            //                        }
            //                        else if (js >= et1)
            //                        {
            //                            x = x + t1;
            //                        }
            //                        else if (js > bt1)
            //                        {
            //                            TimeSpan ts1 = js - bt1;
            //                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                            x = x + d1;
            //                        }
            //                        else
            //                        {
            //                            x = x + 0;
            //                        }
            //                    }
            //                    else//当中几天
            //                    {
            //                        x = x + t1 + t2 + t3;

            //                    }
            //                }
            //                //一个排班分多个段
            //                else if (n == 1)
            //                {
            //                    string bTime = dt.Rows[0]["BTIME"].ToString();
            //                    string eTime = dt.Rows[0]["ETIME"].ToString();
            //                    string times = dt.Rows[0]["TIMES"].ToString();
            //                    string rests = dt.Rows[0]["RESTS"].ToString();
            //                    string xbTime = dt.Rows[0]["XBTIME"].ToString();
            //                    string xeTime = dt.Rows[0]["XETIME"].ToString();
            //                    ToolHelper.CloseSql(conn);
            //                    DateTime bt = Convert.ToDateTime(bTime);
            //                    DateTime et = Convert.ToDateTime(eTime);
            //                    DateTime xb = Convert.ToDateTime(xbTime);
            //                    DateTime xe = Convert.ToDateTime(xeTime);
            //                    double t = Convert.ToDouble(times);
            //                    double r = Convert.ToDouble(rests);
            //                    DateTime st1 = Convert.ToDateTime(start.Hour + ":" + start.Minute);//请假开始时间
            //                    DateTime et1 = Convert.ToDateTime(end.Hour + ":" + end.Minute);
            //                    string strDate = start.ToString("yyyy-MM-dd");
            //                    DateTime startDate = Convert.ToDateTime(strDate);
            //                    //先判断是否是第一天还是最后一天 先不考虑分段排班情况
            //                    if (i == 0)
            //                    {

            //                        if (bt < et)//不跨天
            //                        {
            //                            if (day == 0)//请假开始结束都在同一天
            //                            {
            //                                if (st1 >= bt && et1 <= et)
            //                                {
            //                                    if (st1 <= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - st1;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        TimeSpan ts2 = et1 - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d1 + d2;
            //                                        //先判断是否要存储
            //                                        string dayy = strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 <= xb && et1 <= xe)
            //                                    {
            //                                        TimeSpan ts1 = et1 - st1;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        x = x + d1;
            //                                        string dayy = strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 >= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts2 = et1 - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d2;
            //                                        string dayy = strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d2), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        x = x + 0;
            //                                    }
            //                                }
            //                                else if (st1 >= bt && et1 >= et)
            //                                {
            //                                    if (st1 <= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - bt;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        TimeSpan ts2 = et - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d1 + d2;
            //                                        string dayy = strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 <= xb && et1 <= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - bt;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        x = x + d1;
            //                                        string dayy = strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), strDate + " " + Convert.ToString(xb.Hour) + ":" + Convert.ToString(xb.Minute), Convert.ToString(d1), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 >= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts2 = et - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d2;
            //                                        string dayy = strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d2), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        x = x + 0;
            //                                    }
            //                                }
            //                                else if (st1 <= bt && et1 <= et)
            //                                {
            //                                    if (st1 <= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - st1;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        TimeSpan ts2 = et1 - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d1 + d2;
            //                                        string dayy = strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 <= xb && et1 <= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - st1;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        x = x + d1;
            //                                        string dayy = strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(xb.Hour) + ":" + Convert.ToString(xb.Minute), Convert.ToString(d1), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (st1 >= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts2 = et1 - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d2;
            //                                        string dayy = strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d2), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        x = x + 0;
            //                                    }
            //                                }
            //                                else if (st1 <= bt && et1 >= et)
            //                                {
            //                                    string dayy = strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                    sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' and man_id='" + manid + "' ";
            //                                    conn = ToolHelper.OpenRavoerp();
            //                                    cmd = new OracleCommand(sql, conn);
            //                                    da = new OracleDataAdapter(cmd);
            //                                    dt = new DataTable();
            //                                    da.Fill(dt);
            //                                    num = dt.Rows.Count;
            //                                    ToolHelper.CloseSql(conn);
            //                                    if (num == 0)
            //                                    {
            //                                        OracleManager.Instance.OpenDb();
            //                                        OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(t - r), Convert.ToString(r), oaNo);
            //                                        OracleManager.Instance.CloseDb();
            //                                    }
            //                                }
            //                            }
            //                            else if (i != day)//请假有n天,只是其中的第一天
            //                            {
            //                                if (st1 >= bt)
            //                                {
            //                                    if (st1 >= et)
            //                                    {
            //                                        x = x + 0;
            //                                    }
            //                                    else
            //                                    {
            //                                        if (st1 <= xb)
            //                                        {
            //                                            TimeSpan ts1 = xb - st1;
            //                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                            TimeSpan ts2 = et - xe;
            //                                            double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                            x = x + d1 + d2;
            //                                            string dayy = strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute);
            //                                            sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                            conn = ToolHelper.OpenRavoerp();
            //                                            cmd = new OracleCommand(sql, conn);
            //                                            da = new OracleDataAdapter(cmd);
            //                                            dt = new DataTable();
            //                                            da.Fill(dt);
            //                                            num = dt.Rows.Count;
            //                                            ToolHelper.CloseSql(conn);
            //                                            if (num == 0)
            //                                            {
            //                                                OracleManager.Instance.OpenDb();
            //                                                OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                                OracleManager.Instance.CloseDb();
            //                                            }
            //                                        }
            //                                        else if (st1 >= xb && st1 >= xe)
            //                                        {
            //                                            TimeSpan ts1 = et - st1;
            //                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                            x = x + d1;
            //                                            string dayy = strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute);
            //                                            sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                            conn = ToolHelper.OpenRavoerp();
            //                                            cmd = new OracleCommand(sql, conn);
            //                                            da = new OracleDataAdapter(cmd);
            //                                            dt = new DataTable();
            //                                            da.Fill(dt);
            //                                            num = dt.Rows.Count;
            //                                            ToolHelper.CloseSql(conn);
            //                                            if (num == 0)
            //                                            {
            //                                                OracleManager.Instance.OpenDb();
            //                                                OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1), "0", oaNo);
            //                                                OracleManager.Instance.CloseDb();
            //                                            }
            //                                        }
            //                                        else if (st1 >= xb && st1 <= xe)
            //                                        {
            //                                            TimeSpan ts1 = et - xe;
            //                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                            x = x + d1;
            //                                            string dayy = strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute);
            //                                            sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                            conn = ToolHelper.OpenRavoerp();
            //                                            cmd = new OracleCommand(sql, conn);
            //                                            da = new OracleDataAdapter(cmd);
            //                                            dt = new DataTable();
            //                                            da.Fill(dt);
            //                                            num = dt.Rows.Count;
            //                                            ToolHelper.CloseSql(conn);
            //                                            if (num == 0)
            //                                            {
            //                                                OracleManager.Instance.OpenDb();
            //                                                OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(xe.Hour) + ":" + Convert.ToString(xe.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1), "0", oaNo);
            //                                                OracleManager.Instance.CloseDb();
            //                                            }
            //                                        }
            //                                        else
            //                                        {
            //                                            x = x + 0;
            //                                        }
            //                                    }
            //                                }
            //                                else
            //                                {
            //                                    if (bt <= xb)
            //                                    {
            //                                        TimeSpan ts1 = xb - bt;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        TimeSpan ts2 = et - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d1 + d2;
            //                                        string dayy = strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else if (bt >= xb && bt >= xe)
            //                                    {
            //                                        TimeSpan ts1 = et - st1;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        x = x + d1;
            //                                        string dayy = strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, strDate + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), strDate + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1), "0", oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        x = x + 0;
            //                                    }

            //                                }

            //                            }
            //                        }
            //                        else if (bt > et)//跨天
            //                        {
            //                            if (day == 0)//只有一天
            //                            {
            //                                if (st1 >= bt && et1 <= xb)
            //                                {
            //                                    TimeSpan ts1 = et1 - st1;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;

            //                                }
            //                                else if (st1 >= bt && et1 >= xb)
            //                                {
            //                                    TimeSpan ts1 = xb - st1;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else if (st1 <= bt && et1 >= xb)
            //                                {
            //                                    TimeSpan ts1 = et1 - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                                else if (st1 <= bt && et1 <= xb)
            //                                {
            //                                    TimeSpan ts1 = et1 - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                }
            //                            }
            //                            else if (i != day)
            //                            {
            //                                if (st1 >= bt)
            //                                {
            //                                    TimeSpan ts1 = xb - st1;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    TimeSpan ts2 = et - xe;
            //                                    double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                    x = x + d1 + d2;
            //                                }
            //                                else
            //                                {
            //                                    TimeSpan ts1 = xb - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    TimeSpan ts2 = et - xe;
            //                                    double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                    x = x + d1 + d2;
            //                                }
            //                            }
            //                        }
            //                        else
            //                        {
            //                            x = x + 0;
            //                        }
            //                    }
            //                    else if (i == day)//最后一天
            //                    {
            //                        if (bt < et)//不跨天
            //                        {
            //                            //if (et1 >= et)
            //                            //{
            //                            //    TimeSpan ts1 = et - xe;
            //                            //    double d1 = Math.Round((double)(t - r));
            //                            //    x = x + d1;
            //                            //    string kssj = Convert.ToString(Convert.ToDateTime(strDate).AddDays(i));
            //                            //    OracleManager.Instance.OpenDb();
            //                            //    OracleManager.Instance.CallProcCC(manid, qjlx, kssj + " " + Convert.ToString(st1.Hour) + ":" + Convert.ToString(st1.Minute), strDate + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1), "0", oaNo);
            //                            //    OracleManager.Instance.CloseDb();
            //                            //}
            //                            //else
            //                            //{
            //                            if (et1 <= bt)
            //                            {
            //                                x = x + 0;
            //                            }
            //                            else
            //                            {
            //                                if (et1 >= xb && et1 >= xe)
            //                                {
            //                                    if (et1 >= et)
            //                                    {
            //                                        TimeSpan ts1 = xb - bt;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        TimeSpan ts2 = et - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d1 + d2;
            //                                        DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                                        string kssj = aaa.ToString("yyyy-MM-dd");
            //                                        string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        TimeSpan ts1 = xb - bt;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        TimeSpan ts2 = et1 - xe;
            //                                        double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                        x = x + d1 + d2;
            //                                        DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                                        string kssj = aaa.ToString("yyyy-MM-dd");
            //                                        string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                        sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                        conn = ToolHelper.OpenRavoerp();
            //                                        cmd = new OracleCommand(sql, conn);
            //                                        da = new OracleDataAdapter(cmd);
            //                                        dt = new DataTable();
            //                                        da.Fill(dt);
            //                                        num = dt.Rows.Count;
            //                                        ToolHelper.CloseSql(conn);
            //                                        if (num == 0)
            //                                        {
            //                                            OracleManager.Instance.OpenDb();
            //                                            OracleManager.Instance.CallProcCC(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1 + d2), rests, oaNo);
            //                                            OracleManager.Instance.CloseDb();
            //                                        }
            //                                    }
            //                                }
            //                                else if (et1 >= xb && et1 <= xe)
            //                                {
            //                                    TimeSpan ts1 = xb - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                    DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                                    string kssj = aaa.ToString("yyyy-MM-dd");
            //                                    string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                    sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                    conn = ToolHelper.OpenRavoerp();
            //                                    cmd = new OracleCommand(sql, conn);
            //                                    da = new OracleDataAdapter(cmd);
            //                                    dt = new DataTable();
            //                                    da.Fill(dt);
            //                                    num = dt.Rows.Count;
            //                                    ToolHelper.CloseSql(conn);
            //                                    if (num == 0)
            //                                    {
            //                                        OracleManager.Instance.OpenDb();
            //                                        OracleManager.Instance.CallProcCC(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(xb.Hour) + ":" + Convert.ToString(xb.Minute), Convert.ToString(d1), "0", oaNo);
            //                                        OracleManager.Instance.CloseDb();
            //                                    }
            //                                }
            //                                else if (et1 <= xb && et1 <= xe)
            //                                {
            //                                    TimeSpan ts1 = et1 - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                    DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                                    string kssj = aaa.ToString("yyyy-MM-dd");
            //                                    string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                    sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                    conn = ToolHelper.OpenRavoerp();
            //                                    cmd = new OracleCommand(sql, conn);
            //                                    da = new OracleDataAdapter(cmd);
            //                                    dt = new DataTable();
            //                                    da.Fill(dt);
            //                                    num = dt.Rows.Count;
            //                                    ToolHelper.CloseSql(conn);
            //                                    if (num == 0)
            //                                    {
            //                                        OracleManager.Instance.OpenDb();
            //                                        OracleManager.Instance.CallProcCC(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1), "0", oaNo);
            //                                        OracleManager.Instance.CloseDb();
            //                                    }
            //                                }
            //                                else if (et1 <= xb && et1 >= xe)
            //                                {
            //                                    TimeSpan ts1 = et1 - bt;
            //                                    double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                    x = x + d1;
            //                                    DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                                    string kssj = aaa.ToString("yyyy-MM-dd");
            //                                    string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                                    sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                                    conn = ToolHelper.OpenRavoerp();
            //                                    cmd = new OracleCommand(sql, conn);
            //                                    da = new OracleDataAdapter(cmd);
            //                                    dt = new DataTable();
            //                                    da.Fill(dt);
            //                                    num = dt.Rows.Count;
            //                                    ToolHelper.CloseSql(conn);
            //                                    if (num == 0)
            //                                    {
            //                                        OracleManager.Instance.OpenDb();
            //                                        OracleManager.Instance.CallProcCC(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(et1.Hour) + ":" + Convert.ToString(et1.Minute), Convert.ToString(d1), "0", oaNo);
            //                                        OracleManager.Instance.CloseDb();
            //                                    }
            //                                }
            //                            }
            //                            //}
            //                        }
            //                        else if (bt > et)//跨天 weiwancheng
            //                        {
            //                            if (et1 <= et)
            //                            {
            //                                TimeSpan ts1 = et - et1;
            //                                double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                double s = (x - d1);
            //                                x = s;
            //                            }
            //                            else
            //                            {
            //                                if (et1 >= et && et1 <= bt)
            //                                {
            //                                    x = x + 0;
            //                                }
            //                                else
            //                                {
            //                                    if (et1 <= xb && et1 >= xe)
            //                                    {
            //                                        if (et1 >= bt)
            //                                        {
            //                                            TimeSpan ts1 = et1 - bt;
            //                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                            x = x + d1;
            //                                        }
            //                                        else
            //                                        {
            //                                            //et1 = et1.AddHours(24);
            //                                            TimeSpan ts1 = xb - bt;
            //                                            double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                            TimeSpan ts2 = et1 - xe;
            //                                            double d2 = Math.Round((double)(ts2.Hours * 60 + ts2.Minutes) / 60, 2);
            //                                            x = x + d1 + d2;
            //                                        }

            //                                    }
            //                                    else if (et1 >= xb && et1 >= xe)
            //                                    {
            //                                        TimeSpan ts1 = xb - bt;
            //                                        double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                                        x = x + d1;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                        else
            //                        {
            //                            x = x + 0;
            //                        }
            //                    }
            //                    else//n天当中的几天
            //                    {
            //                        if (bt < et)//不跨天
            //                        {
            //                            x = x + t - r;
            //                            DateTime aaa = Convert.ToDateTime(strDate).AddDays(i);
            //                            string kssj = aaa.ToString("yyyy-MM-dd");
            //                            string dayy = kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute);
            //                            sql = " select * from ask_leave where oano='" + oaNo + "' and leaveday=to_date('" + dayy + "','yyyy-mm-dd hh24:mi:ss') and man_id='" + manid + "' ";
            //                            conn = ToolHelper.OpenRavoerp();
            //                            cmd = new OracleCommand(sql, conn);
            //                            da = new OracleDataAdapter(cmd);
            //                            dt = new DataTable();
            //                            da.Fill(dt);
            //                            num = dt.Rows.Count;
            //                            ToolHelper.CloseSql(conn);
            //                            if (num == 0)
            //                            {
            //                                OracleManager.Instance.OpenDb();
            //                                OracleManager.Instance.CallProcCC(manid, qjlx, kssj + " " + Convert.ToString(bt.Hour) + ":" + Convert.ToString(bt.Minute), kssj + " " + Convert.ToString(et.Hour) + ":" + Convert.ToString(et.Minute), Convert.ToString(t - r), rests, oaNo);
            //                                OracleManager.Instance.CloseDb();
            //                            }
            //                        }
            //                        else//跨天
            //                        {
            //                            x = x + t - r;
            //                            //TimeSpan ts1 = xb - bt;
            //                            //double d1 = Math.Round((double)(ts1.Hours * 60 + ts1.Minutes) / 60, 2);
            //                            //x = x + d1;
            //                        }

            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }

            //    total = x;
            //    string z = Convert.ToString(total);
            //}
            //catch (Exception ex)
            //{
            //}
        }
        //加班单
        private void Button7_Click(object sender, EventArgs e)
        {
            string time = "{\"manid\":\"509271\",\"startTime\":\"2019-07-21 18:00\",\"endTime\":\"2019-07-22 07:30\",\"rest\":\"1.50\",\"oaNo\":\"R2-JBSQD2019070666\",\"dptID\":\"64\"}";
            try
            {
                OTInfo ot = new JavaScriptSerializer().Deserialize<OTInfo>(time);
                string dptID = ot.dptID;
                DateTime start = Convert.ToDateTime(ot.startTime);
                DateTime end = Convert.ToDateTime(ot.endTime);
                string s = start.ToString("T");
                string e1 = end.ToString("T");
                DateTime ss = Convert.ToDateTime(s);
                DateTime ee = Convert.ToDateTime(e1);
                OracleConnection conn = ToolHelper.OpenRavoerp("22");
                if (conn != null)
                {
                    string sql = " select subcompanyid1 from hrmdepartment@ECOLOGY where id='" + dptID + "'";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    string type = dt.Rows[0]["subcompanyid1"].ToString();
                    ToolHelper.CloseSql(conn);
                    if (type != null)
                    {
                        if (ee >= ss)
                        {
                            //不跨天
                            TimeSpan ts = ee - ss;
                            double d = Math.Round((double)(ts.Hours * 60 + ts.Minutes) / 60, 2);
                            double t = d - Convert.ToDouble(ot.rest);
                            string times = Convert.ToString(t);
                            conn = ToolHelper.OpenRavoerp(type);
                            if (conn != null)
                            {
                                //查询是否在数据库里存在
                                sql = " select * from t_kqdxcard where oano='" + ot.oaNo + "' and fbday=to_date('" + ot.startTime + "','yyyy-mm-dd hh24:mi:ss') and fempid='" + ot.manid + "' ";
                                cmd = new OracleCommand(sql, conn);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                var num = dt.Rows.Count;
                                ToolHelper.CloseSql(conn);
                                if (num == 0)
                                {
                                    if (type == "21")
                                    {
                                        OracleManager1.Instance.OpenDb();
                                        OracleManager1.Instance.CallProcOT(ot.manid, ot.startTime, ot.endTime, ot.oaNo, times, ot.rest);
                                        OracleManager1.Instance.CloseDb();
                                    }
                                    else if (type == "23")
                                    {
                                        OracleManager.Instance.OpenDb();
                                        OracleManager.Instance.CallProcOT(ot.manid, ot.startTime, ot.endTime, ot.oaNo, times, ot.rest);
                                        OracleManager.Instance.CloseDb();
                                    }
                                    else if (type == "141")
                                    {
                                        OracleManager3.Instance.OpenDb();
                                        OracleManager3.Instance.CallProcOT(ot.manid, ot.startTime, ot.endTime, ot.oaNo, times, ot.rest);
                                        OracleManager3.Instance.CloseDb();
                                    }
                                    else if (type == "22")
                                    {
                                        OracleManager5.Instance.OpenDb();
                                        OracleManager5.Instance.CallProcOT(ot.manid, ot.startTime, ot.endTime, ot.oaNo, times, ot.rest);
                                        OracleManager5.Instance.CloseDb();
                                    }
                                }
                            }
                        }
                        else
                        {
                            //跨天 开始时间 结束时间
                            string sday = start.ToString("yyyy-MM-dd");
                            string eday = end.ToString("yyyy-MM-dd");

                            DateTime zero = Convert.ToDateTime("1970-01-01 00:00:00");
                            DateTime ze = Convert.ToDateTime(sday + " 23:59:59");
                            TimeSpan ts = ze - start;
                            double d = Math.Round((double)(ts.Hours * 60 + ts.Minutes) / 60, 1);
                            double t = d - Convert.ToDouble(ot.rest);
                            string times = Convert.ToString(t);
                            conn = ToolHelper.OpenRavoerp(type);
                            if (conn != null)
                            {
                                //查询是否在数据库里存在
                                sql = " select * from t_kqdxcard where oano='" + ot.oaNo + "' and fbday=to_date('" + ot.startTime + "','yyyy-mm-dd hh24:mi:ss') and fempid='" + ot.manid + "' ";
                                cmd = new OracleCommand(sql, conn);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                var num = dt.Rows.Count;
                                ToolHelper.CloseSql(conn);
                                if (num == 0)
                                {
                                    if (type == "21")
                                    {
                                        OracleManager1.Instance.OpenDb();
                                        OracleManager1.Instance.CallProcOT(ot.manid, ot.startTime, sday + " 23:59:59", ot.oaNo, times, ot.rest);
                                        OracleManager1.Instance.CloseDb();
                                    }
                                    else if (type == "23")
                                    {
                                        OracleManager.Instance.OpenDb();
                                        OracleManager.Instance.CallProcOT(ot.manid, ot.startTime, sday + " 23:59:59", ot.oaNo, times, ot.rest);
                                        OracleManager.Instance.CloseDb();
                                    }
                                    else if (type == "141")
                                    {
                                        OracleManager3.Instance.OpenDb();
                                        OracleManager3.Instance.CallProcOT(ot.manid, ot.startTime, sday + " 23:59:59", ot.oaNo, times, ot.rest);
                                        OracleManager3.Instance.CloseDb();
                                    }
                                    else if (type == "22")
                                    {
                                        OracleManager5.Instance.OpenDb();
                                        OracleManager5.Instance.CallProcOT(ot.manid, ot.startTime, sday + " 23:59:59", ot.oaNo, times, ot.rest);
                                        OracleManager5.Instance.CloseDb();
                                    }
                                }
                            }
                            string eeday = eday + " 00:00:00";
                            DateTime eDataTime = Convert.ToDateTime(eeday);
                            ts = ee - zero;
                            d = Math.Round((double)(ts.Hours * 60 + ts.Minutes) / 60, 2);
                            t = d - Convert.ToDouble(ot.rest);
                            times = Convert.ToString(t);
                            conn = ToolHelper.OpenRavoerp(type);
                            if (conn != null)
                            {
                                //查询是否在数据库里存在
                                sql = " select * from t_kqdxcard where oano='" + ot.oaNo + "' and fbday=to_date('" + eDataTime + "','yyyy-mm-dd hh24:mi:ss') and fempid='" + ot.manid + "' ";
                                cmd = new OracleCommand(sql, conn);
                                da = new OracleDataAdapter(cmd);
                                dt = new DataTable();
                                da.Fill(dt);
                                var num = dt.Rows.Count;
                                ToolHelper.CloseSql(conn);
                                if (num == 0)
                                {
                                    if (type == "21")
                                    {
                                        OracleManager1.Instance.OpenDb();
                                        OracleManager1.Instance.CallProcOT(ot.manid, eDataTime.ToString("yyyy-MM-dd HH:mm:ss"), ot.endTime, ot.oaNo, times, ot.rest);
                                        OracleManager1.Instance.CloseDb();
                                    }
                                    else if (type == "23")
                                    {
                                        OracleManager.Instance.OpenDb();
                                        OracleManager.Instance.CallProcOT(ot.manid, eDataTime.ToString("yyyy-MM-dd HH:mm:ss"), ot.endTime, ot.oaNo, times, ot.rest);
                                        OracleManager.Instance.CloseDb();
                                    }
                                    else if (type == "141")
                                    {
                                        OracleManager3.Instance.OpenDb();
                                        OracleManager3.Instance.CallProcOT(ot.manid, eDataTime.ToString("yyyy-MM-dd HH:mm:ss"), ot.endTime, ot.oaNo, times, ot.rest);
                                        OracleManager3.Instance.CloseDb();
                                    }
                                    else if (type == "22")
                                    {
                                        OracleManager5.Instance.OpenDb();
                                        OracleManager5.Instance.CallProcOT(ot.manid, eDataTime.ToString("yyyy-MM-dd HH:mm:ss"), ot.endTime, ot.oaNo, times, ot.rest);
                                        OracleManager5.Instance.CloseDb();
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// Dapper查询oracle,返回IEnumerable<dynamic>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button8_Click(object sender, EventArgs e)
        {
            //string connectionString = "DATA SOURCE=ORCL;USER ID=xxx;PASSWORD=xxx";
            //string sql = @"select * from crm.Tb_Cus_Customer where customercode = :customercode";

            //string connectionString = "User ID=ravo2;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            //string sql = @"select * from BANKTB";

            //using (IDbConnection conn = new OracleConnection(connectionString))
            //{
            //    conn.Open();
            //    IEnumerable<dynamic> dynList = conn.Query(sql, new {  });
            //    conn.Close();
            //}


        }
        //取二进制流
        private void Button9_Click(object sender, EventArgs e)
        {
            string connString = "User ID=ravo2;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            OracleConnection conn = new OracleConnection(connString);
            try
            {
                conn.Open();
                string sql = " select * from BANKTB  ";
                List<BANKTB> bankList = new List<BANKTB>();
                List<string> s = new List<string>();
                OracleCommand cmd = new OracleCommand(sql, conn);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //IEnumerable<dynamic> dynList
                        string a = "BANKID = '" + dr.GetValue(0).ToString() + "', BANKNAME = '" + dr.GetValue(1).ToString() + "'";
                        s.Add(a);
                        //+ dr.GetValue(2).ToString() + dr.GetValue(3).ToString() +
                        //dr.GetValue(4).ToString() + dr.GetValue(5).ToString() + dr.GetValue(6).ToString() + dr.GetValue(7).ToString();

                        //BANKTB bankTB = new BANKTB(dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), dr.GetValue(2).ToString(), dr.GetValue(3).ToString(),
                        //    dr.GetValue(4).ToString(), dr.GetValue(5).ToString(), dr.GetValue(6).ToString(), dr.GetValue(7).ToString());
                        //bankList.Add(bankTB);
                    }
                }
                //OracleDataAdapter da = new OracleDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //string x = dt.Rows[0]["x"].ToString();
                conn.Close();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// sqlserver连接方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button10_Click(object sender, EventArgs e)
        {
            //构造连接字符串
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "172.16.11.26";
            scsb.InitialCatalog = "zytk35";
            scsb.UserID = "sa";
            scsb.Password = "123";

            //创建连接 参数为连接字符串
            SqlConnection sqlConn = new SqlConnection(scsb.ToString());

            //打开连接
            sqlConn.Open();

            //需要执行的SQL语句
            String sqlStr = "SELECT  [PerCode] ,[CardID] FROM [zytk35].[dbo].[id_AccountsInfo]";

            //创建用于执行sql语句的对象
            //参数1：sql语句字符串,参数2：已打开的数据连接对象
            SqlCommand sqlComm = new SqlCommand(sqlStr, sqlConn);

            //接收查询到的sql数据
            SqlDataReader reader = sqlComm.ExecuteReader();

            //读取数据 
            while (reader.Read())
            {
                // 可以使用数据库中的字段名，也可以使用角标访问
                string a = reader["PerCode"].ToString();
                string b = reader["CardID"].ToString();
            }
            sqlConn.Close();
        }
        //企业微信
        private void Button11_Click(object sender, EventArgs e)
        {
            try
            {

                OracleConnection conn = OAService.MyPublic.ToolHelper.OpenRavoerp("oa");
                string sql = " select * from OA_FT_POS_POA where FLC_STATA='0' ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                int num1 = dt1.Rows.Count;
                OAService.MyPublic.ToolHelper.CloseSql(conn);
                for (int j = 0; j < num1; j++)
                {


                    conn = OAService.MyPublic.ToolHelper.OpenRavoerp("oa");
                    sql = " select a.*,x.*,b.id name1,c.id name2,d.id name3,e.id name4,f.id name5 from OA_FT_POS_POA a" +
                       " left join HRMRESOURCE b on a.CA_USER_NO1=b.workcode " +
                       " left join HRMRESOURCE c on a.CA_USER_NO2=c.workcode " +
                       " left join HRMRESOURCE d on a.CA_USER_NO3=d.workcode " +
                       " left join HRMRESOURCE e on a.CA_USER_NO4=e.workcode " +
                       " left join HRMRESOURCE f on a.CA_USER_NO5=f.workcode " +
                       " left join OA_FT_POS_POB x on a.id=x.PO_ID where a.FLC_STATA='0' and id='" + dt1.Rows[j]["ID"].ToString() + "'";
                    cmd = new OracleCommand(sql, conn);
                    da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int num = dt.Rows.Count;
                    OAService.MyPublic.ToolHelper.CloseSql(conn);
                    string name1 = dt.Rows[0]["name1"].ToString();
                    string name2 = dt.Rows[0]["name2"].ToString();
                    string name3 = dt.Rows[0]["name3"].ToString();
                    string name4 = dt.Rows[0]["name4"].ToString();
                    string name5 = dt.Rows[0]["name5"].ToString();
                    if (name3 == "")
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
     " <creatorId>" + name1 + "</creatorId> " +
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
                           "         <fieldValue>" + name1 + "</fieldValue> " +
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
                    int b = Convert.ToInt32(name1);//创建人
                    string ab = wsx.doCreateWorkflowRequest(xml, b);
                    int re = Convert.ToInt32(ab);
                    if (re > 0)//成功
                    {
                        conn = OAService.MyPublic.ToolHelper.OpenRavoerp("oa");
                        sql = " update OA_FT_POS_POA set FLC_STATA='1' where id='" + dt1.Rows[j]["ID"].ToString() + "' ";
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        OAService.MyPublic.ToolHelper.CloseSql(conn);
                    }
                    else//失败
                    {
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }



        }

        /// <summary>
        /// 判断socket是否连接成功
        /// </summary>
        private static bool IsConnectionSuccessful = false;
        private static Exception socketexception;
        private static ManualResetEvent TimeoutObject = new ManualResetEvent(false);

        public static TcpClient Connect(string ip, int port, int timeoutMSec, out string success)
        {
            TimeoutObject.Reset();
            socketexception = null;
            TcpClient tcpclient = new TcpClient();
            tcpclient.BeginConnect(ip, port, new AsyncCallback(CallBackMethod), tcpclient);
            if (TimeoutObject.WaitOne(timeoutMSec, false))
            {
                if (IsConnectionSuccessful)
                {
                    success = "YES";
                    return tcpclient;
                }
                else
                {
                    success = "NO";
                    return tcpclient;
                }
            }
            else
            {
                tcpclient.Close();
                success = "NO";
                return tcpclient;
            }
        }
        private static void CallBackMethod(IAsyncResult asyncresult)
        {
            try
            {
                IsConnectionSuccessful = false;
                TcpClient tcpclient = asyncresult.AsyncState as TcpClient;
                if (tcpclient.Client != null)
                {
                    tcpclient.EndConnect(asyncresult);
                    IsConnectionSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                IsConnectionSuccessful = false;
                socketexception = ex;
            }
            finally
            {
                TimeoutObject.Set();
            }
        }
        //public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        public zkemkeeper.CZKEM axCZKEM1 = new zkemkeeper.CZKEM();
        private void Button1_Click_1(object sender, EventArgs e)
        {
            //SSR_SetUserInfo 添加用户
            try
            {

                bool bIsConnected = axCZKEM1.Connect_Net("192.168.31.77", 4370);
                axCZKEM1.EnableDevice(77, false);
                //bool readAllTemplate = axCZKEM1.ReadAllTemplate(77);
                bool readAllTemplate = axCZKEM1.SSR_GetUserInfo(77, "10525", out string name, out string password, out int pr, out bool enable);
                if (readAllTemplate == true)
                {
                    int Flag = 1;
                    string TmpData = "";
                    int TmpLength = 0;
                    //while (axCZKEM1.GetUserTmpEx(77,"10525",0, out Flag, out byte TmpData, out TmpLength))
                    //{

                    //}

                    while (axCZKEM1.GetUserTmpExStr(77, "10525", 6, out Flag, out TmpData, out TmpLength))
                    {
                        string tmp = TmpData;
                        int tl = TmpLength;

                    }
                }
                axCZKEM1.EnableDevice(77, true);
                axCZKEM1.Disconnect();



                ////添加人员
                //while (axCZKEM1.SSR_SetUserInfo(77,"1234","走走走","",0, true))
                //{

                //}


                ////获取人员信息
                //axCZKEM1.ReadAllUserID(77);
                //string sEnrollNumber = "";
                //string sName = "";
                //string sPassword = "";
                //int iPrivilege = 0;
                //bool bEnabled = false;

                //while (axCZKEM1.SSR_GetAllUserInfo(77, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
                //{
                //    string name = sName;
                //    int a = name.LastIndexOf("杰");//获得.的位置
                //    string na = name.Substring(0, a);//获得目标字符串
                //}




            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public static string EncryptString(string str)
        {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }

        public string UrlEncode(string str)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in str)
            {
                if (HttpUtility.UrlEncode(c.ToString()).Length > 1)
                {
                    builder.Append(HttpUtility.UrlEncode(c.ToString()).ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }
        //秒
        public string time()
        {
            long aa = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string t = Convert.ToString(aa);
            return t;
        }
        //毫秒
        public string times(DateTime dt)
        {
            long aa = (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            string t = Convert.ToString(aa);
            return t;
        }
        //新建商品
        private void Button5_Click_1(object sender, EventArgs e)
        {
            string secret = "5cf249cebe8c45eb6254b55e11f9e944";
            string key = "3723429465";
            try
            {
                //构造连接字符串
                SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                scsb.DataSource = "172.16.11.9";
                scsb.InitialCatalog = "FumaCRM8";
                scsb.UserID = "sa";
                scsb.Password = "abc_123";
                //创建连接 参数为连接字符串
                SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                sqlConn.Open();
                string sqlStr = " SELECT  * FROM FumaCRM8.dbo.WLN_ITEM where CHANGED='0' and bar_code='6058-B' ";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sqlStr, sqlConn);//从数据库中查询
                da.Fill(dt);//将数据填充到DataSet
                sqlConn.Close();//关闭连接
                int num = dt.Rows.Count;
                if (num > 0)
                {
                    for (int i = 0; i < num; i++)
                    {
                        string article_number = dt.Rows[i]["article_number"].ToString();
                        string bar_code = dt.Rows[i]["bar_code"].ToString();
                        string item_name = dt.Rows[i]["item_name"].ToString();
                        string sale_price = dt.Rows[i]["sale_price"].ToString();
                        string weight = dt.Rows[i]["weight"].ToString();
                        string unit = dt.Rows[i]["unit"].ToString();
                        string item_pic = dt.Rows[i]["item_pic"].ToString();

                        add_item add = new add_item();
                        add.article_number = article_number;
                        add.bar_code = bar_code;
                        add.item_name = item_name;
                        add.sale_price = sale_price;
                        add.weight = weight;
                        add.unit = unit;
                        add.item_code = article_number;
                        //add.item_pic=
                        string json = new JavaScriptSerializer().Serialize(add);
                        string urlcode = UrlEncode(json);
                        string t = time();
                        string post = secret + "_app=" + key + "&_s=&_t=" + t + "&item=" + urlcode + secret;
                        string m = EncryptString(post);
                        var client = new RestClient("https://open-api.hupun.com/api/erp/goods/add/item");

                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        request.AddParameter("_app", key);
                        request.AddParameter("_s", "");
                        request.AddParameter("_sign", m);
                        request.AddParameter("_t", t);
                        request.AddParameter("item", json);
                        IRestResponse response = client.Execute(request);
                        string str = response.Content;
                        returnInfo re = new JavaScriptSerializer().Deserialize<returnInfo>(str);
                        if (re.code == 0)
                        {
                            //打开连接
                            sqlConn.Open();
                            sqlStr = " update FumaCRM8.dbo.WLN_ITEM  set code='1',message='" + re.message + "',codedate='" + DateTime.Now + "'  where article_number='" + article_number + "' ";
                            SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                            int result = comm.ExecuteNonQuery();
                            sqlConn.Close();//关闭连接
                        }
                        else
                        {
                            //打开连接
                            sqlConn.Open();
                            sqlStr = " update FumaCRM8.dbo.WLN_ITEM  set code='2',message='" + re.message + "',codedate='" + DateTime.Now + "'  where article_number='" + article_number + "' ";
                            SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                            int result = comm.ExecuteNonQuery();
                            sqlConn.Close();//关闭连接
                        }

                    }
                }
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                string secret = "c3b5fee170b52b8397852c8ba03ef109";
                string key = "3123415742";


                //构造连接字符串
                SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                scsb.DataSource = "172.16.11.9";
                scsb.InitialCatalog = "FumaCRM8";
                scsb.UserID = "sa";
                scsb.Password = "abc_123";
                //创建连接 参数为连接字符串
                SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                //打开连接
                sqlConn.Open();
                string sqlStr = " SELECT  * FROM FumaCRM8.dbo.WLN_WHIN where code='0' ";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(sqlStr, sqlConn);//从数据库中查询
                da.Fill(dt);//将数据填充到DataSet
                sqlConn.Close();//关闭连接
                int num = dt.Rows.Count;
                if (num > 0)
                {
                    for (int i = 0; i < num; i++)
                    {
                        string storage_code = dt.Rows[i]["storage_code"].ToString();
                        string spec_code = dt.Rows[i]["spec_code"].ToString();
                        string size = dt.Rows[i]["size"].ToString();
                        string remark = dt.Rows[i]["remark"].ToString();
                        string whtwhfid = dt.Rows[i]["whtwhfid"].ToString();

                        Requestbill_add req = new Requestbill_add();
                        req.storage_code = storage_code;
                        req.remark = remark;
                        List<OpenStockReqBillDetailRequest> openlist = new List<OpenStockReqBillDetailRequest>();
                        OpenStockReqBillDetailRequest open = new OpenStockReqBillDetailRequest();
                        open.spec_code = spec_code;
                        open.size = size;
                        openlist.Add(open);
                        req.details = openlist;
                        string json = new JavaScriptSerializer().Serialize(req); string urlcode = UrlEncode(json);
                        string t = time();
                        //md5加密
                        string post = secret + "_app=" + key + "&_s=&_t=" + t + "&bill=" + urlcode + secret;
                        string m = EncryptString(post);
                        //新增其他入库订单
                        var client = new RestClient("http://114.67.231.162/api/erp/stock/in/requestbill/add");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        request.AddParameter("_app", key);
                        request.AddParameter("_s", "");
                        request.AddParameter("_sign", m);
                        request.AddParameter("_t", t);
                        request.AddParameter("bill", json);
                        IRestResponse response = client.Execute(request);
                        string str = response.Content;
                        returnInfo re = new JavaScriptSerializer().Deserialize<returnInfo>(str);
                        if (re.code == 0)
                        {
                            //打开连接
                            sqlConn.Open();
                            sqlStr = " update FumaCRM8.dbo.WLN_WHIN  set code='1',message='" + re.message + "',codetime='" + DateTime.Now + "'  where whtwhfid='" + whtwhfid + "' ";
                            SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                            int result = comm.ExecuteNonQuery();
                            sqlConn.Close();//关闭连接
                        }
                        else
                        {
                            //打开连接
                            sqlConn.Open();
                            sqlStr = " update FumaCRM8.dbo.WLN_WHIN  set code='2',message='" + re.message + "',codetime='" + DateTime.Now + "'  where whtwhfid='" + whtwhfid + "' ";
                            SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                            int result = comm.ExecuteNonQuery();
                            sqlConn.Close();//关闭连接
                        }

                    }
                }





                //Requestbill_add req = new Requestbill_add();
                //req.storage_code = "00003";
                //OpenStockReqBillDetailRequest open = new OpenStockReqBillDetailRequest();
                //open.spec_code = "00001411";
                //open.size = "1.0";
                //req.details = open;
                //string json = new JavaScriptSerializer().Serialize(req);
                //string aaa = UrlEncode(json);
                //string t = time();
                ////md5加密
                //string post = "c3b5fee170b52b8397852c8ba03ef109_app=3123415742&_s=&_t=" + t + "&bill=%7B%22storage_code%22%3A%2200003%22%2C%22details%22%3A%5B%7B%22spec_code%22%3A%2200001411%22%2C%22size%22%3A1.0%7D%5D%7Dc3b5fee170b52b8397852c8ba03ef109";
                //string m = EncryptString(post);
                ////新增其他入库订单
                //var client = new RestClient("http://114.67.231.162/api/erp/stock/in/requestbill/add");
                //client.Timeout = -1;
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                //request.AddParameter("_app", "3123415742");
                //request.AddParameter("_s", "");
                //request.AddParameter("_sign", m);
                //request.AddParameter("_t", t);
                //request.AddParameter("bill", "{\"storage_code\":\"00003\",\"details\":[{\"spec_code\":\"00001411\",\"size\":1.0}]}");
                //IRestResponse response = client.Execute(request);
                //string str = response.Content;

                ////新增其他出库订单
                //var client = new RestClient("http://114.67.231.162/api/erp/stock/out/requestbill/add");
                //client.Timeout = -1;
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                //request.AddParameter("_app", "3123415742");
                //request.AddParameter("_s", "");
                //request.AddParameter("_sign", "88e2ffe8c472b80ce1e6d712f8c69ff0");
                //request.AddParameter("_t", "1577084053");
                //request.AddParameter("bill", "7B%22storage_code%22%3A%2200003%22%2C%22details%22%3A%5B%7B%22spec_code%22%3A%2200001411%22%2C%22size%22%3A1.0%7D%5D%7D");
                //IRestResponse response = client.Execute(request);
                //string str = response.Content;


                ////查询库存
                //var client = new RestClient("http://114.67.231.162/api/erp/open/inventory/items/get/by/modifytime");
                //client.Timeout = -1;
                //DateTime timeStampStartTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                //long aa = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                //string t = Convert.ToString(aa);
                //string post = "c3b5fee170b52b8397852c8ba03ef109_app=3123415742&_t=" + t + "&modify_time=123123123&page_no=1&page_size=10&storage=001c3b5fee170b52b8397852c8ba03ef109";
                //string md5 = EncryptString(post);
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                //request.AddParameter("_app", "3123415742");
                //request.AddParameter("_t", t);
                //request.AddParameter("_sign", md5);
                //request.AddParameter("modify_time", "123123123");
                //request.AddParameter("page_no", "1");
                //request.AddParameter("page_size", "10");
                //request.AddParameter("storage", "001");
                //IRestResponse response = client.Execute(request);
                //string str = response.Content;


            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }

        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            //构造连接字符串
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "172.16.11.26";
            scsb.InitialCatalog = "zytk35";
            scsb.UserID = "sa";
            scsb.Password = "123";

            //创建连接 参数为连接字符串
            SqlConnection sqlConn = new SqlConnection(scsb.ToString());

            //打开连接
            sqlConn.Open();

            string sqlStr = " SELECT  [PerCode] ,[CardID] FROM [zytk35].[dbo].[id_AccountsInfo] ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlStr, sqlConn);//从数据库中查询
            da.Fill(dt);//将数据填充到DataSet
            sqlConn.Close();//关闭连接
            string a = dt.Rows[0]["PerCode"].ToString();
            string b = dt.Rows[1]["PerCode"].ToString();

        }
        /// <summary>
        /// string转byte
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] stringToByte(string data)
        {
            try
            {
                byte[] bt = System.Text.Encoding.Default.GetBytes(data);
                return bt;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private static string byteToSting(byte[] data)
        {
            try
            {
                string str = System.Text.Encoding.Default.GetString(data);
                return str;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //string encryptStr = AesCode.encrypt(string strXml);
        //string decryptUTF8Str = AesCode.decrypt2UTF8(string str);//将加密串解密成UTF-8格式字符串
        private static string encrypt(string data)
        {
            try
            {
                //byte[] bt= System.Text.Encoding.Default.GetBytes(data);
                //string str = System.Text.Encoding.Default.GetString(byteArray);
                //byte[] result = encrypt(bt);
                //return bytesToString(result);
                byte[] bt = stringToByte(data);
                byte[] encryptBt = AesCode.encrypt(bt);
                string encryptStr = byteToSting(encryptBt);
                return encryptStr;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        //解密
        private static string decrypt(string data)
        {
            try
            {
                string decryptGBKStr = AesCode.decrypt2GBK(data);
                string decryptUTF8Str = AesCode.decrypt2UTF8(data);
                return decryptUTF8Str;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        private void Button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                string jia = encrypt("abc123");
                string jie = decrypt(jia);
                string b = "";
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void Button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                //string secret = "c3b5fee170b52b8397852c8ba03ef109";
                //string key = "3123415742";
                string secret = "5cf249cebe8c45eb6254b55e11f9e944";
                string key = "3723429465";

                //前一天的晚上10点
                stockbill_query sq = new stockbill_query();
                DateTime dt = DateTime.Now.AddDays(-5);
                string ti = dt.ToString("yyyy-MM-dd") + " 22:00:00";
                DateTime dti = Convert.ToDateTime(ti);
                string mt = times(dti);
                //string mt = times(DateTime.Now);
                sq.modify_time = mt;
                sq.page = "1";
                sq.limit = "100";
                string json = new JavaScriptSerializer().Serialize(sq);
                string urlcode = UrlEncode(json);
                string t = time();
                //t = "1558952035";
                string post = secret + "_app=" + key + "&_s=&_t=" + t + "&limit=" + sq.limit + "&modify_time=" + sq.modify_time + "&page=" + sq.page + secret;
                string m = EncryptString(post);
                var client = new RestClient("http://open-api.hupun.com/api/erp/stock/out/stockbill/query");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("_app", key);
                request.AddParameter("_s", "");
                request.AddParameter("_sign", m);
                request.AddParameter("_t", t);
                request.AddParameter("limit", sq.limit);
                request.AddParameter("modify_time", sq.modify_time);
                request.AddParameter("page", sq.page);

                IRestResponse response = client.Execute(request);
                string str = response.Content;
                returnInfos re = new JavaScriptSerializer().Deserialize<returnInfos>(str);
                string mess = re.message;
                int code = re.code;
                if (code == 0)
                {
                    int num = re.data[0].details.Count;
                    for (int i = 0; i < num; i++)
                    {
                        string bill_creater = re.data[0].bill_creater;
                        string bill_date = re.data[0].bill_date;
                        string bill_type = re.data[0].bill_type;
                        string create_time = re.data[0].create_time;
                        string logistic_name = re.data[0].logistic_name;
                        string modified_time = re.data[0].modified_time;
                        string operate_name = re.data[0].operate_name;
                        string reason = re.data[0].reason;
                        string remark = re.data[0].remark;
                        string stock_code = re.data[0].stock_code;
                        string stock_req_bill_code = re.data[0].stock_req_bill_code;
                        string storage_code = re.data[0].storage_code;
                        string storage_name = re.data[0].storage_name;

                        string goods_name = re.data[0].details[i].goods_name;
                        string index = re.data[0].details[i].index;
                        string nums = re.data[0].details[i].nums;
                        string price = re.data[0].details[i].price;
                        string DETAILremark = re.data[0].details[i].remark;
                        string spec_code = re.data[0].details[i].spec_code;
                        string spec_name = re.data[0].details[i].spec_name;
                        string unit = re.data[0].details[i].unit;

                        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                        scsb.DataSource = "172.16.11.9";
                        scsb.InitialCatalog = "FumaCRM8";
                        scsb.UserID = "sa";
                        scsb.Password = "abc_123";
                        //创建连接 参数为连接字符串
                        SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                        sqlConn.Open();
                        string sqlStr = " INSERT INTO FumaCRM8.dbo.WLN_WHOUT (bill_date,create_time,logistic_name,modified_time,operate_name," +
                            "remark,stock_code,storage_code,storage_name,goods_name,indexs,DETAILremark," +
                            "spec_code,unit) VALUES ('" + bill_date + "','" + create_time + "','" + logistic_name +
                            "','" + modified_time + "','" + operate_name + "','" + remark + "','" + stock_code + "','" + storage_code + "','"
                            + storage_name + "','" + goods_name + "','" + index + "','"
                            + DETAILremark + "','" + spec_code + "','" + unit + "') ";
                        SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                        int result = comm.ExecuteNonQuery();
                        sqlConn.Close();//关闭连接

                    }
                }
                else
                {
                    //记录错误信息

                }

            }
            catch (Exception ex)
            {

            }


        }
        //时间戳转日期
        public static DateTime StampToDatetime(long TimeStamp, bool isMinSeconds = true)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));//当地时区
            //返回转换后的日期
            if (isMinSeconds)
                return startTime.AddMilliseconds(TimeStamp);
            else
                return startTime.AddSeconds(TimeStamp);
        }

        private void Button7_Click_1(object sender, EventArgs e)
        {
            try
            {
                //string secret = "c3b5fee170b52b8397852c8ba03ef109";
                //string key = "3123415742";
                string secret = "5cf249cebe8c45eb6254b55e11f9e944";
                string key = "3723429465";
                //前一天的晚上10点
                stockbill_query sq = new stockbill_query();
                DateTime dt = DateTime.Now.AddDays(-1);
                string ti = dt.ToString("yyyy-MM-dd") + " 22:00:00";
                DateTime dti = Convert.ToDateTime(ti);
                string mt = times(dti);
                //string mt = times(DateTime.Now);
                sq.modify_time = mt;
                sq.page = "1";
                sq.limit = "100";
                string json = new JavaScriptSerializer().Serialize(sq);
                string urlcode = UrlEncode(json);
                string t = time();
                //t = "1558952035";
                string post = secret + "_app=" + key + "&_s=&_t=" + t + "&limit=" + sq.limit + "&modify_time=" + sq.modify_time + "&page=" + sq.page + secret;
                string m = EncryptString(post);
                var client = new RestClient("https://open-api.hupun.com/api/erp/stock/out/stockbill/query");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("_app", key);
                request.AddParameter("_s", "");
                request.AddParameter("_sign", m);
                request.AddParameter("_t", t);
                request.AddParameter("limit", sq.limit);
                request.AddParameter("modify_time", sq.modify_time);
                request.AddParameter("page", sq.page);

                IRestResponse response = client.Execute(request);
                string str = response.Content;
                out_query re = new JavaScriptSerializer().Deserialize<out_query>(str);
                string mess = re.message;
                int code = re.code;
                if (code == 0)
                {
                    int num = re.data[0].details.Count;
                    for (int i = 0; i < num; i++)
                    {
                        string stock_code = re.data[0].stock_code;
                        string bd = re.data[0].bill_date;
                        long b = Convert.ToInt64(bd);
                        DateTime bill_date1 = StampToDatetime(b);
                        string bill_date = Convert.ToString(bill_date1);
                        string bill_type = re.data[0].bill_type;
                        string country = re.data[0].country;
                        string ct = re.data[0].create_time;
                        long c = Convert.ToInt64(ct);
                        DateTime create_time = StampToDatetime(c);
                        //string create_time = re.data[0].create_time;
                        string custom_code = re.data[0].custom_code;
                        string custom_name = re.data[0].custom_name;
                        string customer_nick = re.data[0].customer_nick;
                        string customer_nick_type = re.data[0].customer_nick_type;
                        string customer_nick_type_name = re.data[0].customer_nick_type_name;
                        string discount_fee = re.data[0].discount_fee;
                        string from_trade_no = re.data[0].from_trade_no;
                        string inv_no = re.data[0].inv_no;
                        string paid_fee = re.data[0].paid_fee;
                        string pay_type = re.data[0].pay_type;
                        string post_fee = re.data[0].post_fee;
                        string remark = re.data[0].remark;
                        string sale_man = re.data[0].sale_man;
                        string service_fee = re.data[0].service_fee;
                        string shop_name = re.data[0].shop_name;
                        string shop_nick = re.data[0].shop_nick;
                        string shop_source = re.data[0].shop_source;
                        string storage_code = re.data[0].storage_code;
                        string storage_name = re.data[0].storage_name;
                        string sum_sale = re.data[0].sum_sale;
                        string tp_tid = re.data[0].tp_tid;

                        string detail_id = re.data[0].details[i].detail_id;
                        string discount_fee1 = re.data[0].details[i].discount_fee;
                        string goods_name = re.data[0].details[i].goods_name;
                        string nums = re.data[0].details[i].nums;
                        string sku_name = re.data[0].details[i].sku_name;
                        string sku_no = re.data[0].details[i].sku_no;
                        string sku_prop1 = re.data[0].details[i].sku_prop1;
                        string sku_prop2 = re.data[0].details[i].sku_prop2;
                        string sum_cost = re.data[0].details[i].sum_cost;
                        string detail_sum_sale = re.data[0].details[i].sum_sale;
                        string unit = re.data[0].details[i].unit;
                        string indexs = re.data[0].details[i].index;
                        string spec_code = re.data[0].details[i].spec_code;

                        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                        scsb.DataSource = "172.16.11.9";
                        scsb.InitialCatalog = "FumaCRM8";
                        scsb.UserID = "sa";
                        scsb.Password = "abc_123";
                        //创建连接 参数为连接字符串
                        SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                        sqlConn.Open();
                        string sqlStr = " INSERT INTO FumaCRM8.dbo.WLN_SALESWHOUT (bill_date,bill_type,create_time,custom_code,custom_name,customer_nick," +
                            "customer_nick_type,customer_nick_type_name,discount_fee,paid_fee,post_fee,pay_type,from_trade_no,inv_no,remark,sale_man," +
                            "service_fee,shop_name,shop_nick,shop_source,storage_code,storage_name,sum_sale,tp_tid,detail_id,goods_name,nums,sku_name," +
                            "sku_no,sku_prop1,sku_prop2,sum_cost,detail_sum_sale,unit,indexs,codedate,spec_code,stock_code) " +
                            "VALUES ('" + bill_date + "','" + bill_type + "','" + create_time + "','" + custom_code + "','" + custom_name + "','" + customer_nick + "'," +
                            "'" + customer_nick_type + "','" + customer_nick_type_name + "','" + discount_fee + "','" + paid_fee + "','" + post_fee + "','" + pay_type + "'," +
                            "'" + from_trade_no + "','" + inv_no + "','" + remark + "','" + sale_man + "','" + service_fee + "','" + shop_name + "','" + shop_nick + "'," +
                            "'" + shop_source + "','" + storage_code + "','" + storage_name + "','" + sum_sale + "','" + tp_tid + "','" + detail_id + "','" + goods_name + "'," +
                            "'" + nums + "','" + sku_name + "','" + sku_no + "','" + sku_prop1 + "','" + sku_prop2 + "','" + sum_cost + "','" + detail_sum_sale + "'," +
                            "'" + unit + "','" + indexs + "','" + DateTime.Now + "','" + spec_code + "','" + stock_code + "')";
                        SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                        int result = comm.ExecuteNonQuery();
                        sqlConn.Close();//关闭连接

                    }
                }
                else
                {
                    //记录错误信息

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //获取人员信息存入数据库
        private void Button8_Click_1(object sender, EventArgs e)
        {
            try
            {
                //删除管理员
                //bool bIsConnected = axCZKEM1.Connect_Net("192.168.24.11", 4370);
                //bool ac =axCZKEM1.ClearAdministrators(3);


                bool bIsConnected = axCZKEM1.Connect_Net("192.168.31.77", 4370);
                axCZKEM1.EnableDevice(77, false);

                //获取人员信息
                axCZKEM1.ReadAllUserID(77);
                string sEnrollNumber = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;
                while (axCZKEM1.SSR_GetAllUserInfo(77, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
                {
                    int Flag = 1;
                    string TmpData = "";
                    int TmpLength = 0;
                    bool getUser = axCZKEM1.GetUserTmpExStr(77, sEnrollNumber, 6, out Flag, out TmpData, out TmpLength);
                    //先查询获取人员姓名
                    if (getUser == true)
                    {
                        OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                        string sql = " select * from HRMRESOURCE where workcode='" + sEnrollNumber + "' ";
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        string name = "";
                        int n = dt.Rows.Count;
                        if (n > 0)
                        {
                            name = dt.Rows[0]["LASTNAME"].ToString();
                        }
                        //先判断是否存在,不存在就添加,存在就修改

                        sql = " select * from kq_name where workcode='" + sEnrollNumber + "'";
                        cmd = new OracleCommand(sql, conn);
                        da = new OracleDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        var num = dt.Rows.Count;
                        int enable = 0;
                        if (bEnabled == true)
                        {
                            enable = 1;
                        }
                        if (num == 0)//添加
                        {
                            sql = " INSERT INTO kq_name(NAME,TmpData, workcode, Flag ,TmpLength,Privilege, Enabled) VALUES" +
                                "('" + name + "', '" + TmpData + "', '" + sEnrollNumber + "', '" + Flag + "', '" + TmpLength + "', '" + iPrivilege + "','" + enable + "') ";
                            cmd = new OracleCommand(sql, conn);
                            int result = cmd.ExecuteNonQuery();
                        }
                        else//修改
                        {

                        }


                        ToolHelper.CloseSql(conn);


                    }
                }


                axCZKEM1.EnableDevice(77, true);
                axCZKEM1.Disconnect();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //发送人员信息到考勤机
        private void Button9_Click_1(object sender, EventArgs e)
        {
            //发送人员信息到考勤机SetUserTmp ExStr

            //先建立人员信息,后传输指纹模板SSR_SetUserInfo

            try
            {
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                string sql = " select * from KQ_NAME ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ToolHelper.CloseSql(conn);
                int n = dt.Rows.Count;
                if (n > 0)
                {
                    bool bIsConnected = axCZKEM1.Connect_Net("192.168.31.77", 4370);
                    bool d = axCZKEM1.DeleteUserInfoEx(77,10525);
                    for (int i = 0; i < n; i++)
                    {
                        string workcode = dt.Rows[i]["workcode"].ToString();
                        string name = dt.Rows[i]["name"].ToString();
                        string password = dt.Rows[i]["password"].ToString();
                        int privilege = Convert.ToInt32(dt.Rows[i]["privilege"].ToString());
                        int flag = Convert.ToInt32(dt.Rows[i]["flag"].ToString());
                        string tmpdata = dt.Rows[i]["tmpdata"].ToString();
                        bool a = axCZKEM1.SSR_SetUserInfo(77, workcode, name, password, privilege, true);
                        bool b = axCZKEM1.SetUserTmpExStr(77, workcode, 6, flag, tmpdata);

                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public class intA
        {
            public int aa { get; set; }
        }
        //退货单查询
        private void Button10_Click_1(object sender, EventArgs e)
        {
            List<intA> a = new List<intA>();
            for (int i = 0; i < 10; i++)
            {
                intA ia = new intA();
                ia.aa = i;
                a.Add(ia);
            }
            //string secret = "c3b5fee170b52b8397852c8ba03ef109";
            //string key = "3123415742";
            string secret = "5cf249cebe8c45eb6254b55e11f9e944";
            string key = "3723429465";
            try
            {
                DateTime dt = DateTime.Now.AddDays(-7);
                string ti = dt.ToString("yyyy-MM-dd") + " 22:00:00";
                DateTime dti = Convert.ToDateTime(ti);
                string mt = times(dti);
                string t = time();
                string post = secret + "_app=" + key + "&_s=&_t=" + t +"&limit=" + 200 + "&modify_time=" + mt + "&page=" + 1 + secret;
                string m = EncryptString(post);
                var client = new RestClient("https://open-api.hupun.com/api/erp/sale/stock/in/query");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("_app", key);
                request.AddParameter("_s", "");
                request.AddParameter("_sign", m);
                request.AddParameter("_t", t);
                request.AddParameter("limit", 200);
                request.AddParameter("modify_time", mt);
                request.AddParameter("page", 1);
                IRestResponse response = client.Execute(request);
                string str = response.Content;
                out_query re = new JavaScriptSerializer().Deserialize<out_query>(str);
                string mess = re.message;
                int code = re.code;
                if (code == 0)
                {
                    int num = re.data[0].details.Count;
                    for (int i = 0; i < num; i++)
                    {
                        string stock_code = re.data[0].stock_code;
                        string bd = re.data[0].bill_date;
                        long b = Convert.ToInt64(bd);
                        DateTime bill_date1 = StampToDatetime(b);
                        string bill_date = Convert.ToString(bill_date1);
                        string bill_type = re.data[0].bill_type;
                        string country = re.data[0].country;
                        string ct = re.data[0].create_time;
                        long c = Convert.ToInt64(ct);
                        DateTime create_time = StampToDatetime(c);
                        //string create_time = re.data[0].create_time;
                        string custom_code = re.data[0].custom_code;
                        string custom_name = re.data[0].custom_name;
                        string customer_nick = re.data[0].customer_nick;
                        string customer_nick_type = re.data[0].customer_nick_type;
                        string customer_nick_type_name = re.data[0].customer_nick_type_name;
                        string discount_fee = re.data[0].discount_fee;
                        string from_trade_no = re.data[0].from_trade_no;
                        string inv_no = re.data[0].inv_no;
                        string paid_fee = re.data[0].paid_fee;
                        string pay_type = re.data[0].pay_type;
                        string post_fee = re.data[0].post_fee;
                        string remark = re.data[0].remark;
                        string sale_man = re.data[0].sale_man;
                        string service_fee = re.data[0].service_fee;
                        string shop_name = re.data[0].shop_name;
                        string shop_nick = re.data[0].shop_nick;
                        string shop_source = re.data[0].shop_source;
                        string storage_code = re.data[0].storage_code;
                        string storage_name = re.data[0].storage_name;
                        string sum_sale = re.data[0].sum_sale;
                        string tp_tid = re.data[0].tp_tid;
                        string indexs = re.data[0].from_exchange_no;

                        string detail_id = re.data[0].details[i].detail_id;
                        string discount_fee1 = re.data[0].details[i].discount_fee;
                        string goods_name = re.data[0].details[i].goods_name;
                        string nums = re.data[0].details[i].nums;
                        string sku_name = re.data[0].details[i].sku_name;
                        string sku_no = re.data[0].details[i].sku_no;
                        string sku_prop1 = re.data[0].details[i].sku_prop1;
                        string sku_prop2 = re.data[0].details[i].sku_prop2;
                        string sum_cost = re.data[0].details[i].sum_cost;
                        string detail_sum_sale = re.data[0].details[i].sum_sale;
                        string unit = re.data[0].details[i].unit;
                        string spec_code = re.data[0].details[i].spec_code;

                        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                        scsb.DataSource = "172.16.11.9";
                        scsb.InitialCatalog = "FumaCRM8";
                        scsb.UserID = "sa";
                        scsb.Password = "abc_123";
                        //创建连接 参数为连接字符串
                        SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                        sqlConn.Open();
                        string sqlStr = " INSERT INTO FumaCRM8.dbo.WLN_WHRG (bill_date,bill_type,create_time,custom_code,custom_name,customer_nick," +
                            "customer_nick_type,customer_nick_type_name,discount_fee,paid_fee,post_fee,pay_type,from_trade_no,inv_no,remark,sale_man," +
                            "service_fee,shop_name,shop_nick,shop_source,storage_code,storage_name,sum_sale,tp_tid,detail_id,goods_name,nums,sku_name," +
                            "sku_no,sku_prop1,sku_prop2,sum_cost,detail_sum_sale,unit,indexs,codedate,spec_code,stock_code) " +
                            "VALUES ('" + bill_date + "','" + bill_type + "','" + create_time + "','" + custom_code + "','" + custom_name + "','" + customer_nick + "'," +
                            "'" + customer_nick_type + "','" + customer_nick_type_name + "','" + discount_fee + "','" + paid_fee + "','" + post_fee + "','" + pay_type + "'," +
                            "'" + from_trade_no + "','" + inv_no + "','" + remark + "','" + sale_man + "','" + service_fee + "','" + shop_name + "','" + shop_nick + "'," +
                            "'" + shop_source + "','" + storage_code + "','" + storage_name + "','" + sum_sale + "','" + tp_tid + "','" + detail_id + "','" + goods_name + "'," +
                            "'" + nums + "','" + sku_name + "','" + sku_no + "','" + sku_prop1 + "','" + sku_prop2 + "','" + sum_cost + "','" + detail_sum_sale + "'," +
                            "'" + unit + "','" + indexs + "','" + DateTime.Now + "','" + spec_code + "','" + stock_code + "')";
                        SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                        int result = comm.ExecuteNonQuery();
                        sqlConn.Close();//关闭连接

                    }
                }





                ////构造连接字符串
                //SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                //scsb.DataSource = "172.16.11.9";
                //scsb.InitialCatalog = "FumaCRM8";
                //scsb.UserID = "sa";
                //scsb.Password = "abc_123";
                ////创建连接 参数为连接字符串
                //SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                //sqlConn.Open();
                //string sqlStr = " SELECT  * FROM FumaCRM8.dbo.WLN_ITEM where CHANGED='0' and bar_code='6058-B' ";
                //DataTable dt = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter(sqlStr, sqlConn);//从数据库中查询
                //da.Fill(dt);//将数据填充到DataSet
                //sqlConn.Close();//关闭连接
                //int num = dt.Rows.Count;
                //if (num > 0)
                //{
                //    for (int i = 0; i < num; i++)
                //    {
                //        string article_number = dt.Rows[i]["article_number"].ToString();
                //        string bar_code = dt.Rows[i]["bar_code"].ToString();
                //        string item_name = dt.Rows[i]["item_name"].ToString();
                //        string sale_price = dt.Rows[i]["sale_price"].ToString();
                //        string weight = dt.Rows[i]["weight"].ToString();
                //        string unit = dt.Rows[i]["unit"].ToString();
                //        string item_pic = dt.Rows[i]["item_pic"].ToString();

                //        add_item add = new add_item();
                //        add.article_number = article_number;
                //        add.bar_code = bar_code;
                //        add.item_name = item_name;
                //        add.sale_price = sale_price;
                //        add.weight = weight;
                //        add.unit = unit;
                //        add.item_code = article_number;
                //        //add.item_pic=
                //        string json = new JavaScriptSerializer().Serialize(add);
                //        string urlcode = UrlEncode(json);
                //        string t = time();
                //        string post = secret + "_app=" + key + "&_s=&_t=" + t + "&item=" + urlcode + secret;
                //        string m = EncryptString(post);
                //        var client = new RestClient("https://open-api.hupun.com/api/erp/sale/stock/in/query");

                //        client.Timeout = -1;
                //        var request = new RestRequest(Method.POST);
                //        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                //        request.AddParameter("_app", key);
                //        request.AddParameter("_s", "");
                //        request.AddParameter("_sign", m);
                //        request.AddParameter("_t", t);
                //        request.AddParameter("item", json);
                //        IRestResponse response = client.Execute(request);
                //        string str = response.Content;
                //        returnInfo re = new JavaScriptSerializer().Deserialize<returnInfo>(str);
                //        if (re.code == 0)
                //        {
                //            //打开连接
                //            sqlConn.Open();
                //            sqlStr = " update FumaCRM8.dbo.WLN_ITEM  set code='1',message='" + re.message + "',codedate='" + DateTime.Now + "'  where article_number='" + article_number + "' ";
                //            SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                //            int result = comm.ExecuteNonQuery();
                //            sqlConn.Close();//关闭连接
                //        }
                //        else
                //        {
                //            //打开连接
                //            sqlConn.Open();
                //            sqlStr = " update FumaCRM8.dbo.WLN_ITEM  set code='2',message='" + re.message + "',codedate='" + DateTime.Now + "'  where article_number='" + article_number + "' ";
                //            SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                //            int result = comm.ExecuteNonQuery();
                //            sqlConn.Close();//关闭连接
                //        }

                //    }
                //}
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000);
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            try
            {
                int machinenumber = 77;
                bool bIsConnected = axCZKEM1.Connect_Net("192.168.31.77", 4370);
                axCZKEM1.EnableDevice(77, false);

                axCZKEM1.ReadGeneralLogData(77);

                string dwEnrollNumber = "";
                int dwVerifyMode = 0;
                int dwInOutMode = 0;
                int dwYear = 0;
                int dwMonth = 0;
                int dwDay = 0;
                int dwHour = 0;
                int dwMinute = 0;
                int dwSecond = 0;
                int dwWorkcode = 0;
                while (axCZKEM1.SSR_GetGeneralLogData(77, out dwEnrollNumber, out dwVerifyMode, out dwInOutMode, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkcode))
                {
                    //sql = " update OA_FT_POS_POA set FLC_STATA='1' where id='" + dt1.Rows[j]["ID"].ToString() + "' ";
                    //yyyy-mm-dd hh24:mi:ss
                    string date = Convert.ToString(dwYear) + "-" + Convert.ToString(dwMonth) + "-" + Convert.ToString(dwDay) + " " + Convert.ToString(dwHour) + ":" + Convert.ToString(dwMinute) + ":" + Convert.ToString(dwSecond);
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    string sql = " INSERT INTO KQ_RECORD (OANO,MACHINENUMBER,VERIFYMODE,RECORDDATE) VALUES('" + dwEnrollNumber + "','" + machinenumber + "','" + dwVerifyMode + "',to_date('" + date + "','yyyy-mm-dd hh24:mi:ss')) ";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();
                    //OracleDataAdapter da = new OracleDataAdapter(cmd);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);
                    ToolHelper.CloseSql(conn);
                }

                //清楚机器内所有的考勤记录
                axCZKEM1.ClearGLog(77);

                axCZKEM1.EnableDevice(77, true);
                axCZKEM1.Disconnect();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
