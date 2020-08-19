using OAService.MyEntity;
using OAService.MyPublic;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace OAService
{
    /// <summary>
    /// Download 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://172.16.11.19:1915/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Download : System.Web.Services.WebService
    {

        [WebMethod]
        public string DownloadFile(string data)
        {
            //List<DownloadFileInfo> dd = new List<DownloadFileInfo>();
            //string a=new JavaScriptSerializer().Serialize(dd);
            try
            {
                DownloadFileInfo fileInfo = new JavaScriptSerializer().Deserialize<DownloadFileInfo>(data);
                //string id= fileInfo.id;
                string sql = fileInfo.sql;
                string hz = fileInfo.hz;
                string sjk = fileInfo.sjk;
                if (sql != string.Empty  && sjk != string.Empty)
                {

                    string type = "";
                    if (sjk == "RAVO1")
                    {
                        type = "21";
                    }
                    else if (sjk == "RAVO2")
                    {
                        type = "23";
                    }
                    else if (sjk == "RAVO3")
                    {
                        type = "141";
                    }
                    else if (sjk == "RAVO5")
                    {
                        type = "22";
                    }
                    else if (sjk == "RAVO6")
                    {
                        type = "161";
                    }
                    else if (sjk == "RAVO0")
                    {
                        type = "24";
                    }
                    else
                    {
                        type = "23";
                    }
                    string sql2 = sql + "order by id";
                    OracleConnection conn = ToolHelper.OpenRavoerp(type);
                    OracleCommand cmd = new OracleCommand(sql2, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //ToolHelper.CloseSql(conn);
                    int num = dt.Rows.Count;
                    string name = "";
                    byte[] dad = null;
                    string url = "";
                    Stream stream = null;
                    byte[] bytes = null;
                    FileStream fs = null;
                    BinaryWriter bw = null;
                    OracleDataReader reader = null;
                    string id = "";
                    string sql1 = "";
                    for (int i = 0; i < num; i++)
                    {
                        name = dt.Rows[i]["filename"].ToString();
                        string fileSuf = name.Substring(name.LastIndexOf(".") + 1);
                        id = dt.Rows[i]["id"].ToString();
                        sql1 = sql + " and filename='"+name+"'";
                        //conn = ToolHelper.OpenRavoerp(type);
                        cmd = conn.CreateCommand();
                        cmd.CommandText = sql1;//查询获得图片流  
                        reader = cmd.ExecuteReader();//创建一个OracleDateReader对象   
                        reader.Read();
                        stream = new MemoryStream((byte[])reader["DATA"]);
                        //a = dt.Rows[i]["DATA"].ToString();
                        //dad = System.Text.Encoding.Default.GetBytes(a);
                        //stream = new MemoryStream(dad);
                        // 把 Stream 转换成 byte[]
                        bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, bytes.Length);
                        // 设置当前流的位置为流的开始
                        stream.Seek(0, SeekOrigin.Begin);
                        // 把 byte[] 写入文件
                        fs = new FileStream("d:\\oadownload\\" + id +sjk+ "." + fileSuf, FileMode.Create);
                        bw = new BinaryWriter(fs);
                        bw.Write(bytes);
                        bw.Close();
                        fs.Close();
                        //ToolHelper.CloseSql(conn);
                        url = url+"http://172.16.11.19:1916/download.aspx?name=" + id+sjk+"."+ fileSuf + "||";
                    }
                    ToolHelper.CloseSql(conn);
                    url = url.Substring(0, url.Length - 2);

                    //OracleCommand cmd = conn.CreateCommand();
                    //cmd.CommandText = sql;//查询获得图片流  
                    //OracleDataReader reader = cmd.ExecuteReader();//创建一个OracleDateReader对象   
                    //reader.Read();
                    //Stream stream = new MemoryStream((byte[])reader["LS_DATA"]);
                    //string name = Guid.NewGuid().ToString();
                    //// 把 Stream 转换成 byte[]
                    //byte[] bytes = new byte[stream.Length];
                    //stream.Read(bytes, 0, bytes.Length);
                    //// 设置当前流的位置为流的开始
                    //stream.Seek(0, SeekOrigin.Begin);
                    //// 把 byte[] 写入文件
                    //FileStream fs = new FileStream("d:\\oadownload\\" + name + hz, FileMode.Create);
                    //BinaryWriter bw = new BinaryWriter(fs);
                    //bw.Write(bytes);
                    //bw.Close();
                    //fs.Close();
                    //string url = "http://172.16.11.19:1916/download.aspx?name=" + name + hz;
                    ToolHelper.logger.Debug(url);
                    return url;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
