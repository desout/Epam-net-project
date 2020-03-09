using System.Web.Optimization;

namespace EpamNetProject.PLL
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Utils/Scripts/jquery-ui-{version}.js",
                "~/Utils/Scripts/jquery-ui.unobtrusive-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/ISM").Include(
                "~/Utils/Scripts/Custom/ISM.js"));
            bundles.Add(new ScriptBundle("~/bundles/ISM_EDIT").Include(
                "~/Utils/Scripts/Custom/ISM_EDIT.js"));
            bundles.Add(new ScriptBundle("~/bundles/CookieChecker").Include(
                "~/Utils/Scripts/Custom/cookieChecker.js"));
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/jquery-ui-timepicker-addon.css",
                "~/Content/themes/base/jquery.ui.theme.css"));
            bundles.Add(new ScriptBundle("~/bundles/jquery.datetimepicker").Include(
                "~/Utils/Scripts/jquery-ui-timepicker-addon.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Utils/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Utils/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Utils/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Utils/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
        }
    }
}