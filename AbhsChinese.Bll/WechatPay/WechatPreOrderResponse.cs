using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll.WechatPay
{
    public class WechatPreOrderResponse
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

        public string DeviceInfo
        {
            set;
            get;
        }

        public string NonceStr
        {
            set;
            get;
        }

        public string Sign
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

        public string TradeType
        {
            set;
            get;
        }

        public string PrepayId
        {
            set;
            get;
        }


        public string CodeUrl
        {
            set;
            get;
        }

        /// <summary>
        /// 微信H5支付返回的支付链接
        /// </summary>
        public string MWebUrl
        {
            get;
            set;
        }
    }
}
