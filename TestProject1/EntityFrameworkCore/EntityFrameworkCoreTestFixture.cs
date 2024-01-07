﻿using AutoMapper;

namespace TestProject1.EntityFrameworkCore
{
    public class EntityFrameworkCoreTestFixture : IDisposable
    {
        public IMapper _mapper;
        public BookRepository _bookRepository;
        public BookManager _bookManager;
        public EntityFrameworkCoreContext _context;

        public EntityFrameworkCoreTestFixture()
        {
            MapperConfiguration mapperConfiguration =
                new MapperConfiguration(opts =>
                {
                    opts.CreateMap<Book, BookCreate>().ReverseMap();
                    opts.CreateMap<Book, BookEdit>().ReverseMap();
                    opts.CreateMap<Book, BookQuery>().ReverseMap();
                });

            _mapper = mapperConfiguration.CreateMapper();
            _context = new EntityFrameworkCoreContext();
            _bookRepository = new BookRepository(_context);
            _bookManager = new BookManager(_bookRepository, _mapper);

            SetupEnvironment();
        }

        public void SetupEnvironment()
        {
            if (!_bookManager.Query().Any())
            {
                List<dynamic> datas = new List<dynamic>
                {
                    new { Name = "Thompson LLP", Year = 1983, IsSales = true, Price = 2991.89m },
                    new { Name = "Lord and Partners", Year = 2008, IsSales = true, Price = 80.50m },
                    new { Name = "Summers CIC", Year = 1981, IsSales = false, Price = 50.23m },
                    new { Name = "Weaver Group", Year = 2013, IsSales = true, Price = 316.24m },
                    new { Name = "Joyce Inc", Year = 2013, IsSales = false, Price = 790.90m }
                };

                foreach (dynamic item in datas)
                {
                    _bookRepository.Add(new Book
                    {
                        Name = item.Name,
                        Year = item.Year,
                        IsSales = item.IsSales,
                        Price = item.Price
                    });
                }

                _bookRepository.Save();
            }
        }

        public void Dispose()
        {
            CleanupEnvironment();
        }

        private void CleanupEnvironment()
        {
            _context.Database.EnsureDeleted();

            _mapper = null;
            _bookRepository = null;
            _bookManager = null;
        }
    }
}
