namespace OnlineDictionary.Controllers
{
    public class BaseDbContextController : BaseController
    {
        protected readonly IApplicationDbContext _db;

        public BaseDbContextController()
        {
            this._db = new ApplicationDbContext();
        }
    }
}