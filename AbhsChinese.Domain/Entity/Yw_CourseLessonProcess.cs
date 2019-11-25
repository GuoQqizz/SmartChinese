using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    /// <summary>
    /// 课时处理流程对应数据模型
    /// </summary>
    [Table("Yw_CourseLessonProcess")]
    public class Yw_CourseLessonProcess : EntityBase
    {
        private int ylp_id;
        private int ylp_courseid;
        private int ylp_lessonid;
        private int ylp_action;
        private int ylp_status;
        private int ylp_operator;
        private string ylp_remark;
        private DateTime ylp_createtime;
        /// <summary>
        /// 处理流程id
        /// </summary>
        [Key]
        public int Ylp_Id { set { AuditCheck("Ylp_Id", value); ylp_id = value; } get { return ylp_id; } }
        /// <summary>
        /// 课程id
        /// </summary>
        public int Ylp_CourseId { set { AuditCheck("Ylp_CourseId", value); ylp_courseid = value; } get { return ylp_courseid; } }
        /// <summary>
        /// 课时id
        /// </summary>
        public int Ylp_LessonId { set { AuditCheck("Ylp_LessonId", value); ylp_lessonid = value; } get { return ylp_lessonid; } }
        /// <summary>
        /// 流程流转动作(1:创建,2:编辑,3:完成编辑,4:重新编辑,5:审批)
        /// </summary>
        public int Ylp_Action { set { AuditCheck("Ylp_Action", value); ylp_action = value; } get { return ylp_action; } }
        /// <summary>
        /// 流程流转状态(1:未编辑,2:制作中,3,待审批,4:审批中,5:合格,6:不合格)
        /// </summary>
        public int Ylp_Status { set { AuditCheck("Ylp_Status", value); ylp_status = value; } get { return ylp_status; } }
        /// <summary>
        /// 操作人
        /// </summary>
        public int Ylp_Operator { set { AuditCheck("Ylp_Operator", value); ylp_operator = value; } get { return ylp_operator; } }
        /// <summary>
        /// 备注
        /// </summary>
        public string Ylp_Remark { set { AuditCheck("Ylp_Remark", value); ylp_remark = value; } get { return ylp_remark; } }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Ylp_CreateTime { set { AuditCheck("Ylp_CreateTime", value); ylp_createtime = value; } get { return ylp_createtime; } }
    }
}
