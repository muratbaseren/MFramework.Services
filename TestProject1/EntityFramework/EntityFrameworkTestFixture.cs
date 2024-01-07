using AutoMapper;

namespace TestProject1.EntityFramework
{
    public class EntityFrameworkTestFixture : IDisposable
    {
        public IMapper _mapper;
        public SongRepository _songRepository;
        public SongManager _songManager;
        public EntityFrameworkContext _context;

        public EntityFrameworkTestFixture()
        {
            MapperConfiguration mapperConfiguration =
                new MapperConfiguration(opts =>
                {
                    opts.CreateMap<Song, SongCreate>().ReverseMap();
                    opts.CreateMap<Song, SongEdit>().ReverseMap();
                    opts.CreateMap<Song, SongQuery>().ReverseMap();
                });

            _mapper = mapperConfiguration.CreateMapper();
            _context = new EntityFrameworkContext();
            _songRepository = new SongRepository(_context);
            _songManager = new SongManager(_songRepository, _mapper);

            SetupEnvironment();
        }

        public void SetupEnvironment()
        {
            if (!_songManager.Query().Any())
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
                    _songRepository.Add(new Song
                    {
                        Name = item.Name,
                        Year = item.Year,
                        IsSales = item.IsSales,
                        Price = item.Price
                    });
                }

                _songRepository.Save();
            }
        }

        public void Dispose()
        {
            CleanupEnvironment();
        }

        private void CleanupEnvironment()
        {
            _context.Database.Delete();

            _mapper = null;
            _songRepository = null;
            _songManager = null;
        }
    }
}
