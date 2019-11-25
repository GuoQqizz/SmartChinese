using System.Web;
using System.Web.Optimization;

namespace AbhsChinese.School
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
                     "~/Content/style.patch.css"));
            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                     "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/PageScripts/jquery.ajax.extension.js",
                        "~/Scripts/PageScripts/jquery.htmlhelper.js",
                        "~/Scripts/jquery.form.min.js"));

            //jQuery validate
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/PageScripts/jquery.validate.extension.js",
                        "~/Scripts/PageScripts/ajax-form.main.js",
                        "~/Scripts/jquery.validate.messages_zh.js"
                        ));

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
            bundles.Add(new StyleBundle("~/bundles/css/iCheckStyles").Include(
                      "~/Content/custom.css"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/js/icheck").Include(
                      "~/Scripts/icheck.min.js",
                      "~/Scripts/PageScripts/icheck.main.js"));


            // layer CSS
            //bundles.Add(new StyleBundle("~/Content/css/layer").Include(
            //          "~/Content/layer/theme/default/layer.css"));

          

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
            bundles.Add(new StyleBundle("~/bundles/css/datePickerStyles").Include(
                      "~/Content/datepicker/datepicker3.css"));

            // dataPicker
            bundles.Add(new ScriptBundle("~/bundles/js/datePicker").Include(
                      "~/Scripts/datepicker/bootstrap-datepicker.js"
                      //"~/Scripts/datepicker/bootstrap-datetimepicker.zh-CN.js"
                      ));
            // datetimePicker styles
            bundles.Add(new StyleBundle("~/bundles/css/datetimePickerStyles").Include(
                      "~/Content/datetimepicker/bootstrap-datetimepicker.min.css"));
            // datetimePicker
            bundles.Add(new ScriptBundle("~/bundles/js/datetimePicker").Include(
                      "~/Scripts/datetimepicker/bootstrap-datetimepicker.min.js",
                      "~/Scripts/datetimepicker/bootstrap-datetimepicker.zh-CN.js"
                      ));

            // timePicker styles
            bundles.Add(new StyleBundle("~/bundles/css/timePickerStyles").Include(
                      "~/Content/timepicker/bootstrap-timepicker.min.css"));
            // timePicker
            bundles.Add(new ScriptBundle("~/bundles/js/timePicker").Include(
                      "~/Scripts/timepicker/bootstrap-timepicker.min.js"
                      ));

            // wizardSteps styles
            bundles.Add(new StyleBundle("~/bundles/css/wizardStepsStyles").Include(
                      "~/Content/steps/jquery.steps.css"));

            // wizardSteps
            bundles.Add(new ScriptBundle("~/bundles/js/wizardSteps").Include(
                      "~/Scripts/steps/jquery.steps.min.js"));

            // pagebase
            bundles.Add(new ScriptBundle("~/bundles/js/pagebase").Include(
                        "~/Scripts/PageScripts/pagebase.js"));
            //common
            bundles.Add(new ScriptBundle("~/bundles/js/common").Include(
                        "~/Scripts/common/base.js",
                        "~/Scripts/const/index.js"
                        //,"~/Scripts/extend.js"
                        ));


            //layer js
            //bundles.Add(new ScriptBundle("~/js/layer").Include(
            //"~/Content/layer/layer.js"));
        }
    }
}
