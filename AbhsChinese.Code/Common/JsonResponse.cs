namespace AbhsChinese.Code.Common
{
    public class JsonResponse<T> : JsonSimpleResponse
    {
        public T Data
        {
            set; get;
        }
    }



    public class JsonSimpleResponse
    {
        public int ErrorCode
        {
            set; get;
        }

        public string ErrorMsg
        {
            set; get;
        }

        public bool State
        {
            set; get;
        }
    }
}