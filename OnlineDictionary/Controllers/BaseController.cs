using OnlineDictionary.Common;
using System.Web.Mvc;

namespace OnlineDictionary.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly ILogger _logger;

        public BaseController()
        {

        }
    }
}