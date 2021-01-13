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
        public string Rqs_Jrnl_No { get; set; }
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
        public string processAmt { get; set; }
        public string processNum { get; set; }
        public string successAmt { get; set; }
        public string failAmt { get; set; }
        public string failNum { get; set; }
        public string successNum { get; set; }
        public string batchSerialNo { get; set; }
        public List<TransferDtl> transferDtls { get; set; }
    }
    public class TransferDtls
    {
        public string dtlSerialNo { get; set; }
        public string payAcc { get; set; }
        public string rcvAcc { get; set; }
        public string corpCode { get; set; }
        public string rcvBank { get; set; }
        public string rcvBankCode { get; set; }
        public string rcvMobile { get; set; }
        public string amt { get; set; }
        public string difBank { get; set; }
        public string areaSign { get; set; }
        public string purpose { get; set; }
        public string remark { get; set; }
        public string isForIndividual { get; set; }
        public string showFlag { get; set; }
    }
    public class TransferDtl
    {
        public string errDesc { get; set; }
        public string amt { get; set; }
        public string transactionId { get; set; }
        public string serialNo { get; set; }
        public string status { get; set; }
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