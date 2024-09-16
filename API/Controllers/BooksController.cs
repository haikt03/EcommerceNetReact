using Core.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;
using API.Helpers;
using API.DTOs.Params;
using AutoMapper;
using API.Extensions;
using API.DTOs.Books;

namespace API.Controllers
{
    public class BooksController : BaseApiController
    {
        private readonly IMapper _mapper;
        public BooksController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<PagedList<BookDto>>> GetAllAsync([FromQuery] BookParam bookParam, [FromQuery] PaginationParam paginationParam)
        {
            var books = await _unitOfWork.bookRepo
                .GetAllAsync(bookParam.Search, bookParam.Categories, bookParam.Authors, bookParam.Languages, bookParam.MinPrice, bookParam.MaxPrice, bookParam.Sort);
            var booksDtos = _mapper.Map<List<BookDto>>(books).AsQueryable();

            var result = await PagedList<BookDto>.ToPagedList(booksDtos, paginationParam.PageSize, paginationParam.PageIndex);
            Response.AddPaginationHeader(result.MetaData);

            return Ok(result);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetByIdAsync(int id)
        {
            var book = await _unitOfWork.bookRepo.GetByIdAsync(id);
            if (book == null)
                return NotFound("Book not found");

            return Ok(_mapper.Map<BookDto>(book));
        }
    }
}
