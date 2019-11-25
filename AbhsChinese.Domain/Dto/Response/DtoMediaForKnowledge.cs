using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoMediaForKnowledge
    {
        public int Yme_Id { get; set; }

        /// <summary>
        /// <see cref="MediaObjectTypeEnum"/>
        /// </summary>
        public int Yme_MediaType { get; set; }

        public string Yme_Url { get; set; }

        public int Ykl_Id { get; set; }

        public string Ykl_Name { get; set; }

        public int Ykl_ParentId { get; set; }

        public int Ykl_Level { get; set; }

        public string Url => Yme_Url.ToOssPath();
    }
}
