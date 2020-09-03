using KKMWLN.MyEntity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

namespace KKMWLN.MyPublic
{
    public class CommonHelper
    {
        string secret = "5cf249cebe8c45eb6254b55e11f9e944";
        string key = "3723429465";
        //新建商品
        public void Additem()
        {
            while (true)
            {
                try
                {
                    //构造连接字符串
                    SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                    scsb.DataSource = "172.16.11.9";
                    scsb.InitialCatalog = "FumaCRM8";
                    scsb.UserID = "sa";
                    scsb.Password = "abc_123";
                    //创建连接 参数为连接字符串
                    SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                    sqlConn.Open();
                    string sqlStr = " SELECT  * FROM FumaCRM8.dbo.WLN_ITEM where CHANGED='0' and code='0' ";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sqlStr, sqlConn);//从数据库中查询
                    da.Fill(dt);//将数据填充到DataSet
                    sqlConn.Close();//关闭连接
                    int num = dt.Rows.Count;
                    if (num > 0)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            string article_number = dt.Rows[i]["article_number"].ToString();
                            string bar_code = dt.Rows[i]["bar_code"].ToString();
                            string item_name = dt.Rows[i]["item_name"].ToString();
                            string sale_price = dt.Rows[i]["sale_price"].ToString();
                            string weight = dt.Rows[i]["weight"].ToString();
                            string unit = dt.Rows[i]["unit"].ToString();
                            string item_pic = dt.Rows[i]["item_pic"].ToString();

                            add_item add = new add_item();
                            add.article_number = article_number;
                            add.bar_code = bar_code;
                            add.item_name = item_name;
                            add.sale_price = sale_price;
                            add.weight = weight;
                            add.unit = unit;
                            add.item_code = article_number;
                            //add.item_pic=
                            string json = new JavaScriptSerializer().Serialize(add);
                            ToolHelper.logger.Debug(json);
                            string urlcode = ToolHelper.UrlEncode(json);
                            string t = ToolHelper.time();
                            string post = secret + "_app=" + key + "&_s=&_t=" + t + "&item=" + urlcode + secret;
                            string m = ToolHelper.EncryptString(post);
                            var client = new RestClient("https://open-api.hupun.com/api/erp/goods/add/item");

                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                            request.AddParameter("_app", key);
                            request.AddParameter("_s", "");
                            request.AddParameter("_sign", m);
                            request.AddParameter("_t", t);
                            request.AddParameter("item", json);
                            IRestResponse response = client.Execute(request);
                            string str = response.Content;
                            returnInfo re = new JavaScriptSerializer().Deserialize<returnInfo>(str);
                            if (re.code == 0)
                            {
                                //打开连接
                                sqlConn.Open();
                                sqlStr = " update FumaCRM8.dbo.WLN_ITEM  set code='1',message='" + re.message + "',codedate='" + DateTime.Now + "'  where article_number='" + article_number + "' ";
                                SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                                int result = comm.ExecuteNonQuery();
                                sqlConn.Close();//关闭连接
                            }
                            else
                            {
                                //打开连接
                                sqlConn.Open();
                                sqlStr = " update FumaCRM8.dbo.WLN_ITEM  set code='2',message='" + re.message + "',codedate='" + DateTime.Now + "'  where article_number='" + article_number + "' ";
                                SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                                int result = comm.ExecuteNonQuery();
                                sqlConn.Close();//关闭连接
                            }

                        }
                    }
                    Thread.Sleep(100000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(100000);
                }
            }
        }
        //入库
        public void Requestbill_add()
        {
            while (true)
            {
                try
                {
                    //构造连接字符串
                    SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                    scsb.DataSource = "172.16.11.9";
                    scsb.InitialCatalog = "FumaCRM8";
                    scsb.UserID = "sa";
                    scsb.Password = "abc_123";
                    //创建连接 参数为连接字符串
                    SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                    //打开连接
                    sqlConn.Open();
                    string sqlStr = " SELECT  * FROM FumaCRM8.dbo.WLN_WHIN_MAIN where code='0' ";

                    //string sqlStr = " SELECT  * FROM FumaCRM8.dbo.WLN_WHIN where code='0' ";
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sqlStr, sqlConn);//从数据库中查询
                    da.Fill(dt);//将数据填充到DataSet
                    sqlConn.Close();//关闭连接
                    int nums = dt.Rows.Count;
                    if (nums > 0)
                    {
                        for (int j = 0; j < nums; j++)
                        {
                            string twhno = dt.Rows[j]["twhno"].ToString();
                            sqlConn.Open();
                            sqlStr = " SELECT  * FROM FumaCRM8.dbo.WLN_WHIN where twhno='" + twhno + "'";
                            dt = new DataTable();
                            da = new SqlDataAdapter(sqlStr, sqlConn);//从数据库中查询
                            da.Fill(dt);//将数据填充到DataSet
                            sqlConn.Close();//关闭连接
                            int num = dt.Rows.Count;
                            List<OpenStockReqBillDetailRequest> openlist = new List<OpenStockReqBillDetailRequest>();
                            
                            if (num > 0)
                            {

                                string spec_code = "";
                                string size = "";
                                for (int i = 0; i < num; i++)
                                {
                                    OpenStockReqBillDetailRequest open = new OpenStockReqBillDetailRequest();
                                    spec_code = dt.Rows[i]["spec_code"].ToString();
                                    size = dt.Rows[i]["size"].ToString();
                                    open.spec_code = spec_code;
                                    open.size = size;
                                    openlist.Add(open);
                                }
                                

                            }
                            string storage_code = dt.Rows[j]["storage_code"].ToString();
                            string remark = dt.Rows[j]["remark"].ToString();

                            Requestbill_add req = new Requestbill_add();
                            req.storage_code = storage_code;
                            req.remark = remark;
                            req.details = openlist;
                            string json = new JavaScriptSerializer().Serialize(req);
                            ToolHelper.logger.Debug(json);
                            string urlcode = ToolHelper.UrlEncode(json);
                            string t = ToolHelper.time();
                            //md5加密
                            string post = secret + "_app=" + key + "&_s=&_t=" + t + "&bill=" + urlcode + secret;
                            string m = ToolHelper.EncryptString(post);
                            //新增其他入库订单
                            var client = new RestClient("https://open-api.hupun.com/api/erp/stock/in/requestbill/add");
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                            request.AddParameter("_app", key);
                            request.AddParameter("_s", "");
                            request.AddParameter("_sign", m);
                            request.AddParameter("_t", t);
                            request.AddParameter("bill", json);
                            IRestResponse response = client.Execute(request);
                            string str = response.Content;
                            returnInfo re = new JavaScriptSerializer().Deserialize<returnInfo>(str);
                            if (re.code == 0)
                            {
                                //打开连接
                                sqlConn.Open();
                                sqlStr = " update FumaCRM8.dbo.WLN_WHIN_MAIN  set code='1',message='" + re.message + "',codetime='" + DateTime.Now + "'  where twhno='" + twhno + "' ";
                                SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                                int result = comm.ExecuteNonQuery();
                                sqlConn.Close();//关闭连接
                            }
                            else
                            {
                                //打开连接
                                sqlConn.Open();
                                sqlStr = " update FumaCRM8.dbo.WLN_WHIN_MAIN  set code='2',message='" + re.message + "',codetime='" + DateTime.Now + "'  where twhno='" + twhno + "' ";
                                SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                                int result = comm.ExecuteNonQuery();
                                sqlConn.Close();//关闭连接
                            }
                        }




                    }
                    Thread.Sleep(100000);
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    Thread.Sleep(100000);
                }
            }
        }
        //出库
        public void Stockbill_query()
        {
            while (true)
            {
                DateTime nowTime = DateTime.Now;
                string h = Convert.ToString(nowTime.Hour);
                string min = Convert.ToString(nowTime.Minute);
                //if(h=="22" && min == "00")
                if (true)
                {
                    try
                    {
                        //前一天的晚上10点
                        stockbill_query sq = new stockbill_query();
                        DateTime dt = DateTime.Now.AddDays(-1);
                        string ti = dt.ToString("yyyy-MM-dd") + " 22:00:00";
                        //string ti = "2020-08-19 12:00:11";
                        DateTime dti = Convert.ToDateTime(ti);
                        string mt = ToolHelper.times(dti);
                        //string mt = times(DateTime.Now);
                        sq.modify_time = mt;
                        sq.page = "1";
                        sq.limit = "100";
                        string json = new JavaScriptSerializer().Serialize(sq);
                        string urlcode = ToolHelper.UrlEncode(json);
                        string t = ToolHelper.time();
                        //t = "1558952035";
                        string post = secret + "_app=" + key + "&_s=&_t=" + t + "&limit=" + sq.limit + "&modify_time=" + sq.modify_time + "&page=" + sq.page + secret;
                        string m = ToolHelper.EncryptString(post);
                        //var client = new RestClient("https://open-api.hupun.com/api/erp/stock/out/stockbill/query");
                        var client = new RestClient("https://open-api.hupun.com/api/erp/sale/stock/out/query");

                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        request.AddParameter("_app", key);
                        request.AddParameter("_s", "");
                        request.AddParameter("_sign", m);
                        request.AddParameter("_t", t);
                        request.AddParameter("limit", sq.limit);
                        request.AddParameter("modify_time", sq.modify_time);
                        request.AddParameter("page", sq.page);

                        IRestResponse response = client.Execute(request);
                        string str = response.Content;
                        out_query re = new JavaScriptSerializer().Deserialize<out_query>(str);
                        string mess = re.message;
                        int code = re.code;
                        if (code == 0)
                        {
                            int num = re.data.Count;
                            //int num = re.data[0].details.Count;
                            for (int i = 0; i < num; i++)
                            {
                                string stock_code = re.data[i].stock_code;
                                string bd = re.data[i].bill_date;
                                long b = Convert.ToInt64(bd);
                                DateTime bill_date1 = ToolHelper.StampToDatetime(b);
                                string bill_date = Convert.ToString(bill_date1);
                                string bill_type = re.data[i].bill_type;
                                string country = re.data[i].country;
                                string ct = re.data[i].create_time;
                                long c = Convert.ToInt64(ct);
                                DateTime create_time = ToolHelper.StampToDatetime(c);
                                //string create_time = re.data[i].create_time;
                                string custom_code = re.data[i].custom_code;
                                string custom_name = re.data[i].custom_name;
                                string customer_nick = re.data[i].customer_nick;
                                string customer_nick_type = re.data[i].customer_nick_type;
                                string customer_nick_type_name = re.data[i].customer_nick_type_name;
                                string discount_fee = re.data[i].discount_fee;
                                string from_trade_no = re.data[i].from_trade_no;
                                string inv_no = re.data[i].inv_no;
                                string paid_fee = re.data[i].paid_fee;
                                string pay_type = re.data[i].pay_type;
                                string post_fee = re.data[i].post_fee;
                                string remark = re.data[i].remark;
                                string sale_man = re.data[i].sale_man;
                                string service_fee = re.data[i].service_fee;
                                string shop_name = re.data[i].shop_name;
                                string shop_nick = re.data[i].shop_nick;
                                string shop_source = re.data[i].shop_source;
                                string storage_code = re.data[i].storage_code;
                                string storage_name = re.data[i].storage_name;
                                string sum_sale = re.data[i].sum_sale;
                                string tp_tid = re.data[i].tp_tid;

                                string detail_id = re.data[i].details[0].detail_id;
                                string discount_fee1 = re.data[i].details[0].discount_fee;
                                string goods_name = re.data[i].details[0].goods_name;
                                string nums = re.data[i].details[0].nums;
                                string sku_name = re.data[i].details[0].sku_name;
                                string sku_no = re.data[i].details[0].sku_no;
                                string sku_prop1 = re.data[i].details[0].sku_prop1;
                                string sku_prop2 = re.data[i].details[0].sku_prop2;
                                string sum_cost = re.data[i].details[0].sum_cost;
                                string detail_sum_sale = re.data[i].details[0].sum_sale;
                                string unit = re.data[i].details[0].unit;
                                string indexs = "";
                                string spec_code = re.data[i].details[0].spec_code;

                                SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                                scsb.DataSource = "172.16.11.9";
                                scsb.InitialCatalog = "FumaCRM8";
                                scsb.UserID = "sa";
                                scsb.Password = "abc_123";
                                //创建连接 参数为连接字符串
                                SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                                sqlConn.Open();
                                string sqlStr = " select * from WLN_SALESWHOUT where detail_id='"+ detail_id + "' ";
                                DataTable dta = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter(sqlStr, sqlConn);//从数据库中查询
                                da.Fill(dta);//将数据填充到DataSet
                                sqlConn.Close();//关闭连接
                                int numn = dta.Rows.Count;
                                if (numn == 0)
                                {
                                    sqlConn.Open();
                                    sqlStr = " INSERT INTO FumaCRM8.dbo.WLN_SALESWHOUT (bill_date,bill_type,create_time,custom_code,custom_name,customer_nick," +
                                    "customer_nick_type,customer_nick_type_name,discount_fee,paid_fee,post_fee,pay_type,from_trade_no,inv_no,remark,sale_man," +
                                    "service_fee,shop_name,shop_nick,shop_source,storage_code,storage_name,sum_sale,tp_tid,detail_id,goods_name,nums,sku_name," +
                                    "sku_no,sku_prop1,sku_prop2,sum_cost,detail_sum_sale,unit,indexs,codedate,spec_code,stock_code) " +
                                    "VALUES ('" + bill_date + "','" + bill_type + "','" + create_time + "','" + custom_code + "','" + custom_name + "','" + customer_nick + "'," +
                                    "'" + customer_nick_type + "','" + customer_nick_type_name + "','" + discount_fee + "','" + paid_fee + "','" + post_fee + "','" + pay_type + "'," +
                                    "'" + from_trade_no + "','" + inv_no + "','" + remark + "','" + sale_man + "','" + service_fee + "','" + shop_name + "','" + shop_nick + "'," +
                                    "'" + shop_source + "','" + storage_code + "','" + storage_name + "','" + sum_sale + "','" + tp_tid + "','" + detail_id + "','" + goods_name + "'," +
                                    "'" + nums + "','" + sku_name + "','" + sku_no + "','" + sku_prop1 + "','" + sku_prop2 + "','" + sum_cost + "','" + detail_sum_sale + "'," +
                                    "'" + unit + "','" + indexs + "','" + DateTime.Now + "','" + spec_code + "','" + stock_code + "')";
                                    SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                                    int result = comm.ExecuteNonQuery();
                                    sqlConn.Close();//关闭连接
                                }

                            }
                        }
                        else
                        {
                            //记录错误信息

                        }
                        Thread.Sleep(100000);
                    }
                    catch (Exception ex)
                    {
                        ToolHelper.logger.Debug(ex.ToString());
                        Thread.Sleep(100000);
                    }
                }
            }
        }
        //退货
        public void Stock_in_query()
        {
            DateTime nowTime = DateTime.Now;
            string h = Convert.ToString(nowTime.Hour);
            string min = Convert.ToString(nowTime.Minute);
            //if (h == "22" && min == "00")
            if (true)
            {
                try
                {
                    //前一天的晚上10点
                    stockbill_query sq = new stockbill_query();
                    DateTime dt = DateTime.Now.AddDays(-1);
                    string ti = dt.ToString("yyyy-MM-dd") + " 22:00:00";
                    //string ti = "2020-08-19 12:00:11";
                    DateTime dti = Convert.ToDateTime(ti);
                    string mt = ToolHelper.times(dti);
                    string t = ToolHelper.time();
                    string post = secret + "_app=" + key + "&_s=&_t=" + t + "&limit=" + 200 + "&modify_time=" + mt + "&page=" + 1 + secret;
                    string m = ToolHelper.EncryptString(post);
                    var client = new RestClient("https://open-api.hupun.com/api/erp/sale/stock/in/query");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("_app", key);
                    request.AddParameter("_s", "");
                    request.AddParameter("_sign", m);
                    request.AddParameter("_t", t);
                    request.AddParameter("limit", 200);
                    request.AddParameter("modify_time", mt);
                    request.AddParameter("page", 1);
                    IRestResponse response = client.Execute(request);
                    string str = response.Content;
                    out_query re = new JavaScriptSerializer().Deserialize<out_query>(str);
                    string mess = re.message;
                    int code = re.code;
                    if (code == 0)
                    {
                        int num = re.data[0].details.Count;
                        for (int i = 0; i < num; i++)
                        {
                            string stock_code = re.data[0].stock_code;
                            string bd = re.data[0].bill_date;
                            long b = Convert.ToInt64(bd);
                            DateTime bill_date1 = ToolHelper.StampToDatetime(b);
                            string bill_date = Convert.ToString(bill_date1);
                            string bill_type = re.data[0].bill_type;
                            string country = re.data[0].country;
                            string ct = re.data[0].create_time;
                            long c = Convert.ToInt64(ct);
                            DateTime create_time = ToolHelper.StampToDatetime(c);
                            //string create_time = re.data[0].create_time;
                            string custom_code = re.data[0].custom_code;
                            string custom_name = re.data[0].custom_name;
                            string customer_nick = re.data[0].customer_nick;
                            string customer_nick_type = re.data[0].customer_nick_type;
                            string customer_nick_type_name = re.data[0].customer_nick_type_name;
                            string discount_fee = re.data[0].discount_fee;
                            string from_trade_no = re.data[0].from_trade_no;
                            string inv_no = re.data[0].inv_no;
                            string paid_fee = re.data[0].paid_fee;
                            string pay_type = re.data[0].pay_type;
                            string post_fee = re.data[0].post_fee;
                            string remark = re.data[0].remark;
                            string sale_man = re.data[0].sale_man;
                            string service_fee = re.data[0].service_fee;
                            string shop_name = re.data[0].shop_name;
                            string shop_nick = re.data[0].shop_nick;
                            string shop_source = re.data[0].shop_source;
                            string storage_code = re.data[0].storage_code;
                            string storage_name = re.data[0].storage_name;
                            string sum_sale = re.data[0].sum_sale;
                            string tp_tid = re.data[0].tp_tid;
                            string indexs = re.data[0].from_exchange_no;

                            string detail_id = re.data[0].details[i].detail_id;
                            string discount_fee1 = re.data[0].details[i].discount_fee;
                            string goods_name = re.data[0].details[i].goods_name;
                            string nums = re.data[0].details[i].nums;
                            string sku_name = re.data[0].details[i].sku_name;
                            string sku_no = re.data[0].details[i].sku_no;
                            string sku_prop1 = re.data[0].details[i].sku_prop1;
                            string sku_prop2 = re.data[0].details[i].sku_prop2;
                            string sum_cost = re.data[0].details[i].sum_cost;
                            string detail_sum_sale = re.data[0].details[i].sum_sale;
                            string unit = re.data[0].details[i].unit;
                            string spec_code = re.data[0].details[i].spec_code;

                            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                            scsb.DataSource = "172.16.11.9";
                            scsb.InitialCatalog = "FumaCRM8";
                            scsb.UserID = "sa";
                            scsb.Password = "abc_123";
                            //创建连接 参数为连接字符串
                            SqlConnection sqlConn = new SqlConnection(scsb.ToString());
                            sqlConn.Open();
                            string sqlStr = " select * from WLN_WHRG where detail_id='" + detail_id + "' ";
                            DataTable dta = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(sqlStr, sqlConn);//从数据库中查询
                            da.Fill(dta);//将数据填充到DataSet
                            sqlConn.Close();//关闭连接
                            int numn = dta.Rows.Count;
                            if (numn == 0)
                            {
                                sqlConn.Open();
                                sqlStr = " INSERT INTO FumaCRM8.dbo.WLN_WHRG (bill_date,bill_type,create_time,custom_code,custom_name,customer_nick," +
                                   "customer_nick_type,customer_nick_type_name,discount_fee,paid_fee,post_fee,pay_type,from_trade_no,inv_no,remark,sale_man," +
                                   "service_fee,shop_name,shop_nick,shop_source,storage_code,storage_name,sum_sale,tp_tid,detail_id,goods_name,nums,sku_name," +
                                   "sku_no,sku_prop1,sku_prop2,sum_cost,detail_sum_sale,unit,indexs,codedate,spec_code,stock_code) " +
                                   "VALUES ('" + bill_date + "','" + bill_type + "','" + create_time + "','" + custom_code + "','" + custom_name + "','" + customer_nick + "'," +
                                   "'" + customer_nick_type + "','" + customer_nick_type_name + "','" + discount_fee + "','" + paid_fee + "','" + post_fee + "','" + pay_type + "'," +
                                   "'" + from_trade_no + "','" + inv_no + "','" + remark + "','" + sale_man + "','" + service_fee + "','" + shop_name + "','" + shop_nick + "'," +
                                   "'" + shop_source + "','" + storage_code + "','" + storage_name + "','" + sum_sale + "','" + tp_tid + "','" + detail_id + "','" + goods_name + "'," +
                                   "'" + nums + "','" + sku_name + "','" + sku_no + "','" + sku_prop1 + "','" + sku_prop2 + "','" + sum_cost + "','" + detail_sum_sale + "'," +
                                   "'" + unit + "','" + indexs + "','" + DateTime.Now + "','" + spec_code + "','" + stock_code + "')";
                                SqlCommand comm = new SqlCommand(sqlStr, sqlConn);//从数据库中查询                           
                                int result = comm.ExecuteNonQuery();
                                sqlConn.Close();//关闭连接
                            }
                        }
                    }

                    Thread.Sleep(100000);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(100000);
                }
            }
        }

    }
}