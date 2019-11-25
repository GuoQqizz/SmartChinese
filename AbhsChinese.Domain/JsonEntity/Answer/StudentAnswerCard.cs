using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonEntity.Answer
{
    /// <summary>
    /// 学生一页的答题结果
    /// </summary>
    public class StudentAnswerCard
    {
        public StudentAnswerCard()
        {
            AnswerCollection = new List<StudentAnswerBase>();
        }
        /// <summary>
        /// 使用时间
        /// </summary>
        [JsonProperty("UTim")]
        public int UseTime
        {
            set; get;
        }
        /// <summary>
        /// 提交时间
        /// </summary>
        [JsonProperty("STim")]
        public string SubmitTime
        {
            set; get;
        }

        /// <summary>
        /// 获得的总星数 
        /// </summary>
        [JsonProperty("EStar")]
        public int TotalStars
        {
            set;get;
        }

        /// <summary>
        /// 获得的总金币数
        /// </summary>
        [JsonProperty("TCoin")]
        public int TotalCoins
        {
            set; get;
        }
        /// <summary>
        /// 页码id
        /// </summary>
        [JsonProperty("Part")]
        public int Part
        {
            set; get;
        }

        [JsonProperty("Anws")]
        public List<StudentAnswerBase> AnswerCollection
        {
            set; get;
        }
    }
}
