using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Web.Models.SubjectReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StuWrong
{
    public class WrongSubjectVm
    {
        /// <summary>
        /// 当前错题本下的所有错题
        /// Yw_StudentWrongSubject 表主键Id
        /// </summary>
        //public List<int> WrongSubjectIds { get; set; }

        //public int WrongState { get; set; }

        public ReportVm Report { get; set; }

        public DtoMediaForKnowledge KnowledgeInfo { get; set; }

    }
}