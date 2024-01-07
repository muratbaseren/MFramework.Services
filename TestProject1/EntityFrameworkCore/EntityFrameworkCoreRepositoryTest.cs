using Xunit.Abstractions;

namespace TestProject1.EntityFrameworkCore
{
    [Collection("EntityFrameworkCoreCollection")]
    public class EntityFrameworkCoreRepositoryTest : IClassFixture<EntityFrameworkCoreTestFixture>
    {
        private readonly EntityFrameworkCoreTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public EntityFrameworkCoreRepositoryTest(EntityFrameworkCoreTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public async Task Add_ReturnsEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._bookRepository.Add(entity);
            var result2 = _fixture._bookRepository.Save();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            _fixture._bookRepository.Remove(result1);
            _fixture._bookRepository.Save();
        }

        [Fact]
        public async Task AddAsync_ReturnsEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._bookRepository.AddAsync(entity);
            var result2 = await _fixture._bookRepository.SaveAsync();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            await _fixture._bookRepository.RemoveAsync(result1);
            await _fixture._bookRepository.SaveAsync();
        }

        [Fact]
        public async Task Find_EntityId_ReturnsEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._bookRepository.Add(entity);
            var result2 = _fixture._bookRepository.Save();

            var result3 = _fixture._bookRepository.Find(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            _fixture._bookRepository.Remove(result1);
            _fixture._bookRepository.Save();
        }

        [Fact]
        public async Task FindAsync_EntityId_ReturnsEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._bookRepository.AddAsync(entity);
            var result2 = await _fixture._bookRepository.SaveAsync();

            var result3 = await _fixture._bookRepository.FindAsync(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            await _fixture._bookRepository.RemoveAsync(result1);
            await _fixture._bookRepository.SaveAsync();
        }

        [Fact]
        public async Task FirstOrDefaultAsync_Filter_ReturnsEntity()
        {
            var result1 = await _fixture._bookRepository.FirstOrDefaultAsync(x => x.Year == 2013);
            Assert.NotNull(result1);
        }

        [Fact]
        public async Task ListAsync_ReturnsEntities()
        {
            var result1 = await _fixture._bookRepository.ListAsync();
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Count);
            Assert.IsAssignableFrom<IList<Book>>(result1);
        }

        [Fact]
        public async Task Remove_EntityId_RemovesEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._bookRepository.Add(entity);
            var result2 = _fixture._bookRepository.Save();

            _fixture._bookRepository.Remove(result1.Id);
            var result3 = _fixture._bookRepository.Save();

            Assert.Equal(1, result3);
        }

        [Fact]
        public async Task RemoveAsync_EntityId_RemovesEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._bookRepository.AddAsync(entity);
            var result2 = await _fixture._bookRepository.SaveAsync();

            await _fixture._bookRepository.RemoveAsync(result1.Id);
            var result3 = await _fixture._bookRepository.SaveAsync();

            Assert.Equal(1, result3);
        }

        [Fact]
        public async Task Remove_Entity_RemovesEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._bookRepository.Add(entity);
            var result2 = _fixture._bookRepository.Save();

            _fixture._bookRepository.Remove(result1);
            var result3 = _fixture._bookRepository.Save();

            Assert.Equal(1, result3);
        }

        [Fact]
        public async Task RemoveAsync_Entity_RemovesEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._bookRepository.AddAsync(entity);
            var result2 = await _fixture._bookRepository.SaveAsync();

            await _fixture._bookRepository.RemoveAsync(result1);
            var result3 = await _fixture._bookRepository.SaveAsync();

            Assert.Equal(1, result3);
        }

        [Fact]
        public async Task Update_UpdatesEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._bookRepository.Add(entity);
            var result2 = _fixture._bookRepository.Save();

            entity.Name += "_updated";
            entity.IsSales = !entity.IsSales;
            entity.Price += 10;
            entity.Year += 1;

            _fixture._bookRepository.Update(entity.Id, entity);
            var result3 = _fixture._bookRepository.Save();
            Assert.Equal(1, result3);

            var result4 = _fixture._bookRepository.Find(entity.Id);
            Assert.Equal(entity.Name, result4.Name);
            Assert.Equal(entity.IsSales, result4.IsSales);
            Assert.Equal(entity.Price, result4.Price);
            Assert.Equal(entity.Year, result4.Year);

            _fixture._bookRepository.Remove(result4.Id);
            _fixture._bookRepository.Save();
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 =await _fixture._bookRepository.AddAsync(entity);
            var result2 = await _fixture._bookRepository.SaveAsync();

            entity.Name += "_updated";
            entity.IsSales = !entity.IsSales;
            entity.Price += 10;
            entity.Year += 1;

            await _fixture._bookRepository.UpdateAsync(entity.Id, entity);
            var result3 = await _fixture._bookRepository.SaveAsync();
            Assert.Equal(1, result3);

            var result4 = await _fixture._bookRepository.FindAsync(entity.Id);
            Assert.Equal(entity.Name, result4.Name);
            Assert.Equal(entity.IsSales, result4.IsSales);
            Assert.Equal(entity.Price, result4.Price);
            Assert.Equal(entity.Year, result4.Year);

            await _fixture._bookRepository.RemoveAsync(result4.Id);
            await _fixture._bookRepository.SaveAsync();
        }
    }
}