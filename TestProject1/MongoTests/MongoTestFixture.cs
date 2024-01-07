using AutoMapper;
using TestProject1.MongoTests.MongoObjects;

namespace TestProject1.MongoTests
{
    public class MongoTestFixture : IDisposable
    {
        public IMapper _mapper;
        public AlbumManager _albumManager;

        public MongoTestFixture()
        {
            MapperConfiguration mapperConfiguration =
                new MapperConfiguration(opts =>
                {
                    opts.CreateMap<Album, AlbumCreate>().ReverseMap();
                    opts.CreateMap<Album, AlbumEdit>().ReverseMap();
                    opts.CreateMap<Album, AlbumQuery>()
                        .ForMember(x => x.Id,
                            opt => opt.MapFrom((x, y) => x.Id.ToString()));
                });

            _mapper = mapperConfiguration.CreateMapper();
            _albumManager = new AlbumManager(_mapper);

            SetupEnvironment();
        }

        public void SetupEnvironment()
        {
            if (!_albumManager.Query().Any())
            {
                List<dynamic> datas = new List<dynamic>
                {
                    new { Name="Thompson LLP", Year=1983, IsSales = true, Price = 2991.89m },
                    new { Name = "Lord and Partners", Year = 2008, IsSales = true, Price = 80.50m },
                    new { Name = "Summers CIC", Year = 1981, IsSales = false, Price = 50.23m },
                    new { Name = "Weaver Group", Year = 2013, IsSales = true, Price = 316.24m },
                    new { Name = "Joyce Inc", Year = 2013, IsSales = false, Price = 790.90m }
                };

                foreach (dynamic item in datas)
                {
                    _albumManager.Create(new Album
                    {
                        Name = item.Name,
                        Year = item.Year,
                        IsSales = item.IsSales,
                        Price = item.Price
                    });
                }
            }
        }

        public void Dispose()
        {
            CleanupEnvironment();
        }

        private void CleanupEnvironment()
        {
            _albumManager.albumRepository.database.Client.DropDatabase("mframeworktestdb");

            _mapper = null;
            _albumManager = null;
        }
    }
}
