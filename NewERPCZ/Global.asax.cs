﻿using NewERPCZ.MyPublic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace NewERPCZ
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ////单笔
            //Thread SelectFK = new Thread(new ThreadStart(new CommonHelper().SelectFK));
            //SelectFK.IsBackground = true;
            //SelectFK.Start();
            //GC.KeepAlive(SelectFK);

            Thread SelectDownloadNo = new Thread(new ThreadStart(new CommonHelper().SelectDownloadNo));
            SelectDownloadNo.IsBackground = true;
            SelectDownloadNo.Start();
            GC.KeepAlive(SelectDownloadNo);

            //Thread SelectDownloadUrl = new Thread(new ThreadStart(new CommonHelper().SelectDownloadUrl));
            //SelectDownloadUrl.IsBackground = true;
            //SelectDownloadUrl.Start();
            //GC.KeepAlive(SelectDownloadUrl);

            //Thread SelectDownloadUrl1 = new Thread(new ThreadStart(new CommonHelper().SelectDownloadUrl1));
            //SelectDownloadUrl1.IsBackground = true;
            //SelectDownloadUrl1.Start();
            //GC.KeepAlive(SelectDownloadUrl1);

            ////多笔
            //Thread SelectFKS = new Thread(new ThreadStart(new CommonHelper().SelectFKS));
            //SelectFKS.IsBackground = true;
            //SelectFKS.Start();
            //GC.KeepAlive(SelectFKS);

            //Thread SelectDownloadNoS = new Thread(new ThreadStart(new CommonHelper().SelectDownloadNoS));
            //SelectDownloadNoS.IsBackground = true;
            //SelectDownloadNoS.Start();
            //GC.KeepAlive(SelectDownloadNoS);

            //Thread SelectDownloadUrlS = new Thread(new ThreadStart(new CommonHelper().SelectDownloadUrlS));
            //SelectDownloadUrlS.IsBackground = true;
            //SelectDownloadUrlS.Start();
            //GC.KeepAlive(SelectDownloadUrlS);

            //Thread SelectDownloadUrl1S = new Thread(new ThreadStart(new CommonHelper().SelectDownloadUrl1S));
            //SelectDownloadUrl1S.IsBackground = true;
            //SelectDownloadUrl1S.Start();
            //GC.KeepAlive(SelectDownloadUrl1S);
            ////下载地址同步孚盟
            //Thread SvaeURL = new Thread(new ThreadStart(new CommonHelper().SvaeURL));
            //SvaeURL.IsBackground = true;
            //SvaeURL.Start();
            //GC.KeepAlive(SvaeURL);
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