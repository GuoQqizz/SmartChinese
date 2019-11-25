using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoSimpleStuRecentSub
    {
        public int StudentId
        {
            set;get;
        }

        public int SubjectId
        {
            set;get;
        }

        public string Row
        {
            get
            {
                return $"{StudentId},{SubjectId}";
            }
        }
    }
}
