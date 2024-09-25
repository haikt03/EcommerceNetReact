using API.Dtos.Author;
using API.Dtos.Category;
using API.Extensions;
using API.Extensions.Mappings;
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
        public async Task<ActionResult<PagedList<CategoryDto>>> GetAllAsync(PaginationParam paginationParam, string? search = null)
        {
            var categories = await _unitOfWork.categoryRepo.GetAllAsync(paginationParam, search);
            Response.AddPaginationHeader(categories.PaginationHeader);
            var categoryDtos = categories.Select(c => c.ToDto()).ToList();

            return Ok(categoryDtos);
        }

        [HttpGet("hierarchy")]
        public async Task<ActionResult<List<CategoryDto>>> GetAllHierarchyAsync(int? parentId, int? currentDepth, int? maxDepth)
        {
            var categories = await _unitOfWork.categoryRepo.GetAllHierarchyAsync(parentId, currentDepth, maxDepth);
            var categoryDtos = categories.Select(c => c.ToDto()).ToList();

            return Ok(categoryDtos);
        }

        // GET: api/Categories/1
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.categoryRepo.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category.ToDto());
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateAsync(CategoryUpsertDto categoryDto)
        {
            var category = categoryDto.ToEntity();
            _unitOfWork.categoryRepo.Add(category);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetByIdAsync), new { id = category.Id }, category.ToDto());
        }

        // PUT: api/Categories/1
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateAsync(int id, CategoryUpsertDto categoryDto)
        {
            if (!CategoryExists(id))
                return NotFound();
            var category = categoryDto.ToEntity();

            _unitOfWork.categoryRepo.Update(category);
            await _unitOfWork.CompleteAsync();

            return Ok(category.ToDto());
        }

        // DELETE: api/Categories/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var category = await _unitOfWork.categoryRepo.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            _unitOfWork.categoryRepo.Remove(category);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _unitOfWork.categoryRepo.Exists(id);
        }
    }
}
