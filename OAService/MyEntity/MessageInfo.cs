using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAService.MyEntity
{
    public class MessageInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Text text {get; set;}
        /// <summary>
        /// 
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string toparty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string totag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msgtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string agentid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string safe { get; set; }
    }
    //public class Text
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string content { get; set; }
    //}
}