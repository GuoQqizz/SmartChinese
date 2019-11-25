namespace AbhsChinese.Code.Common
{
    public class SuccessJsonResponse : JsonResponse<object>
    {
        public SuccessJsonResponse(object data) : this("操作成功", data)
        {
        }

        public SuccessJsonResponse() : this("操作成功", null)
        {
        }

        public SuccessJsonResponse(string message, object data)
        {
            ErrorMsg = message;
            Data = data;
            State = true;
        }
    }
}