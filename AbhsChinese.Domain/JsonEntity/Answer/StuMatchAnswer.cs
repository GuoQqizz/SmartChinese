using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Answer
{
    public class StuMatchAnswer : StudentAnswerBase
    {
        public StuMatchAnswer() : base(SubjectTypeEnum.连线题)
        {
            Answers = new List<int[]>();
        }

        [JsonProperty("Sanw")]
        public List<int[]> Answers
        {
            set;get;
        }

        [JsonProperty("Slos")]
        public List<int> LeftOptionSequences
        {
            set; get;
        }

        [JsonProperty("Sros")]
        public List<int> RightOptionSequences
        {
            set; get;
        }
    }
}
