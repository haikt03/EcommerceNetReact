using Core.Interfaces;

namespace API.Controllers
{
    public class AuthorsController : BaseApiController
    {
        public AuthorsController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
