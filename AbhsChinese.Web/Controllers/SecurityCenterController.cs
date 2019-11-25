using AbhsChinese.Bll;
using AbhsChinese.Bll.BaiduApi;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Common.SMS;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Web.Models.Common;
using AbhsChinese.Web.Models.Login;
using AbhsChinese.Web.Models.StudentInfo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    public class SecurityCenterController : BaseController
    {
        #region 人脸识别
        public ActionResult Index()
        {
            StudentInfoBll studentBll = new StudentInfoBll();
            var studentInfo = studentBll.GetStudentInfo(GetCurrentUser().StudentId);
            var viewModel = studentInfo.ConvertTo<StudentInfoViewModel>();
            return View(viewModel);
        }

        /// <summary>
        /// 人脸绑定
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BindFace(FaceRegisterRequest request)
        {
            FaceRecognitionResponse response = BaiduApiBll.FaceRegister(request.ImgPath, request.ImgType, GetCurrentUser().StudentId.ToString(), request.UserInfo);
            if (response.error_code == 0)
            {
                StudentInfoBll studentInfoBll = new StudentInfoBll();
                studentInfoBll.AddStudentPassport(GetCurrentUser().StudentId, StudentAccountSourceEnum.人脸识别, response.result.face_token, "");
                return Json(new JsonResponse<FaceRecognitionResponse>() { Data = response, ErrorCode = 1, ErrorMsg = "绑定成功" });
            }
            return Json(new JsonResponse<FaceRecognitionResponse>() { Data = response, ErrorCode = 0, ErrorMsg = "绑定失败" });
        }

        /// <summary>
        /// 删除人脸识别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteFace(SmsInputModel smsModel)
        {
            if (SmsCookie.GetSmsCode == null || !SmsCookie.GetSmsCode.Check(smsModel.Phone, smsModel.SmsCode))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.PhoneCodeFault) };
            }

            StudentInfoBll studentInfoBll = new StudentInfoBll();
            studentInfoBll.UpdateStatus(GetCurrentUser().StudentId, StudentAccountStatusEnum.禁用);

            return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功" });
        }

        /// <summary>
        /// 更新人脸识别
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateFace(FaceRegisterRequest request)
        {
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            var response = BaiduApiBll.FaceUpdate(request.ImgPath, request.ImgType, GetCurrentUser().StudentId.ToString(), request.UserInfo);
            if (response.error_code == 0)
            {
                var result = studentInfoBll.UpdatePassportKey(GetCurrentUser().StudentId, response.result.face_token);
                return Json(new JsonResponse<FaceRecognitionResponse>() { Data = response, ErrorCode = 1, ErrorMsg = "操作成功" });
            }
            return Json(new JsonResponse<FaceRecognitionResponse>() { Data = response, ErrorCode = 0, ErrorMsg = "操作失败" });
        }

        /// <summary>
        /// 人脸检测
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult FaceCheck(FaceRegisterRequest request)
        {
            FaceCheckResponse response = BaiduApiBll.FaceCheck(request.ImgPath, request.ImgType);
            if(response.result==null)
            {
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "检测异常" });
            }
            var faceInfo = response.result.face_list.FirstOrDefault();
            if (response.error_code == 0 && response.result.face_num == 1 && faceInfo.face_probability == 1 && faceInfo.liveness.livemapscore >= 0.8 && faceInfo.quality.blur == 0 && faceInfo.quality.completeness == 1 && faceInfo.quality.illumination > 40 && faceInfo.angle.pitch < 20 && faceInfo.angle.pitch > -20 && faceInfo.angle.roll < 20 && faceInfo.angle.roll > -20 && faceInfo.angle.yaw < 20 && faceInfo.angle.yaw > -20 && faceInfo.quality.occlusion.left_eye <= 0.6 && faceInfo.quality.occlusion.right_eye <= 0.6 && faceInfo.quality.occlusion.nose <= 0.7 && faceInfo.quality.occlusion.mouth <= 0.7 && faceInfo.quality.occlusion.left_cheek <= 0.8 && faceInfo.quality.occlusion.rigth_cheek <= 0.8 && faceInfo.quality.occlusion.chin <= 0.6)
            {
                return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "检测通过" });
            }
            return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "检测异常" });
        }

        public ActionResult IsBindFaceLogin()
        {
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            var isBind = studentInfoBll.IsBindFaceLogin(GetCurrentUser().StudentId);
            return Json(new JsonSimpleResponse() { State = isBind });
        }
        #endregion

        #region 更换手机
        [HttpPost]
        public ActionResult ChangeMobile(SmsInputModel smsModel)
        {
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            var isExsit = studentInfoBll.IsExistPhone(smsModel.Phone);
            if (isExsit)
            {
                return Json(new JsonSimpleResponse() { State = false, ErrorMsg = "该手机号已存在" });
            }
            if (SmsCookie.GetSmsCode == null || !SmsCookie.GetSmsCode.Check(smsModel.Phone, smsModel.SmsCode))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.PhoneCodeFault) };
            }
            studentInfoBll.UpdateMobile(GetCurrentUser().StudentId, smsModel.Phone);
            return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功" });
        }

        #endregion

        #region 修改密码
        public ActionResult UpdatePassword(string oldPassword, string newPassword)
        {
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            var result = studentInfoBll.UpdatePassword(GetCurrentUser().StudentId, oldPassword, newPassword);
            if (result == -1)
            {
                return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作失败",ErrorCode=-1 });
            }
            return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功", ErrorCode=result });
        }
        #endregion

        #region 找回密码
        [HttpPost]
        public ActionResult FindPassword(string password)
        {
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            studentInfoBll.FindPassword(GetCurrentUser().StudentId, password);
            return Json(new JsonSimpleResponse() { State = true, ErrorMsg = "操作成功" });
        }
        #endregion

        #region 登录记录
        public ActionResult GetStudentLogin(int pageIndex, int pageSize)
        {
            StudentInfoBll studentInfoBll = new StudentInfoBll();
            PagingObject paging = new PagingObject(pageIndex, pageSize);
            var studentLogins = studentInfoBll.GetStudentLogin(paging, GetCurrentUser().StudentId);
            var viewModels = studentLogins.ConvertTo<List<StudentLoginViewModel>>(Code.Json.PropertyNamePrefixAction.Remove);
            var table = AbhsTableFactory.Create<StudentLoginViewModel>(viewModels, paging.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 手机验证
        [HttpPost]
        public ActionResult ValidateMobile(SmsInputModel smsModel)
        {
            if (SmsCookie.GetSmsCode == null || !SmsCookie.GetSmsCode.Check(smsModel.Phone, smsModel.SmsCode))
            {
                return new JsonResult() { Data = AjaxResponse.Fail(SmsErrorEnum.PhoneCodeFault) };
            }
            return new JsonResult() { Data = AjaxResponse.Success() };
        }
        #endregion
    }
}