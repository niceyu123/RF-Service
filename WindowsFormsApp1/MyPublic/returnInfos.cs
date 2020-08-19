using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1.MyPublic
{
    public class returnInfos
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<OpenStockBill> data { get; set; }
    }
    public class OpenStockBill
    {
        public string bill_creater { get; set; }
        public string bill_date { get; set; }
        public string bill_type { get; set; }
        public string create_time { get; set; }
        public List<OpenStockDetail> details { get; set; }
        public string logistic_name { get; set; }
        public string modified_time { get; set; }
        public string operate_name { get; set; }
        public string reason { get; set; }
        public string remark { get; set; }
        public string stock_code { get; set; }
        public string stock_req_bill_code { get; set; }
        public string storage_code { get; set; }
        public string storage_name { get; set; }
    }
    public  class OpenStockDetail
    {
        public string goods_name { get; set; }
        public string index { get; set; }
        public string nums { get; set; }
        public string price { get; set; }
        public string remark { get; set; }
        public string spec_code { get; set; }
        public string spec_name { get; set; }
        public string total_money { get; set; }
        public string unit { get; set; }
    }

}
