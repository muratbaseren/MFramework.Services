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



    class Program
    {
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

        private static ObjectId newAddedObjId = ObjectId.Empty;

        private static void CreateTest(AlbumManager albumManager)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("CREATE TEST");
            Console.WriteLine("===========");
            Console.WriteLine();
            Console.ResetColor();

            var album = albumManager.Create(new Album { IsSales = false, Name = "testo11", Price = 10, Year = 2021 });
            newAddedObjId = album.Id;
            Console.WriteLine(album.ToJson());

            Console.WriteLine();
            Console.WriteLine("Album created. ");
            Console.WriteLine("Please enter key to continue..");
            Console.ReadKey();
            Console.WriteLine();
        }

        private static void CreateFakeData(AlbumManager albumManager)
        {
            Console.WriteLine("Checking fake data exists...");
            if (albumManager.Query().Any())
            {
                Console.WriteLine("Fake data has already exist..");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ADDING FAKE DATA");
                Console.WriteLine("================");
                Console.WriteLine();
                Console.ResetColor();

                // Create fake static datas..
                //
                Album album1 = new Album
                {
                    Name = "Thompson LLP",
                    Year = 1983,
                    IsSales = true,
                    Price = 2991.897623784793302m
                };
                Console.WriteLine(albumManager.Create(album1).ToJson());

                Album album2 = new Album
                {
                    Name = "Lord and Partners",
                    Year = 2008,
                    IsSales = true,
                    Price = 80.5049157852795798m
                };
                Console.WriteLine(albumManager.Create(album2).ToJson());

                Album album3 = new Album
                {
                    Name = "Summers CIC",
                    Year = 1981,
                    IsSales = false,
                    Price = 50.2335336088358382m
                };
                Console.WriteLine(albumManager.Create(album3).ToJson());

                Album album4 = new Album
                {
                    Name = "Weaver Group",
                    Year = 2013,
                    IsSales = true,
                    Price = 316.240591920093264m
                };
                Console.WriteLine(albumManager.Create(album4).ToJson());

                Album album5 = new Album
                {
                    Name = "Joyce Inc",
                    Year = 2013,
                    IsSales = false,
                    Price = 790.903162565502480m
                };
                Console.WriteLine(albumManager.Create(album5).ToJson());



                // Create fake dynamic datas..
                //
                //for (int i = 0; i < 5; i++)
                //{
                //    Console.WriteLine(albumManager.Create(new Album
                //    {
                //        Name = NameData.GetCompanyName(),
                //        Price = (decimal)NumberData.GetDouble() * NumberData.GetNumber(100, 4000),
                //        IsSales = BooleanData.GetBoolean(),
                //        Year = NumberData.GetNumber(1980, DateTime.Now.Year)
                //    }).ToJson());
                //}

                Console.WriteLine();
                Console.WriteLine("Fake data created. ");
                Console.WriteLine("Please enter key to continue..");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        private static void QueryTest(AlbumManager albumManager)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LIST TEST");
            Console.WriteLine("=========");
            Console.WriteLine();
            Console.ResetColor();

            Console.WriteLine("List 1 All Items");
            Console.WriteLine("================");
            var list = albumManager.List().ToList();
            list.ForEach(x => Console.WriteLine(x.ToJson()));
            Console.WriteLine();

            Console.WriteLine("List 2 (only IsSale=true)");
            Console.WriteLine("=========================");
            var list2 = albumManager.Query().Where(x => x.IsSales).ToList();
            list2.ForEach(x => Console.WriteLine(x.ToJson()));
            Console.WriteLine();

            Console.WriteLine("List 3 (only Name.StartsWith('T'))");
            Console.WriteLine("==================================");
            var list3 = albumManager.Query().Where(x => x.Name.StartsWith("T")).ToList();
            list3.ForEach(x => Console.WriteLine(x.ToJson()));
            Console.WriteLine();

            Console.WriteLine("List 4 (only Price >= 50 && x.Price <= 100)");
            Console.WriteLine("===========================================");
            var list4 = albumManager.Query().Where(x => x.Price >= 50 && x.Price <= 100).ToList();
            list4.ForEach(x => Console.WriteLine(x.ToJson()));
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Please enter key to continue..");
            Console.ReadKey();
            Console.WriteLine();
        }

        private static void FindTest(AlbumManager albumManager)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("FIND TEST");
            Console.WriteLine("=========");
            Console.WriteLine();
            Console.ResetColor();

            Console.WriteLine($"Find (only id:{newAddedObjId})");
            Console.WriteLine("=======================================");
            var item1 = albumManager.Find(newAddedObjId);
            Console.WriteLine(item1.ToJson());
            Console.WriteLine();

            Console.WriteLine($"Find (Price = 10)");
            Console.WriteLine("=======================================");
            var item2 = albumManager.Find(x => x.Price == 10);
            Console.WriteLine(item2.ToJson());
            Console.WriteLine();

            Console.WriteLine($"Find All (Year > 1980 && Year < 1984)");
            Console.WriteLine("=======================================");
            var list = albumManager.FindAll(x => x.Year > 1980 && x.Year < 1984).ToList();
            list.ForEach(x => Console.WriteLine(x.ToJson()));
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Please enter key to continue..");
            Console.ReadKey();
            Console.WriteLine();
        }

        private static void UpdateTest(AlbumManager albumManager)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("UPDATE TESTS");
            Console.WriteLine("============");
            Console.WriteLine();
            Console.ResetColor();

            var album2 = albumManager.Find(newAddedObjId);
            album2.Name = "testo112";
            albumManager.Update(album2.Id, album2);
            var album3 = albumManager.Find(album2.Id);
            Console.WriteLine(album3.ToJson());
            Console.WriteLine($"Album name updated with \"Update\" method. Please enter key to continue..");
            Console.WriteLine();

            var album4 = albumManager.Find(newAddedObjId);
            albumManager.UpdateProperties(album4.Id, new { Name = "testo113" }.ToExpando());
            var album5 = albumManager.Find(album4.Id);
            Console.WriteLine(album5.ToJson());
            Console.WriteLine($"Album name updated by \"UpdateProperties\" method. ");

            Console.WriteLine();
            Console.WriteLine("Please enter key to continue..");
            Console.ReadKey();
            Console.WriteLine();
        }

        private static void DeleteTest(AlbumManager albumManager)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("DELETE TEST");
            Console.WriteLine("===========");
            Console.WriteLine();
            Console.ResetColor();

            albumManager.Delete(newAddedObjId);
            Console.WriteLine($"Album deleted.(id : {newAddedObjId})");
            Console.WriteLine("Please enter key to continue..");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
