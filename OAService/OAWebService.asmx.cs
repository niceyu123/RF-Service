using OAService.MyEntity;
using OAService.MyPublic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace OAService
{
    /// <summary>
    /// OAWebService 的摘要说明
    /// </summary>
    //[WebService(Namespace = "http://api.ravogroup.com/OAWebService/")]
    [WebService(Namespace = "http://172.16.11.19:1915/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class OAWebService : System.Web.Services.WebService
    {
        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <returns></returns>
        /// string meidaID = new JavaScriptSerializer().Serialize(devInfo);
        /// 
        [WebMethod]
        public bool GetOrderHeadList(string orderHead)
        {
            //传过来json 先反序列化成实体类
            //string a = new JavaScriptSerializer().Serialize(orderHeadInfo);
            OrderHeadInfo orderHeadInfo = new JavaScriptSerializer().Deserialize<OrderHeadInfo>(orderHead);
            //先解密
            //if ()
            //{
            return OAServiceHelper.GetOrderHeadList(orderHeadInfo);
            //}
            //return false;
        }
        /// <summary>
        /// 获取订单产品信息
        /// </summary>
        /// <param name="orderDetailInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetOrderDetailList(string orderDetail)
        {
            OrderDetailInfo orderDetailInfo = new JavaScriptSerializer().Deserialize<OrderDetailInfo>(orderDetail);
            //先解密
            //if ()
            //{
            return OAServiceHelper.GetOrderDetailList(orderDetailInfo);
            //}
            //return false;
        }
    }
}
