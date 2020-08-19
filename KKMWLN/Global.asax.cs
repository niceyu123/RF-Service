using KKMWLN.MyPublic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace KKMWLN
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {   //出库 stockbill/query
                //Thread Stockbill_query = new Thread(new ThreadStart(new CommonHelper().Stockbill_query));
                //Stockbill_query.IsBackground = true;
                //Stockbill_query.Start();


                //新建商品
                Thread Additem = new Thread(new ThreadStart(new CommonHelper().Additem));
                Additem.IsBackground = true;
                Additem.Start();
                GC.KeepAlive(Additem);
                //入库requestbill / add
                Thread Requestbill_add = new Thread(new ThreadStart(new CommonHelper().Requestbill_add));
                Requestbill_add.IsBackground = true;
                Requestbill_add.Start();
                GC.KeepAlive(Requestbill_add);
                //出库
                Thread Stockbill_query = new Thread(new ThreadStart(new CommonHelper().Stockbill_query));
                Stockbill_query.IsBackground = true;
                Stockbill_query.Start();
                GC.KeepAlive(Stockbill_query);
                //退货
                Thread Stock_in_query = new Thread(new ThreadStart(new CommonHelper().Stock_in_query));
                Stock_in_query.IsBackground = true;
                Stock_in_query.Start();
                GC.KeepAlive(Stock_in_query);

            }
            catch (Exception a)
            {
                ToolHelper.logger.Debug(a.ToString());
            }
        }

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