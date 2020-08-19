using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OAWEB
{
    public partial class download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = ToolHelper.GetPostOrGetPar(HttpContext.Current.Request.QueryString["name"], "");
            if (name != string.Empty)
            {
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", name));
                Response.ContentType = "application/octet-stream";
                //using (FileStream fileStream = new FileStream(Server.MapPath("/files/sys_files/ReadCard.exe"), FileMode.Open))
                using (FileStream fileStream = new FileStream("d:\\oadownload\\"+name, FileMode.Open))
                {
                    byte[] tempByte = new byte[fileStream.Length];
                    fileStream.Read(tempByte, 0, tempByte.Length);
                    Response.BinaryWrite(tempByte);
                    Response.Flush();
                }
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                return;
            }
            

        }
    }
}