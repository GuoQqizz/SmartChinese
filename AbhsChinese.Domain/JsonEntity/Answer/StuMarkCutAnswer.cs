using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Answer
{
    public class StuMarkCutAnswer : StudentAnswerBase
    {
        public StuMarkCutAnswer() : base(SubjectTypeEnum.圈点批注断句)
        {
            Answers = new List<int>();
        }

        [JsonProperty("Sanw")]
        public List<int> Answers
        {
            set; get;
        }
    }
}
