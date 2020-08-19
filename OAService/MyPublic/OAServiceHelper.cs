using OAService.MyEntity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAService.MyPublic
{
    public class OAServiceHelper
    {
        /// <summary>
        /// 获取订单信息,并存入数据库
        /// </summary>
        /// <param name="orderListInfo"></param>
        /// <returns></returns>
        public static bool GetOrderHeadList(OrderHeadInfo orderHeadInfo)
        {
            if (orderHeadInfo.DDH != string.Empty && orderHeadInfo.KHDJ != string.Empty)
            {
                //先判断传过来的订单是否已存在 
                //已存在返回true 不存在添加数据库 添加成功返回true 添加失败返回false
                string connString = "User ID=ecology;Password=ecology;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.16)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ecology)))";
                OracleConnection conn = new OracleConnection(connString);
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM ORDER_HEAD WHERE DDH='" + orderHeadInfo.DDH + "'";
                        decimal i = (decimal)cmd.ExecuteScalar();
                        double amt = Convert.ToDouble(orderHeadInfo.AMT);
                        //不存在就添加
                        if (i == 0)
                        {
                            cmd.CommandText = "INSERT INTO ORDER_HEAD(DDH,XDRQ,ZDR,ZDRQ,KHBH,KHMC,KHDJ,KHDDH,LXR,TEL,AMT,KHJQ,BZ) " +
                                " VALUES ('" + orderHeadInfo.DDH + "',to_date('" + orderHeadInfo.XDRQ + "','yyyy-MM-dd HH24:mi:ss'),'" + orderHeadInfo.ZDR + "',to_date('" + orderHeadInfo.ZDRQ + "','yyyy-MM-dd HH24:mi:ss'),'" + orderHeadInfo.KHBH + "','" + orderHeadInfo.KHMC + "','" + orderHeadInfo.KHDJ + "','" + orderHeadInfo.KHDDH + "','" + orderHeadInfo.LXR + "','" + orderHeadInfo.TEL + "','" + amt + "',to_date('" + orderHeadInfo.KHJQ + "','yyyy-MM-dd HH24:mi:ss'),'" + orderHeadInfo.BZ + "')";
                            cmd.ExecuteNonQuery();
                        }
                        //存在就修改
                        else
                        {
                            cmd.CommandText = "UPDATE ORDER_HEAD SET XDRQ=to_date('" + orderHeadInfo.XDRQ + "','yyyy-MM-dd HH24:mi:ss'),ZDR='" + orderHeadInfo.ZDR + "',ZDRQ=to_date('" + orderHeadInfo.ZDRQ + "','yyyy - MM - dd HH24: mi: ss'),KHBH='" + orderHeadInfo.KHBH + "',KHMC='" + orderHeadInfo.KHMC + "',KHDJ='" + orderHeadInfo.KHDJ + "',KHDDH='" + orderHeadInfo.KHDDH + "',LXR='" + orderHeadInfo.LXR + "',TEL='" + orderHeadInfo.TEL + "',AMT='" + amt + "',KHJQ=to_date('" + orderHeadInfo.KHJQ + "','yyyy-MM-dd HH24:mi:ss'),BZ='" + orderHeadInfo.BZ + "' WHERE DDH='" + orderHeadInfo.DDH + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    return false;
                }
                finally
                {
                    conn.Close();
                }
                return true;
            }
            else
            {
                //部分值=null
                ToolHelper.logger.Debug("部分字段为空");
                return false;
            }
           
        }
        public static bool GetOrderDetailList(OrderDetailInfo orderDetailInfo)
        {
            if (orderDetailInfo.DDH != string.Empty && orderDetailInfo.XH != string.Empty)
            {
                //先判断传过来的订单是否已存在 
                //已存在返回true 不存在添加数据库 添加成功返回true 添加失败返回false
                string connString = "User ID=ecology;Password=ecology;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.16)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ecology)))";
                OracleConnection conn = new OracleConnection(connString);
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM ORDER_DETAIL where XH='" + orderDetailInfo.XH + "'";
                        decimal i = (decimal)cmd.ExecuteScalar();
                        if (i == 0)
                        {
                            cmd.CommandText = "INSERT INTO ORDER_DETAIL(DDH,XH,CPBH,CPMC,GGXH,DW,XSSL,XSDJ,ZK,CJDJ,CJJE,ZXL,C,K,G,TJ,JZ,MZ,BZ)" +
                                " VALUES ('" + orderDetailInfo.DDH + "','" + orderDetailInfo.XH + "','" + orderDetailInfo.CPBH + "','" + orderDetailInfo.CPMC + "','" + orderDetailInfo.GGXH + "','" + orderDetailInfo.DW + "','" + orderDetailInfo.XSSL + "','" + orderDetailInfo.XSDJ + "','" + orderDetailInfo.ZK + "','" + orderDetailInfo.CJDJ + "','" + orderDetailInfo.CJJE + "','" + orderDetailInfo.ZXL + "','" + orderDetailInfo.C + "','" + orderDetailInfo.K + "','" + orderDetailInfo.G + "','" + orderDetailInfo.TJ + "','" + orderDetailInfo.JZ + "','" + orderDetailInfo.MZ + "','" + orderDetailInfo.BZ + "')";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = "UPDATE ORDER_DETAIL SET DDH='" + orderDetailInfo.DDH + "" +
                                "',CPBH='" + orderDetailInfo.CPBH +
                                "',CPMC='" + orderDetailInfo.CPMC +
                                "',GGXH='" + orderDetailInfo.GGXH +
                                "',DW='" + orderDetailInfo.DW +
                                "',XSSL='" + orderDetailInfo.XSSL +
                                "',XSDJ='" + orderDetailInfo.XSDJ +
                                "',ZK='" + orderDetailInfo.ZK +
                                "',CJDJ='" + orderDetailInfo.CJDJ +
                                "',CJJE='" + orderDetailInfo.CJJE +
                                "',ZXL='" + orderDetailInfo.ZXL +
                                "',C='" + orderDetailInfo.C +
                                "',K='" + orderDetailInfo.K +
                                "',G='" + orderDetailInfo.G +
                                "',TJ='" + orderDetailInfo.TJ +
                                "',JZ='" + orderDetailInfo.JZ +
                                "',MZ='" + orderDetailInfo.MZ +
                                "',BZ='" + orderDetailInfo.BZ +
                                "' WHERE XH='" + orderDetailInfo.XH + "'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ToolHelper.logger.Debug(ex.ToString());
                    return false;
                }
                finally
                {
                    conn.Close();
                }
                return true;
            }
            else
            {
                ToolHelper.logger.Debug("部分字段为空");
                return false;
            }
            
        }
    }
}