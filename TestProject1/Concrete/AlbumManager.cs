
using AutoMapper;
using MFramework.Services.Business.Mongo;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace TestProject1.Concrete
{
    public class AlbumManager : MongoManager<Album, ObjectId, AlbumRepository>
    {
        public AlbumRepository albumRepository;

        public AlbumManager(IMapper mapper) : base(new AlbumRepository(), mapper)
        {
            albumRepository = repository;
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