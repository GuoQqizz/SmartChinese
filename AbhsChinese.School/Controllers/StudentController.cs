using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Common.SMS;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Request.School;
using AbhsChinese.Domain.Dto.Request.Student;
using AbhsChinese.Domain.Enum;
using AbhsChinese.School.Helpers;
using AbhsChinese.School.Models.Common;
using AbhsChinese.School.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.School.Controllers
{
    public class StudentController : BaseController
    {
        SchoolBll schoolBll = new SchoolBll();
        StudentApplyBll studentApplyBll = new StudentApplyBll();
        StudentBll studentBll = new StudentBll();
        StudentInfoBll studentInfoBll = new StudentInfoBll();
        StudentStudyBll studentStudyBll = new StudentStudyBll();
        // GET: Student
        #region 学生列表相关操作
        public ActionResult Index()
        {
            DtoSchoolStudentSearch search = new DtoSchoolStudentSearch();
            search.SchoolId = CurrentUser.Teacher.Yoh_SchoolId;
            return View(search);
        }

        public ActionResult GetStudentList(DtoSchoolStudentSearch search)
        {
            var list = studentBll.GetSchoolStudentList(search);
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StudentInfo(int studentId)
        {
            StudentInfoVM vm = new StudentInfoVM();
            var studentInfo = studentInfoBll.GetStudentInfo(studentId);
            if (studentInfo == null || studentInfo.Bst_SchoolId != CurrentUser.School.Bsl_Id)
            {
                return RedirectToAction("Index");
            }
            vm.StudentInfo = studentInfo;
            return View(vm);
        }
        [HttpPost]
        public ActionResult AddStudent(string phone, string code)
        {
            bool success = false;
            string msg = "";
            var student = studentInfoBll.GetStudentInfoByAccount(phone);
            bool checkCode = true;
            try
            {
                checkCode = SmsCookie.GetSmsCode.Check(phone, code);
            }
            catch (Exception)
            {
                checkCode = false;
            }

            if (student != null && student.Bst_SchoolId == 0 && checkCode)
            {
                success = studentApplyBll.StudentApply(student.Bst_Id, CurrentUser.Teacher.Yoh_Phone, CurrentUser.Teacher.Yoh_Id);
                msg = success ? "添加成功" : "添加失败";
            }
            else
            {
                if (student == null)
                {
                    msg = "该手机号未注册";
                }
                else if (student.Bst_SchoolId > 0)
                {
                    msg = "该学生已绑定校区";
                }
                else
                {
                    msg = "验证码错误";
                }
                success = false;
            }
            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }
        [HttpPost]
        public ActionResult RegistStudent(string phone, string pwd, string name, int grade, string code)
        {
            bool success = false;
            int studentId = 0;
            string msg = "";
            bool checkCode = true;
            var student = studentInfoBll.GetStudentInfoByAccount(phone);
            if (student != null)
            {
                msg = "该手机号已注册";
            }
            else
            {
                try
                {
                    checkCode = SmsCookie.GetSmsCode.Check(phone, code);
                }
                catch (Exception)
                {
                    checkCode = false;
                }
                if (checkCode)
                {
                    DtoStudentRegister model = new DtoStudentRegister();
                    model.Phone = phone;
                    model.Grade = grade;
                    model.Name = name;
                    model.OperatorId = CurrentUser.Teacher.Yoh_Id;
                    model.PassportType = Domain.Enum.StudentAccountSourceEnum.手机;
                    model.Password = Encrypt.GetMD5Pwd(pwd);
                    model.Phone = phone;
                    model.SmsCode = code;
                    model.SchoolId = 0;
                    model.Source = Domain.Enum.RegisterRegSourceEnum.校区注册;

                    studentId = studentInfoBll.Register(model);
                    if (studentId > 0)
                    {
                        success = studentApplyBll.StudentApply(studentId, CurrentUser.Teacher.Yoh_Phone, CurrentUser.Teacher.Yoh_Id);
                        msg = success ? "注册成功" : "注册成功,添加失败";
                    }
                    else
                    {
                        success = false;
                        msg = "注册失败";
                    }
                    success = true;

                }
                else
                {
                    success = false;
                    msg = "验证码错误";
                }
            }


            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }


        #endregion
        #region 申请学生相关操作
        public ActionResult ApplyIndex()
        {
            DtoApplyStudentSearch search = new DtoApplyStudentSearch();
            search.SchoolId = CurrentUser.Teacher.Yoh_SchoolId;
            return View(search);
        }

        public ActionResult GetApplyList(DtoApplyStudentSearch search)
        {
            var list = studentApplyBll.GetToDoApplyList(search);
            list.ForEach(s =>
            {
                if (!s.Bst_Phone.HasValue() || s.Bst_Phone.Length < 11)
                {
                    s.Bst_PhoneShow = "";
                }
                else
                {
                    var tmp = s.Bst_Phone.Substring(0, 4) + "****" + s.Bst_Phone.Substring(7, 4);
                    s.Bst_PhoneShow = tmp;
                }
                s.Bst_Phone = "";
            });
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateApplyStatus(int applyId, int toStatus)
        {
            bool success = false;
            success = studentApplyBll.UpdateApplyStatus(applyId, toStatus, (int)ApplyStatusEnum.申请, CurrentUser.Teacher.Yoh_Id);
            string msg = success ? "保存成功" : "保存失败";
            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }

        [HttpPost]
        public ActionResult UnBindSchool(int studentId)
        {
            bool success = true;
            success = studentBll.UnBindSchool(studentId, CurrentUser.Teacher.Yoh_SchoolId, CurrentUser.Teacher.Yoh_Id);
            string msg = success ? "解绑成功" : "解绑失败";
            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }
        #endregion

        #region 未分班学生相关操作
        public ActionResult NoClassStudent()
        {
            DtoSchoolNoClassStudentSearch search = new DtoSchoolNoClassStudentSearch();
            search.SchoolId = CurrentUser.Teacher.Yoh_SchoolId;
            return View(search);
        }

        public ActionResult GetNoClassList(DtoSchoolNoClassStudentSearch search)
        {
            var list = studentStudyBll.GetNoClassStudent(search);
            list.ForEach(s =>
            {
                if (s.Bst_Phone.HasValue() && s.Bst_Phone.Length == 11)
                {
                    var tmp = s.Bst_Phone.Substring(0, 4) + "****" + s.Bst_Phone.Substring(7, 4);
                    s.Bst_Phone = tmp;
                }
            });
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AssignClass(int yspId, int classId)
        {
            bool success = false;
            success = studentStudyBll.AssignClass(yspId, classId, CurrentUser.Teacher.Yoh_Id);
            string msg = success ? "分班成功" : "分班失败";
            return Json(new JsonSimpleResponse() { State = success, ErrorMsg = msg });
        }
        #endregion

    }
}