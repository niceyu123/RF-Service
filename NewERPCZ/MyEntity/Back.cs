using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewERPCZ.MyEntity
{
    public class Head
    {
        /// <summary>
        /// 
        /// </summary>
        public string Rsp_Dt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Rsp_Tm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Txn_Rsp_Cd_Dsc { get; set; }
        /// <summary>
        /// 交易成功
        /// </summary>
        public string Txn_Rsp_Inf { get; set; }
    }

    public class Data
    {
        public TransferDtl transferDtl { get; set; }
        public string downloadNo { get; set; }
        public string custId { get; set; }
        public string retCode { get; set; }
        public string retMsg { get; set; }
        public string transMsg { get; set; }
        public string downloadUrl { get; set; }
        public string transState { get; set; }
        public Failures failures { get; set; }
    }
    public class Failures
    {
        public string serialNo { get; set; }
        public string errorCode { get; set; }
        public string errorMsg { get; set; }
    }
    public class Back
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