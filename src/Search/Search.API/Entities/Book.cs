using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Search.API.Entities
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        public string Title { get; set; }
        public int PaperCount { get; set; }
        public string Isbn { get; set; }
        public string PublisherName { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
