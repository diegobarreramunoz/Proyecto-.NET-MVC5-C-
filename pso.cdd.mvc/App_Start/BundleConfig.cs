using System.Web;
using System.Web.Optimization;

namespace pso.cdd.mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/scripts/charts").Include(
                "~/js/libs/jquery-2.1.1.min.js",
                "~/js/libs/jquery-ui-1.10.3.min.js",
                "~/js/bootstrap/bootstrap.min.js",
                "~/js/notification/SmartNotification.min.js",
                "~/js/plugin/markdown/markdown.min.js",
                "~/js/plugin/markdown/to-markdown.min.js",
                "~/js/plugin/markdown/bootstrap-markdown.min.js",
                "~/js/plugin/moment/moment.min.js",
                "~/js/plugin/fullcalendar/jquery.fullcalendar.min.js",
                "~/js/plugin/select2/select2.min.js",
                "~/js/plugin/datatables/jquery.dataTables.min.js",
                "~/js/plugin/datatables/dataTables.colVis.min.js",
                "~/js/plugin/datatables/dataTables.tableTools.min.js",
                "~/js/plugin/datatables/dataTables.bootstrap.min.js",
                "~/js/plugin/datatable-responsive/datatables.responsive.min.js",
                //,"~/js/app.min.js"

                "~/js/app.config.seed.js",
                "~/js/app.seed.js",
                "~/js/custom.js",

                "~/js/plugin/chartjs/chart.min.js",
                "~/js/plugin/highChartCore/highcharts-custom.min.js",
                "~/js/plugin/highchartTable/jquery.highchartTable.min.js",

                "~/js/plugin/jquery-touch/jquery.ui.touch-punch.min.js",
                "~/js/bootstrap/bootstrap.min.js",
                "~/js/notification/SmartNotification.min.js",
                "~/js/smartwidgets/jarvis.widget.min.js"
                //,"~/js/app.min.js"

                

            ));
        }
    }
}
