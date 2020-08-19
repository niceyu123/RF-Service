using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAService.MyEntity
{
    public class OrderHeadInfo
    {
        public OrderHeadInfo()
        {
            DDH = string.Empty;
            XDRQ = string.Empty;
            ZDR = string.Empty;
            ZDRQ = string.Empty;
            KHBH = string.Empty;
            KHMC = string.Empty;
            KHDJ = string.Empty;
            KHDDH = string.Empty;
            LXR= string.Empty;
            TEL = string.Empty;
            AMT = string.Empty;
            KHJQ = string.Empty;
            BZ = string.Empty;
        }
        public string DDH { get; set; }
        public string XDRQ { get; set; }
        public string ZDR { get; set; }
        public string ZDRQ { get; set; }
        public string KHBH { get; set; }
        public string KHMC { get; set; }
        public string KHDJ { get; set; }
        public string KHDDH { get; set; }
        public string LXR { get; set; }
        public string TEL { get; set; }
        public string AMT { get; set; }
        public string KHJQ { get; set; }
        public string BZ { get; set; }
    }
}