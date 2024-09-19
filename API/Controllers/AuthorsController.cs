using AutoMapper;
using Core.Interfaces;

namespace API.Controllers
{
    public class AuthorsController : BaseApiController
    {
        private readonly IMapper _mapper;
        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
