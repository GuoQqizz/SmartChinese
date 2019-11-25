using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_MediaObject")]
    public class Yw_MediaObject : EntityBase
    {
        [Key]
        public int Yme_Id { get; set; }

        private string yme_name;
        public string Yme_Name
        {
            set
            {
                AuditCheck("Yme_Name", value);
                yme_name = value;
            }
            get
            {
                return yme_name;
            }
        }

        private int yme_mediatype;
        public int Yme_MediaType
        {
            set
            {
                AuditCheck("Yme_MediaType", value);
                yme_mediatype = value;
            }
            get
            {
                return yme_mediatype;
            }
        }
        
        private string yme_url;
        public string Yme_Url
        {
            set
            {
                AuditCheck("Yme_Url", value);
                yme_url = value;
            }
            get
            {
                return yme_url;
            }
        }

        private int yme_imagecoverid;
        public int Yme_ImageCoverId
        {
            set
            {
                AuditCheck("Yme_ImageCoverId", value);
                yme_imagecoverid = value;
            }
            get
            {
                return yme_imagecoverid;
            }
        }

        private int yme_textobjectid;
        public int Yme_TextObjectId
        {
            set
            {
                AuditCheck("Yme_TextObjectId", value);
                yme_textobjectid = value;
            }
            get
            {
                return yme_textobjectid;
            }
        }

        private string yme_description;
        public string Yme_Description
        {
            set
            {
                AuditCheck("Yme_Description", value);
                yme_description = value;
            }
            get
            {
                return yme_description;
            }
        }

        private int yme_status;
        public int Yme_Status
        {
            set
            {
                AuditCheck("Yme_Status", value);
                yme_status = value;
            }
            get
            {
                return yme_status;
            }
        }

        private DateTime yme_createtime;
        public DateTime Yme_CreateTime
        {
            set
            {
                AuditCheck("Yme_CreateTime", value);
                yme_createtime = value;
            }
            get
            {
                return yme_createtime;
            }
        }

        private int yme_creator;
        public int Yme_Creator
        {
            set
            {
                AuditCheck("Yme_Creator", value);
                yme_creator = value;
            }
            get
            {
                return yme_creator;
            }
        }

        private DateTime yme_updatetime;
        public DateTime Yme_UpdateTime
        {
            set
            {
                AuditCheck("Yme_UpdateTime", value);
                yme_updatetime = value;
            }
            get
            {
                return yme_updatetime;
            }
        }

        private int yme_editor;
        public int Yme_Editor
        {
            set
            {
                AuditCheck("Yme_Editor", value);
                yme_editor = value;
            }
            get
            {
                return yme_editor;
            }
        }
    }
}
