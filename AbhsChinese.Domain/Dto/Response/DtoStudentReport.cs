using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoStudentReport
    {
        public DtoStudentReport()
        {
            Knowledge = new List<TopKnowledge>();
        }
        /// <summary>
        /// 学习时间
        /// </summary>
        public string StudyDate { get; set; }

        /// <summary>
        /// 获得星数
        /// </summary>
        public int ResultStars { get; set; }

        /// <summary>
        /// 获得总星数
        /// </summary>
        public int TotalStars { get; set; }

        /// <summary>
        /// 总得分（星数）
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        /// 学习时长
        /// </summary>
        public int StudyTime { get; set; }

        /// <summary>
        /// 获得金币
        /// </summary>
        public int ResultCoins { get; set; }

        /// <summary>
        /// 获得总金币数
        /// </summary>
        public int TotalCoins { get; set; }

        /// <summary>
        /// 练习题数量
        /// </summary>
        public int SubjectCount { get; set; }

        /// <summary>
        /// 优秀率（五星率）
        /// </summary>
        public double ExcellentRates { get; set; }

        /// <summary>
        /// 知识点得分
        /// </summary>
        public double KnowledgeRates { get; set; }

        public List<TopKnowledge> Knowledge { get; set; }

        public EvaluateEnum Evaluate
        {
            get
            {
                //var result = ExcellentRates / 100;
                if (KnowledgeRates <= 0.3)
                {
                    return EvaluateEnum.有待提高;
                }
                else if (KnowledgeRates <= 0.6)
                {
                    return EvaluateEnum.还不错;
                }
                else if (KnowledgeRates <= 0.8)
                {
                    return EvaluateEnum.很好;
                }
                else
                {
                    return EvaluateEnum.完美;
                }
            }
        }

        public int ImporveCount
        {
            get
            {
                return Knowledge.Sum(s => s.ChildKnowledge.Where(x => x.ResultScore <= 0.3).Count());
            }
        }

        public string ImporveKnow
        {
            get
            {
                var child = new List<ChildKnowledge>();
                foreach (var item in Knowledge)
                {
                    child.AddRange(item.ChildKnowledge.Where(s => s.ResultScore <= 0.3));
                }
                var improveKnowList = child.OrderBy(x => x.ResultScore).Select(m => m.ChildName).Take(10);
                var strKnow = improveKnowList.Count() > 10 ? string.Join("，", improveKnowList) + "..." : string.Join("，", improveKnowList);
                return strKnow;
            }
        }

        public string GoodKnow
        {
            get
            {
                var child = new List<ChildKnowledge>();
                foreach (var item in Knowledge)
                {
                    child.AddRange(item.ChildKnowledge.Where(s => s.ResultScore > 0.3));
                }
                var goodKnowList = child.OrderByDescending(x => x.ResultScore).Select(m => m.ChildName).Take(10).ToList();
                var strKnow = goodKnowList.Count() > 10 ? string.Join("，", goodKnowList) + "..." : string.Join("，", goodKnowList);
                return strKnow;
            }
        }

        public string StudyAdvice { get; set; }
    }

    public class TopKnowledge
    {
        public TopKnowledge()
        {
            ChildKnowledge = new List<ChildKnowledge>();
        }
        public int TopId { get; set; }

        public string TopName { get; set; }

        public string Path { get; set; }

        public List<ChildKnowledge> ChildKnowledge { get; set; }
    }

    public class ChildKnowledge
    {
        public int ChildId { get; set; }

        public string ChildName { get; set; }
        /// <summary>
        /// 知识点得分
        /// </summary>
        public double ResultScore { get; set; }

        /// <summary>
        /// 知识点称号描述
        /// </summary>
        public int Desc
        {
            get
            {
                if (ResultScore > 0.8)
                {
                    return (int)EvaluateEnum.完美;
                }
                else if (ResultScore > 0.6)
                {
                    return (int)EvaluateEnum.很好;
                }
                else if (ResultScore > 0.3)
                {
                    return (int)EvaluateEnum.还不错;
                }
                else
                {
                    return (int)EvaluateEnum.有待提高;
                }
            }
        }
    }
}
