using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1.MyPublic
{
    public class out_query
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<OpenSaleStockBill> data { get; set; }
    }
    public class OpenSaleStockBill
    {
        public string stock_code { get; set; }
        public string bill_date { get; set; }
        public string bill_type { get; set; }
        public string country { get; set; }
        public string create_time { get; set; }
        public string custom_code { get; set; }
        public string custom_name { get; set; }
        public string customer_nick { get; set; }
        public string customer_nick_type { get; set; }
        public string customer_nick_type_name { get; set; }
        public List<OpenSaleStockDetail> details { get; set; }
        public string discount_fee { get; set; }
        public string from_trade_no { get; set; }
        public string inv_no { get; set; }
        public string paid_fee { get; set; }
        public string pay_type { get; set; }
        public string from_exchange_no { get; set; }
        public string post_fee { get; set; }
        public string remark { get; set; }
        public string sale_man { get; set; }
        public string service_fee { get; set; }
        public string shop_name { get; set; }
        public string shop_nick { get; set; }
        public string shop_source { get; set; }
        public string storage_code { get; set; }
        public string storage_name { get; set; }
        public string sum_sale { get; set; }
        public string tp_tid { get; set; }
    }
    public class OpenSaleStockDetail
    {
        public List<OpenBatchInfo> batch_infos { get; set; }
        public string detail_id { get; set; }
        public string discount_fee { get; set; }
        public string goods_name { get; set; }
        public string nums { get; set; }
        public string sku_name { get; set; }
        public string sku_no { get; set; }
        public string sku_prop1 { get; set; }
        public string sku_prop2 { get; set; }
        public string sum_cost { get; set; }
        public string sum_sale { get; set; }
        public string unit { get; set; }
        public string index { get; set; }
        public string spec_code { get; set; }
    }
    public class OpenBatchInfo
    {
        public string batch_no { get; set; }
        public string nums { get; set; }
    }
}
