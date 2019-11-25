using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Answer
{
    public class StuTrueFalseAnswer : StudentAnswerBase
    {
        public StuTrueFalseAnswer() : base(SubjectTypeEnum.判断题)
        {
           
        }

        [JsonProperty("Sanw")]
        public int Answer
        {
            set;get;
        }
    }
}
