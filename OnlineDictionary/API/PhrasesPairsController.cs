using OnlineDictionary.Models;
using OnlineDictionary.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnlineDictionary.API
{
    [RoutePrefix("api/PhrasesPairs")]
    public class PhrasesPairsController : BaseApiController
    {
        [Route("Create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreatePhrasesPair(PhrasesPairViewModel vm)
        {
            var firstPhrase = new Phrase()
            {
                Text = vm.FirstPhrase.Text,
                Description = vm.FirstPhrase.Description,
                Language = vm.FirstPhrase.Language,
                OwnerId = User.Identity.Name
            };
            var secondPhrase = new Phrase()
            {
                Text = vm.SecondPhrase.Text,
                Description = vm.SecondPhrase.Description,
                Language = vm.SecondPhrase.Language,
                OwnerId = User.Identity.Name
            };

            var firstPhraseId = _dbContext.CreatePhrase(firstPhrase).Id;
            var secondPhraseId = _dbContext.CreatePhrase(secondPhrase).Id;

            var phrasesPair = new PhrasesPair()
            {
                CreationDate = DateTime.Now,
                DictionaryId = vm.DictionaryId,
                FirstPhraseId = firstPhraseId,
                SecondPhraseId = secondPhraseId,
                OwnerId = User.Identity.Name,
                IsConfirmed = false
            };

            _dbContext.CreatePhrasesPair(phrasesPair);

            var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == vm.DictionaryId);
            dictionary.LastChangeDate = DateTime.Now;

            await _dbContext.SaveDbChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK, phrasesPair);
        }

        [Route("Edit/{phrasesPairId}")]
        [HttpPut]
        public async Task<HttpResponseMessage> EditPhraesPair([FromUri]Guid phrasesPairId, PhrasesPairViewModel vm)
        {
            var phrasesPair = await _dbContext.PhrasesPairs
                                        .Where(p => p.Id == phrasesPairId)
                                        .Include(p => p.FirstPhrase)
                                        .Include(p => p.SecondPhrase)
                                        .FirstOrDefaultAsync();
            if (phrasesPair != null)
            {
                phrasesPair.FirstPhrase.Description = vm.FirstPhrase.Description;
                phrasesPair.FirstPhrase.Text = vm.FirstPhrase.Text;

                phrasesPair.SecondPhrase.Description = vm.SecondPhrase.Description;
                phrasesPair.SecondPhrase.Text = vm.SecondPhrase.Text;

                var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == vm.DictionaryId);
                dictionary.LastChangeDate = DateTime.Now;

                await _dbContext.SaveDbChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK, phrasesPair);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("Remove/{id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> RemovePhrasesPair(Guid id)
        {
            var pairToRemove = await _dbContext.PhrasesPairs.FirstOrDefaultAsync(p => p.Id == id);
            if (pairToRemove != null)
            {
                var res = await _dbContext.RemovePhrasesPair(pairToRemove);

                var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Id == res.DictionaryId);
                dictionary.LastChangeDate = DateTime.Now;

                await _dbContext.SaveDbChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("Transalte")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<dynamic> FindTranslate([FromUri]FindTranslateViewModel vm)
        {
            if (vm == null || string.IsNullOrEmpty(vm.Text)) Request.CreateResponse(HttpStatusCode.BadRequest);

            var phrases = _dbContext.PhrasesPairs
                .Include(p => p.FirstPhrase)
                .Include(p => p.SecondPhrase);

            var leftPhrases = phrases.Where(p => p.FirstPhrase.Text.ToLower().Contains(vm.Text.ToLower()));
            var rigthPhrases = phrases.Where(p => p.SecondPhrase.Text.ToLower().Contains(vm.Text.ToLower()));

            if(!string.IsNullOrEmpty(vm.SourceLanguage))
            {
                leftPhrases = leftPhrases.Where(p => p.FirstPhrase.Language == vm.SourceLanguage);
                rigthPhrases = rigthPhrases.Where(p => p.SecondPhrase.Language == vm.SourceLanguage);
            }
            if(!string.IsNullOrEmpty(vm.TargetLanguage))
            {
                leftPhrases = leftPhrases.Where(p => p.SecondPhrase.Language == vm.TargetLanguage);
                rigthPhrases = rigthPhrases.Where(p => p.FirstPhrase.Language == vm.SourceLanguage);
            }

            var leftRes = leftPhrases.Select(p => new
            {
                SourceText = p.FirstPhrase.Text,
                TargetText = p.SecondPhrase.Text,
                SourceLang = p.FirstPhrase.Language,
                TargetLang = p.SecondPhrase.Language
            });
            var rightRes = rigthPhrases.Select(p => new
            {
                SourceText = p.SecondPhrase.Text,
                TargetText = p.FirstPhrase.Text,
                SourceLang = p.SecondPhrase.Language,
                TargetLang = p.FirstPhrase.Language
            });

            var res = await leftRes.Union(rightRes).ToListAsync();
            res.Sort((pair1, pair2) =>
            {
                var index1 = pair1.SourceLang.ToLower().IndexOf(vm.Text.ToLower());
                var index2 = pair2.SourceLang.ToLower().IndexOf(vm.Text.ToLower());
                return index1 < index2 ? -1 : index1 > index2 ? 1 : 0;
            });
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
    }
}
