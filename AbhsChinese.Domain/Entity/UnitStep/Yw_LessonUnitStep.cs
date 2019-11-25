using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity.UnitStep
{
    /// <summary>
    /// 课时单元步骤冗余表对应实体
    /// </summary>
    [Table("Yw_LessonUnitStep")]
    public class Yw_LessonUnitStep : EntityBase
    {
        private int yls_courseid;
        private int yls_lessonid;
        private int yls_unitid;
        private int yls_unitindex;
        private int yls_status;
        private string yls_steps;
        private int yls_coins;
        private string yls_subjectids;
        private DateTime yls_createtime;
        private DateTime yls_updatetime;
        /// <summary>
        /// 课程id
        /// </summary>
        public int Yls_CourseId
        {
            get { return yls_courseid; }
            set
            {
                AuditCheck("Yls_CourseId", value);
                yls_courseid = value;
            }
        }
        /// <summary>
        /// 课时id
        /// </summary>
        public int Yls_LessonId
        {
            get { return yls_lessonid; }
            set
            {
                AuditCheck("Yls_LessonId", value);
                yls_lessonid = value;
            }
        }
        /// <summary>
        /// 课时单元id(讲义id)
        /// </summary>
        [ExplicitKey]
        public int Yls_UnitId
        {
            get { return yls_unitid; }
            set
            {
                AuditCheck("Yls_UnitId", value);
                yls_unitid = value;
            }
        }
        /// <summary>
        /// 单元(讲义)序号
        /// </summary>
        public int Yls_UnitIndex
        {
            get { return yls_unitindex; }
            set
            {
                AuditCheck("Yls_UnitIndex", value);
                yls_unitindex = value;
            }
        }
        /// <summary>
        /// 单元步骤状态
        /// </summary>
        public int Yls_Status
        {
            get { return yls_status; }
            set
            {
                AuditCheck("Yls_Status", value);
                yls_status = value;
            }
        }
        /// <summary>
        /// 单元步骤json
        /// </summary>
        public string Yls_Steps
        {
            get { return yls_steps; }
            set
            {
                AuditCheck("Yls_Steps", value);
                yls_steps = value;
            }
        }
        /// <summary>
        /// 当页金币数
        /// </summary>
        public int Yls_Coins
        {
            get { return yls_coins; }
            set
            {
                AuditCheck("Yls_Coins", value);
                yls_coins = value;
            }
        }
        /// <summary>
        /// 课程题目内容
        /// </summary>
        public string Yls_SubjectIds
        {
            get { return yls_subjectids; }
            set
            {
                AuditCheck("Yls_SubjectIds", value);
                yls_subjectids = value;
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Yls_CreateTime
        {
            get { return yls_createtime; }
            set
            {
                AuditCheck("Yls_CreateTime", value);
                yls_createtime = value;
            }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime Yls_UpdateTime
        {
            get { return yls_updatetime; }
            set
            {
                AuditCheck("Yls_UpdateTime", value);
                yls_updatetime = value;
            }
        }

    }
}







