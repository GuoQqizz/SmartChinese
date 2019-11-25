using AbhsChinese.Bll;
using AbhsChinese.Bll.Message;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Message;
using System;
using System.Collections.Generic;

namespace AbhsChinese.ReportService.Jobs
{
    public class AdvertisingStatisticJob : JobBase
    {
        private DateTime nextRunTime = new DateTime(1900, 1, 1);

        public override string JobName()
        {
            return "广告统计";
        }

        public override bool IsTimeToRun(DateTime now)
        {
            if (now >= nextRunTime)
            {
                return true;
            }
            return false;
        }

        public override void Run(DateTime now)
        {
            nextRunTime = new DateTime(1900, 1, 1);

            MessageBll messageBll = new MessageBll();
            StudentPracticeBll practiceBll = new StudentPracticeBll();

            Dictionary<int, AdvertisingNumber> dic = new Dictionary<int, AdvertisingNumber>();

            int count = 1;
            while (count <= 100)
            {
                try
                {
                    MessageBody body = messageBll.ReceiveMessageBody(MessageChannel.ADVERTISING_CHANNEL);
                    if (body != null && !string.IsNullOrEmpty(body.Data))
                    {
                        string message = body.Data;
                        string[] array = message.Split(',');
                        if (array.Length == 3)
                        {
                            int advertisingId = Convert.ToInt32(array[0]);
                            if (!dic.ContainsKey(advertisingId))
                            {
                                AdvertisingNumber action = new AdvertisingNumber(advertisingId, 0, 0);
                                dic.Add(advertisingId, action);
                            }
                            dic[advertisingId].HitNumber += Convert.ToInt32(array[1]);
                            dic[advertisingId].OrderNumber += Convert.ToInt32(array[2]);
                        }
                    }
                    else
                    {
                        nextRunTime = DateTime.Now.AddSeconds(5);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLog(this.JobName(), ex);
                }
                count++;
            }

            if (dic.Count > 0)
            {
                AdvertisingBll bll = new AdvertisingBll();

                foreach (KeyValuePair<int, AdvertisingNumber> item in dic)
                {
                    if (item.Value.HitNumber > 0)
                    {
                        bll.IncrementHitCount(item.Value.AdvertisingId, item.Value.HitNumber);
                    }
                    if (item.Value.OrderNumber > 0)
                    {
                        bll.IncrementValidCount(item.Value.AdvertisingId, item.Value.OrderNumber);
                    }
                }
            }
        }

        private class AdvertisingNumber
        {
            public AdvertisingNumber(int advertisingId, int hitNumber, int orderNumber)
            {
                AdvertisingId = advertisingId;
                HitNumber = hitNumber;
                OrderNumber = orderNumber;
            }

            public int AdvertisingId
            {
                set; get;
            }

            public int HitNumber
            {
                set; get;
            }

            public int OrderNumber
            {
                set; get;
            }
        }
    }
}

