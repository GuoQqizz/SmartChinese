using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    /// <summary>
    /// 课时表对应模型
    /// </summary>
    /// 
    [Table("Yw_CourseLesson")]
    public class Yw_CourseLesson : EntityBase
    {
        private int ycl_courseid;
        private string ycl_name;
        private int ycl_index;
        private int ycl_status;
        private int ycl_unitcount;
        private int ycl_producer;
        private int ycl_approver;
        private DateTime ycl_createtime;
        private int ycl_creator;
        private DateTime ycl_updatetime;
        private int ycl_editor;

        /// <summary>
        /// 课时id
        /// </summary>
        [Key]
        public int Ycl_Id { get; set; }
        /// <summary>
        /// 课程id
        /// </summary>
        public int Ycl_CourseId
        {
            set
            {
                AuditCheck("Ycl_CourseId", value); ycl_courseid = value;
            }
            get { return ycl_courseid; }
        }
        /// <summary>
        /// 课时名称
        /// </summary>
        public string Ycl_Name
        {
            set
            {
                AuditCheck("Ycl_Name", value); ycl_name = value;
            }
            get { return ycl_name; }
        }
        /// <summary>
        /// 课时序号
        /// </summary>
        public int Ycl_Index
        {
            set
            {
                AuditCheck("Ycl_Index", value); ycl_index = value;
            }
            get { return ycl_index; }
        }
        /// <summary>
        /// 课时状态
        /// </summary>
        public int Ycl_Status
        {
            set
            {
                AuditCheck("Ycl_Status", value); ycl_status = value;
            }
            get { return ycl_status; }
        }
        /// <summary>
        /// 课时单元(讲义)数量
        /// </summary>
        public int Ycl_UnitCount
        {
            set
            {
                AuditCheck("Ycl_UnitCount", value); ycl_unitcount = value;
            }
            get { return ycl_unitcount; }
        }
        /// <summary>
        /// 制作人
        /// </summary>
        public int Ycl_Producer
        {
            set
            {
                AuditCheck("Ycl_Producer", value); ycl_producer = value;
            }
            get { return ycl_producer; }
        }
        /// <summary>
        /// 审批人
        /// </summary>
        public int Ycl_Approver
        {
            set
            {
                AuditCheck("Ycl_Approver", value); ycl_approver = value;
            }
            get { return ycl_approver; }
        }
        /// <summary>
        /// 课时创建时间
        /// </summary>
        public DateTime Ycl_CreateTime
        {
            set
            {
                AuditCheck("Ycl_CreateTime", value); ycl_createtime = value;
            }
            get { return ycl_createtime; }
        }
        /// <summary>
        /// 课时创建人
        /// </summary>
        public int Ycl_Creator
        {
            set
            {
                AuditCheck("Ycl_Creator", value); ycl_creator = value;
            }
            get { return ycl_creator; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Ycl_UpdateTime
        {
            set
            {
                AuditCheck("Ycl_UpdateTime", value); ycl_updatetime = value;
            }
            get { return ycl_updatetime; }
        }
        /// <summary>
        /// 更新人
        /// </summary>
        public int Ycl_Editor
        {
            set
            {
                AuditCheck("Ycl_Editor", value); ycl_editor = value;
            }
            get { return ycl_editor; }
        }

    }
}
