using System.Web;
using System.Web.Optimization;

namespace TiendaEnLinea.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));



            //< !--Scripts Layout-- > Para el nuevo diseño 
            bundles.Add(new ScriptBundle("~/bundles/Layaot").Include(
                      "~/js/jquery-2.1.1.js",
                      "~/js/plugins/jquery-ui/jquery-ui.min.js",
                      "~/js/bootstrap.min.js",
                      "~/js/inspinia.js",
                       "~/js/bootstrap.file-input.js"
                      ));

            //Aqui agregar las referencias de los plugins a acupar}
            // que estan en la plantilla

            bundles.Add(new ScriptBundle("~/bundles/Plugins").Include(
                      "~/js/plugins/metisMenu/jquery.metisMenu.js",
                      "~/js/plugins/slimscroll/jquery.slimscroll.min.js",
                      "~/js/plugins/pace/pace.min.js",
                      "~/js/plugins/jvectormap/jquery-jvectormap-2.0.2.min.js",
                      "~/js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                      "~/js/plugins/sweetalert/sweetalert.min.js",
                      "~/js/plugins/footable/footable.all.min.js",
                      "~/js/plugins/datapicker/bootstrap-datepicker.js",
                      "~/js/plugins/clockpicker/clockpicker.js",
                       "~/js/plugins/iCheck/icheck.min.js"
                      ));

            //< !--jQuery UI-- >
            //bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
            //            "~/js/plugins/jquery-ui/jquery-ui.min.js"));



            ////<!-- Custom and plugin javascript -->
            //bundles.Add(new ScriptBundle("~/bundles/Custom").Include(
            //          "~/js/inspinia.js",
            //          "~/js/plugins/pace/pace.min.js"));


            ////<!-- Jvectormap -->
            //bundles.Add(new ScriptBundle("~/bundles/Jvectormap").Include(
            //          "~/js/plugins/jvectormap/jquery-jvectormap-2.0.2.min.js",
            //          "~/js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));


            //<!-- Styles Plantilla -->
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/css/bootstrap.min.css",
                      "~/font-awesome/css/font-awesome.css",
                      "~/css/animate.css",
                      "~/css/style.css",
                      "~/css/custom.css"
                      ));


            //<!-- Styles Plantilla -->

            bundles.Add(new StyleBundle("~/Content/plugins").Include(
                      "~/css/plugins/sweetalert/sweetalert.css",
                      "~/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css",
                      "~/css/plugins/footable/footable.core.css",
                      "~/css/plugins/datapicker/datepicker3.css",
                      "~/css/plugins/clockpicker/clockpicker.css",
                      "~/css/plugins/iCheck/custom.css"
                      ));



            //<!-- Styles Bitworks-->
            bundles.Add(new StyleBundle("~/css/bitworkstyles").Include(
                        "~/css/BitworksStyle/style.css"
                        ));



            bundles.Add(new ScriptBundle("~/bundles/jqval").Include(
            "~/Scripts/jquery.validate.js",
            "~/Scripts/jquery.validate.unobtrusive.min.js",
            "~/Scripts/expressive.annotations.validate.min.js"
            ));


            //Librerias Bitworks Se han agregago la utilidades bitworks,ventanas y BitworksAjaxNavigation
            bundles.Add(new ScriptBundle("~/bundles/bitlibs").Include(
                "~/js/CustomLibrerias/jquery.history.js",
                "~/js/CustomLibrerias/utilidadesBitworks.js",
                "~/js/CustomLibrerias/BitworksAjaxNavigation.js",
                "~/js/ventanas/ventanas.js",
                "~/js/CustomLibrerias/customBitworks.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));
        }
    }
}
