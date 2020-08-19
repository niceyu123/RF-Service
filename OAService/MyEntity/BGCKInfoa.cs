using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAService.MyEntity
{
    public class BGCKInfoa
    {
        public List<BGCKInfob> BGCKInfoc { get; set; }
        
    }
    public class BGCKInfob
    {
        public string oano { get; set; }
        public string sqr { get; set; }
        public string sqbm { get; set; }
        public string sqrq { get; set; }
        public int xh { get; set; }
        public int zs { get; set; }
        public string wlid { get; set; }
        public string xqsl { get; set; }
        public string dwid { get; set; }
        public string cid { get; set; }
    }
}