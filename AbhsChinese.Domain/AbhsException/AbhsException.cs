
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.AbhsException
{
    public class AbhsException : Exception
    {
        private int errorCode;
        public int ErrorCode
        {
            set
            {
                errorCode = value;
            }
            get
            {
                return errorCode;
            }
        }

        public ErrorCodeEnum errorCodeEnum;
        public ErrorCodeEnum ErrorCodeEnum
        {
            set
            {
                errorCodeEnum = value;
                errorCode = (int)(value);
            }
            get
            {
                return errorCodeEnum;
            }
        }


        public AbhsException(ErrorCodeEnum errorEnum,string errorMsg):this((int)errorEnum,errorMsg)
        {

        }

        public AbhsException(ErrorCodeEnum errorEnum,string errorMsg, params object[] inputParams)
            : this(errorEnum, string.Format(errorMsg, inputParams))
        {

        }

        public AbhsException(int errorCode, string errorMsg)
            : base(errorMsg)
        {
            ErrorCode = errorCode;
        }
    }
}
