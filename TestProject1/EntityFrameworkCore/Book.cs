using MFramework.Services.Entities.Abstract;

namespace TestProject1.EntityFrameworkCore
{
    public class Book : EntityBase<int>
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }

    public class BookCreate
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }

    public class BookEdit
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }

    public class BookQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsSales { get; set; }
        public decimal Price { get; set; }
    }
}