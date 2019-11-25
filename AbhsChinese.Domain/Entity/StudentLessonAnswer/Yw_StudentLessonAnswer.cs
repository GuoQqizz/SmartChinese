using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_StudentLessonAnswer")]
    public class Yw_StudentLessonAnswer : EntityBase
    {
        [Key]
        public int Yla_Id { get; set; }

        private int yla_StudentId;
        /// <summary>
        /// 学生id
        /// </summary>
        public int Yla_StudentId
        {
            set
            {
                AuditCheck("Yla_StudentId", value);
                yla_StudentId = value;
            }
            get
            {
                return yla_StudentId;
            }
        }

        private int yla_CourseId;
        /// <summary>
        /// 课程id
        /// </summary>
        public int Yla_CourseId
        {
            set
            {
                AuditCheck("Yla_CourseId", value);
                yla_CourseId = value;
            }
            get
            {
                return yla_CourseId;
            }
        }

        private int yla_LessonId;
        /// <summary>
        /// 课时id
        /// </summary>
        public int Yla_LessonId
        {
            set
            {
                AuditCheck("Yla_LessonId", value);
                yla_LessonId = value;
            }
            get
            {
                return yla_LessonId;
            }
        }

        private int yla_LessonProgressId;
        /// <summary>
        /// 课时进度id
        /// </summary>
        public int Yla_LessonProgressId
        {
            set
            {
                AuditCheck("Yla_LessonProgressId", value);
                yla_LessonProgressId = value;
            }
            get
            {
                return yla_LessonProgressId;
            }
        }

        private string yla_StudentAnswer;
        /// <summary>
        /// 学生答案json
        /// </summary>
        public string Yla_StudentAnswer
        {
            set
            {
                AuditCheck("Yla_StudentAnswer", value);
                yla_StudentAnswer = value;
            }
            get
            {
                return yla_StudentAnswer;
            }
        }

        private string yla_StudentCoin;
        /// <summary>
        /// 学生学习的金币数据记录
        /// </summary>
        public string Yla_StudentCoin
        {
            set
            {
                AuditCheck("Yla_StudentCoin", value);
                yla_StudentCoin = value;
            }
            get
            {
                return yla_StudentCoin;
            }
        }

        private DateTime yla_CreateTime;
        public DateTime Yla_CreateTime
        {
            set
            {
                AuditCheck("Yla_CreateTime", value);
                yla_CreateTime = value;
            }
            get
            {
                return yla_CreateTime;
            }
        }

        private DateTime yla_UpdateTime;
        public DateTime Yla_UpdateTime
        {
            set
            {
                AuditCheck("Yla_UpdateTime", value);
                yla_UpdateTime = value;
            }
            get
            {
                return yla_UpdateTime;
            }
        }
    }
}
