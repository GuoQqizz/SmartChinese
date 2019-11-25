using AbhsChinese.Code.Common;

namespace AbhsChinese.Admin.Models.Common
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
}