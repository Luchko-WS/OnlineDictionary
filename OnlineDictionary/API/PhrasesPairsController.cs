using OnlineDictionary.Common;
using OnlineDictionary.Models;
using OnlineDictionary.ViewModels;
using System;
using System.Data.Entity;
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
        public async Task<HttpResponseMessage> CreatePhrasesPair(PhrasesPairViewModel viewModel)
        {
            var firstPhrase = new Phrase()
            {
                Text = viewModel.FirstPhrase.Text,
                Description = viewModel.FirstPhrase.Description,
                Language = viewModel.FirstPhrase.Language,
                OwnerId = User.Identity.Name
            };
            var secondPhrase = new Phrase()
            {
                Text = viewModel.SecondPhrase.Text,
                Description = viewModel.SecondPhrase.Description,
                Language = viewModel.SecondPhrase.Language,
                OwnerId = User.Identity.Name
            };

            var firstPhraseId = _dbContext.CreatePhrase(firstPhrase).Id;
            var secondPhraseId = _dbContext.CreatePhrase(secondPhrase).Id;

            var phrasesPair = new PhrasesPair()
            {
                CreationDate = DateTime.Now,
                DictionaryId = viewModel.DictionaryId,
                FirstPhraseId = firstPhraseId,
                SecondPhraseId = secondPhraseId,
                OwnerId = User.Identity.Name,
                IsConfirmed = false
            };

            _dbContext.CreatePhrasesPair(phrasesPair);

            await _dbContext.SaveDbChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK, phrasesPair);
        }

        [Route("Remove/{id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> RemovePhrasesPair(Guid id)
        {
            var pairToRemove = await _dbContext.PhrasesPairs.FirstOrDefaultAsync(p => p.Id == id);
            if (pairToRemove != null)
            {
                var res = _dbContext.RemovePhrasesPair(pairToRemove);
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
