using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Code.Common
{
    public static class SqlBuilder
    {
        public static string GenerateOrderBy(
            IEnumerable<int> ids,
            string fieldNameOfId,
            out string orderbyParameters)
        {
            Check.IfNull(ids, nameof(ids));
            Check.IfNullOrEmpty(fieldNameOfId, nameof(fieldNameOfId));

            string orderbyids = "," + string.Join(",", ids) + ",";
            string orderby = " ORDER BY CHARINDEX(','+  convert(varchar(20)," + fieldNameOfId + ") +',', @OrderbyParameters)";
            orderbyParameters = orderbyids;
            return orderby;
        }
    }
}
