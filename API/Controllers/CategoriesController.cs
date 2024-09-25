using Core.Interfaces;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
