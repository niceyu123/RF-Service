using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using OAService.MyPublic;
using Oracle.ManagedDataAccess.Client;
using Spire.Xls;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string oano = "R0-ZCYCKC2020121470";
                OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                OracleCommand myCommand = conn.CreateCommand();
                string sql = " select B.* from FORMTABLE_MAIN_458 a left join FORMTABLE_MAIN_458_DT1 b on a.ID=b.MAINID where lcbh='" + oano + "' ";
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                int num = dt.Rows.Count;
                if (num > 0)
                {
                    for (int i = 0; i < num; i++)
                    {
                        string cid = dt.Rows[i]["cid"].ToString();
                        string memo = dt.Rows[i]["memo"].ToString();
                        string companyid = dt.Rows[i]["companyid"].ToString();
                        string orderid = dt.Rows[i]["orderid"].ToString();
                        string ITEMNO = dt.Rows[i]["ITEMNO"].ToString();
                        conn = ToolHelper.OpenRavoerp("middle");
                        sql = " update STORAGE_DELAY set memo='" + memo + "' where companyid='" + companyid + "' and orderid='" + orderid + "' and ITEMNO='" + ITEMNO + "' ";
                        cmd = new OracleCommand(sql, conn);
                        int resultt = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }


        private void Button2_Click(object sender, EventArgs e)
        {
            //string OrignFile, NewFile;
            //OrignFile = "D:\\DATA.xls";
            //NewFile = "D:\\321.xlsx";
            //File.Copy(OrignFile, NewFile, true);
            try
            {
                Process.Start(@"D:excel\covxls2xlsx.xlsm");
                ////载入xls文档
                //Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
                //workbook.LoadFromFile("D:\\DATA.xls");
                ////保存为xlsx格式
                //workbook.SaveToFile("D:\\XlsToXlsx1.xlsx", ExcelVersion.Version2013);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public static string GetAddress()
        {
            if (File.Exists(@"C:\DATA.xls"))
            {
                return "C:\\DATA.xls";
            }else if (File.Exists(@"D:\DATA.xls"))
            {
                return "D:\\DATA.xls";
            }
            else if (File.Exists(@"E:\DATA.xls"))
            {
                return "E:\\DATA.xls";
            }else if (File.Exists(@"F:\DATA.xls"))
            {
                return "F:\\DATA.xls";
            }
            else if (File.Exists(@"G:\DATA.xls"))
            {
                return "G:\\DATA.xls";
            }
            else if (File.Exists(@"H:\DATA.xls"))
            {
                return "H:\\DATA.xls";
            }
            else
            {
                return "";
            }
        }
        //查找文件是否存在
        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                string address = GetAddress();
                if (address == "")//查找不到文件
                {

                }
                else
                {
                    if (Directory.Exists(@"D:\Dev") == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(@"D:\Dev");
                    }
                    if (File.Exists(@"D:\Dev\DATA.xls"))//存在就删除
                    {
                        File.Delete(@"D:\Dev\DATA.xls");
                    }
                    File.Copy(address, @"D:\Dev\DATA.xls");
                    //File.Copy(@"D: \excel\covxls2xlsx.xlsm", @"D:\Dev\covxls2xlsx.xlsm");
                    //转换文件
                    Process.Start(@"D:\Dev\covxls2xlsx.xlsm");
                    if (File.Exists(@"D:\Dev\DATA.xlsx"))
                    {
                        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                        string n = Convert.ToString((DateTime.Now - startTime).TotalSeconds);
                        string name = "D:\\Dev\\" + n + ".xlsx";
                        File.Copy(@"D:\Dev\DATA.xlsx", name);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            



        }
    }
}
