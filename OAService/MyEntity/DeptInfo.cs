using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAService.MyEntity
{
    public class DeptInfo
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public List<DepartmentItem> department { get; set; }
    }
    public class DepartmentItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parentid { get; set; }
        public string order { get; set; }
    }
}