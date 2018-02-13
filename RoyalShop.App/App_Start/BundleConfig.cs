using RoyalShop.Common;
using System.Web;
using System.Web.Optimization;

namespace RoyalShop.App
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include("~/Common//Client/js/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/js/plugin").Include(
                "~/Common/Admin/libs/jquery-ui/jquery-ui.min.js",
                "~/Common/Admin/libs/mustache/mustache.js",
                "~/Common/Admin/libs/numeral/numeral.js",
                "~/Common/Admin/libs/jquery-validation/dist/jquery.validate.js",
                "~/Common/Admin/libs/jquery-validation/dist/additional-methods.min.js",
                "~/Common/Client/js/Common.js"
                ));

            bundles.Add(new StyleBundle("~/css/base")
                .Include("~/Common//Client/css/bootstrap.css",new CssRewriteUrlTransform())
                .Include("~/Common/Client/font-awesome-4.7.0/css/font-awesome.css", new CssRewriteUrlTransform())
                .Include("~/Common/Admin/libs/jquery-ui/themes/smoothness/jquery-ui.min.css", new CssRewriteUrlTransform())
                .Include("~/Common//Client/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Common/Client/css/custom.css", new CssRewriteUrlTransform())
                );

            //BundleTable.EnableOptimizations = true;
            BundleTable.EnableOptimizations = bool.Parse(ConfigHelper.GetByKey("EnableBundles"));
        }
    }
}
