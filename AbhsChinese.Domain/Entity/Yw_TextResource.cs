using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_TextResource")]
    public class Yw_TextResource : EntityBase
    {
        [Key]
        public int Ytr_Id { get; set; }

        private string ytr_name;
        public string Ytr_Name
        {
            set
            {
                AuditCheck("Ytr_Name", value);
                ytr_name = value;
            }
            get
            {
                return ytr_name;
            }
        }

        private int ytr_texttype;
        public int Ytr_TextType
        {
            set
            {
                AuditCheck("Ytr_TextType", value);
                ytr_texttype = value;
            }
            get
            {
                return ytr_texttype;
            }
        }

        private int ytr_grade;
        public int Ytr_Grade
        {
            set
            {
                AuditCheck("Ytr_Grade", value);
                ytr_grade = value;
            }
            get
            {
                return ytr_grade;
            }
        }

        private int ytr_textobjectid;
        public int Ytr_TextObjectId
        {
            set
            {
                AuditCheck("Ytr_TextObjectId", value);
                ytr_textobjectid = value;
            }
            get
            {
                return ytr_textobjectid;
            }
        }

        private string ytr_keywords;
        public string Ytr_Keywords
        {
            set
            {
                AuditCheck("Ytr_Keywords", value);
                ytr_keywords = value;
            }
            get
            {
                return ytr_keywords;
            }
        }

        private int ytr_status;
        public int Ytr_Status
        {
            set
            {
                AuditCheck("Ytr_Status", value);
                ytr_status = value;
            }
            get
            {
                return ytr_status;
            }
        }

        private DateTime ytr_createtime;
        public DateTime Ytr_CreateTime
        {
            set
            {
                AuditCheck("Ytr_CreateTime", value);
                ytr_createtime = value;
            }
            get
            {
                return ytr_createtime;
            }
        }

        private int ytr_creator;
        public int Ytr_Creator
        {
            set
            {
                AuditCheck("Ytr_Creator", value);
                ytr_creator = value;
            }
            get
            {
                return ytr_creator;
            }
        }

        private DateTime ytr_updatetime;
        public DateTime Ytr_UpdateTime
        {
            set
            {
                AuditCheck("Ytr_UpdateTime", value);
                ytr_updatetime = value;
            }
            get
            {
                return ytr_updatetime;
            }
        }

        private int ytr_editor;
        public int Ytr_Editor
        {
            set
            {
                AuditCheck("Ytr_Editor", value);
                ytr_editor = value;
            }
            get
            {
                return ytr_editor;
            }
        }
    }
}
