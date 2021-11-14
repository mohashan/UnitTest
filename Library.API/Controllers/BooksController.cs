using Library.API.Data.Models;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookServices _service;

        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookServices service)
        {
            _service = service;
        }

        // Get api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var items = _service.GetAll();
            return Ok(items);
        }

        // Get api/books/5
        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // Post api/books
        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Add(book);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // Delete api/books/5
        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            _service.Remove(id);
            return Ok();
        }
    }
}