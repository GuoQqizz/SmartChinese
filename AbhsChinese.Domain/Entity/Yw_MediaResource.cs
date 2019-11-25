using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_MediaResource")]
    public class Yw_MediaResource:EntityBase
    {
        [Key]
        public int Ymr_Id { get; set; }

        private string ymr_name;
        public string Ymr_Name
        {
            set
            {
                AuditCheck("Ymr_Name", value);
                ymr_name = value;
            }
            get
            {
                return ymr_name;
            }
        }

        private int ymr_mediatype;
        public int Ymr_MediaType
        {
            set
            {
                AuditCheck("Ymr_MediaType", value);
                ymr_mediatype = value;
            }
            get
            {
                return ymr_mediatype;
            }
        }

        private int ymr_grade;
        public int Ymr_Grade
        {
            set
            {
                AuditCheck("Ymr_Grade", value);
                ymr_grade = value;
            }
            get
            {
                return ymr_grade;
            }
        }

        private int ymr_mediaobjectid;
        public int Ymr_MediaObjectId
        {
            set
            {
                AuditCheck("Ymr_MediaObjectId", value);
                ymr_mediaobjectid = value;
            }
            get
            {
                return ymr_mediaobjectid;
            }
        }

        private string ymr_keywords;
        public string Ymr_Keywords
        {
            set
            {
                AuditCheck("Ymr_Keywords", value);
                ymr_keywords = value;
            }
            get
            {
                return ymr_keywords;
            }
        }
        
        private int ymr_status;
        public int Ymr_Status
        {
            set
            {
                AuditCheck("Ymr_Status", value);
                ymr_status = value;
            }
            get
            {
                return ymr_status;
            }
        }

        private DateTime ymr_createtime;
        public DateTime Ymr_CreateTime
        {
            set
            {
                AuditCheck("Ymr_CreateTime", value);
                ymr_createtime = value;
            }
            get
            {
                return ymr_createtime;
            }
        }

        private int ymr_creator;
        public int Ymr_Creator
        {
            set
            {
                AuditCheck("Ymr_Creator", value);
                ymr_creator = value;
            }
            get
            {
                return ymr_creator;
            }
        }

        private DateTime ymr_updatetime;
        public DateTime Ymr_UpdateTime
        {
            set
            {
                AuditCheck("Ymr_UpdateTime", value);
                ymr_updatetime = value;
            }
            get
            {
                return ymr_updatetime;
            }
        }

        private int ymr_editor;
        public int Ymr_Editor
        {
            set
            {
                AuditCheck("Ymr_Editor", value);
                ymr_editor = value;
            }
            get
            {
                return ymr_editor;
            }
        }
    }
}
