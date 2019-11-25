using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoResourceGroupItem
    {
        //媒体资源
        public int Id { get; set; }

        public string Name { get; set; }

        public int MediaType { get; set; }

        public int TextType { get; set; }

        public int SubjectType { get; set; }

        public int Grade { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public int Difficulty { get; set; }

        public string Keywords { get; set; }

        public int Status { get; set; }

        public DateTime CreateTime { get; set; }

        public int Creator { get; set; }

        public DateTime UpdateTime { get; set; }

        public int Editor { get; set; }

        public int GroupItemCount { get; set; }


        //Common
        public string UploadTime => CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

        public string Type {
            get
            {
                if (MediaType > 0)
                {
                    return CustomEnumHelper.Parse(typeof(MediaResourceTypeEnum), MediaType);
                }
                if (TextType > 0)
                {
                    return CustomEnumHelper.Parse(typeof(TextResourceTypeEnum), TextType);
                }
                if (SubjectType > 0)
                {
                    return CustomEnumHelper.Parse(typeof(SubjectTypeEnum), SubjectType);
                }
                return CustomEnumHelper.Parse(typeof(MediaResourceTypeEnum), MediaType);
            }
        }

        public string Grades=> CustomEnumHelper.Parse(typeof(GradeEnum), Grade);
    }
}
