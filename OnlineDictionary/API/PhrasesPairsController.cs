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
    [RoutePrefix("api/PhrasesPairs")]
    public class PhrasesPairsController : BaseApiController
    {
        [Route("Create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreatePhrasesPair()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
