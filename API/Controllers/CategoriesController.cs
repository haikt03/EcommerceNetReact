using API.Dtos.Book;
using API.Dtos.Category;
using API.Extensions;
using API.Extensions.Mappings;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<CategoryDto>>> GetAllCategories([FromQuery] PaginationParam paginationParam, string? search = null)
        {
            var categories = await _unitOfWork.categoryRepo.GetAllAsync(paginationParam, search);
            Response.AddPaginationHeader(categories.PaginationHeader);
            var categoryDtos = categories.Select(c => c.ToDto()).ToList();

            return Ok(categoryDtos);
        }

        // GET: api/Categories/1
        [HttpGet("{id}", Name = nameof(GetCategoryById))]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _unitOfWork.categoryRepo.GetByIdAsync(id);
            if (category == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy thể loại" });

            return Ok(category.ToDto());
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryUpsertDto categoryDto)
        {
            var category = categoryDto.ToEntity();

            if (category.PId != null)
            {
                var pCategory = await _unitOfWork.categoryRepo.GetByIdAsync(category.PId.Value);
                if (pCategory == null)
                    return NotFound(new ProblemDetails { Title = "Không tìm thấy thể loại cha" });

                category.PCategory = pCategory;
                if (pCategory.CCategories == null)
                    pCategory.CCategories = new List<Category>();
                pCategory.CCategories.Add(category);
            }

            _unitOfWork.categoryRepo.Add(category);
            var result = await _unitOfWork.CompleteAsync();
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Tạo mới không thành công" });

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category.ToDto());
        }

        // PUT: api/Categories/1
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(CategoryUpsertDto categoryDto, int id)
        {
            var category = await _unitOfWork.categoryRepo.GetByIdAsync(id);
            if (category == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy thể loại" });

            if (category.PId != categoryDto.PId)
            {
                if (categoryDto.PId == null)
                {
                    category.PCategory?.CCategories?.Remove(category);
                    category.PCategory = null;
                }
                else
                {
                    var pCategory = await _unitOfWork.categoryRepo.GetByIdAsync(categoryDto.PId.Value);
                    if (pCategory == null)
                        return NotFound(new ProblemDetails { Title = "Không tìm thấy thể loại cha" });

                    if (category.PId != null)
                    {
                        category.PCategory?.CCategories?.Remove(category);
                        category.PCategory = null;
                    }

                    category.PCategory = pCategory;
                    if (pCategory.CCategories == null)
                        pCategory.CCategories = new List<Category>();
                    pCategory.CCategories.Add(category);
                }
            }
            category = categoryDto.ToEntity(category);

            _unitOfWork.categoryRepo.Update(category);
            var result = await _unitOfWork.CompleteAsync();
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Cập nhật không thành công" });

            return Ok(category.ToDto());
        }

        // DELETE: api/Categories/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _unitOfWork.categoryRepo.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            _unitOfWork.categoryRepo.Remove(category);
            var result = await _unitOfWork.CompleteAsync();
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Xoá không thành công" });

            return NoContent();
        }

        // GET: api/Categories/1/books
        [HttpGet("{id}/categories")]
        public async Task<ActionResult<PagedList<BookDto>>> GetAllBooksByCategory([FromQuery] PaginationParam paginationParam, int id)
        {
            var books = await _unitOfWork.bookRepo.GetAllByCategory(paginationParam, id);
            Response.AddPaginationHeader(books.PaginationHeader);
            var bookDtos = books.Select(b => b.ToDto()).ToList();

            return Ok(bookDtos);
        }
    }
}
