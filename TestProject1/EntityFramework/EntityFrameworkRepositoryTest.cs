using Xunit.Abstractions;

namespace TestProject1.EntityFramework
{
    [Collection("EntityFrameworkCollection")]
    public class EntityFrameworkRepositoryTest : IClassFixture<EntityFrameworkTestFixture>
    {
        private readonly EntityFrameworkTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public EntityFrameworkRepositoryTest(EntityFrameworkTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public async Task Add_ReturnsEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._songRepository.Add(entity);
            var result2 = _fixture._songRepository.Save();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            _fixture._songRepository.Remove(result1);
            _fixture._songRepository.Save();
        }

        [Fact]
        public async Task AddAsync_ReturnsEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._songRepository.AddAsync(entity);
            var result2 = await _fixture._songRepository.SaveAsync();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            await _fixture._songRepository.RemoveAsync(result1);
            await _fixture._songRepository.SaveAsync();
        }

        [Fact]
        public async Task Find_EntityId_ReturnsEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._songRepository.Add(entity);
            var result2 = _fixture._songRepository.Save();

            var result3 = _fixture._songRepository.Find(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            _fixture._songRepository.Remove(result1);
            _fixture._songRepository.Save();
        }

        [Fact]
        public async Task FindAsync_EntityId_ReturnsEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._songRepository.AddAsync(entity);
            var result2 = await _fixture._songRepository.SaveAsync();

            var result3 = await _fixture._songRepository.FindAsync(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            await _fixture._songRepository.RemoveAsync(result1);
            await _fixture._songRepository.SaveAsync();
        }

        [Fact]
        public async Task FirstOrDefaultAsync_Filter_ReturnsEntity()
        {
            var result1 = await _fixture._songRepository.FirstOrDefaultAsync(x => x.Year == 2013);
            Assert.NotNull(result1);
        }

        [Fact]
        public async Task ListAsync_ReturnsEntities()
        {
            var result1 = await _fixture._songRepository.ListAsync();
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Count);
            Assert.IsAssignableFrom<IList<Song>>(result1);
        }

        [Fact]
        public async Task Remove_EntityId_RemovesEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._songRepository.Add(entity);
            var result2 = _fixture._songRepository.Save();

            _fixture._songRepository.Remove(result1.Id);
            var result3 = _fixture._songRepository.Save();

            Assert.Equal(1, result3);
        }

        [Fact]
        public async Task RemoveAsync_EntityId_RemovesEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._songRepository.AddAsync(entity);
            var result2 = await _fixture._songRepository.SaveAsync();

            await _fixture._songRepository.RemoveAsync(result1.Id);
            var result3 = await _fixture._songRepository.SaveAsync();

            Assert.Equal(1, result3);
        }

        [Fact]
        public async Task Remove_Entity_RemovesEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._songRepository.Add(entity);
            var result2 = _fixture._songRepository.Save();

            _fixture._songRepository.Remove(result1);
            var result3 = _fixture._songRepository.Save();

            Assert.Equal(1, result3);
        }

        [Fact]
        public async Task RemoveAsync_Entity_RemovesEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._songRepository.AddAsync(entity);
            var result2 = await _fixture._songRepository.SaveAsync();

            await _fixture._songRepository.RemoveAsync(result1);
            var result3 = await _fixture._songRepository.SaveAsync();

            Assert.Equal(1, result3);
        }

        [Fact]
        public async Task Update_UpdatesEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._songRepository.Add(entity);
            var result2 = _fixture._songRepository.Save();

            entity.Name += "_updated";
            entity.IsSales = !entity.IsSales;
            entity.Price += 10;
            entity.Year += 1;

            _fixture._songRepository.Update(entity.Id, entity);
            var result3 = _fixture._songRepository.Save();
            Assert.Equal(1, result3);

            var result4 = _fixture._songRepository.Find(entity.Id);
            Assert.Equal(entity.Name, result4.Name);
            Assert.Equal(entity.IsSales, result4.IsSales);
            Assert.Equal(entity.Price, result4.Price);
            Assert.Equal(entity.Year, result4.Year);

            _fixture._songRepository.Remove(result4.Id);
            _fixture._songRepository.Save();
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 =await _fixture._songRepository.AddAsync(entity);
            var result2 = await _fixture._songRepository.SaveAsync();

            entity.Name += "_updated";
            entity.IsSales = !entity.IsSales;
            entity.Price += 10;
            entity.Year += 1;

            await _fixture._songRepository.UpdateAsync(entity.Id, entity);
            var result3 = await _fixture._songRepository.SaveAsync();
            Assert.Equal(1, result3);

            var result4 = await _fixture._songRepository.FindAsync(entity.Id);
            Assert.Equal(entity.Name, result4.Name);
            Assert.Equal(entity.IsSales, result4.IsSales);
            Assert.Equal(entity.Price, result4.Price);
            Assert.Equal(entity.Year, result4.Year);

            await _fixture._songRepository.RemoveAsync(result4.Id);
            await _fixture._songRepository.SaveAsync();
        }
    }
}