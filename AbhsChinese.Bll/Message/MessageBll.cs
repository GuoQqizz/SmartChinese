using AbhsChinese.Bll.Report;
using AbhsChinese.Code.Cache;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Message;
using AbhsChinese.Repository.IRepository;
using AbhsChinese.Repository.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll
{
    public class MessageBll : BllBase
    {
        public MessageBll() : base()
        {

        }

        public void PublishMessage(string messageChannel, MessageTypeEnum messageType, string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                MessageBody body = new MessageBody() { MessageType = messageType, Data = msg };
                RedisCache.Instance.EnQueue(messageChannel, JsonConvert.SerializeObject(body));
            }
        }

        public string ReceiveMessage(string messageChannel)
        {
            return RedisCache.Instance.DeQueue(messageChannel);
        }

        public MessageBody ReceiveMessageBody(string messageChannel)
        {
            string msg = RedisCache.Instance.DeQueue(messageChannel);
            if (!string.IsNullOrEmpty(msg))
            {
                MessageBody msgBody = JsonConvert.DeserializeObject<MessageBody>(msg);
                return msgBody;
            }

            return null;
        }
    }
}
