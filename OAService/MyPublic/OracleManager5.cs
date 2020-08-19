using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OAService.MyPublic
{
    public class OracleManager5


    {
        static private OracleConnection conn;
        private OracleManager5()
        {
            //未更改
            string connStr = "User ID=ravo5;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            conn = new OracleConnection(connStr);
        }


        /// <summary>
        /// 单例模式
        /// </summary>
        static public OracleManager5 Instance = new OracleManager5();


        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void OpenDb()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
            }
            catch (OracleException ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseDb()
        {
            try
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        //产品报价
        public void CallProcCPBJ(string OAID, string CPMC, string CPLX, string GYMS, string BZYQ, string TSYQ, string MANY, string SQR)
        {
            try
            {
                OracleCommand orc = conn.CreateCommand();
                orc.CommandType = CommandType.StoredProcedure;
                orc.CommandText = "P_ECOLOGY_TBBJD";
                orc.Parameters.Add("OAID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["OAID"].Value = OAID;

                orc.Parameters.Add("CPMC", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["CPMC"].Value = CPMC;

                orc.Parameters.Add("CPLX", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["CPLX"].Value = CPLX;

                orc.Parameters.Add("GYMS", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["GYMS"].Value = GYMS;

                orc.Parameters.Add("BZYQ", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["BZYQ"].Value = BZYQ;

                orc.Parameters.Add("TSYQ", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["TSYQ"].Value = TSYQ;

                orc.Parameters.Add("MANY", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["MANY"].Value = MANY;

                orc.Parameters.Add("SQR", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["SQR"].Value = SQR;

                orc.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

            }
        }
        //请假
        public void CallProc(string MANID, string LEAVETYPE, string LEAVEDAY, string ENDTIME, string LEAVENUM, string REST, string OANO)
        {
            try
            {
                OracleCommand orc = conn.CreateCommand();
                orc.CommandType = CommandType.StoredProcedure;
                orc.CommandText = "KAOQIN_LEAVE_OA_TB";
                orc.Parameters.Add("MANID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["MANID"].Value = MANID;

                orc.Parameters.Add("LEAVETYPE", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["LEAVETYPE"].Value = LEAVETYPE;

                orc.Parameters.Add("LEAVEDAY", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["LEAVEDAY"].Value = LEAVEDAY;

                orc.Parameters.Add("ENDTIME", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["ENDTIME"].Value = ENDTIME;

                orc.Parameters.Add("LEAVENUM", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["LEAVENUM"].Value = LEAVENUM;

                orc.Parameters.Add("REST", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["REST"].Value = REST;

                orc.Parameters.Add("OANO", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["OANO"].Value = OANO;
                orc.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

            }
        }
        //调休
        public void CallProcTX(string MANID, string LEAVETYPE, string LEAVEDAY, string ENDTIME, string LEAVENUM, string REST, string OANO)
        {
            try
            {
                OracleCommand orc = conn.CreateCommand();
                orc.CommandType = CommandType.StoredProcedure;
                orc.CommandText = "KAOQIN_TX_OA_TB";
                orc.Parameters.Add("MANID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["MANID"].Value = MANID;

                orc.Parameters.Add("LEAVETYPE", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["LEAVETYPE"].Value = LEAVETYPE;

                orc.Parameters.Add("LEAVEDAY", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["LEAVEDAY"].Value = LEAVEDAY;

                orc.Parameters.Add("ENDTIME", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["ENDTIME"].Value = ENDTIME;

                orc.Parameters.Add("LEAVENUM", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["LEAVENUM"].Value = LEAVENUM;

                orc.Parameters.Add("REST", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["REST"].Value = REST;

                orc.Parameters.Add("OANO", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["OANO"].Value = OANO;
                orc.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

            }
        }
        //出差
        public void CallProcCC(string MANID, string LEAVETYPE, string LEAVEDAY, string ENDTIME, string LEAVENUM, string REST, string OANO)
        {
            try
            {
                OracleCommand orc = conn.CreateCommand();
                orc.CommandType = CommandType.StoredProcedure;
                orc.CommandText = "KAOQIN_CC_OA_TB";
                orc.Parameters.Add("MANID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["MANID"].Value = MANID;

                orc.Parameters.Add("LEAVETYPE", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["LEAVETYPE"].Value = LEAVETYPE;

                orc.Parameters.Add("LEAVEDAY", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["LEAVEDAY"].Value = LEAVEDAY;

                orc.Parameters.Add("ENDTIME", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["ENDTIME"].Value = ENDTIME;

                orc.Parameters.Add("LEAVENUM", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["LEAVENUM"].Value = LEAVENUM;

                orc.Parameters.Add("REST", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["REST"].Value = REST;

                orc.Parameters.Add("OANO", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["OANO"].Value = OANO;
                orc.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                ToolHelper.logger.Debug(ex.ToString);
            }
        }
        //加班
        public void CallProcOT(string MANID, string STARTTIME, string ENDTIME, string OANO, string TIMES, string REST)
        {
            try
            {
                OracleCommand orc = conn.CreateCommand();
                orc.CommandType = CommandType.StoredProcedure;
                orc.CommandText = "KAOQIN_OT_OA_TB";
                orc.Parameters.Add("MANID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["MANID"].Value = MANID;

                orc.Parameters.Add("STARTTIME", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["STARTTIME"].Value = STARTTIME;

                orc.Parameters.Add("ENDTIME", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["ENDTIME"].Value = ENDTIME;

                orc.Parameters.Add("OANO", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["OANO"].Value = OANO;

                orc.Parameters.Add("TIMES", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["TIMES"].Value = TIMES;

                orc.Parameters.Add("REST", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                orc.Parameters["REST"].Value = REST;
                orc.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}