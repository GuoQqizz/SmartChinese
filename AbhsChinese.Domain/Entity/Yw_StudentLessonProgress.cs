using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_StudentLessonProgress")]
    public class Yw_StudentLessonProgress : EntityBase
    {
        [Key]
        public int Yle_Id { get; set; }

        private int yle_StudentId;
        /// <summary>
        /// 学生id
        /// </summary>
        public int Yle_StudentId
        {
            set
            {
                AuditCheck("Yle_StudentId", value);
                yle_StudentId = value;
            }
            get
            {
                return yle_StudentId;
            }
        }

        private int yle_CourseId;
        /// <summary>
        /// 课程id
        /// </summary>
        public int Yle_CourseId
        {
            set
            {
                AuditCheck("Yle_CourseId", value);
                yle_CourseId = value;
            }
            get
            {
                return yle_CourseId;
            }
        }

        private int yle_LessonId;
        /// <summary>
        /// 课时id
        /// </summary>
        public int Yle_LessonId
        {
            set
            {
                AuditCheck("Yle_LessonId", value);
                yle_LessonId = value;
            }
            get
            {
                return yle_LessonId;
            }
        }

        private int yle_LessonIndex;
        /// <summary>
        /// 课时序号
        /// </summary>
        public int Yle_LessonIndex
        {
            set
            {
                AuditCheck("Yle_LessonIndex", value);
                yle_LessonIndex = value;
            }
            get
            {
                return yle_LessonIndex;
            }
        }

        private DateTime yle_StartStudyTime;
        /// <summary>
        /// 开始学习时间
        /// </summary>
        public DateTime Yle_StartStudyTime
        {
            set
            {
                AuditCheck("Yle_StartStudyTime", value);
                yle_StartStudyTime = value;
            }
            get
            {
                return yle_StartStudyTime;
            }
        }

        private DateTime yle_FinishStudyTime;
        /// <summary>
        /// 结束学习时间
        /// </summary>
        public DateTime Yle_FinishStudyTime
        {
            set
            {
                AuditCheck("Yle_FinishStudyTime", value);
                yle_FinishStudyTime = value;
            }
            get
            {
                return yle_FinishStudyTime;
            }
        }

        private int yle_UnitIndex;
        /// <summary>
        /// 开始学习单元
        /// </summary>
        public int Yle_UnitIndex
        {
            set
            {
                AuditCheck("Yle_UnitIndex", value);
                yle_UnitIndex = value;
            }
            get
            {
                return yle_UnitIndex;
            }
        }

        private bool yle_IsFinished;
        /// <summary>
        /// 是否完成学习
        /// </summary>
        public bool Yle_IsFinished
        {
            set
            {
                AuditCheck("Yle_IsFinished", value);
                yle_IsFinished = value;
            }
            get
            {
                return yle_IsFinished;
            }
        }

        private int yle_CoinFromUIndex;
        /// <summary>
        /// 最大已发金币单元
        /// </summary>
        public int Yle_CoinFromUIndex
        {
            set
            {
                AuditCheck("Yle_CoinFromUIndex", value);
                yle_CoinFromUIndex = value;
            }
            get
            {
                return yle_CoinFromUIndex;
            }
        }

        private DateTime yle_LastStudyTime;
        /// <summary>
        /// 最后学习时间
        /// </summary>
        public DateTime Yle_LastStudyTime
        {
            set
            {
                AuditCheck("Yle_LastStudyTime", value);
                yle_LastStudyTime = value;
            }
            get
            {
                return yle_LastStudyTime;
            }
        }

        private int yle_StudySeconds;
        /// <summary>
        /// 学习时长
        /// </summary>
        public int Yle_StudySeconds
        {
            set
            {
                AuditCheck("Yle_StudySeconds", value);
                yle_StudySeconds = value;
            }
            get
            {
                return yle_StudySeconds;
            }
        }

        private bool yle_IsLastest;
        /// <summary>
        /// 是否最新一次学习
        /// </summary>
        public bool Yle_IsLastest
        {
            set
            {
                AuditCheck("Yle_IsLastest", value);
                yle_IsLastest = value;
            }
            get
            {
                return yle_IsLastest;
            }
        }

        private int yle_SubjectCount;
        /// <summary>
        /// 已答题目数
        /// </summary>
        public int Yle_SubjectCount
        {
            set
            {
                AuditCheck("Yle_SubjectCount", value);
                yle_SubjectCount = value;
            }
            get
            {

                return yle_SubjectCount;
            }
        }

        private int yle_RightSubjectCount;
        /// <summary>
        /// 学生正确题目数
        /// </summary>
        public int Yle_RightSubjectCount
        {
            set
            {
                AuditCheck("Yle_RightSubjectCount", value);
                yle_RightSubjectCount = value;
            }
            get
            {
                return yle_RightSubjectCount;
            }
        }
        
        private int yle_GainCoins;
        /// <summary>
        /// 获得金币数量
        /// </summary>
        public int Yle_GainCoins
        {
            set
            {
                AuditCheck("Yle_GainCoins", value);
                yle_GainCoins = value;
            }
            get
            {
                return yle_GainCoins;
            }
        }
        
        private int yle_Percent;
        /// <summary>
        /// 题目正确率
        /// </summary>
        public int Yle_Percent
        {
            set
            {
                AuditCheck("Yle_Percent", value);
                yle_Percent = value;
            }
            get
            {
                return yle_Percent;
            }
        }

        private string yle_Key;
        /// <summary>
        /// 学习秘钥(每次打开学习界面都会更新)
        /// </summary>
        public string Yle_key
        {
            set
            {
                AuditCheck("Yle_key", value);
                yle_Key = value;
            }
            get
            {
                return yle_Key;
            }
        }




        private DateTime yle_CreateTime;
        public DateTime Yle_CreateTime
        {
            set
            {
                AuditCheck("Yle_CreateTime", value);
                yle_CreateTime = value;
            }
            get
            {
                return yle_CreateTime;
            }
        }

        private DateTime yle_UpdateTime;
        public DateTime Yle_UpdateTime
        {
            set
            {
                AuditCheck("Yle_UpdateTime", value);
                yle_UpdateTime = value;
            }
            get
            {
                return yle_UpdateTime;
            }
        }
    }
}
