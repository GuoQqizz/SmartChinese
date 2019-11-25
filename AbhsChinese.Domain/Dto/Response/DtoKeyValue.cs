using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoKeyValue<T1, T2>
    {
      
        public T1 key { get; set; }
        
        public T2 value { get; set; }
    }
}
