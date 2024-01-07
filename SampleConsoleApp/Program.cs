using AutoMapper;
using MFramework.Services.Business.Mongo;
using MFramework.Services.Common.Extensions;
using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Context;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;
using MFramework.Services.Entities.Abstract;
using MFramework.Services.FakeData;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SampleConsoleApp
{
    public class MyMongoContext : MongoDBContextBase
    {
        public MyMongoContext() : base(null, "mongodb://localhost:27017", "mongotestdb")
        {

        }
    }

    public class Album : EntityBase<ObjectId>
    {
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int Year { get; set; }
        public bool IsSales { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
    }

    [Collection("albums")]
    public class AlbumRepository : MongoRepository<Album, ObjectId>
    {
        public AlbumRepository() : base(new MyMongoContext())
        {
        }
    }

    public class AlbumManager : MongoManager<Album, ObjectId, AlbumRepository>
    {
        public AlbumManager(IMapper mapper) : base(new AlbumRepository(), mapper)
        {
        }

        public Album Find(Expression<Func<Album, bool>> filter)
        {
            return repository.Find(filter);
        }

        public IEnumerable<Album> FindAll(Expression<Func<Album, bool>> filter)
        {
            return repository.FindAll(filter);
        }
    }

}

namespace SampleConsoleApp
{

    class Program
    {
        private static ObjectId newAddedObjId = ObjectId.Empty;
        static void Main(string[] args)
        {
            MapperConfiguration mapperConfiguration =
                new MapperConfiguration(opts => opts.CreateMap<Album, Album>());

            IMapper mapper = mapperConfiguration.CreateMapper();

            AlbumManager albumManager = new AlbumManager(mapper);
            
            CreateFakeData(albumManager);
            QueryTest(albumManager);

            CreateTest(albumManager);
            QueryTest(albumManager);
            FindTest(albumManager);

            UpdateTest(albumManager);
            QueryTest(albumManager);

            DeleteTest(albumManager);
            QueryTest(albumManager);

            Console.WriteLine();
            Console.WriteLine("Please enter to quit!!..");
            Console.ReadKey();
        }



        private static void CreateTest(AlbumManager albumManager)
        {
            WriteTitle("CREATE TEST");

            var album = albumManager.Create(new Album { IsSales = false, Name = "testo11", Price = 10, Year = 2021 });
            newAddedObjId = album.Id;
            Console.WriteLine(album.ToJson());

            LastStatement("Album created. ");
        }

        private static void CreateFakeData(AlbumManager albumManager)
        {
            "Checking fake data exists...".ToConsoleWithNewLine();
            if (albumManager.Query().Any())
            {
                "Fake data has already exist..".ToConsoleWithNewLine();
            }
            else
            {
                WriteTitle("ADDING FAKE DATA");

                // Create fake static datas..
                //
                Album album1 = new Album
                {
                    Name = "Thompson LLP",
                    Year = 1983,
                    IsSales = true,
                    Price = 2991.897623784793302m
                };
                albumManager.Create(album1).ToJson().ToConsoleLine();

                Album album2 = new Album
                {
                    Name = "Lord and Partners",
                    Year = 2008,
                    IsSales = true,
                    Price = 80.5049157852795798m
                };
                albumManager.Create(album2).ToJson().ToConsoleLine();

                Album album3 = new Album
                {
                    Name = "Summers CIC",
                    Year = 1981,
                    IsSales = false,
                    Price = 50.2335336088358382m
                };
                albumManager.Create(album3).ToJson().ToConsoleLine();

                Album album4 = new Album
                {
                    Name = "Weaver Group",
                    Year = 2013,
                    IsSales = true,
                    Price = 316.240591920093264m
                };
                albumManager.Create(album4).ToJson().ToConsoleLine();

                Album album5 = new Album
                {
                    Name = "Joyce Inc",
                    Year = 2013,
                    IsSales = false,
                    Price = 790.903162565502480m
                };
                albumManager.Create(album5).ToJson().ToConsoleLine();



                // Create fake dynamic datas..
                //
                //for (int i = 0; i < 5; i++)
                //{
                //    albumManager.Create(new Album
                //    {
                //        Name = NameData.GetCompanyName(),
                //        Price = (decimal)NumberData.GetDouble() * NumberData.GetNumber(100, 4000),
                //        IsSales = BooleanData.GetBoolean(),
                //        Year = NumberData.GetNumber(1980, DateTime.Now.Year)
                //    }).ToJson().ToConsoleLine();
                //}

                LastStatement("Fake data created. ");
            }
        }

        private static void QueryTest(AlbumManager albumManager)
        {
            WriteTitle("LIST TEST");

            WriteSubTitle("List 1 All Items");
            var list = albumManager.List().ToList();
            list.ForEach(x => x.ToJson().ToConsoleLine());
            Console.WriteLine();

            WriteSubTitle("List 2 (only IsSale=true)");
            var list2 = albumManager.Query().Where(x => x.IsSales).ToList();
            list2.ForEach(x => x.ToJson().ToConsoleLine());
            Console.WriteLine();

            WriteSubTitle("List 3 (only Name.StartsWith('T'))");
            var list3 = albumManager.Query().Where(x => x.Name.StartsWith("T")).ToList();
            list3.ForEach(x => x.ToJson().ToConsoleLine());
            Console.WriteLine();

            WriteSubTitle("List 4 (only Price >= 50 && x.Price <= 100)");
            var list4 = albumManager.Query().Where(x => x.Price >= 50 && x.Price <= 100).ToList();
            list4.ForEach(x => x.ToJson().ToConsoleLine());
            Console.WriteLine();

            LastStatement(null);
        }

        private static void FindTest(AlbumManager albumManager)
        {
            WriteTitle("FIND TEST");

            WriteSubTitle($"Find (only id:{newAddedObjId})");
            var item1 = albumManager.Find(newAddedObjId);
            item1.ToJson().ToConsoleWithNewLine();

            WriteSubTitle($"Find (Price = 10)");
            var item2 = albumManager.Find(x => x.Price == 10);
            item2.ToJson().ToConsoleWithNewLine();

            WriteSubTitle($"Find All (Year > 1980 && Year < 1984)");
            var list = albumManager.FindAll(x => x.Year > 1980 && x.Year < 1984).ToList();
            list.ForEach(x => x.ToJson().ToConsoleLine());
            Console.WriteLine();

            LastStatement(null);
        }

        private static void UpdateTest(AlbumManager albumManager)
        {
            WriteTitle("UPDATE TESTS");

            var album2 = albumManager.Find(newAddedObjId);
            album2.Name = "testo112";
            albumManager.Update(album2.Id, album2);
            var album3 = albumManager.Find(album2.Id);
            album3.ToJson().ToConsoleLine();
            $"Album name updated with \"Update\" method. Please enter key to continue..".ToConsoleWithNewLine();

            var album4 = albumManager.Find(newAddedObjId);
            albumManager.UpdateProperties(album4.Id, new { Name = "testo113" }.ToExpando());
            var album5 = albumManager.Find(album4.Id);
            album5.ToJson().ToConsoleLine();
            $"Album name updated by \"UpdateProperties\" method. ".ToConsoleWithNewLine();

            LastStatement(null);
        }

        private static void DeleteTest(AlbumManager albumManager)
        {
            WriteTitle("DELETE TEST");

            albumManager.Delete(newAddedObjId);

            LastStatement($"Album deleted.(id : {newAddedObjId})");
        }



        private static void LastStatement(string message)
        {
            Console.WriteLine();
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }
            Console.WriteLine("Please enter key to continue..");
            Console.ReadKey();
            Console.WriteLine();
        }

        private static void WriteTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(title);
            Console.WriteLine("=".PadRight(title.Length, '='));
            Console.WriteLine();
            Console.ResetColor();
        }

        private static void WriteSubTitle(string subtitle)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(subtitle);
            Console.WriteLine("=".PadRight(subtitle.Length, '='));
            Console.ResetColor();
        }
    }

    public static class StringExtensions
    {
        public static void ToConsoleLine(this string str)
        {
            Console.WriteLine(str);
        }

        public static void ToConsoleWithNewLine(this string str)
        {
            Console.WriteLine(str);
            Console.WriteLine();
        }
    }
}
