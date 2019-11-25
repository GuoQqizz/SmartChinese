using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    /// <summary>
    /// 课时审批记录表对应模型
    /// </summary>
    [Table("Yw_CourseLessonUnitProcess")]
    public class Yw_CourseLessonUnitProcess : EntityBase
    {

        private int yup_id;
        private int yup_courseid;
        private int yup_lessonid;
        private int yup_lessonprocessid;
        private int yup_unitid;
        private int ylp_status;
        private int ylp_operator;
        private string ylp_remark;
        private DateTime ylp_createtime;
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Yup_Id { set { AuditCheck("Yup_Id", value); yup_id = value; } get { return yup_id; } }
        /// <summary>
        /// 课程id
        /// </summary>
        public int Yup_CourseId { set { AuditCheck("Yup_CourseId", value); yup_courseid = value; } get { return yup_courseid; } }
        /// <summary>
        /// 课时id
        /// </summary>
        public int Yup_LessonId { set { AuditCheck("Yup_LessonId", value); yup_lessonid = value; } get { return yup_lessonid; } }
        /// <summary>
        /// 课时审批id
        /// </summary>
        public int Yup_LessonProcessId { set { AuditCheck("Yup_LessonProcessId", value); yup_lessonprocessid = value; } get { return yup_lessonprocessid; } }
        /// <summary>
        /// 课时单元id
        /// </summary>
        public int Yup_UnitId { set { AuditCheck("Yup_UnitId", value); yup_unitid = value; } get { return yup_unitid; } }
        /// <summary>
        /// 课时状态(1:正常,2:有错误,3:有疑问)
        /// </summary>
        public int Yup_Status { set { AuditCheck("Yup_Status", value); ylp_status = value; } get { return ylp_status; } }
        /// <summary>
        /// 操作人id
        /// </summary>
        public int Yup_Operator { set { AuditCheck("Yup_Operator", value); ylp_operator = value; } get { return ylp_operator; } }
        /// <summary>
        /// 审批信息
        /// </summary>
        public string Yup_Remark { set { AuditCheck("Yup_Remark", value); ylp_remark = value; } get { return ylp_remark; } }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime Yup_CreateTime { set { AuditCheck("Yup_CreateTime", value); ylp_createtime = value; } get { return ylp_createtime; } }
    }
}
