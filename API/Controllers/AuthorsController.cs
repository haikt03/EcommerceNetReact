using API.Dtos.Author;
using API.Extensions.Mappings;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AuthorsController : BaseApiController
    {
        private readonly ICloudImageService _cloudImageService;
        public AuthorsController(IUnitOfWork unitOfWork, ICloudImageService cloudImageService) : base(unitOfWork)
        {
            _cloudImageService = cloudImageService;
        }

        // GET: api/Authors
        [HttpGet("all")]
        public async Task<ActionResult<List<AuthorDto>>> GetAllAsync()
        {
            var authors = await _unitOfWork.authorRepo.GetAllAsync();
            var authorDtos = authors.Select(a => a.ToDto()).ToList();

            return Ok(authorDtos);
        }

        // GET: api/Authors?
        [HttpGet]
        public async Task<ActionResult<PagedList<Author>>> GetAllAsync(PaginationParam paginationParam, string? search = null)
        {
            return await _unitOfWork.authorRepo.GetAllAsync(paginationParam, search);
        }

        // GET: api/Authors/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetByIdAsync(int id)
        {
            var author = await _unitOfWork.authorRepo.GetByIdAsync(id);
            if (author == null) return NotFound();

            return Ok(author.ToDto());
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAsync(AuthorUpsertDto authorDto)
        {
            var author = authorDto.ToEntity();

            var uploadResult = new UploadImageResult();
            if (authorDto.Image != null)
                uploadResult = await _cloudImageService.UploadImageAsync(authorDto.Image.FileData, authorDto.Image.FileName);
            author.Image = new Image
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url
            };

            _unitOfWork.authorRepo.Add(author);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetByIdAsync), new { id = author.Id }, author.ToDto());
        }

        // PUT: api/Authors/1
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> UpdateAsync(int id, AuthorUpsertDto authorDto)
        {
            if (!AuthorExists(id))
                return NotFound();
            var author = authorDto.ToEntity();

            _unitOfWork.authorRepo.Update(author);
            await _unitOfWork.CompleteAsync();

            return Ok(author.ToDto());
        }

        // DELETE: api/Authors/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var author = await _unitOfWork.authorRepo.GetByIdAsync(id);
            if (author == null)
                return NotFound();

            _unitOfWork.authorRepo.Remove(author);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _unitOfWork.authorRepo.Exists(id);
        }
    }
}
