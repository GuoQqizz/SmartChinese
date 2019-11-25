using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenDu.ZhiBo.BLL.WechatPay
{
    public class WechatPayNotifyResponse
    {
        public string ReturnCode
        {
            set;
            get;
        }

        public string ReturnMsg
        {
            set;
            get;
        }

        public string AppId
        {
            set;
            get;
        }

        public string MchId
        {
            set;
            get;
        }

        public string ResultCode
        {
            set;
            get;
        }

        public string ErrCode
        {
            set;
            get;
        }

        public string ErrCodeDes
        {
            set;
            get;
        }

        public string OpenId
        {
            set;
            get;
        }

        public string TradeType
        {
            set;
            get;
        }

        public int TotalFee
        {
            set;
            get;
        }

        public string TransactionId
        {
            set;
            get;
        }


        public string OutTradeNo
        {
            set;
            get;
        }

        public bool IsSuccess
        {
            set;
            get;
        }

        public string OrderNumber
        {
            set;
            get;
        }
    }
}
