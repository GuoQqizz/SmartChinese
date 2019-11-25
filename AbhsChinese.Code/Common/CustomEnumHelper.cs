using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AbhsChinese.Code.Common
{
    public static class CustomEnumHelper
    {
        public static IDictionary<int, string> GetElements(Type enumType)
        {
            Check.IfNull(enumType, nameof(enumType));

            IDictionary<int, string> elements = new Dictionary<int, string>();
            string[] names = Enum.GetNames(enumType);
            foreach (var name in names)
            {
                int value = (int)Enum.Parse(enumType, name);
                elements.Add(value, name);
            }
            return elements;
        }

        public static string Parse(Type enumType, int? value)
        {
            if (value == null)
            {
                return Enum.GetNames(enumType).FirstOrDefault();
            }

            int realValue = value.Value;
            string result = Enum.GetName(enumType, realValue);
            return result;
        }

        public static IDictionary<int, string> ParseBinaryAnd(Type enumType, int value)
        {
            IDictionary<int, string> elements = new Dictionary<int, string>();

            string[] names = Enum.GetNames(enumType);
            foreach (var name in names)
            {
                int tmp = (int)Enum.Parse(enumType, name);
                if ((tmp & value) == tmp)
                {
                    elements.Add(tmp, name);
                }
            }
            return elements;
        }

        public static string Description(this Enum e)
        {
            object[] attributes = e.GetType().GetField(e.ToString()).GetCustomAttributes(false);
            if (attributes.Count() > 0)
            {
                return (attributes.First() as DescriptionAttribute).Description;

            }
            return string.Empty;
        }
    }
}