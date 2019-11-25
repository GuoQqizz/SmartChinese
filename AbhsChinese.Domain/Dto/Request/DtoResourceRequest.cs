using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoResourceRequest
    {
        public int Id { get; set; }
        public int TextObjectId { get; set; }
        public string Name { get; set; }
        public TextResourceTypeEnum TextType { get; set; }
        public MediaResourceTypeEnum MediaType { get; set; }
        public int Grade { get; set; }
        public string Content { get; set; }
        public string AudioContent { get; set; }
        public int Status
        {
            get
            {
                if(!IsStatus)
                {
                    return IsEnabled == "on" ? 1 : 0;
                }
                return State;
            }
        }
        public int Creator { get; set; }
        public int Editor { get; set; }
        public ResourceTypeEnum ResourceType { get; set; }
        public string Url { get; set; }
        public int ImgId { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public MediaObjectTypeEnum MediaObjectType { get; set; }


        public string IsEnabled { get; set; }

        public string Keyword => !string.IsNullOrEmpty(Key)?Key.Replace('，', ','):"";
        public string Key { get; set; }

        public bool IsStatus { get; set; } = false;
        public int State { get; set; }
    }
}