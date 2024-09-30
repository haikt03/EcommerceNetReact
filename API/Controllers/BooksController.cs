using API.Dtos.Book;
using API.Extensions;
using API.Extensions.Mappings;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BooksController : BaseApiController
    {
        private readonly ICloudImageService _cloudImageService;
        public BooksController(IUnitOfWork unitOfWork, ICloudImageService cloudImageService) : base(unitOfWork)
        {
            _cloudImageService = cloudImageService;
        }

        // GET: api/Books?
        [HttpGet]
        public async Task<ActionResult<PagedList<BookDto>>> GetAllBooks([FromQuery] BookParam bookParam)
        {
            var books = await _unitOfWork.bookRepo.GetAllAsync(bookParam);
            Response.AddPaginationHeader(books.PaginationHeader);
            var bookDtos = books.Select(b => b.ToDto()).ToList();

            return Ok(bookDtos);
        }

        // GET: api/Books/1
        [HttpGet("{id}", Name = nameof(GetBookById))]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _unitOfWork.bookRepo.GetByIdAsync(id);
            if (book == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy sách" });

            return Ok(book.ToDto());
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(BookUpsertDto bookDto)
        {
            var book = bookDto.ToEntity();

            if (bookDto.Files != null && bookDto.Files.Count > 0 && bookDto.Files.Any(f => f != null && f.Length > 0))
            {
                book = await UploadImages(book, bookDto.Files);
                if (book.Images == null || book.Images.Count == 0)
                    return BadRequest(new ProblemDetails { Title = "Tải ảnh lên không thành công" });
            }

            _unitOfWork.bookRepo.Add(book);
            var result = await _unitOfWork.CompleteAsync();
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Tạo mới không thành công" });

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book.ToDto());
        }

        // PUT: api/Books/1
        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateBook(BookUpsertDto bookDto, int id)
        {
            var book = await _unitOfWork.bookRepo.GetByIdAsync(id);
            if (book == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy sách" });

            if (bookDto.Files != null && bookDto.Files.Count > 0 && bookDto.Files.Any(f => f != null && f.Length > 0))
            {
                book = await UploadImages(book, bookDto.Files);
                if (book.Images == null || book.Images.Count == 0)
                    return BadRequest(new ProblemDetails { Title = "Tải ảnh lên không thành công" });
            }
            book = bookDto.ToEntity(book);

            _unitOfWork.bookRepo.Update(book);
            var result = await _unitOfWork.CompleteAsync();
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Cập nhật không thành công" });

            return Ok(book.ToDto());
        }

        // DELETE: api/Books/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _unitOfWork.bookRepo.GetByIdAsync(id);
            if (book == null)
                return NotFound(new ProblemDetails { Title = "Không tìm thấy sách" });

            if (book.Images != null && book.Images.Count > 0)
            {
                foreach (var image in book.Images)
                {
                    var deleteImageResult = await _cloudImageService.DeleteImageAsync(image.PublicId);
                    if (!deleteImageResult)
                        return BadRequest(new ProblemDetails { Title = "Xoá ảnh không thành công" });
                }
            }

            _unitOfWork.bookRepo.Remove(book);
            var result = await _unitOfWork.CompleteAsync();
            if (!result)
                return BadRequest(new ProblemDetails { Title = "Xóa không thành công" });

            return NoContent();
        }

        private async Task<Book> UploadImages(Book book, List<IFormFile> files)
        {
            foreach (var file in files)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadResult = await _cloudImageService.UploadImageAsync(new UploadImageParam { FileStream = stream, FileName = file.FileName });
                    book.Images = new List<Image> { new Image { Url = uploadResult.Url, PublicId = uploadResult.PublicId } };
                }
            }
            return book;
        }
    }
}
