using System;

namespace AbhsChinese.Code.Common
{
    public static class Check
    {
        public static void IfNull(object parameterValue, string parameterName)
        {
            if (parameterValue == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void IfNullOrEmpty(string parameterName, string parameterValue)
        {
            if (string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void NotSupported(bool condition)
        {
            if (condition)
            {
                throw new NotSupportedException();
            }
        }
    }
}