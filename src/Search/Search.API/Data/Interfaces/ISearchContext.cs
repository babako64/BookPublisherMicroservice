using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Search.API.Entities;

namespace Search.API.Data.Interfaces
{
    public interface ISearchContext
    {
        IMongoCollection<Book> Books { get; set; }
    }
}
