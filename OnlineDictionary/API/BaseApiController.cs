using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Http;

namespace OnlineDictionary.API
{
    [Authorize]
    public class BaseApiController : ApiController
    {
        protected ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
    }
}