using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAService.MyEntity
{
    public class UpdateDptInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string name_en { get; set; }
        public int parentid { get; set; }
        public int order { get; set; }
    }
}