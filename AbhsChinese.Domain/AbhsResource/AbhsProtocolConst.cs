using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.AbhsResource
{
    public static class AbhsProtocolConst
    {
        /// <summary>
        /// 协议错误返回的Httpcode
        /// </summary>
        public static readonly System.Net.HttpStatusCode HttpCode_CustomError = (System.Net.HttpStatusCode)440;
        
        /// <summary>
        /// 服务端返回错误代码的header名称
        /// </summary>
        public static readonly string ERROR_HEADER_NAME = "x-error-code";
    }
}
