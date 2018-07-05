using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineDictionary.Controllers
{
    public class DictionariesController : Controller
    {
        [Authorize]
        public ActionResult MyDictionaries()
        {
            return View();
        }

        public ActionResult AllDictionaries()
        {
            return View();
        }

        public ActionResult CreateDictionary()
        {
            return PartialView();
        }
    }
}