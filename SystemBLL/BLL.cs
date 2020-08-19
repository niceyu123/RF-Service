using System;
using System.Collections.Generic;
using System.Text;
using SystemModel;
using SystemDAL;
using System.Data;

namespace SystemBLL
{
    public class BLL
    {
        public BLL()
        {

        }
        /// <summary>
        /// 插入方法
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        public static int InsertData(LoginModel M)
        {
            string sql = "INSERT INTO Login VALUES(@LoginName,@LoginPassword)";
            try
            {
                DAL dal = new DAL();
                return DAL.ExecuteNonQuery(sql);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        /// <summary>
        /// 修改方法
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        public static int UpdateTData(LoginModel M)
        {
            string sql = "UPDATE Login SET LoginName=@LoginName,LoginPassword=@LoginPassword WHERE ID=@ID";
            try
            {
                DAL dal = new DAL();
                return DAL.ExecuteNonQuery(sql);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        public static int DeleteData(LoginModel M)
        {
            string sql = "DELETE  FROM Login WHERE ID=@ID";
            try
            {
                DAL dal = new DAL();
                return DAL.ExecuteNonQuery(sql);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        public static DataTable Login(string LoginName, string LoginPassword)
        {
            string sql = "SELECT * FROM LOGINMODEL WHERE LoginName='" + LoginName+"' AND LoginPassword='"+LoginPassword+"'";
            try
            {
                DAL dal = new DAL();
                return DAL.ExecuteDataTable(sql);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        public static DataTable GetUser(LoginModel M)
        {
            try
            {
                string sql = "SELECT * FROM Login";
                DAL dal = new DAL();
                return DAL.ExecuteDataTable(sql);
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        /// <summary>
        /// 查询单个用户
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        public static DataTable GetUserID(LoginModel M)
        {
            try
            {
                string sql = "SELECT * FROM Login WHERE ID=@ID";
                DAL dal = new DAL();
                return DAL.ExecuteDataTable(sql);
            }
            catch (Exception E)
            {

                throw E;
            }
        }
    }
}
