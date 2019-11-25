

using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.AbhsException
{
    public class AbhsAdvancedException<T> : AbhsException
    {
        public AbhsAdvancedException(ErrorCodeEnum errorEnum, string errorMsg) : this((int)errorEnum, errorMsg)
        {

        }

        public AbhsAdvancedException(ErrorCodeEnum errorEnum, string errorMsg, params object[] inputParams)
            : this(errorEnum, string.Format(errorMsg, inputParams))
        {

        }

        public AbhsAdvancedException(int errorCode, string errorMsg)
            : base(errorCode, errorMsg)
        {
            ErrorCode = errorCode;
        }

        public AbhsAdvancedException(int errorCode, string errorMsg, T r)
         : base(errorCode, errorMsg)
        {
            ErrorCode = errorCode;
            Result = r;
        }

        public T Result
        {
            set;
            get;
        }
    }
}
