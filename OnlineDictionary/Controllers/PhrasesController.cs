using System.Web.Mvc;

namespace OnlineDictionary.Controllers
{
    public class PhrasesController : BaseController
    {
        // GET: Phrases
        public ActionResult Index()
        {
            return View();
        }
    }
}