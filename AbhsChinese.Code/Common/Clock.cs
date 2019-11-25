using AbhsChinese.Code.Extension;
using System;

namespace AbhsChinese.Code.Common
{
    public static class Clock
    {
        public static DateTime MinValue { get { return DateTimeExtensions.DefaultDateTime; } }
        public static DateTime Now { get { return DateTime.Now; } }
        public static string Format { get { return "yyyy-MM-dd HH:mm:ss"; } }
    }
}