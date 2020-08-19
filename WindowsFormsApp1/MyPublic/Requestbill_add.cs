using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1.MyPublic
{
    public class Requestbill_add
    {
        public Requestbill_add()
        {
            address= string.Empty;
            bill_code = string.Empty;
            city = string.Empty;
            district = string.Empty;
            mobile = string.Empty;
            outer_bill_code = string.Empty;
            province = string.Empty;
            reason = string.Empty;
            receiver = string.Empty;
            remark = string.Empty;
            storage_code = string.Empty;
        }
        public string address { get; set; }
        public string bill_code { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string mobile { get; set; }
        public string outer_bill_code { get; set; }
        public string province { get; set; }
        public string reason { get; set; }
        public string receiver { get; set; }
        public string remark { get; set; }
        public string storage_code { get; set; }
        public List<OpenStockReqBillDetailRequest> details { get; set; }
    }
    public class OpenStockReqBillDetailRequest
    {
        public OpenStockReqBillDetailRequest()
        {
            index= string.Empty;
            remark= string.Empty;
            size = string.Empty;
            spec_code = string.Empty;
        }
        public string index { get; set; }
        public string remark { get; set; }
        public string size { get; set; }
        public string spec_code { get; set; }
    }
}
