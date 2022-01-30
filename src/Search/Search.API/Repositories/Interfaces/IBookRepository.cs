using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Search.API.Entities;

namespace Search.API.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<Book>> GetBookByTitle(string title);
        Task<Book> GetBook(string id);
        Task Create(Book book);
        Task<bool> Delete(string id);
    }
}
