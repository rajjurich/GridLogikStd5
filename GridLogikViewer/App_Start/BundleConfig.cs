using System.Web;
using System.Web.Optimization;

namespace GridLogikViewer
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/js/jquery-1.11.min.js",
                "~/Content/js/bootstrap.min.js",
                //"~/Content/js/bootstrap-datepicker.js",
                "~/Scripts/ProjectType.js",
                "~/Scripts/moment.js",
                "~/Scripts/progress.bar.js",
                "~/Content/js/multipleAccordion.js",
                "~/Content/js/layout.js",
                "~/Scripts/jquery.dataTables.editable.js",
                "~/Scripts/jquery.dataTables.js",
                //"~/Scripts/jquery.dataTables.js",
                //"~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.jeditable.js",
                //"~/Scripts/jquery.dataTables.editable.js",
                //"~/Scripts/dataTables.bootstrap.js",
                "~/Scripts/jquery.jmHighlight.js",
                "~/Scripts/jquery-confirm.js",
                //"~/Scripts/jquery-{version}.js",
                //"~/Scripts/jquery-ui-{version}.js",
           "~/Scripts/jquery-1.10.2.js",
           "~/Scripts/jquery-ui-1.11.4.min.js",
                //"~/Scripts/progress.bar.js",
           "~/Scripts/ConfirmExitGridLogik.js",
           "~/Scripts/respond.js",
                //"~/Content/js/bootstrap.min.js",
           "~/Scripts/Highcharts-4.0.1/js/HighChart_Modify.js",
           "~/Scripts/zoom.js",
           "~/Content/js/target-admin.js",
           "~/Scripts/jquery.dataTables.js",
           "~/Scripts/dataTables.bootstrap.js",
           "~/Scripts/bootstrap-select.js",
           "~/Scripts/chosen.jquery.js",
           "~/Content/select2/js/select2.min.js",
           "~/Scripts/Print.js",
           "~/Scripts/graphDateFormat.js",
           "~/Scripts/jquery.table2excel.js",
           "~/Scripts/bootstrap-select.js"
                //"~/Content/js/main.js"
                //"~/Content/js/dataTables.buttons.min.js"

           //For Energy Log
                //"~/Scripts/jqxcore.js",
                //"~/Scripts/jqxbuttons.js",
                //"~/Scripts/jqxlistbox.js",
                //"~/Scripts/jqxlistbox.js",
                //"~/Scripts/jqxscrollbar.js",
                //"~/Scripts/jqxdragdrop.js",
                //"~/Scripts/dataTables.colReorder.js"
           ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
            "~/Content/css/target-admin.css",
            "~/Content/css/jquery-ui-1.9.2.custom.min.css",
            "~/Content/css/custom.css",
            "~/Content/css/dataTables.bootstrap.css",
            "~/Content/css/bootstrap.widgetbox-min.css",
            "~/Content/css/userdefine-stylesheet/main-stylesheet.css",
            "~/Content/css/bootstrap-select.css",
            "~/Content/css/style-stickyheader.css",
            "~/Content/css/jquery-ui.min.css",
            "~/Content/css/Site.css",
            "~/Content/css/chosen.css",
            "~/Content/css/jquery-confirm.css"
                     ));


            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                     "~/Content/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                      "~/Scripts/jquery.dataTables.js",
                      "~/Scripts/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(                
                      "~/Scripts/jquery.validate.js",
                      "~/Scripts/jquery.validate.unobtrusive.js"
                //"~/Scripts/CustomValidation/CustomValidation.js",

                //"~/Scripts/ConfirmExit.js",
                //"~/Scripts/jquery-confirm.js"
));//Model validation function

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                    "~/Scripts/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/empjs").Include(
                     "~/Scripts/Emp.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
