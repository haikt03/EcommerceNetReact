using API.Dtos.Author;
using API.Dtos.Book;
using API.Extensions;
using API.Extensions.Mappings;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthorsController : BaseApiController
    {
        private readonly ICloudImageService _cloudImageService;
        public AuthorsController(IUnitOfWork unitOfWork, ICloudImageService cloudImageService) : base(unitOfWork)
        {
            _cloudImageService = cloudImageService;
        }

        // GET: api/Authors?
        [HttpGet]
        public async Task<ActionResult<PagedList<AuthorDto>>> GetAllAuthors([FromQuery] PaginationParam paginationParam, string? search = null)
        {
            var authors = await _unitOfWork.authorRepo.GetAllAsync(paginationParam, search);
            Response.AddPaginationHeader(authors.PaginationHeader);
            var authorDtos = authors.Select(a => a.ToDto()).ToList();

            return Ok(authorDtos);
        }

        // GET: api/Authors/1
        [HttpGet("{id}", Name = nameof(GetAuthorById))]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(int id)
        {
            var author = await _unitOfWork.authorRepo.GetByIdAsync(id);
            if (author == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy tác giả" });

            return Ok(author.ToDto());
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor(AuthorUpsertDto authorDto)
        {
            var author = authorDto.ToEntity();

            if (authorDto.File != null && authorDto.File.Length > 0)
            {
                author = await UploadImage(author, authorDto.File);
                if (author.Image == null)
                    return BadRequest(new ProblemDetails { Title = "Tải ảnh lên không thành công" });
            }

            _unitOfWork.authorRepo.Add(author);
            var result = await _unitOfWork.CompleteAsync();
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Tạo mới không thành công" });

            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author.ToDto());
        }

        // PUT: api/Authors/1
        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDto>> UpdateAuthor(AuthorUpsertDto authorDto, int id)
        {
            var author = await _unitOfWork.authorRepo.GetByIdAsync(id);
            if (author == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy tác giả" });

            if (authorDto.File != null && authorDto.File.Length > 0)
            {
                author = await UploadImage(author, authorDto.File);
                if (author.Image == null)
                    return BadRequest(new ProblemDetails { Title = "Tải ảnh lên không thành công" });

                if (author?.Image?.PublicId != null)
                {
                    var deleteResult = await _cloudImageService.DeleteImageAsync(author.Image.PublicId);
                    if (deleteResult)
                        return BadRequest(new ProblemDetails { Title = "Xoá ảnh cũ lên không thành công" });
                }
            }
            author = authorDto.ToEntity(author);

            _unitOfWork.authorRepo.Update(author);
            var result = await _unitOfWork.CompleteAsync();
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Cập nhật không thành công" });

            return Ok(author.ToDto());
        }

        // DELETE: api/Authors/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _unitOfWork.authorRepo.GetByIdAsync(id);

            if (author == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy tác giả" });

            if (author.Image != null)
            {
                var deleteImageResult = await _cloudImageService.DeleteImageAsync(author.Image.PublicId);
                if (!deleteImageResult)
                    return BadRequest(new ProblemDetails { Title = "Xoá ảnh không thành công" });
            }

            _unitOfWork.authorRepo.Remove(author);
            var result = await _unitOfWork.CompleteAsync();
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Xoá không thành công" });

            return NoContent();
        }

        // GET: api/Authors/1/books
        [HttpGet("{id}/books")]
        public async Task<ActionResult<PagedList<BookDto>>> GetAllBooksByAuthor([FromQuery] PaginationParam paginationParam, int id)
        {
            var books = await _unitOfWork.bookRepo.GetAllByAuthor(paginationParam, id);
            Response.AddPaginationHeader(books.PaginationHeader);
            var bookDtos = books.Select(b => b.ToDto()).ToList();

            return Ok(bookDtos);
        }

        private async Task<Author> UploadImage(Author author, IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadResult = await _cloudImageService.UploadImageAsync(new UploadImageParam { FileStream = stream, FileName = file.FileName });
                author.Image = new Image
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.Url
                };
            }
            return author;
        }
    }
}
