
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Search.API.Data.Interfaces;
using Search.API.Entities;
using Search.API.Repositories.Interfaces;
using MongoDB.Driver.Linq;

namespace Search.API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ISearchContext _context;

        public BookRepository(ISearchContext context)
        {
            _context = context;
        }

        public async Task Create(Book book) => await _context.Books.InsertOneAsync(book);
        

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _context.Books.AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBookByTitle(string title)
        {
            var books = await _context.Books.AsQueryable().Where(b =>  b.Title.ToLower() == title.ToLower()).ToListAsync();
            return books;
        }

        public async Task<Book> GetBook(string id)
        {
            return await _context.Books.AsQueryable().FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _context.Books.DeleteOneAsync(b => b.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
