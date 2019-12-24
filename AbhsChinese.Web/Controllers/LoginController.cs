using AbhsChinese.Bll;
using AbhsChinese.Bll.AccessCheckPolicy;
using AbhsChinese.Bll.BaiduApi;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Common.AccessCheck;
using AbhsChinese.Code.Common.SMS;
using AbhsChinese.Domain.Dto.Request.Student;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Web.App_Start.Filter;
using AbhsChinese.Web.Models.Login;
using AbhsChinese.Web.Models.StudentInfo;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        #region 登录
        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginIndex(int active = 0)
        {
            return View(active);
        }

        [HttpGet]
        public ActionResult GetAuthCode()
        {
            return File(VerifyCode.GetVerifyCode(), @"image/Gif");
        }

        /// <summary>
        /// 账号密码登录
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckLogin(LoginInputModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.PassportKey.Trim()) || string.IsNullOrEmpty(inputModel.Password.Trim()))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.InputNotNull) };
            }

            if (!string.IsNullOrEmpty(inputModel.SmsCode))
            {
                if (WebHelper.GetCookie("abhs_verifycode") != Encrypt.MD5(inputModel.SmsCode.ToLower()))
                {
                    return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.AuthCodeError) };
                }
            }

            //LogonUserAccessCheckPolicy checkPolicy = new LogonUserAccessCheckPolicy(
            //    AccessCheckKeyEnum.Chinese_StudentLoginCode_Check,
            //    inputModel.PassportKey,
            //    60,
            //    3);
            //var status = checkPolicy.CheckAdvanced();

            int code = 0;
            //if (status >= AccessCheckEnum.PreLimited)
            //{
            //    code = (int)AccessCheckEnum.Limited;
            //}
            
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            inputModel.Password = Encrypt.GetMD5Pwd(inputModel.Password);
            var student = studentInfoBll.Login(inputModel.PassportKey, inputModel.Password);
            if (student != null)
            {
                var sumStudent = studentInfoBll.GetSumStudentTip(student.Bsp_StudentId);
                LoginInfo info = sumStudent.ConvertTo<LoginInfo>();
                LoginStudent.Info = JsonConvert.SerializeObject(info);
                return Json(new JsonSimpleResponse() { State = true, ErrorCode = 1, ErrorMsg = "登录成功" });
            }
            return Json(new JsonResponse<int>() { State = false, ErrorCode = code, Data = -1, ErrorMsg = "账号或密码错误" });
        }

        /// <summary>
        /// 手机验证码登录
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MobileCodeLogin(LoginInputModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.Phone.Trim()) || string.IsNullOrEmpty(inputModel.SmsCode.Trim()))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.InputNotNull) };
            }

            if (SmsCookie.GetSmsCode == null || !SmsCookie.GetSmsCode.Check(inputModel.Phone, inputModel.SmsCode))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.PhoneCodeFault) };
            }
            
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            var result = studentInfoBll.MobileLogin(inputModel.Phone);
            if (result != null)
            {

                var sumStudent = studentInfoBll.GetSumStudentTip(result.Bsp_StudentId);
                LoginInfo info = sumStudent.ConvertTo<LoginInfo>();
                LoginStudent.Info = JsonConvert.SerializeObject(info);
                return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "登录成功", ErrorCode = 1 });
            }
            return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "登录失败", ErrorCode = -1 });
        }

        /// <summary>
        /// 人脸登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FaceLogin(FaceRegisterRequest request)
        {

            FaceRecognitionResponse response = BaiduApiBll.FaceLogin(request.ImgPath, request.ImgType);

            //状态码不为0或者识别得分小于80，验证失败
            if (response.error_code > 0 || response.result.user_list.FirstOrDefault().score < 80)
            {
                var errorCode = -1;
                //状态码为18，QPS超限额
                if (response.error_code == 18)
                {
                    errorCode = -2;
                }
                return Json(new JsonResponse<FaceRecognitionResponse>() { State = false, ErrorCode = errorCode, Data = response, ErrorMsg = "登录失败，请稍后再试" });
            }

            StudentInfoBll studentInfoBll = new StudentInfoBll();
            var studentPassport = studentInfoBll.GetStudentPassport(response.result.user_list.FirstOrDefault().user_id._ToInt32(), StudentAccountSourceEnum.人脸识别);
            if (studentPassport != null)
            {
                var sumStudent = studentInfoBll.GetSumStudentTip(studentPassport.Bsp_StudentId);
                LoginInfo info = sumStudent.ConvertTo<LoginInfo>();
                LoginStudent.Info = JsonConvert.SerializeObject(info);
                return Json(new JsonSimpleResponse() { State = true, ErrorCode = 1, ErrorMsg = "登录成功" });
            }
            return Json(new JsonSimpleResponse() { State = false, ErrorCode = -1, ErrorMsg = "登录失败" });
        }

        public ActionResult FaceBind()
        {
            return View();
        }

        public ActionResult FaceBindSuccess()
        {
            return View();
        }

        public ActionResult ForgetPwd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePwd(RegisterInputModel inputModel)
        {
            if (SmsCookie.GetSmsCode == null || !SmsCookie.GetSmsCode.Check(inputModel.Phone, inputModel.SmsCode))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.PhoneCodeFault) };
            }
            
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            studentInfoBll.UpdatePasswordByPhone(inputModel.Phone, inputModel.Password);
            return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功", ErrorCode = 1 });
        }

        /// <summary>
        /// 人脸绑定
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BindFace(FaceBindInputModel inputModel)
        {
            if (SmsCookie.GetSmsCode == null || !SmsCookie.GetSmsCode.Check(inputModel.Phone, inputModel.SmsCode))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.PhoneCodeFault) };
            }

            StudentInfoBll studentInfoBll = new StudentInfoBll();
            var studentPassort = studentInfoBll.GetByPassportKey(inputModel.Phone);
            if (studentPassort == null)
            {
                return Json(new JsonResponse<int>() { State = false, ErrorCode = -1, ErrorMsg = "用户名不存在" });
            }
            
            FaceRecognitionResponse response = BaiduApiBll.FaceRegister(inputModel.Image, inputModel.ImageType, studentPassort.Bsp_StudentId.ToString(), "");
            if (response.error_code == 0)
            {

                studentInfoBll.AddStudentPassport(studentPassort.Bsp_StudentId, StudentAccountSourceEnum.人脸识别, response.result.face_token, "");
                return Json(new JsonResponse<FaceRecognitionResponse>() { Data = response, ErrorCode = 1, ErrorMsg = "绑定成功" });
            }
            return Json(new JsonResponse<FaceRecognitionResponse>() { Data = response, ErrorCode = 0, ErrorMsg = "绑定失败" });
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutLogin()
        {
            LoginStudent.Info = "";
            return RedirectToAction("LoginIndex");
        }
        #endregion

        #region 注册
        /// <summary>
        /// 注册页
        /// </summary>
        /// <returns></returns>

        public ActionResult RegIndex()
        {
            return View();
        }

        public ActionResult AuthCode()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterInputModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.Phone.Trim()))
            {
                return Json(new JsonSimpleResponse() { ErrorCode = -5, ErrorMsg = "请输入手机号" });
            }
            if (string.IsNullOrEmpty(inputModel.SmsCode.Trim()))
            {
                return Json(new JsonSimpleResponse() { ErrorCode = -6, ErrorMsg = "请输入手机验证码" });
            }
            if (string.IsNullOrEmpty(inputModel.Name.Trim()))
            {
                return Json(new JsonSimpleResponse() { ErrorCode = -7, ErrorMsg = "请输入姓名" });
            }
            if (inputModel.Grade <= 0)
            {
                return Json(new JsonSimpleResponse() { ErrorCode = -8, ErrorMsg = "请选择年级" });
            }

            if (SmsCookie.GetSmsCode == null || !SmsCookie.GetSmsCode.Check(inputModel.Phone, inputModel.SmsCode))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.PhoneCodeFault) };
            }

            StudentInfoBll studentInfoBll = new StudentInfoBll();
            var isExist = studentInfoBll.IsExistPhone(inputModel.Phone);
            if (isExist)
            {
                return Json(new JsonSimpleResponse() { ErrorCode = -9, ErrorMsg = "手机号已存在" });
            }
            inputModel.Password = Encrypt.GetMD5Pwd(inputModel.Password);
            DtoStudentRegister register = inputModel.ConvertTo<DtoStudentRegister>();
            int studentId = studentInfoBll.Register(register);
            if (studentId > 0)
            {
                DtoSumStudentTip sumStudent = studentInfoBll.GetSumStudentTip(studentId);
                LoginInfo info = sumStudent.ConvertTo<LoginInfo>();
                LoginStudent.Info = JsonConvert.SerializeObject(info);
                return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "注册成功", ErrorCode = 1 });
            }
            return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "注册失败", ErrorCode = -1 });
        }
        #endregion


        public ActionResult StudentLogin(string sign, string t, int courseId)
        {
            TimeSpan span = new TimeSpan(Convert.ToInt64(t)) - new TimeSpan(DateTime.Now.Ticks);
            if (span.TotalSeconds > 60)
            {
                throw new Exception();
            }
            string rightsign = Encrypt.MD5Hash(t, "8j0pf");
            if (rightsign != sign)
            {
                throw new Exception();
            }

            StudentInfoBll studentInfoBll = new StudentInfoBll();
            var student = studentInfoBll.GetBySchoolId();
            var sumStudent = studentInfoBll.GetSumStudentTip(student.Bst_Id);
            LoginInfo info = sumStudent.ConvertTo<LoginInfo>();
            LoginStudent.Info = JsonConvert.SerializeObject(info);
            return new RedirectResult($"/Course/PreviewCourse/{courseId}");
        }
    }
}