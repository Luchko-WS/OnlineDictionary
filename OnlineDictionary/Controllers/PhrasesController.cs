using System.Web.Mvc;

namespace OnlineDictionary.Controllers
{
    public class PhrasesController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Translate()
        {
            return View();
        }
    }
}