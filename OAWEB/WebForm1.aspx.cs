using PrintHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OAWEB
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 获取Url中去掉文件名的路径
        /// </summary>
        /// <returns></returns>
        private string GetUrlPath()
        {
            string strUrl = Request.Url.ToString();
            int iEnd = strUrl.LastIndexOf("/");
            strUrl = strUrl.Substring(0, iEnd + 1);

            return strUrl;
        }
        /// <summary>
        /// 打印数据参数：服务器的URL+打印的文件名，转化为Base64编码
        /// </summary>
        protected string strPrintData;
        protected void BtnDepositPreview_Click(object sender, EventArgs e)
        {
            string strPrintFileName = Server.MapPath("./ReportFile/") + "abc.fr3";
            PrintJson pJson = new PrintJson(Server.MapPath("./PrintTemp"), strPrintFileName);
            pJson.CheckRegister("杨赟", "14E482A24108C9F642635C2BD6ACB0A1");  //注册信息
            //构造连接字符串
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "172.16.11.67";
            scsb.InitialCatalog = "SCMDB";
            scsb.UserID = "sa";
            scsb.Password = "Sa123456";
            string strSql = "Select top 1 * From CostomerMeala ";
            //创建连接 参数为连接字符串
            SqlConnection sqlConn = new SqlConnection(scsb.ToString());
            //打开连接
            sqlConn.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(strSql, sqlConn);
            adp.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            sqlConn.Close();
            string strPrintTempFile = pJson.ShowReport(dt); //产生JSON文件内容
            //把服务器的URL + 此文件名 传递给控件，由控件下载还原数据进行打印
            string strServerURL = GetUrlPath() + "PrintTemp/";
            string strData = strServerURL + strPrintTempFile;
            strPrintData = PrintFunction.EnBase64(strData);
        }

    }
}