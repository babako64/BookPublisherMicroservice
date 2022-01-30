using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Search.API.Entities;

namespace Search.API.Data
{
    public class SearchContextSeed
    {
        public static void SeedData(IMongoCollection<Book> productCollection)
        {
            bool existProducts = productCollection.Find(p => true).Any();
            if (!existProducts)
            {
                productCollection.InsertMany(getPreConfiuredProducts());
            }
        }

        private static IEnumerable<Book> getPreConfiuredProducts()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Title = "Title-1",
                    PaperCount = 150,
                    Isbn = "1111111111111",
                    PublisherName = "publisher-1",
                    AuthorName = "Author-1",
                    PublishDate = new DateTime(1999, 01, 05),
                    ImageFile = "image-1",
                    Price = 150
                },
                new Book
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Title = "Title-2",
                    PaperCount = 110,
                    Isbn = "22222222222222",
                    PublisherName = "publisher-2",
                    AuthorName = "Author-2",
                    PublishDate = new DateTime(2003, 03, 15),
                    ImageFile = "image-2",
                    Price = 100
                }
            };

        }
    }
}
