using System.Web;
using System.Web.Optimization;

namespace PracticaIIICO
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //Bundle Booststrap 5
            bundles.Add(new ScriptBundle("~/bundles/bootstrap5").Include(
                "~/Scripts/Boostrap_5/bootstrap.js"
                ));

            //Bundle Style Booststrap 5
            bundles.Add(new StyleBundle("~/Style/bootstrap5").Include(
                    "~/Content/Boostrap_5/css/bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Boostrap_5/css/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
