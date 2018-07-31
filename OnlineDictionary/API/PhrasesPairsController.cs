﻿using OnlineDictionary.Common;
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
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync();
            if (phrasesPair != null)
            {
                phrasesPair.FirstPhrase = Mapper.MapProperties<Phrase>(vm.FirstPhrase);
                phrasesPair.SecondPhrase = Mapper.MapProperties<Phrase>(vm.SecondPhrase); ;

                //add some code here

                //await _dbContext.SaveDbChangesAsync();
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
