using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SystemBLL;

namespace OAWEB
{
    public partial class SelectKQList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DataTable table1 = BLL.Login(this.TextBox1.Text, this.TextBox2.Text);
            if (table1.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('登录成功！');</script>");
                Response.Redirect("test.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('登录失败！');</script>");
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            this.TextBox1.Text = null;
            this.TextBox2.Text = null;
        }
    }

}
