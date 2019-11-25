using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    /// <summary>
    /// 课程详情对应数据模型
    /// </summary>
    [Table("Yw_CourseIntroduction")]
    public class Yw_CourseIntroduction : EntityBase
    {
        private System.String yci_introduction;
        private System.String yci_arrange;
        private System.DateTime yci_createtime;
        private System.Int32 yci_creator;
        private System.DateTime yci_updatetime;
        private System.Int32 yci_editor;

        /// <summary>
        /// 课程主键
        /// </summary>
        [ExplicitKey]
        public System.Int32 Yci_CourseId
        {
            get; set;
        }
        /// <summary>
        /// 课程详情
        /// </summary>
        public System.String Yci_Introduction
        {
            set
            {
                AuditCheck("Yci_Introduction", value);
                yci_introduction = value;
            }
            get
            {
                return yci_introduction;
            }
        }
        /// <summary>
        /// 课程安排
        /// </summary>
        public System.String Yci_Arrange
        {
            set
            {
                AuditCheck("Yci_Arrange", value);
                yci_arrange = value;
            }
            get
            {
                return yci_arrange;
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime Yci_CreateTime
        {
            set
            {
                AuditCheck("Yci_CreateTime", value);
                yci_createtime = value;
            }
            get
            {
                return yci_createtime;
            }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public System.Int32 Yci_Creator
        {
            set
            {
                AuditCheck("Yci_Creator", value);
                yci_creator = value;
            }
            get
            {
                return yci_creator;
            }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime Yci_UpdateTime
        {
            set
            {
                AuditCheck("Yci_UpdateTime", value);
                yci_updatetime = value;
            }
            get
            {
                return yci_updatetime;
            }
        }
        /// <summary>
        /// 更新人
        /// </summary>
        public System.Int32 Yci_Editor
        {
            set
            {
                AuditCheck("Yci_Editor", value);
                yci_editor = value;
            }
            get
            {
                return yci_editor;
            }
        }
    }
}
