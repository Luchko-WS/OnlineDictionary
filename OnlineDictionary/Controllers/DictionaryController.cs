using System;
using System.Web.Mvc;

namespace OnlineDictionary.Controllers
{
    public class DictionariesController : BaseDbContextController
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

        public ActionResult SearchDictionary()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult Dictionary(Guid id)
        {
            return View(id);
        }
    }
}