using OnlineDictionary.Common;
using OnlineDictionary.ViewModels;
using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
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

        [AllowAnonymous]
        public async Task<ActionResult> Dictionary(Guid id)
        {
            var dictionary = await _db.Dictionaries.FirstOrDefaultAsync(d => d.Id == id);
            if (dictionary == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            if (!User.Identity.IsAuthenticated || dictionary.OwnerId != User.Identity.Name) return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            return View(Mapper.MapProperties<DictionaryViewModel>(dictionary));
        }
    }
}