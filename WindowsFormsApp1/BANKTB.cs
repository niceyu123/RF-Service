using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public class BANKTB
    {
        public string BANKID { get; set; }
        public string BANKNAME { get; set; }
        public string MYIDCODE { get; set; }
        public string LIFECODE { get; set; }
        public string BEGINDAY { get; set; }
        public string ENDDAY { get; set; }
        public string BZ { get; set; }
        public string BANKFLAG { get; set; }

        public BANKTB() { }

        public BANKTB(string BANKID, string BANKNAME, string MYIDCODE, string LIFECODE, 
            string BEGINDAY, string ENDDAY, string BZ, string BANKFLAG)
        {
            this.BANKID = BANKID;
            this.BANKNAME = BANKNAME;
            this.MYIDCODE = MYIDCODE;
            this.LIFECODE = LIFECODE;
            this.BEGINDAY = BEGINDAY;
            this.ENDDAY = ENDDAY;
            this.BZ = BZ;
            this.BANKFLAG = BANKFLAG;
        }
    }
}
