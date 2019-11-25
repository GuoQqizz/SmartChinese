using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.Message;
using AbhsChinese.Domain.AbhsResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll.Report
{
    public abstract class ReportSubscribeBase
    {
        protected string message;
        public ReportSubscribeBase(string msg)
        {
            this.message = msg;
        }
        public abstract void Handle();
    }

    public class ReportHandlerFactory
    {
        public static ReportSubscribeBase Create(MessageBody message)
        {
            if (message.MessageType== MessageTypeEnum.课程学习)
            {
                return new CourseStudyReport(message.Data);
            }
            else if (message.MessageType == MessageTypeEnum.课后任务)
            {
                return new TaskStudyReport(message.Data);
            }
            else if (message.MessageType == MessageTypeEnum.课后练习)
            {
                return new PractiseStudyReport(message.Data);
            }
            //else if (message.MessageType == MessageTypeEnum.订单支付)
            //{
            //    return new ReportOrderPaySub(message.Data);
            //}
            throw new AbhsException(ErrorCodeEnum.NotImplementLogic, AbhsErrorMsg.ConstNotImplementLogic);
        }
    }
}
