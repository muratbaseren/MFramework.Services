using AutoMapper;
using MFramework.Services.Business.EntityFramework;

namespace TestProject1.EntityFramework
{
    public class SongManager : EFManager<Song, int, SongRepository>
    {
        public SongManager(SongRepository repository) : base(repository)
        {
        }

        public SongManager(SongRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}