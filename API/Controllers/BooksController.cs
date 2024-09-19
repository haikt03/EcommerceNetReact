using Core.Interfaces;
using AutoMapper;

namespace API.Controllers
{
    public class BooksController : BaseApiController
    {
        private readonly IMapper _mapper;
        public BooksController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }
    }
}
