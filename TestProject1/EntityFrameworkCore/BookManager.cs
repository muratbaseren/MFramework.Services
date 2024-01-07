using AutoMapper;
using MFramework.Services.Business.EntityFramework;

namespace TestProject1.EntityFrameworkCore
{
    public class BookManager : EFManager<Book, int, BookRepository>
    {
        public BookManager(BookRepository repository) : base(repository)
        {
        }

        public BookManager(BookRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}