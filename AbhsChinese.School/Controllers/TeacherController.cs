using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Common.SMS;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using AbhsChinese.School.Helpers;
using AbhsChinese.School.Models;
using AbhsChinese.School.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.School.Controllers
{
    public class TeacherController : BaseController
    {

        //SchoolBll schoolBll = new SchoolBll();
        SchoolTeacherBll schoolTeacherBll = new SchoolTeacherBll();
        // GET: Teacher
        public ActionResult Index()
        {
            DtoSchoolTeacherSearch search = new DtoSchoolTeacherSearch(CurrentUser.Teacher.Yoh_SchoolId);

            return View(search);
        }
        public ActionResult Edit(int teacherId)
        {
            SchoolTeacherInputModel viewModel = new SchoolTeacherInputModel();
            viewModel.Yoh_SchoolId = CurrentUser.Teacher.Yoh_SchoolId;
            if (teacherId > 0)
            {
                var teacher = schoolTeacherBll.GetSchoolTeacher(teacherId);
                viewModel = viewModel.FromDbModel(teacher);
            }
            return View(viewModel);
        }

        public ActionResult GetTeacher(DtoSchoolTeacherSearch search)
        {
            var list = schoolTeacherBll.GetSchoolTeacherList(search);
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveTeacher(SchoolTeacherInputModel viewModel)
        {
            bool success = false;
            string msg = "";
            bool checkCode = true;
            if (viewModel.Yoh_Id == 0)
            {
                try
                {
                    checkCode = SmsCookie.GetSmsCode.Check(viewModel.Yoh_Phone, viewModel.VerificationCode);
                }
                catch (Exception)
                {
                    checkCode = false;
                }
            }

            if (checkCode)
            {
                var oldTeacher = new Yw_SchoolTeacher();
                if (viewModel.Yoh_Id > 0)
                {
                    oldTeacher = schoolTeacherBll.GetEntity(viewModel.Yoh_Id);
                    CheckUpdateEntity(oldTeacher);
                }
                var teacher = viewModel.ToDbModel(oldTeacher);
                if (viewModel.Yoh_Password.HasValue())
                {
                    teacher.Yoh_Password = Encrypt.GetMD5Pwd(viewModel.Yoh_Password);
                }
                if (teacher.Yoh_Id == 0)
                {
                    teacher.Yoh_CreateTime = DateTime.Now;
                    teacher.Yoh_Creator = CurrentUser.Teacher.Yoh_Id;
                }
                teacher.Yoh_UpdateTime = DateTime.Now;
                teacher.Yoh_Editor = CurrentUser.Teacher.Yoh_Id;
                success = schoolTeacherBll.SaveSchoolTeacher(teacher);
                msg = success ? "保存成功" : "保存失败";
            }
            else
            {
                msg = "验证码错误";
            }

            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }
    }
}