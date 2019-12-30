﻿using System.Web.Optimization;

namespace EpamNetProject.PLL
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery-ui.unobtrusive-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/ISM").Include(
                "~/Scripts/Custom/ISM.js"));
            bundles.Add(new ScriptBundle("~/bundles/ISM_EDIT").Include(
                "~/Scripts/Custom/ISM_EDIT.js"));
            bundles.Add(new ScriptBundle("~/bundles/CookieChecker").Include(
                "~/Scripts/Custom/cookieChecker.js"));
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/jquery-ui-timepicker-addon.css",
                "~/Content/themes/base/jquery.ui.theme.css"));
            bundles.Add(new ScriptBundle("~/bundles/jquery.datetimepicker").Include(
                "~/Scripts/jquery-ui-timepicker-addon.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
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
