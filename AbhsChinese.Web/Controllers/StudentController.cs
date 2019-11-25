using Abhs.Service.Client;
using Abhs.Service.Client.Results;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response.School;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Web.Models.StudentInfo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    public class StudentController : BaseController
    {
        public ActionResult StudentInfo()
        {
            StudentInfoBll studentBll = new StudentInfoBll();
            var studentInfo = studentBll.GetStudentInfo(GetCurrentUser().StudentId);
            var viewModel = studentInfo.ConvertTo<StudentInfoViewModel>();
            return View(viewModel);
        }

        public ActionResult EditStudentInfo()
        {
            StudentInfoBll studentBll = new StudentInfoBll();
            var studentInfo = studentBll.GetStudentInfo(GetCurrentUser().StudentId);
            var viewModel = studentInfo.ConvertTo<StudentInfoViewModel>();
            return View(viewModel);
        }

        public ActionResult AddStudentInfo(StudentInfoInputModel inputModel)
        {
            try
            {
                StudentInfoBll studentInfoBll = new StudentInfoBll();
                Bas_Student student = new Bas_Student();

                student.Bst_Id = GetCurrentUser().StudentId;
                student.Bst_NickName = inputModel.NickName;
                student.Bst_Name = inputModel.Name;
                student.Bst_Sex = inputModel.Sex;
                student.Bst_Birthday = inputModel.Birthday.CompareTo(Convert.ToDateTime("0001-01-01"))==0?Convert.ToDateTime("1900-01-01"): inputModel.Birthday;
                student.Bst_Grade = inputModel.Grade;
                student.Bst_StudySchool = inputModel.StudySchool;
                student.Bst_Province = inputModel.Bst_Province;
                student.Bst_City = inputModel.Bst_City;
                student.Bst_County = inputModel.Bst_County;
                student.Bst_Address = inputModel.Address;

                if (!string.IsNullOrEmpty(inputModel.HeadPhoto))
                {
                    student.Bst_Avatar = Upload(inputModel.HeadPhoto).FileUrl;
                }

                studentInfoBll.Update(student);
                return RedirectToAction("StudentInfo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetSchoolName(string phone)
        {
            SchoolTeacherBll schoolTeacherBll = new SchoolTeacherBll();
            var result = schoolTeacherBll.GetSchoolTeacherByPhone(phone);
            if (result != null && result.Yoh_SchoolId > 0)
            {
                return Json(new JsonResponse<string>() { State = true, ErrorMsg = "查询成功", Data = result.Bsl_SchoolName });
            }
            return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "查询失败" });
        }
        public ActionResult BindSchool(string phone)
        {
            StudentApplyBll studentApplyBll = new StudentApplyBll();
            var result = studentApplyBll.StudentApply(GetCurrentUser().StudentId, phone);
            if (result)
            {
                SchoolTeacherBll schoolTeacherBll = new SchoolTeacherBll();
                var school = schoolTeacherBll.GetSchoolTeacherByPhone(phone);
                return Json(new JsonResponse<DtoSchoolTeacher>() { State = true, ErrorMsg = "申请成功",Data=school });
            }
            return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "申请失败" });
        }

        public ActionResult StudentInfoEdit()
        {
            return View();
        }

        [HttpPost]
        public FileUploadResult Upload(string base64String)
        {
            try
            {
                string directory = $"StuAvatar/Images";
                FileManageClient client = new FileManageClient(ConfigurationManager.AppSettings["uploadUrl"]);
                //base64String = base64String.Replace("data:image/png;base64,", string.Empty);
                base64String = base64String.Split(',')[1];

                string fileName = Guid.NewGuid().ToString("N") + ".png";
                //Base64形式的String转成byte[]
                var imgByte = Convert.FromBase64String(base64String);
                MemoryStream fs = null;
                FileUploadResult response = null;
                using (fs = new MemoryStream(imgByte))
                {
                    response = client.Upload(fileName, fs, ConfigurationManager.AppSettings["OssSubject"], directory, true);
                }
                if (response.Code == FileManageStatusCode.Success)
                {
                    response.FileUrl = $"/{directory}/{response.FileName}";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("网络出错了，头像上传失败");
            }
        }

        public ActionResult GetSumStudentInfo()
        {
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            if(GetCurrentUser()==null)
            {
                return RedirectToAction("LoginIndex", "Login");
            }
            DtoSumStudentTip sumStudent = studentInfoBll.GetSumStudentTip(GetCurrentUser().StudentId);
            var lastLoginTime = WebHelper.GetCookie("LastLoginTime");
            if(string.IsNullOrEmpty(lastLoginTime))
            {
                sumStudent.PrevLoginTime = "";
            }
            else
            {
                sumStudent.PrevLoginTime = Encrypt.DecryptQueryString(lastLoginTime);
            }
            return Json(new JsonResponse<DtoSumStudentTip>() { State = true, ErrorMsg = "查询成功", Data = sumStudent },JsonRequestBehavior.AllowGet);
        }
    }
}