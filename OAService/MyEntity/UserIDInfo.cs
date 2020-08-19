using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAService.MyEntity
{
    public class Text
    {
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
    }



    public class Extattr
    {
        /// <summary>
        /// 
        /// </summary>
        public List<AttrsItem> attrs { get; set; }
    }


    public class External_attrItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 对外企业
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Text text { get; set; }
    }

    public class External_profile
    {
        /// <summary>
        /// 
        /// </summary>
        public List<External_attrItem> external_attr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string external_corp_name { get; set; }
    }

    public class UserlistItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 张明忠
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> department { get; set; }
        /// <summary>
        /// 经理
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int enable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isleader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Extattr extattr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int hide_mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string english_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public External_profile external_profile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string qr_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string @alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> is_leader_in_dept { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thumb_avatar { get; set; }
    }

    public class UserIDInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<UserlistItem> userlist { get; set; }
    }
}