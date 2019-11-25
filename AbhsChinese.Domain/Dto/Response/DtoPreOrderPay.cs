using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoPreOrderPay
    {
        public bool State { get; set; }
        public bool IsFreeOrder { get; set; }
        public string Base64Img { get; set; }

        public string ErrorMsg { get; set; }
    }
}
