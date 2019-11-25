using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Domain.JsonEntity.Coin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request.Student
{
    /// <summary>
    /// 学生课时单元学习提交数据
    /// </summary>
    public class DtoStudnetUnitSubmit
    {
        /// <summary>
        /// 学生id
        /// </summary>
        public int StudnetId { get; set; }

        /// <summary>
        /// 课程id
        /// </summary>
        public int CoruseId { get; set; }

        /// <summary>
        /// 课时id
        /// </summary>
        public int LessonId { get; set; }

        /// <summary>
        /// 课时序号
        /// </summary>
        public int LessonNum { get; set; }

        /// <summary>
        /// 课时id
        /// </summary>
        public int UnitID { get; set; }

        /// <summary>
        /// 课时序号
        /// </summary>
        public int UnitNum { get; set; }

        /// <summary>
        /// 总页码数
        /// </summary>
        public int TotalUnitNum { get; set; }

        /// <summary>
        /// 学习时间(秒)
        /// </summary>
        public int UseTime { get; set; }

        /// <summary>
        /// 最大金币数
        /// </summary>
        public int AllCoin { get; set; }

        /// <summary>
        /// 课时进度id
        /// </summary>
        public int ProgressID { get; set; }

        /// <summary>
        /// 课时进度秘钥
        /// </summary>
        public string ProgressKey { get; set; }

        /// <summary>
        /// 答题结果对象
        /// </summary>
        public List<StudentAnswerBase> Answers { get; set; } = new List<StudentAnswerBase>();

        /// <summary>
        /// 金币结果对象
        /// </summary>
        public List<StuLessonCoinItem> Coins { get; set; } = new List<StuLessonCoinItem>();
    }
}
