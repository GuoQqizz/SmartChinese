using System;

namespace AbhsChinese.Domain.Entity
{
    /// <summary>
    /// 课程信息表
    /// </summary>
    [Table("Yw_Course")]
    public class Yw_Course : EntityBase
    {
        public int ycs_ApprovalLesCount;
        /// <summary>
        /// 课时合格数量
        /// </summary>
        public int Ycs_ApprovalLesCount
        {
            get { return ycs_ApprovalLesCount; }
            set
            {
                AuditCheck(nameof(Ycs_ApprovalLesCount), value);
                ycs_ApprovalLesCount = value;
            }
        }

        private int ycs_CourseType;
        /// <summary>
        /// 课程类型
        /// </summary>
        public int Ycs_CourseType
        {
            get { return ycs_CourseType; }
            set
            {
                AuditCheck(nameof(Ycs_CourseType), value);
                ycs_CourseType = value;
            }
        }

        private string ycs_CoverImage;
        /// <summary>
        /// 课程封面图
        /// </summary>
        public string Ycs_CoverImage
        {
            get { return ycs_CoverImage; }
            set
            {
                AuditCheck(nameof(Ycs_CoverImage), value);
                ycs_CoverImage = value;
            }
        }

        private DateTime ycs_CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Ycs_CreateTime
        {
            get { return ycs_CreateTime; }
            set
            {
                AuditCheck(nameof(Ycs_CreateTime), value);
                ycs_CreateTime = value;
            }
        }

        private int ycs_Creator;
        /// <summary>
        /// 创建人
        /// </summary>
        public int Ycs_Creator
        {
            get { return ycs_Creator; }
            set
            {
                AuditCheck(nameof(Ycs_Creator), value);
                ycs_Creator = value;
            }
        }

        private string ycs_Description;
        /// <summary>
        /// 课程描述
        /// </summary>
        public string Ycs_Description
        {
            get { return ycs_Description; }
            set
            {
                AuditCheck(nameof(Ycs_Description), value);
                ycs_Description = value;
            }
        }

        private int ycs_Editor;
        /// <summary>
        /// 更新人
        /// </summary>
        public int Ycs_Editor
        {
            get { return ycs_Editor; }
            set
            {
                AuditCheck(nameof(Ycs_Editor), value);
                ycs_Editor = value;
            }
        }

        private string ycs_Employees;
        /// <summary>
        /// 教研老师
        /// </summary>
        public string Ycs_Employees
        {
            get { return ycs_Employees; }
            set
            {
                AuditCheck(nameof(Ycs_Employees), value);
                ycs_Employees = value;
            }
        }

        private int ycs_Grade;
        /// <summary>
        /// 课程年级
        /// </summary>
        public int Ycs_Grade
        {
            get { return ycs_Grade; }
            set
            {
                AuditCheck(nameof(Ycs_Grade), value);
                ycs_Grade = value;
            }
        }

        /// <summary>
        /// 课程id
        /// </summary>
        [Key]
        public int Ycs_Id { get; set; }

        private int ycs_LessonCount;
        /// <summary>
        /// 课时数量
        /// </summary>
        public int Ycs_LessonCount
        {
            get { return ycs_LessonCount; }
            set
            {
                AuditCheck(nameof(Ycs_LessonCount), value);
                ycs_LessonCount = value;
            }
        }

        private string ycs_Name;
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Ycs_Name
        {
            get { return ycs_Name; }
            set
            {
                AuditCheck(nameof(Ycs_Name), value);
                ycs_Name = value;
            }
        }

        private int ycs_Owner;
        /// <summary>
        /// 负责人
        /// </summary>
        public int Ycs_Owner
        {
            get { return ycs_Owner; }
            set
            {
                AuditCheck(nameof(Ycs_Owner), value);
                ycs_Owner = value;
            }
        }

        private DateTime ycs_PublishTime;
        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime Ycs_PublishTime
        {
            get { return ycs_PublishTime; }
            set
            {
                AuditCheck(nameof(Ycs_PublishTime), value);
                ycs_PublishTime = value;
            }
        }

        private int ycs_ResourceGroupId;
        /// <summary>
        /// 资源组
        /// </summary>
        public int Ycs_ResourceGroupId
        {
            get { return ycs_ResourceGroupId; }
            set
            {
                AuditCheck(nameof(Ycs_ResourceGroupId), value);
                ycs_ResourceGroupId = value;
            }
        }

        private int ycs_SellCount;
        /// <summary>
        /// 课程购买数量
        /// </summary>
        public int Ycs_SellCount
        {
            get { return ycs_SellCount; }
            set
            {
                AuditCheck(nameof(Ycs_SellCount), value);
                ycs_SellCount = value;
            }
        }

        private int ycs_Status;
        /// <summary>
        /// 状态
        /// </summary>
        public int Ycs_Status
        {
            get { return ycs_Status; }
            set
            {
                AuditCheck(nameof(Ycs_Status), value);
                ycs_Status = value;
            }
        }

        private DateTime ycs_UpdateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Ycs_UpdateTime
        {
            get { return ycs_UpdateTime; }
            set
            {
                AuditCheck(nameof(Ycs_UpdateTime), value);
                ycs_UpdateTime = value;
            }
        }
    }
}