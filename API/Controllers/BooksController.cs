using Core.Interfaces;

namespace API.Controllers
{
    public class BooksController : BaseApiController
    {
        public BooksController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
