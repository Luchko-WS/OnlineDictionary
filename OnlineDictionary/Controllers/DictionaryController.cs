using System.Web.Mvc;

namespace OnlineDictionary.Controllers
{
    [Authorize]
    public class DictionariesController : Controller
    {
        public ActionResult MyDictionaries()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AllDictionaries()
        {
            return View();
        }

        public ActionResult CreateDictionary()
        {
            return PartialView();
        }

        public ActionResult EditDictionary()
        {
            return PartialView();
        }
    }
}