namespace System
{
    public static class StringExtension
    {
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}