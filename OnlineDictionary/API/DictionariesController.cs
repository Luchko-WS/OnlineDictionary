using OnlineDictionary.Models;
using OnlineDictionary.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnlineDictionary.API
{
    [RoutePrefix("api/Dictionaries")]
    public class DictionariesController : BaseApiController
    {
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage CreateDictionary(DictionaryViewModel vm)// public async Task<HttpResponseMessage> CreateDictionary()
        {
            Dictionary newDictionary = new Dictionary()
            {
                Name = vm.Name,
                Description = vm.Description,
                FromLanguage = vm.FromLanguage,
                ToLanguage = vm.ToLanguage,
                IsPublic = vm.IsPublic,
                CreationDate = DateTime.Now,
                LastChangeDate = DateTime.Now,
                OwnerId = User.Identity.Name
            };

            return Request.CreateResponse(HttpStatusCode.OK, newDictionary);
        }
    }
}
