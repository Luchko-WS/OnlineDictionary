using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OnlineDictionary.API
{
    [Authorize]
    public class BaseApiController : ApiController
    {
        private string language;

        protected readonly IApplicationDbContext _dbContext;

        protected BaseApiController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public string UserLanguage
        {
            get
            {
                if (this.language == null)
                {
                    language = (RequestContext.Principal.Identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == "Lang")?.Value;
                    if (this.language == null)
                    {
                        var langCookie = Request.Headers.GetCookies("culture").FirstOrDefault();
                        if (langCookie != null)
                        {
                            language = langCookie["culture"].Value;
                        }
                    }
                }
                return this.language ?? "en";
            }
        }
    }
}