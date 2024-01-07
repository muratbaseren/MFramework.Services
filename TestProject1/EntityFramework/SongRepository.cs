using MFramework.Services.DataAccess.EntityFramework;

namespace TestProject1.EntityFramework
{
    public class SongRepository : EFRepository<Song, int, EntityFrameworkContext>
    {
        public SongRepository(EntityFrameworkContext context) : base(context)
        {
        }
    }
}