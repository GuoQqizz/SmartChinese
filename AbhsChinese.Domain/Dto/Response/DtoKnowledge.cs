using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoKnowledge
    {
        public int Ykl_Id { get; set; }
        /// <summary>
        /// 知识点名字
        /// </summary>
        public string Ykl_Name { get; set; }

        public int Ykl_ParentId { get; set; }

        public int Ykl_Level { get; set; }

        public string Ykl_Keywords { get; set; }

        public int Ykl_ResourceId { get; set; }

        public string Ymr_Name { get; set; }

        public string ParentName { get; set; }
        public bool Ysw_IsMain { get; set; }
        public int Ysw_KnowledgeId { get; set; }
    }
}
