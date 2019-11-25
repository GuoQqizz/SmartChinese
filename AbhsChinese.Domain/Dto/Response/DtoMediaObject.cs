using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoMediaObject
    {
        public int MediaId { get; set; }

        public string MediaName { get; set; }

        public int MediaType { get; set; }

        public string MediaUrl { get; set; }

        public int ImgId { get; set; }

        public string ImgUrl { get; set; }

        public int TextId { get; set; }

        public string TextContent { get; set; }

        public string Description { get; set; }
    }
}
