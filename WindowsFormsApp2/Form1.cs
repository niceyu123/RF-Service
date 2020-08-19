using com;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// string转byte
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] stringToByte(string data)
        {
            try
            {
                byte[] bt = System.Text.Encoding.UTF8.GetBytes(data);
                return bt;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        private static string byteToSting(byte[] data)
        {
            try
            {
                string str = System.Text.Encoding.UTF8.GetString(data);
                return str;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string encrypt(string data)
        {
            try
            {
                byte[] bt = stringToByte(data);
                byte[] encryptBt = AesCode.encrypt(bt);
                string encryptStr = AesCode.bytesToString(encryptBt);
                return encryptStr;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        //解密
        private static string decrypt(string data)
        {
            try
            {
                string decryptGBKStr = AesCode.decrypt2GBK(data);
                //string decryptUTF8Str = AesCode.decrypt2UTF8(data);
                return decryptGBKStr;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string Json2XML(string str)
        {
            try
            {
                string result = null;
                XmlDocument xml = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(str);
                result = xml.OuterXml;
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                erpPlatform.erpPlatformPortTypeClient erp = new erpPlatform.erpPlatformPortTypeClient();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<body>"
                + "<head>"
                + "<erpSysCode>ERP001</erpSysCode>"
                + "<custNo>0000082075</custNo>"
                + "<tradeName>ERP_QUERYACCLIST</tradeName>"
                + "</head>"
                + "<map>"
                + "<queryCustNo>0000082075</queryCustNo>"
                + "</map>"
                + "</body>";

                string xmls = encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = decrypt(re);//解密
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                erpPlatform.erpPlatformPortTypeClient erp = new erpPlatform.erpPlatformPortTypeClient();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
                + "<body>"
                + "<head>"
                + "<erpSysCode>ERP001</erpSysCode>"
                + "<custNo>0000082075</custNo>"
                + "<tradeName>ERP_QUERYCURDTL</tradeName>"
                + "</head>"
                + "<map>"
                + "<bankAcc>30010122000533641</bankAcc>"

                + "</map>"
                + "</body>";

                string xmls = encrypt(xml);//加密
                string re = erp.serverErpXml(xmls);//传输 接收
                string bb = decrypt(re);//解密

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(bb);
                XmlNode rootNode = xmlDoc.SelectSingleNode("/body/head/retMsg");
                foreach (XmlNode xxNode in rootNode.ChildNodes)
                {
                    string dsf = xxNode.InnerText;
                    string sdf = xxNode.Name;

                }
                //rootNode = xmlDoc.SelectSingleNode("/body/map");
                //foreach (XmlNode xxNode in rootNode.ChildNodes)
                //{
                //    string dsf = xxNode.InnerText;
                //    string sdf = xxNode.Name;

                //}

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                erpPlatform.erpPlatformPortTypeClient erp = new erpPlatform.erpPlatformPortTypeClient();
                string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                    "<body>" +
                    "<head>" +
                    "<erpSysCode>ERP001</erpSysCode>" +
                    "<custNo>0000082075</custNo>" +
                    "<tradeName>ERP_TRANSFER</tradeName>" +
                    "</head>" +
                    "<map>" +
                    "<payerAccNo>30010122000533641</payerAccNo>" +
                    "<payerCorpCode>1000</payerCorpCode>" +
                    "<payerCorpName>75625430X</payerCorpName>" +
                    "<erpPayerCorpCode></erpPayerCorpCode>" +
                    "<payeeAccNo>110907487210802</payeeAccNo>" +
                    "<payeeAccName>海角先锋（北京）科技有限公司</payeeAccName>" +
                    "<payeeBankName>招商银行股份有限公司北京首体支行</payeeBankName>" +
                    "<payeeBankCode></payeeBankCode>" +
                    "<payeeProv></payeeProv>" +
                    "<payeeCity></payeeCity>" +
                    "<payMoney>100000.88</payMoney>" +
                    "<areaSign>1</areaSign>" +
                    "<difSign>1</difSign>" +
                    "<payPurpose>ERP付款</payPurpose>" +
                    "<erpReqNo>CSAA0001</erpReqNo>" +
                    "<erpReqUser></erpReqUser>" +
                    "<allowEditPayeeAcc>0</allowEditPayeeAcc>" +
                    "<allowEditPayMoney>0</allowEditPayMoney>" +
                    "<allowEditPayerAcc>1</allowEditPayerAcc>" +
                    "<reverse1></reverse1>" +
                    "<reverse2></reverse2>" +
                    "<reverse3></reverse3>" +
                    "<reverse4></reverse4>" +
                    "<reverse5></reverse5>" +
                    "</map>" +
                    "</body>";

                string xmls = encrypt(xml);
                string asas = erp.serverErpXml(xmls);
                string bb = decrypt(asas);


                string aaaa = "";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       


    }
}
