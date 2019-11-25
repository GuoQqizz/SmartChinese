using AbhsChinese.Domain.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Answer
{
    public abstract class StudentAnswerBase
    {
        public StudentAnswerBase(SubjectTypeEnum type)
        {
            Type = (int)type;
        }
        /// <summary>
        /// 题目类型
        /// </summary>
        [JsonProperty("Type")]
        public int Type
        {
            set;get;
        }
        /// <summary>
        /// 题目id
        /// </summary>
        [JsonProperty("SbId")]
        public int SubjectId
        {
            set;get;
        }
        /// <summary>
        /// 得到星数
        /// </summary>
        [JsonProperty("Rtar")]
        public int ResultStars
        {
            set;get;
        }
        /// <summary>
        /// 题目金币(第二次学习时就为0)
        /// </summary>
        [JsonProperty("SCon")]
        public int SubjectCoins
        {
            set;get;
        }
        /// <summary>
        /// 获得金币
        /// </summary>
        [JsonProperty("RCon")]
        public int ResultCoins
        {
            set;get;
        }

        /// <summary>
        /// 知识点id
        /// </summary>
        [JsonProperty("Kdge")]
        public int KnowledgeId
        {
            set;get;
        }
        /// <summary>
        /// 是否正确
        /// </summary>
        [JsonProperty("IsRt")]
        public bool IsRight
        {
            get
            {
                return ResultStars == 5;
            }
        }
    }
}
