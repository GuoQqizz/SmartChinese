using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{

    /// <summary>
    /// 课时单元(讲义页)数据实体对应模型
    /// </summary>
    [Table("Yw_CourseLessonUnit")]
    public class Yw_CourseLessonUnit : EntityBase
    {
        private int ycu_id;
        private int ycu_courseid;
        private int ycu_lessonid;
        private int ycu_index;
        private string ycu_name;
        private string ycu_screenshot;
        private int ycu_status;
        private DateTime ycu_createtime;
        private int ycu_creator;
        private DateTime ycu_updatetime;
        private int ycu_editor;
        /// <summary>
        /// id
        /// </summary>
        [Key]
        public int Ycu_Id
        {
            set
            {
                AuditCheck("Ycu_Id", value);
                ycu_id = value;
            }
            get { return ycu_id; }
        }
        /// <summary>
        /// 课程id
        /// </summary>
        public int Ycu_CourseId
        {
            set
            {
                AuditCheck("Ycu_CourseId", value);
                ycu_courseid = value;
            }
            get { return ycu_courseid; }
        }
        /// <summary>
        /// 课时id
        /// </summary>
        public int Ycu_LessonId
        {
            set
            {
                AuditCheck("Ycu_LessonId", value);
                ycu_lessonid = value;
            }
            get { return ycu_lessonid; }
        }
        /// <summary>
        /// 单元(讲义)序号
        /// </summary>
        public int Ycu_Index
        {
            set
            {
                AuditCheck("Ycu_Index", value);
                ycu_index = value;
            }
            get { return ycu_index; }
        }
        /// <summary>
        /// 单元(讲义)名称
        /// </summary>
        public string Ycu_Name
        {
            set
            {
                AuditCheck("Ycu_Name", value);
                ycu_name = value;
            }
            get { return ycu_name; }
        }
        /// <summary>
        /// 缩略图id
        /// </summary>
        public string Ycu_Screenshot
        {
            set
            {
                AuditCheck("Ycu_Screenshot", value);
                ycu_screenshot = value;
            }
            get { return ycu_screenshot; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int Ycu_Status
        {
            set
            {
                AuditCheck("Ycu_Status", value);
                ycu_status = value;
            }
            get { return ycu_status; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Ycu_CreateTime
        {
            set
            {
                AuditCheck("Ycu_CreateTime", value);
                ycu_createtime = value;
            }
            get { return ycu_createtime; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Ycu_Creator
        {
            set
            {
                AuditCheck("Ycu_Creator", value);
                ycu_creator = value;
            }
            get { return ycu_creator; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Ycu_UpdateTime
        {
            set
            {
                AuditCheck("Ycu_UpdateTime", value);
                ycu_updatetime = value;
            }
            get { return ycu_updatetime; }
        }
        /// <summary>
        /// 更新人
        /// </summary>
        public int Ycu_Editor
        {
            set
            {
                AuditCheck("Ycu_Editor", value);
                ycu_editor = value;
            }
            get { return ycu_editor; }
        }
        
    }
}