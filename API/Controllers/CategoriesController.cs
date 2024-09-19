using AutoMapper;
using Core.Interfaces;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly IMapper _mapper;
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
