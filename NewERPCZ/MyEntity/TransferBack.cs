using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewERPCZ.MyEntity
{
    public class TransferDtl
    {
        /// <summary>
        /// 
        /// </summary>
        public string voucherNo { get; set; }
        /// <summary>
        /// 交易成功
        /// </summary>
        public string errDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string amt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string serialNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
    }

    public class TransferBack
    {
        /// <summary>
        /// 
        /// </summary>
        public Head Head { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data Data { get; set; }
    }
}