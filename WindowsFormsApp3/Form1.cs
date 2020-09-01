using FastReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // 显示设计界面
            CreateReport(true);
        }

        private void CreateReport(bool tfDesigin)
        {
            // 获得当前程序的运行路径
            string path = Application.StartupPath;
            // 定义报表
            Report report = new Report();
            string strDirectory = path + "\\ReportFiles";

            // 判断文件路径是否存在，不存在则创建文件夹
            if (!Directory.Exists(strDirectory))
            {
                // 不存在就创建目录
                Directory.CreateDirectory(strDirectory);
            }

            // 判断文件是否存在
            if (!File.Exists(strDirectory + "\\产品明细.frx"))
            {
                report.FileName = strDirectory + "\\产品明细.frx";
            }
            else
            {
                report.Load(strDirectory + "\\产品明细.frx");
            }

            // 创建报表文件的数据源
            DataSet ds = new DataSet();
            DataTable dt = GetDataSource();
            DataTable dtSource = dt.Copy();
            dtSource.TableName = "ProductDetail";
            ds.Tables.Add(dtSource);
            report.RegisterData(ds);

            if (tfDesigin)
            {
                // 打开设计界面
                report.Design();
            }
            else
            {
                // 打开预览界面
                report.Show();
            }
        }

        private DataTable GetDataSource()
        {
            DataTable dt = new DataTable();
            // 数据库连接
            //string strCon = @"Initial Catalog=StudentSystem;     Integrated Security=False;User Id=sa;Password=1qaz@WSX;Data Source=127.0.0.1;Failover Partner=127.0.0.1;Application Name=TransForCCT";
            //SqlConnection conn = new SqlConnection(strCon);
            //string strSql = @"SELECT p.ProductId,p.ProductName,p.Price,c.CategoryName FROM ProductDetail p INNER JOIN Category c
            //                  ON p.CategoryId=c.CategoryId";
            //// 使用Dapper获取数据
            //IDataReader reader = conn.ExecuteReader(strSql);
            //dt.Load(reader);
            return dt;
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            // 显示预览界面
            CreateReport(false);
        }

    }
}
