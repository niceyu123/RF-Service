using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public class OracleManager3


    {
        static private OracleConnection conn;
        private OracleManager3()
        {
            //未更改
            string connStr = "User ID=ravo3;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";
            conn = new OracleConnection(connStr);
        }


        /// <summary>
        /// 单例模式
        /// </summary>
        static public OracleManager3 Instance = new OracleManager3();


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
