using DSMT.MEDIA.WinForm.MyPublic;
using DSMT.MEDIA.WinService.MyPublic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace DSMT.MEDIA.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //开启Tcp端口
            TcpHelper tcpHelper = new TcpHelper();
            tcpHelper.Address = IPAddress.Parse("0.0.0.0");
            tcpHelper.Port = 51219;
            tcpHelper.EventRecive += MediaEvent;
            tcpHelper.Start();
            //发送TCP请求
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse("192.168.1.46"), 51219));
            byte[] byteDevInNo = Encoding.GetEncoding("GB2312").GetBytes("12");
            byte[] byteStatus = BitConverter.GetBytes(1);
            string mediaID1 = "3190120b-abf0-4f1c-8de0-075746d7e88f";
            DevInfo devInfo = new DevInfo();
            devInfo.meidaID = mediaID1;
            string meidaID = new JavaScriptSerializer().Serialize(devInfo);
            byte[] byteDevNo = Encoding.GetEncoding("GB2312").GetBytes(meidaID);
            int a = byteDevNo.Length;
            byte[] byteLen = BitConverter.GetBytes(byteDevNo.Length);
            byte[] byteArr = new byte[56];
            MyPublic.ByteHelper.ByteArrToByteArr(byteArr, 0, byteDevInNo, 2);
            MyPublic.ByteHelper.ByteArrToByteArr(byteArr, 2, byteStatus, 2);
            MyPublic.ByteHelper.ByteArrToByteArr(byteArr, 4, byteLen, 2);
            MyPublic.ByteHelper.ByteArrToByteArr(byteArr, 6, byteDevNo, 50);
            //string mediaID1 = ByteHelper.ByteToString(byteMediaID, 36);
            //byte[] byteLen = BitConverter.GetBytes(byteMediaID.Length);
            //byte[] byteArr = new byte[42];
            //MyPublic.ByteHelper.ByteArrToByteArr(byteArr, 0, byteDevInNo, 2);
            //MyPublic.ByteHelper.ByteArrToByteArr(byteArr, 2, byteStatus, 2);
            //MyPublic.ByteHelper.ByteArrToByteArr(byteArr, 4, byteLen, 2);
            //MyPublic.ByteHelper.ByteArrToByteArr(byteArr, 6, byteMediaID, 36);
            //socket.Send(byteArr);
            //接收TCP响应
            byte[] buffer = new byte[102400];
            socket.Receive(buffer, 2, SocketFlags.None);
            int statusNum = MyPublic.ByteHelper.ByteToInt(buffer, 2);
            socket.Receive(buffer, 2, SocketFlags.None);
            int len = MyPublic.ByteHelper.ByteToInt(buffer, 2);
            socket.Receive(buffer, len, SocketFlags.None);
            string message = MyPublic.ByteHelper.ByteToString(buffer, len);
            //byte[] byteStatusNum = BitConverter.GetBytes(1);
            //byte[] byteArr = new byte[4 + byteMediaFile.Length];
            //byte[] byteArrLen = BitConverter.GetBytes(byteMediaFile.Length);
            //ByteHelper.ByteArrToByteArr(byteArr, 0, byteStatusNum, 2);
            //ByteHelper.ByteArrToByteArr(byteArr, 2, byteArrLen, 2);
            //ByteHelper.ByteArrToByteArr(byteArr, 4, byteMediaFile, byteMediaFile.Length);
            //byte[] buffer = new byte[102400];
            //socket.Receive(buffer, 2, SocketFlags.None);
            //int statusNum = ByteHelper.ByteToInt(buffer, 2);
            //socket.Receive(buffer, 2, SocketFlags.None);
            //int len=ByteHelper.ByteToInt(buffer, 2);
            //socket.Receive(buffer, len, SocketFlags.None);
            //string message = ByteHelper.ByteToString(buffer, len);

            //byte[] byteStatusNum = BitConverter.GetBytes(1);
            //byte[] byteArr = new byte[4 + byteMediaFile.Length];
            //byte[] byteArrLen = BitConverter.GetBytes(byteMediaFile.Length);
            //ByteHelper.ByteArrToByteArr(byteArr, 0, byteStatusNum, 2);
            //ByteHelper.ByteArrToByteArr(byteArr, 2, byteArrLen, 2);
            //ByteHelper.ByteArrToByteArr(byteArr, 4, byteMediaFile, byteMediaFile.Length);


            socket.Close();
            ////接收TCP响应
            //byte[] buffer = new byte[1024];
            //socket.Receive(buffer, 2, SocketFlags.None);
            //int statusNum = ByteHelper.ByteToInt(buffer, 2);
            //socket.Receive(buffer, 7, SocketFlags.None);
            //DateTime timeStamp = ByteHelper.ByteToDateTime(buffer);
            //socket.Receive(buffer, 2, SocketFlags.None);
            //int dataLen = ByteHelper.ByteToInt(buffer, 2);
            //socket.Receive(buffer, dataLen, SocketFlags.None);
            //string strData = ByteHelper.ByteToString(buffer, dataLen);
            //socket.Receive(buffer, 2, SocketFlags.None);
            //dataLen = ByteHelper.ByteToInt(buffer, 2);
            //socket.Receive(buffer, dataLen, SocketFlags.None);
            //string strSign = ByteHelper.ByteToString(buffer, dataLen);
            //if (DateTime.Now.Subtract(timeStamp).Duration().TotalSeconds <= 300 && new MD5Helper("123456").CheckEncrypt(strData + timeStamp.ToString("yyyyMMddHHmmss"), strSign))
            //{
            //    strData = new DESHelper("12345678", "12345678").Decrypt(strData);
            //}
            //socket.Close();

        }
        private void MediaEvent(int devInNo, int cmdID, byte[] byteData, TcpClient tcpClient, EventArgs e)
        {
            switch (cmdID)
            {
                case 0:
                    //校对服务器时间
                    byte[] byteArr0 = MyPublic.ByteHelper.DateTimeToByte(DateTime.Now);
                    tcpClient.Client.Send(byteArr0, byteArr0.Length, SocketFlags.None);
                    break;
                case 1:
                ////获取媒体信息
                //byte[] byteArr1 = new MediaHelper().SendMediaInfo(devInNo, byteData);
                //tcpClient.Client.Send(byteArr1, byteArr1.Length, SocketFlags.None);
                //break;
                    //下载媒体信息
                    new MediaHelper().SendMediaFile(devInNo, byteData, tcpClient);
                    break;
                //case 2:
                //    byte[] byteArr2 = new MediaHelper().GetMediaFile(devInNo, byteData);
                //    tcpClient.Client.Send(byteArr2, byteArr2.Length, SocketFlags.None);
                //    break;
                //case 2:
                //    //ID在线查询账户余额(不分开显示余额)
                //    byte[] byteArr2 = new XFTradeHelper().IDOnLineQueryAccBalance(devInNo, byteData, false);
                //    tcpClient.Client.Send(byteArr2, byteArr2.Length, SocketFlags.None);
                //    break;
                //case 3:
                //    //ID在线消费模式-卡类餐段定值(不分开显示余额)
                //    byte[] byteArr3 = new XFTradeHelper().IDOnLineXFForCardMealType(devInNo, byteData, false);
                //    tcpClient.Client.Send(byteArr3, byteArr3.Length, SocketFlags.None);
                //    break;
                //case 4:
                //    //ID在线充值模式(不分开显示余额)
                //    byte[] byteArr4 = new XFTradeHelper().IDOnLineRec(devInNo, byteData, false);
                //    tcpClient.Client.Send(byteArr4, byteArr4.Length, SocketFlags.None);
                //    break;
                default: break;
            }
        }
    }
}
