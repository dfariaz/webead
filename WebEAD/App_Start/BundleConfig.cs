using System.Web;
using System.Web.Optimization;

namespace WebEAD
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/semanticjs").Include(
                        "~/Scripts/semantic.js"));

            bundles.Add(new ScriptBundle("~/bundles/sitejs").Include(
                        "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/disciplinas.ead.js").Include(
                        "~/Scripts/Disciplinas.EAD.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                "~/Scripts/ckeditor/ckeditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                       "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
            "~/Scripts/jquery.inputmask/inputmask.js",
            "~/Scripts/jquery.inputmask/jquery.inputmask.js",
            "~/Scripts/jquery.inputmask/inputmask.extensions.js",
            "~/Scripts/jquery.inputmask/inputmask.date.extensions.js",
            //and other extensions you want to include
            "~/Scripts/jquery.inputmask/inputmask.numeric.extensions.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
          "~/Content/semantic.css"));

            bundles.Add(new StyleBundle("~/Content/toastr").Include(
                       "~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/sitecss").Include(
          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/intracss").Include(
          "~/Content/intra.css"));

            bundles.Add(new StyleBundle("~/Content/eadcss").Include(
          "~/Content/ead.css"));

            bundles.Add(new StyleBundle("~/Content/logincss").Include(
          "~/Content/login.css"));
        }
    }
}
