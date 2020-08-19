using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAService.MyPublic
{
    public class SqlSaveHelper
    {
        public static void SaveCPBJ(string type, string a, string b, string c, string d, string e, string f, string g, string h)
        {
            if (type == "0")//工业
            {
                OracleManager.Instance.OpenDb();
                OracleManager.Instance.CallProcCPBJ(a, b, c, d, e, f, g,h);
                OracleManager.Instance.CloseDb();
            }
            else if (type == "1")//实业
            {
                OracleManager1.Instance.OpenDb();
                OracleManager1.Instance.CallProcCPBJ(a, b, c, d, e, f, g,h);
                OracleManager1.Instance.CloseDb();
            }
            else if (type == "2")//隆威
            {
                OracleManager5.Instance.OpenDb();
                OracleManager5.Instance.CallProcCPBJ(a, b, c, d, e, f, g,h);
                OracleManager5.Instance.CloseDb();
            }
            else if (type == "3")//汇隆
            {
                OracleManager3.Instance.OpenDb();
                OracleManager3.Instance.CallProcCPBJ(a, b, c, d, e, f, g, h);
                OracleManager3.Instance.CloseDb();
            }
        }
        /// <summary>
        /// 请假
        /// </summary>
        public static void SaveQJ(string type, string a, string b, string c, string d, string e, string f, string g)
        {
            if (type == "21")
            {
                OracleManager1.Instance.OpenDb();
                OracleManager1.Instance.CallProc(a, b, c, d, e, f, g);
                OracleManager1.Instance.CloseDb();
            }
            else if (type == "23")
            {
                OracleManager.Instance.OpenDb();
                OracleManager.Instance.CallProc(a, b, c, d, e, f, g);
                OracleManager.Instance.CloseDb();
            }
            else if (type == "141")
            {
                OracleManager3.Instance.OpenDb();
                OracleManager3.Instance.CallProc(a, b, c, d, e, f, g);
                OracleManager3.Instance.CloseDb();
            }
            else if (type == "22")
            {
                OracleManager5.Instance.OpenDb();
                OracleManager5.Instance.CallProc(a, b, c, d, e, f, g);
                OracleManager5.Instance.CloseDb();
            }
            else if (type == "161")
            {
                OracleManager6.Instance.OpenDb();
                OracleManager6.Instance.CallProc(a, b, c, d, e, f, g);
                OracleManager6.Instance.CloseDb();
            }
        }
        /// <summary>
        /// 调休
        /// </summary>
        public static void SaveTX(string type,string a, string b, string c, string d, string e, string f, string g)
        {
            if (type == "21")
            {
                OracleManager1.Instance.OpenDb();
                OracleManager1.Instance.CallProcTX(a, b, c, d, e, f, g);
                OracleManager1.Instance.CloseDb();
            }
            else if (type == "23")
            {
                OracleManager.Instance.OpenDb();
                OracleManager.Instance.CallProcTX(a, b, c, d, e, f, g);
                OracleManager.Instance.CloseDb();
            }
            else if (type == "141")
            {
                OracleManager3.Instance.OpenDb();
                OracleManager3.Instance.CallProcTX(a, b, c, d, e, f, g);
                OracleManager3.Instance.CloseDb();
            }
            else if (type == "22")
            {
                OracleManager5.Instance.OpenDb();
                OracleManager5.Instance.CallProcTX(a, b, c, d, e, f, g);
                OracleManager5.Instance.CloseDb();
            }
            else if (type == "161")
            {
                OracleManager6.Instance.OpenDb();
                OracleManager6.Instance.CallProcTX(a, b, c, d, e, f, g);
                OracleManager6.Instance.CloseDb();
            }
        }
        /// <summary>
        /// 出差
        /// </summary>
        public static void SaveCC(string type, string a, string b, string c, string d, string e, string f, string g)
        {
            if (type == "21")
            {
                OracleManager1.Instance.OpenDb();
                OracleManager1.Instance.CallProcCC(a, b, c, d, e, f, g);
                OracleManager1.Instance.CloseDb();
            }
            else if (type == "23")
            {
                OracleManager.Instance.OpenDb();
                OracleManager.Instance.CallProcCC(a, b, c, d, e, f, g);
                OracleManager.Instance.CloseDb();
            }
            else if (type == "141")
            {
                OracleManager3.Instance.OpenDb();
                OracleManager3.Instance.CallProcCC(a, b, c, d, e, f, g);
                OracleManager3.Instance.CloseDb();
            }
            else if (type == "22")
            {
                OracleManager5.Instance.OpenDb();
                OracleManager5.Instance.CallProcCC(a, b, c, d, e, f, g);
                OracleManager5.Instance.CloseDb();
            }
            else if (type == "161")
            {
                OracleManager6.Instance.OpenDb();
                OracleManager6.Instance.CallProcCC(a, b, c, d, e, f, g);
                OracleManager6.Instance.CloseDb();
            }
        }
        /// <summary>
        /// 加班
        /// </summary>
        public static void SaveOT(string type, string a, string b, string c, string d, string e, string f)
        {
            if (type == "21")
            {
                OracleManager1.Instance.OpenDb();
                OracleManager1.Instance.CallProcOT(a, b, c, d, e, f);
                OracleManager1.Instance.CloseDb();
            }
            else if (type == "23")
            {
                OracleManager.Instance.OpenDb();
                OracleManager.Instance.CallProcOT(a, b, c, d, e, f);
                OracleManager.Instance.CloseDb();
            }
            else if (type == "141")
            {
                OracleManager3.Instance.OpenDb();
                OracleManager3.Instance.CallProcOT(a, b, c, d, e, f);
                OracleManager3.Instance.CloseDb();
            }
            else if (type == "22")
            {
                OracleManager5.Instance.OpenDb();
                OracleManager5.Instance.CallProcOT(a, b, c, d, e, f);
                OracleManager5.Instance.CloseDb();
            }
            else if (type == "161")
            {
                OracleManager6.Instance.OpenDb();
                OracleManager6.Instance.CallProcOT(a, b, c, d, e, f);
                OracleManager6.Instance.CloseDb();
            }
        }
    }
}