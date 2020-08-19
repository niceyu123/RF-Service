using Newtonsoft.Json;
using OAService.MyEntity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace OAService.MyPublic
{
    public class QYWeixinHelper
    {
        static string corpid = "ww226ac420227ca99f";
        static string corpsecret = "ofSnN2DGGfTnwe7VE9W7_pbqv3i5wzj1bH6OXJ1-YKo";
        static string corpsecretTXL = "RJImeiGrEG7b6146BB4WWT0oTY1NmjBElRLsiOhUW3M";
        static string messageSendURl = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";

        /// <summary>
        /// 获取企业号的accessToken
        /// </summary>
        /// <param name="corpid">企业号ID</param>
        /// <param name="corpsecret">管理组密钥</param>
        /// <returns></returns>
        public static string GetQYAccessToken(string corpid, string corpsecret)
        {
            string getAccessTokenUrl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken";
            string accessToken = "";
            //获取josn数据
            try
            {
                string url = getAccessTokenUrl + "?corpid=" + corpid + "&corpsecret=" + corpsecret;
                string res = ToolHelper.GetHttpResponse(url, 6000);
                TokenInfo tokenInfo = new JavaScriptSerializer().Deserialize<TokenInfo>(res);
                if (Convert.ToString(tokenInfo.errcode) == "0")
                {
                    return tokenInfo.access_token;
                }
                else
                {
                    return accessToken;
                }
            }
            catch (Exception)
            {
                return accessToken;
            }
           
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //using (Stream resStream = response.GetResponseStream())
            //{
            //    StreamReader reader = new StreamReader(resStream, Encoding.Default);
            //    respText = reader.ReadToEnd();
            //    resStream.Close();
            //}
            //try
            //{
            //    JavaScriptSerializer Jss = new JavaScriptSerializer();
            //    Dictionary<string, object> respDic = (Dictionary<string, object>)Jss.DeserializeObject(respText);
            //    //通过键access_token获取值
            //    accessToken = respDic["access_token"].ToString();
            //}
            //catch (Exception ex)
            //{
            //    ToolHelper.logger.Info(ex.ToString());
            //}

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
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
                SendMessageInfo send = new JavaScriptSerializer().Deserialize<SendMessageInfo>(ret);
                ret = send.errcode;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// 推送信息
        /// </summary>
        /// <param name="corpid">企业号ID</param>
        /// <param name="corpsecret">管理组密钥</param>
        /// <param name="paramData">提交的数据json</param>
        /// <param name="dataEncode">编码方式</param>
        /// <returns></returns>
        public static string SendText(string empCode, string message)
        {
            try
            {
                string accessToken = "";
                string postUrl = "";
                string param = "";
                string postResult = "";

                accessToken = GetQYAccessToken(corpid, corpsecret);
                postUrl = string.Format(messageSendURl, accessToken);
                string json = "{\"text\":{\"content\":\""+ message + "\"},\"touser\":\""+ empCode + "\",\"toparty\":null,\"totag\":null,\"msgtype\":\"text\",\"agentid\":\"1000006\",\"safe\":\"0\"}";
                //ToolHelper.logger.Info(json);
                foreach (string item in empCode.Split('|'))
                {
                    //param = JsonConvert.SerializeObject(mes);
                    postResult = PostWebRequest(postUrl, json, Encoding.UTF8);
                }
                //if(postResult == "40008")
                //{
                //    string aa = "";
                //}
                return postResult;
            }
            catch (Exception ex)
            {
                ToolHelper.logger.Info(ex.ToString());
                throw;
            }
        }
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departmentname"></param>
        /// <param name="subcompanyid1"></param>
        /// <param name="supdepid"></param>
        public static void AddDept(string id, string departmentname, string subcompanyid1, string supdepid)
        {
            try
            {
                //根据OA部门的上级ID 找到微信部门的上级ID
                if (supdepid == "0")
                {
                    int parentid = QYWeixinHelper.GetWXParentID(subcompanyid1);
                    if (parentid != 99999)
                    {
                        int wxID = Convert.ToInt32("6" + id);
                        OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                        string sql = "update OA_WX_DEPT set wxid=" + wxID + ",name='" + departmentname + "',parentid=" + parentid + ", status=1,endtime=sysdate' where id='" + id + "' ";
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn);
                    }
                }
                else//新建的wx部门id=6+OA部门id
                {
                    OracleConnection conn = ToolHelper.OpenRavoerp("oa");
                    string sql = " select * from OA_WX_DEPT where id ='" + supdepid + "'";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //获取上级企业微信部门id
                    string wxidp = dt.Rows[0]["WXID"].ToString();

                    ToolHelper.CloseSql(conn);
                    //新建wx部门
                    AddDptInfo addDep = new AddDptInfo();
                    addDep.name = departmentname;
                    addDep.parentid = Convert.ToInt32(wxidp);
                    addDep.id = Convert.ToInt32("6" + id);
                    AddDptReturn addDptReturn = QYWeixinHelper.AddDptAPI(addDep);//??考虑部门已存在的情况
                    if (Convert.ToString(addDptReturn.errcode) == "0")
                    {
                        //修改已成功的状态
                        conn = ToolHelper.OpenRavoerp("oa");
                        sql = "update OA_WX_DEPT set wxid=" + addDep.id + ",name='" + departmentname + "',parentid=" + addDep.parentid + ", status=1,endtime=sysdate where id='" + id + "' ";
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn);

                    }
                    else
                    {
                        //失败
                        conn = ToolHelper.OpenRavoerp("oa");
                        sql = "update OA_WX_DEPT set reason='" + addDptReturn.errcode + addDptReturn.errmsg + "',endtime=sysdate' where id='" + id + "' ";
                        cmd = new OracleCommand(sql, conn);
                        int result = cmd.ExecuteNonQuery();
                        ToolHelper.CloseSql(conn);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
        /// <summary>
        /// 添加部门接口传输
        /// </summary>
        /// <param name="addDptInfo"></param>
        /// <returns></returns>
        public static AddDptReturn AddDptAPI(AddDptInfo addDptInfo)
        {
            AddDptReturn addDptReturn = new AddDptReturn();
            try
            {
                string accessToken = GetQYAccessToken(corpid, corpsecretTXL);
                string strJson = JsonConvert.SerializeObject(addDptInfo);
                //post请求
                string url = "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token=" + accessToken;
                string param = strJson;
                string callback = ToolHelper.Post(url, param);
                addDptReturn = new JavaScriptSerializer().Deserialize<AddDptReturn>(callback);
                return addDptReturn;
            }
            catch (Exception ex)
            {
                addDptReturn.errcode = "9";
                addDptReturn.errmsg = ex.ToString();
                addDptReturn.id = 99999;
                return addDptReturn;
            }
        }
        /// <summary>
        /// 获取OA上级编号为0时,wx上级部门编号
        /// </summary>
        /// <param name="subcompanyid1"></param>
        /// <returns></returns>
        public static int GetWXParentID(string subcompanyid1)
        {
            if (subcompanyid1 == "21")//河南实业
            {
                return 77;
            }
            else if (subcompanyid1 == "22")//隆威婴儿
            {
                return 11;
            }
            else if (subcompanyid1 == "23")//工业
            {
                return 45;
            }
            else if (subcompanyid1 == "24")//瑞孚集团
            {
                return 23;
            }
            else if (subcompanyid1 == "25")//华孚进出口
            {
                return 170;
            }
            else if (subcompanyid1 == "26")//可可萌
            {
                return 17;
            }
            else if (subcompanyid1 == "141")//汇隆
            {
                return 530;
            }
            else if (subcompanyid1 == "161")//研森
            {
                return 443;
            }
            else
            {
                return 99999;
            }
        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departmentname"></param>
        /// <param name="subcompanyid1"></param>
        /// <param name="supdepid"></param>
        public static void UpdateDept(string id, string departmentname, string subcompanyid1, string supdepid, string wxid, string name, string parentid)
        {
            OracleConnection conn = ToolHelper.OpenRavoerp("oa");
            string sql = " select * from OA_WX_DEPT where id ='" + supdepid + "'";
            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //获取上级企业微信部门id
            string wxidp = dt.Rows[0]["WXID"].ToString();
            ToolHelper.CloseSql(conn);
            if (supdepid == "0")
            {
                int parentidu = QYWeixinHelper.GetWXParentID(subcompanyid1);
                if (parentidu != 99999)
                {
                    int wxID = Convert.ToInt32("6" + id);
                    conn = ToolHelper.OpenRavoerp("oa");
                    sql = "update OA_WX_DEPT set name='" + departmentname + "',parentid=" + parentidu + ", status=1,endtime=sysdate' where id='" + id + "' ";
                    cmd = new OracleCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();
                    ToolHelper.CloseSql(conn);
                }
            }
            else
            {
                UpdateDptInfo updateDptInfo = new UpdateDptInfo();
                updateDptInfo.id = Convert.ToInt32(wxid);
                updateDptInfo.name = departmentname;
                updateDptInfo.parentid = Convert.ToInt32(wxidp);
                DptReturn dptReturn = QYWeixinHelper.UpdateDptAPI(updateDptInfo);
                if (Convert.ToString(dptReturn.errcode) == "0")
                {
                    //修改已成功的状态
                    conn = ToolHelper.OpenRavoerp("oa");
                    sql = "update OA_WX_DEPT set name='" + departmentname + "',parentid=" + updateDptInfo.parentid + ", status=1,endtime=sysdate where id='" + id + "' ";
                    cmd = new OracleCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();
                    ToolHelper.CloseSql(conn);
                }
                else
                {
                    conn = ToolHelper.OpenRavoerp("oa");
                    sql = "update OA_WX_DEPT set reason='" + dptReturn.errcode + dptReturn.errmsg + "',endtime=sysdate' where id='" + id + "' ";
                    cmd = new OracleCommand(sql, conn);
                    int result = cmd.ExecuteNonQuery();
                    ToolHelper.CloseSql(conn);
                }
            }
        }
        /// <summary>
        /// 修改部门信息接口
        /// </summary>
        /// <param name="updateDptInfo"></param>
        /// <returns></returns>
        public static DptReturn UpdateDptAPI(UpdateDptInfo updateDptInfo)
        {
            DptReturn dptReturn = new DptReturn();
            try
            {
                string accessToken = GetQYAccessToken(corpid, corpsecretTXL);


                string strJson = JsonConvert.SerializeObject(updateDptInfo);
                //post请求
                string url = "https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token=" + accessToken;
                string param = strJson;
                string callback = ToolHelper.Post(url, param);
                dptReturn = new JavaScriptSerializer().Deserialize<DptReturn>(callback);
                return dptReturn;
            }
            catch (Exception ex)
            {
                dptReturn.errcode = "9";
                dptReturn.errmsg = ex.ToString();
                return dptReturn;
            }
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departmentname"></param>
        /// <param name="subcompanyid1"></param>
        /// <param name="supdepid"></param>
        public static void DeleteDept(string id, string departmentname, string subcompanyid1, string supdepid)
        {

            //string url = "https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token=" + accessToken + "&department_id=1&fetch_child=1&status=0";
            //string res = ToolHelper.GetHttpResponse(url, 6000);
            //UserIDInfo userIDInfo = new JavaScriptSerializer().Deserialize<UserIDInfo>(res);
        }

    }
}