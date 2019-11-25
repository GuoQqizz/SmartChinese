using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.School;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.AbhsResource;
using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity.School;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class SchoolController : BaseController
    {
        SchoolBll schoolBll = new SchoolBll();
        SchoolTeacherBll schoolTeacherBll = new SchoolTeacherBll();
        // GET: School
        #region school
        public ActionResult Index()
        {
            DtoSchoolSearch search = new DtoSchoolSearch();
            return View(search);
        }

        public ActionResult GetSchool(DtoSchoolSearch search)
        {
            var list = schoolBll.GetSchoolList(search);
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int schoolId)
        {
            SchoolInputModel viewModel = new SchoolInputModel();
            viewModel.IsValidStr = "on";
            var school = schoolBll.GetSchool(schoolId);
            var teacher = new Yw_SchoolTeacher();
            if (school != null)
            {
                teacher = schoolTeacherBll.GetSchoolTeacher(school.Bsl_SchoolMasterId);
                if (teacher.Yoh_Status != (int)StatusEnum.有效)
                {
                    teacher = null;
                }
                viewModel = viewModel.FromDbModel(school, teacher);
                viewModel.LoginPwd = "";
            }
            return View(viewModel);
        }

        public ActionResult SaveSchool(SchoolInputModel viewModel)
        {
            Bas_School oldSchool = new Bas_School();
            Yw_SchoolTeacher oldTeacher = new Yw_SchoolTeacher();
            if (viewModel.Bsl_Id > 0)
            {
                oldSchool = schoolBll.GetSchool(viewModel.Bsl_Id);
                if (oldSchool == null)
                {
                    throw new AbhsException(ErrorCodeEnum.ParameterInvalid, AbhsErrorMsg.ConstParameterInvalid);
                }
            }
            var school = viewModel.ToSchoolDbModel(oldSchool);
            var teacher = viewModel.ToSchoolTeacherDbModel();

            if (school.Bsl_Id == 0)
            {
                school.Bsl_CreateTime = DateTime.Now;
                school.Bsl_Creator = CurrentUserID;
            }
            school.Bsl_Editor = CurrentUserID;
            school.Bsl_UpdateTime = DateTime.Now;

            if (teacher.Yoh_Id == 0)
            {
                teacher.Yoh_CreateTime = DateTime.Now;
                teacher.Yoh_Creator = CurrentUserID;
            }
            teacher.Yoh_UpdateTime = DateTime.Now;
            teacher.Yoh_Editor = CurrentUserID;
            if (teacher.Yoh_Password.HasValue())
            {
                teacher.Yoh_Password = Encrypt.GetMD5Pwd(teacher.Yoh_Password);
            }
            bool success = schoolBll.SaveSchool(school, teacher);
            var msg = success ? "保存成功" : "保存失败";
            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }

        public ActionResult CheckSchoolTeacherAccount(string accont)
        {
            try
            {
                var count = schoolTeacherBll.GetSchoolTeacherCountByPhone(accont);
                var result = count == 0;
                return Json(new JsonSimpleResponse() { State = result, ErrorMsg = count.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = ex.Message });
            }
        }
        #endregion
        #region schoollevel

        public ActionResult LevelIndex()
        {
            return View();
        }

        public ActionResult GetSchoolLevel()
        {
            var list = schoolBll.GetSchoolLevelList();
            int count = list == null ? 0 : list.Count;
            var table = AbhsTableFactory.Create(list, count);
            return Json(table, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSchoolLevelForSelect()
        {
            var list = schoolBll.GetSchoolLevelList();
            var res = list.Select(s => { return new DtoKeyValue<int, string>() { key = s.Bhl_Id, value = s.Bhl_Name }; }).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditLevel(int bhlId = 0)
        {
            Bas_SchoolLevel viewModel = new Bas_SchoolLevel();
            if (bhlId > 0)
            {
                viewModel = schoolBll.GetSchoolLevel(bhlId);
            }
            return View(viewModel);
        }

        public ActionResult SaveSchoolLevel(Bas_SchoolLevel inputModel)
        {
            Bas_SchoolLevel changeModel;
            if (inputModel.Bhl_Id == 0)
            {
                inputModel.Bhl_CreateTime = DateTime.Now;
                inputModel.Bhl_Creator = CurrentUserID;
                changeModel = inputModel;
            }
            else
            {
                changeModel = schoolBll.GetSchoolLevel(inputModel.Bhl_Id);
                if (changeModel == null)
                {
                    throw new AbhsException(ErrorCodeEnum.ParameterInvalid, AbhsErrorMsg.ConstParameterInvalid);
                }
                changeModel.Bhl_Name = inputModel.Bhl_Name;
                changeModel.Bhl_DividePercent = inputModel.Bhl_DividePercent;
                changeModel.Bhl_Remark = inputModel.Bhl_Remark;
            }
            changeModel.Bhl_Status = (int)StatusEnum.有效;
            changeModel.Bhl_UpdateTime = DateTime.Now;
            changeModel.Bhl_Editor = CurrentUserID;
            bool success = schoolBll.SaveSchoolLevel(changeModel);
            var msg = success ? "保存成功" : "保存失败";
            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }
        #endregion
    }
}