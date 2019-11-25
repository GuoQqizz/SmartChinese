using System.Web.Optimization;
using WenDu.NetSchool.Admin.Models.Common;

namespace AbhsChinese.Admin
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Bootstrap CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Content/animate.css",
                     "~/Content/style.css",
                     "~/Content/style.patch.css")
                     .Include("~/Content/custom.css", new CssRewriteUrlTransformWrapper()));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                     "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/PageScripts/jquery.ajax.extension.js",
                        "~/Scripts/jquery.form.min.js"));

            //jQuery validate
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.messages_zh.js",
                        "~/Scripts/PageScripts/jquery.validate.extension.js",
                        "~/Scripts/PageScripts/ajax-form.main.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/popper.min.js",
                      "~/Scripts/bootstrap.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/bundles/js/slimScroll").Include(
                      "~/Scripts/jquery.slimscroll.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/js/inspinia").Include(
                      "~/Scripts/jquery.metisMenu.js",
                      "~/Scripts/inspinia.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/bundles/css/iCheckStyles").Include("~/Content/custom.css", new CssRewriteUrlTransformWrapper()));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/js/icheck").Include(
                      "~/Scripts/icheck.min.js",
                      "~/Scripts/PageScripts/icheck.main.js"));

            //// layer CSS
            //bundles.Add(new StyleBundle("~/Content/layer").Include(
            //          "~/Content/layer/theme/default/layer.css"));

            ////layer
            //bundles.Add(new ScriptBundle("~/Script/layer").Include(
            //         "~/Content/layer/layer.js"));

            //图片裁剪插件Cropper CSS
            bundles.Add(new StyleBundle("~/bundles/css/cropperStyles").Include(
                      "~/Content/cropper/cropper.min.css"));

            //图片裁剪插件Cropper js
            bundles.Add(new ScriptBundle("~/bundles/js/cropper").Include(
                "~/Content/cropper/cropper.min.js",
                "~/Content/cropper/cropper.patch.js"));

            //abhsTable css
            bundles.Add(new StyleBundle("~/bundles/css/abhsTableStyles").Include(
                "~/Content/abhsTable/abhsTable.css"));

            //abhsTable js
            bundles.Add(new ScriptBundle("~/bundles/js/abhsTable").Include(
                "~/Content/abhsTable/abhsTable.js",
                "~/Scripts/PageScripts/abhstable.main.js"));

            //checkbox 插件 switchery css
            bundles.Add(new StyleBundle("~/bundles/css/switcheryStyles").Include(
                "~/Content/switchery/switchery.css"));

            //checkbox 插件 switchery js
            bundles.Add(new ScriptBundle("~/bundles/js/switchery").Include(
                "~/Content/switchery/switchery.js"));

            // Select2 Styless
            bundles.Add(new StyleBundle("~/bundles/css/select2Styles").Include(
                      "~/Content/select2/select2.min.css"));

            // Select2
            bundles.Add(new ScriptBundle("~/bundles/js/select2").Include(
                      "~/Content/select2/select2.full.min.js",
                      "~/Scripts/PageScripts/select2.main.js"));

            // Taginputs Styless
            bundles.Add(new StyleBundle("~/bundles/css/tagInputsStyles").Include(
                      "~/Content/bootstrap-tagsinput/bootstrap-tagsinput.css"));

            // Taginputs
            bundles.Add(new ScriptBundle("~/bundles/js/tagInputs").Include(
                      "~/Scripts/bootstrap-tagsinput/bootstrap-tagsinput.js"));

            // Typehead
            bundles.Add(new ScriptBundle("~/bundles/js/typehead").Include(
                      "~/Content/typehead/bootstrap3-typeahead.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/bundles/css/dataPickerStyles").Include(
                      "~/Content/datepicker/datepicker3.css"));

            // dataPicker
            bundles.Add(new ScriptBundle("~/bundles/js/dataPicker").Include(
                      "~/Scripts/datepicker/bootstrap-datepicker.js"
                      //"~/Scripts/datepicker/bootstrap-datetimepicker.zh-CN.js"
                      ));

            // wizardSteps styles
            bundles.Add(new StyleBundle("~/bundles/css/wizardStepsStyles").Include(
                      "~/Content/steps/jquery.steps.css"));

            // wizardSteps
            bundles.Add(new ScriptBundle("~/bundles/js/wizardSteps").Include(
                      "~/Scripts/steps/jquery.steps.min.js"));

            // const
            bundles.Add(new ScriptBundle("~/bundles/js/const/index").Include(
                      "~/Scripts/const/index.js"));

            // htmlhelper
            bundles.Add(new ScriptBundle("~/bundles/js/htmlhelper").Include(
                        "~/Scripts/PageScripts/jquery.htmlhelper.js"));
            //common
            bundles.Add(new ScriptBundle("~/bundles/js/common").Include(
                        "~/Scripts/common/base.js",
                        "~/Scripts/const/index.js"
                        //,"~/Scripts/extend.js"
                        ));
        }
    }
}