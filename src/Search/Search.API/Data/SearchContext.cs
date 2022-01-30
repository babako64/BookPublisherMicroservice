using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Search.API.Data.Interfaces;
using Search.API.Entities;
using Search.API.Settings;

namespace Search.API.Data
{
    public class SearchContext : ISearchContext
    {

        public SearchContext(IMongoDatabase database, ISearchDatabaseSetting databaseSetting)
        {
            Books = database.GetCollection<Book>(databaseSetting.CollectionName);
            SearchContextSeed.SeedData(Books);
        }

        public IMongoCollection<Book> Books { get; set; }
    }
}
