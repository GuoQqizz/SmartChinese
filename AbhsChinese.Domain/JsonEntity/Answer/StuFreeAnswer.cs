using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Answer
{
    public class StuFreeAnswer : StudentAnswerBase
    {
        public StuFreeAnswer() : base(SubjectTypeEnum.主观题)
        {
        }

        [JsonProperty("Sanw")]
        public string Answer
        {
            set;get;
        }
    }
}
