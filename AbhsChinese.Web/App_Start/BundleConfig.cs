using AbhsChinese.Web.Models.Common;
using System.Web;
using System.Web.Optimization;

namespace AbhsChinese.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/Common/jquery.ajax.extension.js"
                        ));
            //jQuery-spin
            bundles.Add(new ScriptBundle("~/bundles/ajaxspin").Include(
                        "~/Scripts/Common/spin.js",
                        "~/Scripts/Common/jquery.ajax.extension.spin.js"
                        ));

            //课程列表（选课中心）
            bundles.Add(new ScriptBundle("~/bundles/courseSelect").Include(
                        "~/Scripts/course/common.js",
                        "~/Scripts/course/select.js"));
            //课程中心（课程详情）
            bundles.Add(new ScriptBundle("~/bundles/courseDetail").Include(
                        "~/Scripts/json2.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/course/common.js",
                        "~/Scripts/course/detail.js",
                        "~/Scripts/voucher/voucher.js",
                        "~/Scripts/voucher/voucherCourseDetail.js"));
            //课程中心（课程订单-支付）
            bundles.Add(new ScriptBundle("~/bundles/orderpayment").Include(
                        "~/Scripts/json2.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/course/common.js",
                        "~/Scripts/course/order.js",
                        "~/Scripts/voucher/voucher.js",
                        "~/Scripts/voucher/voucherOrder.js"));

            //通用样式
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/StudentInfo/style.css"));

            //abhsTable css
            bundles.Add(new StyleBundle("~/bundles/css/abhsTableStyles").Include(
                "~/Content/abhsTable/abhsTable.css"));

            //abhsTable js
            bundles.Add(new ScriptBundle("~/bundles/js/abhsTable").Include(
                "~/Content/abhsTable/abhsTable.js",
                "~/Scripts/Common/abhstable.main.js"));



            //错题本（错题列表）
            bundles.Add(new ScriptBundle("~/bundles/wrongbook").Include(
                        "~/Scripts/parger.js",
                        "~/Scripts/StudentInfo/LeftBar.js",
                        "~/Scripts/Common/abhs.date.js",
                        "~/Scripts/StudentWrong/index.js"));
            //错题本（清除错题）
            bundles.Add(new ScriptBundle("~/bundles/clearwrong").Include(
                        "~/Scripts/template-native.js",
                        "~/Content/LessonStudy/javascript/jquery.raty.js",
                        "~/Scripts/Subject/onLine.js",
                        "~/Scripts/Subject/makeLine.js",
                        "~/Scripts/Subject/AfterClass.js",
                        "~/Scripts/StudentWrong/clearwrong.js"
                        ));
            //错题本（错题xq）
            bundles.Add(new StyleBundle("~/Content/wrongdetail").Include(
                      "~/Content/StudentInfo/audioplayer.css"));
            //错题本（错题详情）
            bundles.Add(new ScriptBundle("~/bundles/wrongdetail").Include(
                        "~/Scripts/StudentInfo/LeftBar.js",
                        "~/Scripts/Subject/showQuestion.js",
                        "~/Scripts/StudentWrong/wrongdetail.js"));


            //学习中心-学习过程
            bundles.Add(new StyleBundle("~/Content/curriculum").Include(
                      "~/Content/LessonStudy/css/reset.css", new CssRewriteUrlTransformWrapper()).Include("~/Content/LessonStudy/css/animate.css", new CssRewriteUrlTransformWrapper()).Include("~/Content/LessonStudy/css/style.css", new CssRewriteUrlTransformWrapper()));

            //学习中心-学习过程-其他合并
            bundles.Add(new ScriptBundle("~/bundles/curriculumOther").Include(
                        "~/Content/LessonStudy/javascript/tryAutoPlay.js",
                        "~/Content/LessonStudy/javascript/extend.js",
                        "~/Content/LessonStudy/javascript/onLine.js",
                        "~/Content/LessonStudy/javascript/template-native.js"));
            //学习中心-学习过程-主逻辑js
            bundles.Add(new ScriptBundle("~/bundles/curriculum").Include(
                        "~/Content/LessonStudy/javascript/Curriculum.js"));


            //登录
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                        "~/Scripts/StudentInfo/login.js"));
            //注册
            bundles.Add(new ScriptBundle("~/bundles/register").Include(
                        "~/Scripts/StudentInfo/register.js"));
            //忘记密码
            bundles.Add(new ScriptBundle("~/bundles/forgetpwd").Include(
                        "~/Scripts/StudentInfo/forgetpwd.js"));
            //安全中心
            bundles.Add(new ScriptBundle("~/bundles/SecurityCenter").Include(
                        "~/Scripts/StudentInfo/LeftBar.js",
                        "~/Scripts/StudentInfo/cropper.min.js",
                        "~/Scripts/StudentInfo/cropper.patch.js",
                        "~/Scripts/StudentInfo/webcam.js",
                        "~/Scripts/StudentInfo/SecurityCenter.js"));

            //学习报告
            bundles.Add(new ScriptBundle("~/bundles/report").Include(
                        "~/Scripts/Subject/showQuestion.js",
                        "~/Scripts/StudentReport/subjectfactory.js"));
            //错题本（错题详情）
            bundles.Add(new ScriptBundle("~/bundles/practice").Include(
                        "~/Scripts/template-native.js",
                        "~/Scripts/Subject/onLine.js",
                        "~/Scripts/StudyTask/studytask.practice.js"));

        }
    }
}
