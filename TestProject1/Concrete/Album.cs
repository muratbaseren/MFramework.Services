using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestProject1.Concrete
{
    public class Album : EntityBase<ObjectId>
    {
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int Year { get; set; }
        public bool IsSales { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
    }

    public class AlbumCreate
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }

    public class AlbumEdit
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }

    public class AlbumQuery
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }
}