using AbhsChinese.Code.Common;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// Object对象扩展...
    /// </summary>
    public static class ObjectEx
    {
        /// <summary>
        /// 格式化分数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string _FormatScore(this decimal? obj)
        {
            if (obj == null) return "0";

            // 向上取整后比原数大 说明小数点后是有效数字 返回原始分数 否则去掉后面的小数
            decimal res = Math.Ceiling((decimal)obj);
            if (res > obj)
                return obj.ToString();
            else
                return Convert.ToInt16(obj)._ToString();
        }

        public static string _HtmlDecode(this object obj)
        {
            return System.Web.HttpUtility.HtmlDecode(obj.ToString());
        }

        public static string _HtmlEncode(this object obj)
        {
            return System.Web.HttpUtility.HtmlEncode(obj.ToString());
        }

        /// <summary>
        /// 检测字符串是否保存电话号码...
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool _IsContainsTel(this string obj)
        {
            return Tools.CheckIsTel(obj);
        }

        public static bool _ToBoolean(this object obj)
        {
            if (obj == null)
                return false;

            return Convert.ToBoolean(obj);
        }

        public static byte _ToByte(this object obj)
        {
            return SafeConvert.ToByte(obj);
        }

        public static char _ToChar(this object obj)
        {
            return SafeConvert.ToChar(obj);
        }

        public static DateTime _ToDateTime(this object obj)
        {
            return SafeConvert.ToDateTime(obj);
        }

        public static string _ToDateTimeToString(this object obj, string format)
        {
            if (obj != null)
                return obj._ToDateTime().ToString(format);
            else
                return "";
        }

        public static decimal _ToDecimal(this object obj)
        {
            return SafeConvert.ToDecimal(obj);
        }

        public static string _ToHTMLString(this object obj)
        {
            if (obj != null)
                return Tools.MyFormatDestr(obj.ToString());
            else
                return "";
        }

        public static int _ToInt32(this object obj)
        {
            return SafeConvert.ToInt32(obj);
        }

        public static string _ToString(this object obj)
        {
            return SafeConvert.ToString(obj);
        }

        public static string _ToString(this object obj, string defaultValue)
        {
            string val = SafeConvert.ToString(obj);
            if (val == "")
            {
                return defaultValue;
            }
            else
            {
                return val;
            }
        }

        public static string _ToStringByFilter(this object obj)
        {
            if (obj != null)
                return Tools.TextFilter(obj.ToString());
            else
                return "";
        }

        public static string _UrlDecodeByGB2312(this object obj)
        {
            return System.Web.HttpUtility.UrlDecode(SafeConvert.ToString(obj), Encoding.GetEncoding("gb2312"));
        }

        public static string _UrlDecodeByUTF8(this object obj)
        {
            return System.Web.HttpUtility.UrlDecode(SafeConvert.ToString(obj), Encoding.UTF8);
        }

        public static string _UrlEncodeByGB2312(this object obj)
        {
            return System.Web.HttpUtility.UrlEncode(SafeConvert.ToString(obj), Encoding.GetEncoding("gb2312"));
        }

        public static string _UrlEncodeByUTF8(this object obj)
        {
            return System.Web.HttpUtility.UrlEncode(SafeConvert.ToString(obj), Encoding.UTF8);
        }

        /// <summary>
        /// 格式化字符串...
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string text, params object[] args)
        {
            return string.Format(text, args);
        }

        /// <summary>
        /// 扩展方法，获得枚举的Description
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <param name="nameInstend">当枚举没有定义DescriptionAttribute,是否用枚举名代替，默认使用</param>
        /// <returns>枚举的Description</returns>
        public static string GetDescription(this Enum value, bool nameInstend = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null && nameInstend)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }
    }
}