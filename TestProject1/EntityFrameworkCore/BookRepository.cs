
using MFramework.Services.DataAccess.EntityFrameworkCore;

namespace TestProject1.EntityFrameworkCore
{
    public class BookRepository : EFRepository<Book, int, EntityFrameworkCoreContext>
    {
        public BookRepository(EntityFrameworkCoreContext context) : base(context)
        {
        }
    }
}