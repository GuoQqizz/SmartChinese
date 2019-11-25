using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Code.Extension
{
    public static class DateTimeExtensions
    {
        public static DateTime DefaultDateTime => new DateTime(1900, 1, 1);

        public static bool IsDefaultTime(this DateTime dt)
        {
            return dt == DateTime.MinValue || dt == DefaultDateTime;
        }

        public static string ToTimestamp(this DateTime time)
        {
            DateTime startTime =
                TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));//当地时区
            long timeStamp = (long)(time - startTime).TotalMilliseconds;//相差毫秒数

            return timeStamp.ToString();
        }
    }
}
