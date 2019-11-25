using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Web.Models.Subjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    public class SubjectController : BaseController
    {
        [HttpGet]
        public JsonResult GetSubjectsOfLearning(List<int> subjectIds)
        {
            SubjectBll subjectBll = new SubjectBll();
            var result = subjectBll.GetCompleteContentsOfSubject(subjectIds);
            return Json(new SuccessJsonResponse(result), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSubjectOfLearning(int subjectId)
        {
            SubjectBll subjectBll = new SubjectBll();
            var result = subjectBll.GetCompleteContentsOfSubject(new List<int> { subjectId });

            return Json(
                new SuccessJsonResponse(result.FirstOrDefault()), 
                JsonRequestBehavior.AllowGet);
        }
    }
}