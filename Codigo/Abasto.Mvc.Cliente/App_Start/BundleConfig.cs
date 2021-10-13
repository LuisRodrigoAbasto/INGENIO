using System.Web;
using System.Web.Optimization;

namespace Abasto.Mvc.Cliente
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jscookie").Include(
                       "~/Scripts/js-cookie/js.cookie.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/dx").Include(
                        "~/Scripts/jszip.js",
                        "~/Scripts/dx.all.js",
                        "~/Scripts/devextreme-localization/dx.messages.es.js"
                        //, "~/Scripts/dx.viz.js"
                        ));            
            bundles.Add(new ScriptBundle("~/bundles/shared").Include(
                 "~/ScriptsApp/Shared/_Layout.js", "~/ScriptsApp/Shared/Page.js"));

            bundles.Add(new ScriptBundle("~/bundles/exportardatagrid").Include(
                "~/Scripts/libs/exceljs.min.js",
                "~/Scripts/libs/FileSaver.min.js",
                 "~/Scripts/libs/polyfill.min.js",
                 "~/Scripts/libs/jspdf.umd.min.js",
                 "~/Scripts/libs/jspdf.plugin.autotable.min.js"
                 ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
