using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Search.API.Entities;
using Search.API.Repositories.Interfaces;

namespace Search.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public SearchController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> GetBooks()
        {
            var books = await _bookRepository.GetBooks();
            return Ok(books);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> CreateBook([FromBody]Book book)
        {
            
            await _bookRepository.Create(book);
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Book>> GetBookById(string id)
        {
            var book = await _bookRepository.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [Route("[action]/{title}", Name = "GetBookByTitle")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByTitle(string title)
        {
            var books = await _bookRepository.GetBookByTitle(title);
            return Ok(books);
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteBook")]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBookById(string id)
        {
            return Ok(await _bookRepository.Delete(id));
        }
    }
}
