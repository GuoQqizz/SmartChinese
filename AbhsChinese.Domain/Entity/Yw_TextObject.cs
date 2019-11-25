using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Entity
{
    [Table("Yw_TextObject")]
    public class Yw_TextObject : EntityBase
    {
        [Key]
        public int Yxo_Id { get; set; }

        private string yxo_content;
        public string Yxo_Content
        {
            set
            {
                AuditCheck("Yxo_Content", value);
                yxo_content = value;
            }
            get
            {
                return yxo_content;
            }
        }

        private int yxo_status;
        public int Yxo_Status
        {
            set
            {
                AuditCheck("Yxo_Status", value);
                yxo_status = value;
            }
            get
            {
                return yxo_status;
            }
        }
        
        private DateTime yxo_createtime;
        public DateTime Yxo_CreateTime
        {
            set
            {
                AuditCheck("Yxo_CreateTime", value);
                yxo_createtime = value;
            }
            get
            {
                return yxo_createtime;
            }
        }

        private int yxo_creator;
        public int Yxo_Creator
        {
            set
            {
                AuditCheck("Yxo_Creator", value);
                yxo_creator = value;
            }
            get
            {
                return yxo_creator;
            }
        }

        private DateTime yxo_updatetime;
        public DateTime Yxo_UpdateTime
        {
            set
            {
                AuditCheck("Yxo_UpdateTime", value);
                yxo_updatetime = value;
            }
            get
            {
                return yxo_updatetime;
            }
        }

        private int yxo_editor;
        public int Yxo_Editor
        {
            set
            {
                AuditCheck("Yxo_Editor", value);
                yxo_editor = value;
            }
            get
            {
                return yxo_editor;
            }
        }
    }
}
