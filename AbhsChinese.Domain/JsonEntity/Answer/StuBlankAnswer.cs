using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Answer
{
    public class StuBlankAnswer : StudentAnswerBase
    {
        public StuBlankAnswer() : base(SubjectTypeEnum.填空题)
        {
            Answers = new List<BlankAnswerItem>();
        }

        [JsonProperty("Sanw")]
        public List<BlankAnswerItem> Answers
        {
            set; get;
        }

        public class BlankAnswerItem
        {
            [JsonProperty("Indx")]
            public int Index
            {
                set;get;
            }

            [JsonProperty("Text")]
            public string Text
            {
                set;get;
            }

            [JsonProperty("Scor")]
            public double Score
            {
                set;get;
            }
        }
    }
}