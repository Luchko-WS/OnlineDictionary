using OnlineDictionary.Models;
using OnlineDictionary.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;

namespace OnlineDictionary.API
{
    [RoutePrefix("api/Dictionaries")]
    public class DictionariesController : BaseApiController
    {
        [Route("GetAllPublicDictionaries")]
        [HttpGet]
        public async Task<dynamic> GetAllPublicDictionaries()
        {
            var dictionaries = await _dbContext.Dictionaries
                .Where(d => d.IsPublic)
                .ToListAsync();

            return dictionaries.Any() ?
                Request.CreateResponse(HttpStatusCode.OK, dictionaries) :
                Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("GetMyDictionaries")]
        [HttpGet]
        public async Task<dynamic> GetMyDictionaries()
        {
            var dictionaries = await _dbContext.Dictionaries
                .Where(d => d.OwnerId == User.Identity.Name)
                .ToListAsync();

            return dictionaries.Any() ?
                Request.CreateResponse(HttpStatusCode.OK, dictionaries) :
                Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateDictionary(DictionaryViewModel vm)
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

            _dbContext.CreateDictionary(newDictionary);
            await _dbContext.SaveDbChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, newDictionary);
        }
    }
}
