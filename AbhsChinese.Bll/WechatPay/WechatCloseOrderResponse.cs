using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Bll.WechatPay
{
    public class WechatCloseOrderResponse
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
        public string ResultMsg
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
    }
}
