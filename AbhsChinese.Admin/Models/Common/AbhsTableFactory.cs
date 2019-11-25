using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Common
{
    public static class AbhsTableFactory
    {
        public static AbhsTableJsonResponse Create<T>(
            IEnumerable<T> data,
            int totalRecord) where T : class, new()
        {
            AbhsTableJsonResponse table = new AbhsTableJsonResponse
            {
                Data = data,
                TotalRecord = totalRecord
            };
            return table;
        }
    }
}