using OnlineDictionary.Common;
using System;
using System.Web;
using System.Web.Mvc;

namespace OnlineDictionary.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly ILogger _logger;

        public BaseController()
        {
            //_logger = ILogger();
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string userLanguage;

            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                userLanguage = langCookie.Value;
            }
            else
            {
                var userLang = Request.UserLanguages != null ? Request.UserLanguages[0] : string.Empty;
                userLanguage = string.IsNullOrEmpty(userLang) ? LanguagesManager.GetDefaultLanguage() : userLang;
            }
            SetCurrentLanguage(userLanguage);

            return base.BeginExecuteCore(callback, state);
        }

        protected void SetCurrentLanguage(string language)
        {
            if (!string.IsNullOrWhiteSpace(language))
            {
                LanguagesManager.SetLanguage(language);
                ViewBag.Language = language;
            }
        }
    }
}