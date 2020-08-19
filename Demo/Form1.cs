using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
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
                string Path = "D:\\Dev\\1590414462.77855.xlsx";
                string strConn = "Provider=Microsoft.ACE.OLEDB.16.0;" + "Data Source=" + Path + ";" + "Extended Properties=\"Excel 8.0;HDR=NO;\"";
                //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";

                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();

                string tableName = "DATA$";
                System.Data.DataTable dtExcel = new System.Data.DataTable();
                DataSet ds = new DataSet();
                string strSql = "select * from [" + tableName + "]";
                //获取Excel指定Sheet表中的信息
                OleDbCommand objCmd = new OleDbCommand(strSql, conn);
                OleDbDataAdapter myData = new OleDbDataAdapter(strSql, conn);
                myData.Fill(ds, tableName);//填充数据
                conn.Close();
                //dtExcel即为excel文件中指定表中存储的信息
                dtExcel = ds.Tables[tableName];
                string a = "";
                int r = dtExcel.Rows.Count;//行
                int c = dtExcel.Columns.Count;//列
                DateTime nowTime = DateTime.Now;
                string time = nowTime.ToString("HH:mm:ss");
                DateTime times = Convert.ToDateTime(time).AddHours(-2);
                int rows = 0;
                for (int row = 7; row <= dtExcel.Rows.Count - 1; row++)
                {
                    //nowTime=Convert.ToDateTime(dtExcel.Rows[row][1].ToString());
                    DateTime exTime = Convert.ToDateTime(dtExcel.Rows[row][0].ToString() + " " + dtExcel.Rows[row][1].ToString());
                    if (nowTime < times)
                    {
                        rows = row;
                        break;
                    }
                }
                rows = 7;//需注释
                if (rows != 0)
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    DataColumn dc = null;
                    dc = dt.Columns.Add("ID", Type.GetType("System.Int32"));
                    dc = dt.Columns.Add("DateTime", Type.GetType("System.String"));
                    dc = dt.Columns.Add("Samplet", Type.GetType("System.String"));
                    dc = dt.Columns.Add("Vol", Type.GetType("System.String"));
                    dc = dt.Columns.Add("Unnits", Type.GetType("System.String"));
                    dc = dt.Columns.Add("um3", Type.GetType("System.Int32"));
                    dc = dt.Columns.Add("um5", Type.GetType("System.Int32"));
                    dc = dt.Columns.Add("um10", Type.GetType("System.Int32"));
                    dc = dt.Columns.Add("um20", Type.GetType("System.Int32"));
                    dc = dt.Columns.Add("um50", Type.GetType("System.Int32"));
                    dc = dt.Columns.Add("um100", Type.GetType("System.Int32"));
                    dc = dt.Columns.Add("Scale", Type.GetType("System.String"));
                    dc = dt.Columns.Add("Location", Type.GetType("System.String"));
                    //dc = dt.Columns.Add("um", Type.GetType("System.Int32"));
                    DataRow newRow;
                    for (int i = rows; i < dtExcel.Rows.Count ; i++)
                    {
                        newRow = dt.NewRow();
                        newRow["DateTime"] = rows;
                        newRow["DateTime"] = dtExcel.Rows[i][0].ToString() + " " + dtExcel.Rows[i][1].ToString();
                        newRow["Samplet"] = dtExcel.Rows[i][2].ToString();
                        newRow["Vol"] = dtExcel.Rows[i][3].ToString();
                        newRow["Unnits"] = dtExcel.Rows[i][4].ToString();
                        newRow["um3"] = Convert.ToInt32(dtExcel.Rows[i][5].ToString());
                        newRow["um5"] = Convert.ToInt32(dtExcel.Rows[i][6].ToString());
                        newRow["um10"] = Convert.ToInt32(dtExcel.Rows[i][7].ToString());
                        newRow["um20"] = Convert.ToInt32(dtExcel.Rows[i][8].ToString());
                        newRow["um50"] = Convert.ToInt32(dtExcel.Rows[i][9].ToString());
                        newRow["um100"] = Convert.ToInt32(dtExcel.Rows[i][10].ToString());
                        newRow["Scale"] = dtExcel.Rows[i][11].ToString();
                        newRow["Location"] = dtExcel.Rows[i][12].ToString();
                        //newRow["um"] = Convert.ToInt32(dtExcel.Rows[i][5].ToString());
                        dt.Rows.Add(newRow);
                    }
                    //取最大值
                    int max = (int)dt.Select("", "um3 DESC")[0]["um3"];
                    int min = (int)dt.Select("", "um3 ASC")[0]["um3"];
                    //int num=min/max
                    if (min / max > 0.33)
                    {
                        string message = "请重新采集大气本底数据!";
                    }
                    int max80 = Convert.ToInt32(max * 0.8);
                    DataRow[] drArr = dt.Select("um3>"+max80 +" and um3<"+max, "DateTime DESC");
                    //判断值附近是否存在连续测试数据，<+-5%属于连续测试，检查数据量
                    int fm = 0;
                    string aa = "";
                    if (drArr.Length ==0)
                    {
                        fm = max;
                    }else if (drArr.Length==1)
                    {
                        fm = Convert.ToInt32(drArr[0]["um3"].ToString());
                    }
                    else if (drArr.Length==2)
                    {
                        int fm1= Convert.ToInt32(drArr[0]["um3"].ToString());
                        int fm2= Convert.ToInt32(drArr[1]["um3"].ToString());
                        fm = (fm1 + fm2) / 2;
                    }
                    else if (drArr.Length == 3)
                    {
                        fm= Convert.ToInt32(drArr[1]["um3"].ToString());
                    }
                    else
                    {
                        int fm3 = 0;
                        for (int i = 0; i < drArr.Length; i++)
                        {
                            if (i!=0 || i!= drArr.Length-1)
                            {
                                fm3+= Convert.ToInt32(drArr[0]["um3"].ToString());
                            }
                            fm = fm3 / (drArr.Length - 2);
                        }
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
