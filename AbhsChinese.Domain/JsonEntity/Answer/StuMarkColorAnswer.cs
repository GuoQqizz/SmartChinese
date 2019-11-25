using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Answer
{
    public class StuMarkColorAnswer : StudentAnswerBase
    {
        public StuMarkColorAnswer() : base(SubjectTypeEnum.圈点批注标色)
        {
            Answers = new List<int[]>();
        }

        [JsonProperty("Sanw")]
        public List<int[]> Answers
        {
            set; get;
        }
    }
}