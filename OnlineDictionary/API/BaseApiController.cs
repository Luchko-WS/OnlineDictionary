using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace OnlineDictionary.API
{
    [Authorize]
    public class BaseApiController : ApiController
    {
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
    }
}