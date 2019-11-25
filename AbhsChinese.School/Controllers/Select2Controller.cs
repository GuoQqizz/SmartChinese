using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.School.Controllers;
using AbhsChinese.School.Models.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class Select2Controller : BaseController
    {

        RegionBll regionBll = new RegionBll();
        CourseBll courseBll = new CourseBll();
        SchoolBll schoolBll = new SchoolBll();
        SchoolClassBll schoolClassBll = new SchoolClassBll();
        SchoolTeacherBll schoolTeacherBll = new SchoolTeacherBll();

        [HttpGet]
        public ActionResult GetCourseTypes()
        {
            var options = CustomEnumHelper.GetElements(typeof(CourseCategoryEnum));
            var result = OptionFactory.CreateOptions(options);
            return Select2(result);
        }

        [HttpGet]
        public ActionResult GetCurriculumCategaries()
        {
            var categories = CustomEnumHelper.GetElements(typeof(CourseCategoryEnum));
            var result = OptionFactory.CreateOptions(categories);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDifficulties()
        {
            var difficuties = CustomEnumHelper.GetElements(typeof(DifficultyEnum));
            var result = OptionFactory.CreateOptions(difficuties);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeeStatus()
        {
            var textType = CustomEnumHelper.GetElements(typeof(StatusEnum));
            var result = OptionFactory.CreateOptions(textType);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGrades()
        {
            var difficuties = CustomEnumHelper.GetElements(typeof(GradeEnum));
            var result = OptionFactory.CreateOptions(difficuties);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMediaType()
        {
            var mediaType = CustomEnumHelper.GetElements(typeof(MediaResourceTypeEnum));
            var result = OptionFactory.CreateOptions(mediaType);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMediaTypeExceptXiaoAi()
        {
            var mediaType = CustomEnumHelper.GetElements(typeof(MediaResourceTypeEnum));
            mediaType.Remove((int)MediaResourceTypeEnum.小艾说);
            mediaType.Remove((int)MediaResourceTypeEnum.开场语);
            mediaType.Remove((int)MediaResourceTypeEnum.小艾变);
            var result = OptionFactory.CreateOptions(mediaType);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCourse(int type, int grade)
        {
            var dic = courseBll.GetCourseByTypeAndGrade(type, grade);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetTeacherByGrade(int grade)
        {
            int schoolId = CurrentUser.Teacher.Yoh_SchoolId;
            var dic = schoolTeacherBll.GetTeacherByGrade(grade, schoolId);
            return Json(dic, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTextType()
        {
            var textType = CustomEnumHelper.GetElements(typeof(TextResourceTypeEnum));
            var result = OptionFactory.CreateOptions(textType);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetVoucherStatus()
        {
            var voucherStatus = CustomEnumHelper.GetElements(typeof(VoucherStatusEnum));
            var result = OptionFactory.CreateOptions(voucherStatus);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStuentCashVoucherStatus()
        {
            var voucherStatus = CustomEnumHelper.GetElements(typeof(StudentCashVoucherStatusEnum));
            var result = OptionFactory.CreateOptions(voucherStatus);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetRegion(int parentId = 0)
        {
            var list = regionBll.GetRegionList(parentId).ToDictionary(k => { return k.Reg_ID; }, v => { return v.Reg_Name; });
            var result = OptionFactory.CreateOptions(list);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSchoolClassByCourseId(int courseId)
        {
            int schoolId = CurrentUser.Teacher.Yoh_SchoolId;
            var dic = schoolClassBll.GetSchoolClassByCourseId(courseId, schoolId).ToDictionary(k => { return k.Ycc_Id; }, v => { return v.Ycc_Name; });
            var result = OptionFactory.CreateOptions(dic);
            return Select2(result, JsonRequestBehavior.AllowGet);
        }
        protected JsonResult Select2(
            object data,
            JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            return Json(new { results = data }, null, null, behavior);
        }
    }
}