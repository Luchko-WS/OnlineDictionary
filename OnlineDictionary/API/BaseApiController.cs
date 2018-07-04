using Microsoft.AspNet.Identity.Owin;
using System.Web;

namespace OnlineDictionary.API
{
    public class BaseApiController
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