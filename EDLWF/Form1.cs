using EDLWF.MyPublic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace EDLWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SolEdiProxyWebService.SolEdiProxyWebService sw = new SolEdiProxyWebService.SolEdiProxyWebService();
            SolEdiProxyWebService.ShipmentApplyInfo sh = new SolEdiProxyWebService.ShipmentApplyInfo();
            sh.corpSerialNo = "123456";
            sh.clientNo = "123";
            sh.policyNo = "CH000269-040400";
            sh.buyerNo = "USA/089971";
            sh.invoiceNo = "2014QAF211";
            sh.insureSum = 100;
            sh.moneyId = "USD";
            sh.payTerm = 30;
            sh.payMode = "4";
            sh.feePayMode = "4";
            sh.transportDate = DateTime.Now.AddDays(5);
            sh.code10 = "9404909000";
            SolEdiProxyWebService.ShipmentApplyInfo[] aaa = { sh };

            sw.doEdiShipmentApply(aaa);

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //post请求
            string postUrl = "http://172.16.11.19:8065/ediserver/gateway.do";
            DateTime time = DateTime.Now.AddDays(5);
            string json = "{\"datas\":[{\"buyerEngName\":\"\",\"buyerFax\":\"\",\"item2\":\"\",\"buyerTel\":\"\",\"item1\":\"\",\"contractNo\":\"\",\"corpSerialNo\":\"123458\",\"clientNo\":\"20000592\",\"policyNo\":\"SCH047100-202000\",\"remark\":\"\",\"lcno\":\"\",\"buyerChnName\":\"\",\"bankEngName\":\"\",\"moneyId\":\"USD\",\"transportBillNo\":\"\",\"bankNo\":\"\",\"customsBillNo\":\"\",\"payerName\":\"\",\"corpBankNo\":\"\",\"feePayMode\":\"4\",\"bankCountryName\":\"\",\"invoiceNo\":\"2014QAF213\",\"applyTime\":\"\",\"goodsName\":\"\",\"buyerRegNo\":\"\",\"employeeName\":\"\",\"orderNo\":\"\",\"payTerm\":\"30\",\"payMode\":\"4\",\"transportDate\":\"1614211200000\",\"ifFinancing\":\"\",\"bankChnName\":\"\",\"bankAddr\":\"\",\"invoiceSum\":\"\",\"corpBuyerNo\":\"\",\"applicantName\":\"\",\"buyerEngAddr\":\"\",\"insureSum\":\"100\",\"buyerChnAddr\":\"\",\"buyerCountryCode\":\"\",\"expectdate\":\"\",\"bankCountryCode\":\"\",\"buyerNo\":\"USA/089971\",\"code10\":\"9404909000\",\"trafficCode\":\"\",\"item4\":\"\",\"item3\":\"\",\"item5\":\"\"}],\"imethod\":\"doEdiShipmentApply\"}";
            string json1 = "{\"datas\":[{\"corpSerialNo\":\"123457\",\"clientNo\":\"20000592\",\"policyNo\":\"CH000269-040400\",\"buyerNo\":\"USA/089971\",\"invoiceNo\":\"2014QAF212\",\"insureSum\":\"100\",\"moneyId\":\"USD\",\"payTerm\":\"30\",\"payMode\":\"4\",\"feePayMode\":\"4\",\"transportDate\":\"" + time + "\",\"code10\":\"9404909000\"}],\"imethod\":\"doEdiShipmentApply\"}";
            string postResult = PostWebRequest(postUrl, json, Encoding.UTF8);
        }
        /// <summary>
        /// Post数据接口
        /// </summary>
        /// <param name="postUrl">接口地址</param>
        /// <param name="paramData">提交json数据</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
                //SendMessageInfo send = new JavaScriptSerializer().Deserialize<SendMessageInfo>(ret);
                //ret = send.errcode;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string postUrl = "http://172.16.11.19:8065/ediserver/gateway.do";
            string json = "{\"datas\":{\"endDate\":\"1614355199000\",\"policyNo\":\"\",\"startDate\":\"1614182400000\"},\"imethod\":\"getEdiShipmentApproveInfo\"}";
            string postResult = PostWebRequest(postUrl, json, Encoding.UTF8);
        }
    }
}
