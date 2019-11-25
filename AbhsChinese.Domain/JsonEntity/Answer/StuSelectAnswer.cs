using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Answer
{
    public class StuSelectAnswer : StudentAnswerBase
    {
        public StuSelectAnswer() : base(SubjectTypeEnum.选择题)
        {
            Answers = new List<int>();
            OptionSequences = new List<int>();
        }

        [JsonProperty("Sanw")]
        public List<int> Answers
        {
            set; get;
        }

        [JsonProperty("SOps")]
        public List<int> OptionSequences
        {
            set;get;
        }
    }
}
