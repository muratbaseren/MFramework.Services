
using AutoMapper;
using MFramework.Services.Business.Mongo;
using MongoDB.Bson;

namespace TestProject1.MongoTests
{
    public class AlbumManager : MongoManager<Album, ObjectId, AlbumRepository>
    {
        public AlbumRepository albumRepository;

        public AlbumManager(IMapper mapper) : base(new AlbumRepository(), mapper)
        {
            albumRepository = repository;
        }
    }
}