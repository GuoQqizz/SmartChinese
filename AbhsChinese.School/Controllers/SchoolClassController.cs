using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.School.Models;
using AbhsChinese.School.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.School.Controllers
{
    public class SchoolClassController : BaseController
    {

        SchoolBll schoolBll = new SchoolBll();

        SchoolClassBll schoolClassBll = new SchoolClassBll();
        // GET: SchoolClass
        public ActionResult Index()
        {
            DtoSchoolClassSearch search = new DtoSchoolClassSearch();
            search.SchoolId = CurrentUser.Teacher.Yoh_SchoolId;
            return View(search);
        }

        public ActionResult GetClassList(DtoSchoolClassSearch search)
        {
            
            var list = schoolClassBll.GetSchoolClassList(search);
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }
 
        public ActionResult Edit(int classId)
        {
            SchoolClassInputModel viewModel = new SchoolClassInputModel();
            viewModel.Ycc_SchoolId = CurrentUser.Teacher.Yoh_SchoolId;
            if (classId > 0)
            {
                var schoolClass = schoolClassBll.GetSchoolClass(classId);
                var classSchedules = schoolClassBll.GetSchoolClassSchedule(classId);
                viewModel = viewModel.FromDbModel(schoolClass, classSchedules);
            }
            return View(viewModel);
        }

        public ActionResult SaveClass(SchoolClassInputModel viewModel)
        {
            bool success = false;
            Yw_SchoolClass oldModel = new Yw_SchoolClass();
            if (viewModel.Ycc_Id > 0)
            {
                oldModel = schoolClassBll.GetSchoolClass(viewModel.Ycc_Id);
                CheckUpdateEntity(oldModel);
            }
            var schoolClass = viewModel.ToSchoolClassDbModel(oldModel);
            var schedules = viewModel.ToSchoolClassScheduleDbModel();
            schedules.ForEach(s =>
            {
                s.Ywd_UpdateTime = DateTime.Now;
                s.Ywd_CreateTime = DateTime.Now;
                s.Ywd_Creator = CurrentUser.Teacher.Yoh_Id;
                s.Ywd_Editor = CurrentUser.Teacher.Yoh_Id;
            });
            if (schoolClass.Ycc_Id == 0)
            {
                schoolClass.Ycc_CreateTime = DateTime.Now;
                schoolClass.Ycc_Creator = CurrentUser.Teacher.Yoh_Id;
            }
            schoolClass.Ycc_UpdateTime = DateTime.Now;
            schoolClass.Ycc_Editor = CurrentUser.Teacher.Yoh_Id;
            success = schoolClassBll.SaveSchoolClass(schoolClass, schedules);
            string msg = success ? "保存成功" : "保存失败";
            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }
    }
}