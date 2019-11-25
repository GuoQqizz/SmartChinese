using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Code.Common
{
    public class DateTool
    {
        /// <summary>   
        /// 计算本周的第一天  
        /// </summary>   
        /// <param name="someDate">该周中任意一天</param>   
        /// <returns>返回礼拜一日期，后面的具体时、分、秒和传入值相等</returns>   
        public static DateTime FirstDayOfWeek(DateTime date)
        {
            int i = date.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。   
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return date.Subtract(ts).Date;
        }

        /// <summary>   
        /// 计算本月的第一天   
        /// </summary>   
        /// <returns></returns> 
        public static DateTime FirstDayOfMonth(DateTime date)
        {
            return date.AddDays(1 - date.Day).Date;
        }

        /// <summary>
        /// 计算本年度的第一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }
    }
}
