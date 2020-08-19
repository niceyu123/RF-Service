using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApp1.MyPublic
{
    public class ToolHelper
    {
        //时间戳转日期
        public static DateTime StampToDatetime( long TimeStamp, bool isMinSeconds = false)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));//当地时区
            //返回转换后的日期
            if (isMinSeconds)
                return startTime.AddMilliseconds(TimeStamp);
            else
                return startTime.AddSeconds(TimeStamp);
        }
    }
}
