using MongoDB.Bson;
using MongoDB.Driver;
using Xunit.Abstractions;

namespace TestProject1.MongoTests
{
    [Collection("MongoCollection")]
    public class MongoRepositoryTest : IClassFixture<MongoTestFixture>
    {
        private readonly MongoTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public MongoRepositoryTest(MongoTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public async Task Any_ChecksHasDoc()
        {
            var result1 = _fixture._albumManager.albumRepository.Any();
            Assert.True(result1);

            var result2 = await _fixture._albumManager.albumRepository.AnyAsync();
            Assert.True(result2);
        }

        [Fact]
        public async Task Count_ReturnsCount()
        {
            var result1 = _fixture._albumManager.albumRepository.Count();
            Assert.Equal(5, result1);

            var result2 = await _fixture._albumManager.albumRepository.CountAsync();
            Assert.Equal(5, result2);
        }

        [Fact]
        public async Task List_ChecksCount()
        {
            var result1 = _fixture._albumManager.albumRepository.List();
            Assert.Equal(5, result1.Count);

            var result2 = await _fixture._albumManager.albumRepository.ListAsync();
            Assert.Equal(5, result2.Count);
        }

        [Fact]
        public async Task InsertFindUpdateDelete_OperatesCrud()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._albumManager.albumRepository.Insert(album);
            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            ObjectId id = result1.Id;
            _output.WriteLine("New id : " + id);

            result1.Name += "_updated";
            result1.IsSales = false;
            result1.Price += 10;
            result1.Year += 1;

            var result2 = _fixture._albumManager.albumRepository.Update(id, album);
            Assert.Equal(1, result2);

            var result3 = _fixture._albumManager.albumRepository.Find(id);
            Assert.Equal(result1.Name, result3.Name);
            Assert.Equal(result1.IsSales, result3.IsSales);
            Assert.Equal(result1.Price, result3.Price);
            Assert.Equal(result1.Year, result3.Year);

            var result4 = _fixture._albumManager.albumRepository.Delete(id);
            Assert.Equal(1, result4);

            var result5 = _fixture._albumManager.albumRepository.Find(id);
            Assert.Null(result5);
        }

        [Fact]
        public async Task InsertFindUpdateDeleteAsync_OperatesCrud()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._albumManager.albumRepository.InsertAsync(album);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            ObjectId id = result1.Id;
            _output.WriteLine("New id : " + id);

            result1.Name += "_updated";
            result1.IsSales = false;
            result1.Price += 10;
            result1.Year += 1;

            var result2 = await _fixture._albumManager.albumRepository.UpdateAsync(id, album);
            Assert.Equal(1, result2);

            var result3 = await _fixture._albumManager.albumRepository.FindAsync(id);
            Assert.Equal(result1.Name, result3.Name);
            Assert.Equal(result1.IsSales, result3.IsSales);
            Assert.Equal(result1.Price, result3.Price);
            Assert.Equal(result1.Year, result3.Year);

            var result4 = await _fixture._albumManager.albumRepository.DeleteAsync(id);
            Assert.Equal(1, result4);

            var result5 = await _fixture._albumManager.albumRepository.FindAsync(id);
            Assert.Null(result5);
        }

        [Fact]
        public async Task Find_WithFilter_GetsEntity()
        {
            Album album = new Album
            {
                Name = "Test 8888",
                IsSales = true,
                Price = 8888,
                Year = 1984
            };

            var result1 = _fixture._albumManager.albumRepository.Insert(album);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            ObjectId id = result1.Id;
            _output.WriteLine("New id : " + id);

            var result2 = _fixture._albumManager.albumRepository.Find(x =>
                x.Id == id && x.Price > 8887 && x.Price < 8889);
            Assert.NotNull(result2);

            var result3 = _fixture._albumManager.albumRepository.Find(x =>
                x.Id == id && x.IsSales);
            Assert.NotNull(result3);

            var result4 = _fixture._albumManager.albumRepository.Find(x =>
                x.Id == id && x.Year > 1983 && x.Year < 1985);
            Assert.NotNull(result4);

            var result5 = _fixture._albumManager.albumRepository.Find(x =>
                x.Name == "Test 8888");
            Assert.NotNull(result5);

            var result6 = _fixture._albumManager.albumRepository.Find(x =>
                x.Name.StartsWith("Test 8"));
            Assert.NotNull(result6);

            // Async methods
            var result7 = _fixture._albumManager.albumRepository.FindAsync(x =>
                x.Id == id && x.Price > 8887 && x.Price < 8889);
            Assert.NotNull(result7);

            var result8 = _fixture._albumManager.albumRepository.FindAsync(x =>
                x.Id == id && x.IsSales);
            Assert.NotNull(result8);

            var result9 = _fixture._albumManager.albumRepository.FindAsync(x =>
                x.Id == id && x.Year > 1983 && x.Year < 1985);
            Assert.NotNull(result9);

            var result10 = _fixture._albumManager.albumRepository.FindAsync(x =>
                x.Name == "Test 8888");
            Assert.NotNull(result10);

            var result11 = _fixture._albumManager.albumRepository.FindAsync(x =>
                x.Name.StartsWith("Test 8"));
            Assert.NotNull(result11);

            //
            _fixture._albumManager.Delete(id);
        }

        [Fact]
        public async Task FindAll_WithFilter_GetsEntity()
        {
            List<ObjectId> ids = new List<ObjectId>();

            for (int i = 0; i < 5; i++)
            {
                Album album = new Album
                {
                    Name = "Test " + (9990 + i),
                    IsSales = i % 2 == 0,
                    Price = 9990 + i,
                    Year = 1990 + i
                };

                var result1 = _fixture._albumManager.albumRepository.Insert(album);
                Assert.NotEqual(ObjectId.Empty, result1.Id);

                _output.WriteLine($"New id {i} : " + result1.Id);
                ids.Add(result1.Id);
            }

            var result2 = _fixture._albumManager.albumRepository.FindAll(x => x.Price > 9989 && x.Price < 9995);
            Assert.NotNull(result2);

            var result3 = _fixture._albumManager.albumRepository.FindAll(x => x.IsSales);
            Assert.All(result3, x => Assert.True(x.IsSales));

            var result4 = _fixture._albumManager.albumRepository.FindAll(x => x.Year > 1989 && x.Year < 1995);
            Assert.Equal(5, result4.Count);

            var result6 = _fixture._albumManager.albumRepository.FindAll(x => x.Name.StartsWith("Test 999"));
            Assert.Equal(5, result6.Count);

            // Async methods
            var result7 = await _fixture._albumManager.albumRepository.FindAllAsync(x => x.Price > 9989 && x.Price < 9995);
            Assert.NotNull(result7);

            var result8 = await _fixture._albumManager.albumRepository.FindAllAsync(x => x.IsSales);
            Assert.All(result8, x => Assert.True(x.IsSales));

            var result9 = await _fixture._albumManager.albumRepository.FindAllAsync(x => x.Year > 1989 && x.Year < 1995);
            Assert.Equal(5, result9.Count);

            var result10 = await _fixture._albumManager.albumRepository.FindAllAsync(x => x.Name.StartsWith("Test 999"));
            Assert.Equal(5, result10.Count);

            //
            ids.ForEach(id => _fixture._albumManager.Delete(id));
        }

        [Fact]
        public async Task Update_UpdateDefinition_UpdatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = false,
                Price = 500,
                Year = 2000
            };

            var result1 = _fixture._albumManager.albumRepository.Insert(album);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            ObjectId id = result1.Id;
            _output.WriteLine("New id : " + id);
            _output.WriteLine(result1.ToJson());

            var updateDefinitions = Builders<Album>.Update
                .Set(x => x.Name, result1.Name + "_updated")
                .Set(x => x.IsSales, !result1.IsSales)
                .Set(x => x.Price, result1.Price + 100)
                .Set(x => x.Year, result1.Year + 2);

            var result2 = _fixture._albumManager.albumRepository.Update(id, updateDefinitions);
            Assert.Equal(1, result2);

            var result3 = _fixture._albumManager.albumRepository.Find(id);
            Assert.Equal(result1.Name + "_updated", result3.Name);
            Assert.Equal(!result1.IsSales, result3.IsSales);
            Assert.Equal(result1.Price + 100, result3.Price);
            Assert.Equal(result1.Year + 2, result3.Year);
            _output.WriteLine(result3.ToJson());

            //
            _fixture._albumManager.albumRepository.Delete(id);
        }

        [Fact]
        public async Task UpdateAsync_UpdateDefinition_UpdatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = false,
                Price = 500,
                Year = 2000
            };

            var result1 = _fixture._albumManager.albumRepository.Insert(album);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            ObjectId id = result1.Id;
            _output.WriteLine("New id : " + id);
            _output.WriteLine(result1.ToJson());

            var updateDefinitions = Builders<Album>.Update
                .Set(x => x.Name, result1.Name + "_updated")
                .Set(x => x.IsSales, !result1.IsSales)
                .Set(x => x.Price, result1.Price + 100)
                .Set(x => x.Year, result1.Year + 2);

            var result2 = await _fixture._albumManager.albumRepository.UpdateAsync(id, updateDefinitions);
            Assert.Equal(1, result2);

            var result3 = await _fixture._albumManager.albumRepository.FindAsync(id);
            Assert.Equal(result1.Name + "_updated", result3.Name);
            Assert.Equal(!result1.IsSales, result3.IsSales);
            Assert.Equal(result1.Price + 100, result3.Price);
            Assert.Equal(result1.Year + 2, result3.Year);
            _output.WriteLine(result3.ToJson());

            //
            await _fixture._albumManager.albumRepository.DeleteAsync(id);
        }
    }
}