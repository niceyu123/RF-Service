using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace SystemDAL
{
    public class DAL
    {
        //连接字符串
        public static string connectionString = "User ID=ravo2;Password=loginserver;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.11.113)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = ravoerp)))";

        public DAL()
        {
            //connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString();
        }

        /// <summary>
        /// 执行单条语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            int x = 0;
            try
            {
                // Open a connection to the DB.
                OracleConnection connOra = new OracleConnection(connectionString);
                connOra.Open();
                OracleTransaction tran = connOra.BeginTransaction();
                // Create a command to execute the sql statement.

                OracleCommand cmdOra = connOra.CreateCommand();
                cmdOra.CommandText = sql;

                x = cmdOra.ExecuteNonQuery();

                tran.Commit();

                connOra.Close();
                connOra.Dispose();
                cmdOra.Dispose();
            }
            catch (Exception ex)
            {
                //log.Error(ex.StackTrace);
            }
            return x;

        }

        public static DataTable ExecuteDataTable(String cmdText)
        {
            DataTable dt = new DataTable();
            //DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

            try
            {
                // Open a connection to the DB.
                //DbConnection connOra = factory.CreateConnection();
                OracleConnection connOra = new OracleConnection(connectionString);
                //connOra.ConnectionString = connectionString;
                connOra.Open();

                // Create a command to execute the sql statement.
                //DbCommand cmd = factory.CreateCommand();

                OracleCommand cmd = connOra.CreateCommand();
                cmd.CommandText = cmdText;

                OracleDataAdapter ada = new OracleDataAdapter();
                //DbDataAdapter ada = factory.CreateDataAdapter();// new OracleDataAdapter(cmd1);                
                ada.SelectCommand = cmd;
                ada.Fill(dt);

                connOra.Close();
                connOra.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            return dt;

        }

    }
}
