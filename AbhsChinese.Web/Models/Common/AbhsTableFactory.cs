using AbhsChinese.Code.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.Common
{
    public class AbhsTableJsonResponse : JsonResponse<object>
    {
        public new bool State { get; private set; }
        public int TotalRecord { get; set; }

        public AbhsTableJsonResponse()
        {
            State = true;
        }

        public AbhsTableJsonResponse(object data, int totalRecord)
        {
            State = true;
            Data = data;
            TotalRecord = totalRecord;
        }
    }
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