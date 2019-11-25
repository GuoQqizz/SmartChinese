using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request.StudentWrong
{
    public class DtoStudentWrongSearch : DtoSearch
    {
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }
        public int BookId { get; set; }

        public int StudentId { get; set; }
    }
}
