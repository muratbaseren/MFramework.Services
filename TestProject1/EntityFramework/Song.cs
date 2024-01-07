using MFramework.Services.Entities.Abstract;

namespace TestProject1.EntityFramework
{
    public class Song : EntityBase<int>
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }

    public class SongCreate
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }

    public class SongEdit
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }

    public class SongQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }
}