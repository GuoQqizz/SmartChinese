using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Message
{
    public class PayOrderMessage
    {
        public int StudentId
        {
            set;get;
        }

        public string OrderNo
        {
            set;get;
        }
    }
}
