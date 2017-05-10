using i18n;
using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace i18nSample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Blacklist certain URLs from being 'localized'.
            i18n.UrlLocalizer.QuickUrlExclusionFilter = new System.Text.RegularExpressions.Regex(@"(?:sitemap\.xml|\.css|\.jpg|\.png|\.svg|\.woff|\.woff2|\.eot|\.js|\.html|\.json)$|(?:elmah|bundles)");

            //https://github.com/turquoiseowl/i18n#project-configuration
            // Change from the of temporary redirects during URL localization
            i18n.LocalizedApplication.Current.PermanentRedirects = false;

            // Change the URL localization scheme from Scheme1.
            i18n.UrlLocalizer.UrlLocalizationScheme = i18n.UrlLocalizationScheme.Scheme1;

            // Filter certain URLs from being 'localized'.
            i18n.UrlLocalizer.OutgoingUrlFilters += delegate (string url, Uri currentRequestUrl)
            {
                Uri uri;
                if (Uri.TryCreate(url, UriKind.Absolute, out uri)
                    || Uri.TryCreate(currentRequestUrl, url, out uri))
                {
                    if (url.StartsWith("?", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }
                return true;
            };

            i18n.LocalizedApplication.Current.DefaultLanguage = "pt-BR";
            i18n.LocalizedApplication.Current.CookieName = "i18n_langtag";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }
    }
}
