using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Message
{
    public class MessageBody
    {
        public MessageTypeEnum MessageType
        {
            set; get;
        }

        public string Data
        {
            set; get;
        }
    }
}