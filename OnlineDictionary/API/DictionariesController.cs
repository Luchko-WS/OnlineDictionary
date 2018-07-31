using OnlineDictionary.Models;
using OnlineDictionary.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using System.Data.Entity;
using OnlineDictionary.Common;

namespace OnlineDictionary.API
{
    [RoutePrefix("api/Dictionaries")]
    public class DictionariesController : BaseApiController
    {
        [Route("GetAllPublicDictionaries")]
        [HttpGet]
        [AllowAnonymous]
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

        [Route("Dictionary/{dictionaryId}/{skip}/{take}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> GetDictionary([FromUri]Guid dictionaryId, int skip, int take)
        {
            var dictionary = await _dbContext.Dictionaries
                .Include(d => d.PhrasesPairs)
                .Include(d => d.PhrasesPairs.Select(p => p.FirstPhrase))
                .Include(d => d.PhrasesPairs.Select(p => p.SecondPhrase))
                .FirstOrDefaultAsync(d => d.Id == dictionaryId);
            if (dictionary == null) return Request.CreateResponse(HttpStatusCode.NotFound);
            if (!dictionary.IsPublic && dictionary.OwnerId != User.Identity.Name) return Request.CreateResponse(HttpStatusCode.Forbidden);
            var res = Mapper.MapProperties<DictionaryViewModel>(dictionary);
            res.IsMyDictionary = dictionary.OwnerId == User.Identity.Name;
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateDictionary(DictionaryViewModel vm)
        {
            Dictionary newDictionary = new Dictionary()
            {
                Name = vm.Name,
                Description = vm.Description,
                SourceLanguage = vm.SourceLanguage,
                TargetLanguage = vm.TargetLanguage,
                IsPublic = vm.IsPublic,
                CreationDate = DateTime.Now,
                LastChangeDate = DateTime.Now,
                OwnerId = User.Identity.Name
            };

            _dbContext.CreateDictionary(newDictionary);
            await _dbContext.SaveDbChangesAsync();
            return Request.CreateResponse(HttpStatusCode.OK, newDictionary);
        }

        [Route("Edit/{dictionaryId}")]
        [HttpPost]
        public async Task<HttpResponseMessage> EditDictionary([FromUri]Guid dictionaryId, DictionaryViewModel vm)
        {
            var dictionary = await _dbContext.Dictionaries
                                        .Where(d => d.Id == dictionaryId)
                                        .FirstOrDefaultAsync();
            if (dictionary != null)
            {
                dictionary.Name = vm.Name;
                dictionary.Description = vm.Description;
                dictionary.IsPublic = vm.IsPublic;
                dictionary.LastChangeDate = DateTime.Now;

                await _dbContext.SaveDbChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, dictionary);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("Remove/{id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveDictionary(Guid id)
        {
            var dictionaryToRemove = await _dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == id);
            if (dictionaryToRemove != null)
            {
                var res = _dbContext.RemoveDictionary(dictionaryToRemove);
                await _dbContext.SaveDbChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
