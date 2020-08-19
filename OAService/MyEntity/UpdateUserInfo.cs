using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAService.MyEntity
{
    //public class UpdateUserInfo
    //{
        public class AttrsItem
        {
        /// <summary>
        /// 手机短号
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Text text { get; set; }
    }

       



        public class UpdateUserInfo
    {
            /// <summary>
            /// 
            /// </summary>
            public string userid { get; set; }
            /// <summary>
            /// 李四
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<int> department { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<int> order { get; set; }
            /// <summary>
            /// 后台工程师
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
            public List<int> is_leader_in_dept { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int enable { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string avatar_mediaid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string telephone { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string @alias { get; set; }
            /// <summary>
            /// 广州市海珠区新港中路
            /// </summary>
            public string address { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Extattr extattr { get; set; }
            /// <summary>
            /// 工程师
            /// </summary>
            public string external_position { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public External_profile external_profile { get; set; }
        }
    //    public UserlistItem userlist { get; set; }
    //}
}