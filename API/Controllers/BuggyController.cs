using API.Dtos.Book;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("not-found")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("server-error")]
        public IActionResult GetInternalServerlError()
        {
            throw new Exception("Lỗi máy chủ");
        }

        [HttpPost("validation-error")]
        public IActionResult GetValidationError(BookUpsertDto book)
        {
            return Ok();
        }
    }
}
