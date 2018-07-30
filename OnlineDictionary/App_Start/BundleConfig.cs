using System.Web.Optimization;

namespace OnlineDictionary
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs")
                .Include("~/Scripts/angularjs/angular.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new ScriptBundle("~/bundles/momentjs")
                .Include("~/Scripts/moment/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/Scripts/app/app.js")
                .Include("~/Scripts/app/Shared/Services/MessageService.js")
                .Include("~/Scripts/app/Shared/Directives/AsyncPageWithLoaderDirective.js"));

            bundles.Add(new ScriptBundle("~/bundles/Dictionaries")
                .Include("~/Scripts/app/Dictionaries/DictionariesSerrvice.js")
                .Include("~/Scripts/app/Dictionaries/AllDictionariesCtrl.js")
                .Include("~/Scripts/app/Dictionaries/MyDictionariesCtrl.js")
                .Include("~/Scripts/app/Dictionaries/CreateDictionaryCtrl.js")
                .Include("~/Scripts/app/Dictionaries/EditDictionaryCtrl.js")
                .Include("~/Scripts/app/Dictionaries/DictionaryCtrl.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/bootstrap/bootstrap-datepicker/bootstrap-datepicker.js")
                .Include("~/Scripts/bootstrap/bootstrap-datepicker/locales/bootstrap-datepicker.uk.min.js")
                .Include("~/Scripts/bootstrap/bootstrap-datepicker/locales/bootstrap-datepicker.ru.min.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/css/bootstrap/bootstrap.css",
                         "~/Content/css/bootstrap/bootstrap-datepicker/bootstrap-datepicker3.min.css",
                         "~/Content/css/site.css"));
        }
    }
}
