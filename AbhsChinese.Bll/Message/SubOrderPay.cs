using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Message;
using Newtonsoft.Json;

namespace AbhsChinese.Bll.Report
{
    public class ReportOrderPaySub : ReportSubscribeBase
    {
        public ReportOrderPaySub(string msg) : base(msg)
        {
        }

        public override void Handle()
        {
            PayOrderMessage msgObject = JsonConvert.DeserializeObject<PayOrderMessage>(message);
            //TODO:
        }
    }
}
