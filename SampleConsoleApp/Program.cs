using AutoMapper;
using MFramework.Services.Business.Mongo;
using MFramework.Services.Common.Extensions;
using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Context;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;
using MFramework.Services.Entities.Abstract;
using MFramework.Services.FakeData;
using MongoDB.Bson;
using System;
using System.Linq;

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
        public int Year { get; set; }
        public bool IsSales { get; set; }
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
    }



    class Program
    {
        static void Main(string[] args)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(opts => opts.CreateMap<Album, Album>());
            IMapper mapper = mapperConfiguration.CreateMapper();

            AlbumManager albumManager = new AlbumManager(mapper);
            CreateFakeData(albumManager);

            //QueryTest(albumManager);
            //UpdateTest(albumManager);

            Console.WriteLine();
            Console.WriteLine("List retrieve is completed. Please enter to quit!!..");
            Console.ReadKey();
        }

        private static void CreateFakeData(AlbumManager albumManager)
        {
            if (albumManager.Query().Any() == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    albumManager.Create(new Album
                    {
                        Name = NameData.GetCompanyName(),
                        Price = (decimal)NumberData.GetDouble() * NumberData.GetNumber(100, 4000),
                        IsSales = BooleanData.GetBoolean(),
                        Year = NumberData.GetNumber(1980, DateTime.Now.Year)
                    });
                }

                Console.WriteLine("Sample data created. Please enter for continue..");
                Console.WriteLine();
                Console.ReadKey();
            }
        }

        private static void QueryTest(AlbumManager albumManager)
        {
            var list = albumManager.Query().Where(x => x.IsSales).ToList();
            list.ForEach(x => Console.WriteLine(x.ToJson()));

            //var list = albumManager.Query().Where(x => x.Name.StartsWith("A")).ToList();
            //list.ForEach(x => Console.WriteLine(x.ToJson()));

            //var list = albumManager.Query().Where(x => x.Price >= 200 && x.Price <= 300).ToList();
            //list.ForEach(x => Console.WriteLine(x.ToJson()));
        }

        private static void UpdateTest(AlbumManager albumManager)
        {
            var album = albumManager.Create(new Album { IsSales = false, Name = "testo11", Price = 10, Year = 2021 });
            Console.WriteLine(album.ToJson());
            Console.WriteLine("Album created.");
            Console.WriteLine();
            Console.ReadKey();

            album = albumManager.Find(album.Id);
            album.Name = "testo112";
            albumManager.Update(album.Id, album);
            Console.WriteLine(album.ToJson());
            Console.WriteLine("Album updated 1.");
            Console.WriteLine();
            Console.ReadKey();

            album = albumManager.Find(album.Id);
            albumManager.UpdateProperties(album.Id, new { Name = "testo113" }.ToExpando());
            Console.WriteLine(album.ToJson());
            Console.WriteLine("Album updated 2.");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
