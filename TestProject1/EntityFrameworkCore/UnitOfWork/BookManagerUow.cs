using AutoMapper;
using MFramework.Services.Business.EntityFramework;

namespace TestProject1.EntityFrameworkCore.UnitOfWork
{
    public class BookManagerUow : EFManagerWithUnitOfWork<Book, int, BookRepository, IEntityFrameworkCoreUow>
    {
        public BookManagerUow(EntityFrameworkCoreUow uow) : base(uow)
        {
        }

        public BookManagerUow(EntityFrameworkCoreUow uow, IMapper mapper) : base(uow, mapper)
        {
        }
    }
}