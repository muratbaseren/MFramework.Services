using Xunit.Abstractions;

namespace TestProject1.EntityFrameworkCore
{
    [Collection("EntityFrameworkCoreCollection")]
    public class EntityFrameworkCoreManagerTest : IClassFixture<EntityFrameworkCoreTestFixture>
    {
        private readonly EntityFrameworkCoreTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public EntityFrameworkCoreManagerTest(EntityFrameworkCoreTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public async Task Create_ReturnsEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._bookManager.Create(entity);
            var result2 = _fixture._bookManager.Save();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            _fixture._bookManager.Delete(result1.Id);
            _fixture._bookManager.Save();
        }

        [Fact]
        public async Task CreateAsync_ReturnsEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._bookManager.CreateAsync(entity);
            var result2 = await _fixture._bookManager.SaveAsync();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            await _fixture._bookManager.DeleteAsync(result1.Id);
            await _fixture._bookManager.SaveAsync();
        }

        [Fact]
        public async Task Create_Model_ReturnsEntity()
        {
            BookCreate entity = new BookCreate
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._bookManager.Create(entity);
            var result2 = _fixture._bookManager.Save();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            _fixture._bookManager.Delete(result1.Id);
            _fixture._bookManager.Save();
        }

        [Fact]
        public async Task CreateAsync_Model_ReturnsEntity()
        {
            BookCreate entity = new BookCreate
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._bookManager.CreateAsync(entity);
            var result2 = await _fixture._bookManager.SaveAsync();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            await _fixture._bookManager.DeleteAsync(result1.Id);
            await _fixture._bookManager.SaveAsync();
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

            var result1 = _fixture._bookManager.Create(entity);
            var result2 = _fixture._bookManager.Save();

            var result3 = _fixture._bookManager.Find(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            _fixture._bookManager.Delete(result1.Id);
            _fixture._bookManager.Save();
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

            var result1 = await _fixture._bookManager.CreateAsync(entity);
            var result2 = await _fixture._bookManager.SaveAsync();

            var result3 = await _fixture._bookManager.FindAsync(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            await _fixture._bookManager.DeleteAsync(result1.Id);
            await _fixture._bookManager.SaveAsync();
        }

        [Fact]
        public async Task Find_EntityId_ReturnsEntityQuery()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._bookManager.Create(entity);
            var result2 = _fixture._bookManager.Save();

            var result3 = _fixture._bookManager.Find<BookQuery>(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            _fixture._bookManager.Delete(result1.Id);
            _fixture._bookManager.Save();
        }

        [Fact]
        public async Task FindAsync_EntityId_ReturnsEntityQuery()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._bookManager.CreateAsync(entity);
            var result2 = await _fixture._bookManager.SaveAsync();

            var result3 = await _fixture._bookManager.FindAsync<BookQuery>(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            await _fixture._bookManager.DeleteAsync(result1.Id);
            await _fixture._bookManager.SaveAsync();
        }

        [Fact]
        public async Task Find_Filter_ReturnsEntity()
        {
            var result1 = _fixture._bookManager.Find(x => x.Year == 1981);
            Assert.NotNull(result1);
            Assert.Equal(1981, result1.Year);

            var result2 = _fixture._bookManager.Find(x => x.Name == "Summers CIC");
            Assert.NotNull(result2);
            Assert.Equal("Summers CIC", result2.Name);
        }

        [Fact]
        public async Task FindAsync_Filter_ReturnsEntity()
        {
            var result1 = await _fixture._bookManager.FindAsync(x => x.Year == 1981);
            Assert.NotNull(result1);
            Assert.Equal(1981, result1.Year);

            var result2 = await _fixture._bookManager.FindAsync(x => x.Name == "Summers CIC");
            Assert.NotNull(result2);
            Assert.Equal("Summers CIC", result2.Name);
        }

        [Fact]
        public async Task Find_Filter_ReturnsEntityQuery()
        {
            var result1 = _fixture._bookManager.Find<BookQuery>(x => x.Year == 1981);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<BookQuery>(result1);
            Assert.Equal(1981, result1.Year);

            var result2 = _fixture._bookManager.Find<BookQuery>(x => x.Name == "Summers CIC");
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<BookQuery>(result2);
            Assert.Equal("Summers CIC", result2.Name);
        }

        [Fact]
        public async Task FindAsync_Filter_ReturnsEntityQuery()
        {
            var result1 = await _fixture._bookManager.FindAsync<BookQuery>(x => x.Year == 1981);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<BookQuery>(result1);
            Assert.Equal(1981, result1.Year);

            var result2 = await _fixture._bookManager.FindAsync<BookQuery>(x => x.Name == "Summers CIC");
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<BookQuery>(result2);
            Assert.Equal("Summers CIC", result2.Name);
        }

        [Fact]
        public async Task FindAll_Filter_ReturnsEntity()
        {
            var result1 = _fixture._bookManager.FindAll(x => x.IsSales);
            Assert.NotNull(result1);
            Assert.Equal(3, result1.Count());

            var result2 = _fixture._bookManager.FindAll(x => x.Year > 2007 && x.Year < 2014);
            Assert.NotNull(result2);
            Assert.Equal(3, result2.Count());

            var result3 = _fixture._bookManager.FindAll(x => x.Price > 2000);
            Assert.NotNull(result3);
            Assert.Single(result3);
        }

        [Fact]
        public async Task FindAllAsync_Filter_ReturnsEntity()
        {
            var result1 = await _fixture._bookManager.FindAllAsync(x => x.IsSales);
            Assert.NotNull(result1);
            Assert.Equal(3, result1.Count());

            var result2 = await _fixture._bookManager.FindAllAsync(x => x.Year > 2007 && x.Year < 2014);
            Assert.NotNull(result2);
            Assert.Equal(3, result2.Count());

            var result3 = await _fixture._bookManager.FindAllAsync(x => x.Price > 2000);
            Assert.NotNull(result3);
            Assert.Single(result3);
        }

        [Fact]
        public async Task FindAll_Filter_ReturnsEntityQuery()
        {
            var result1 = _fixture._bookManager.FindAll<BookQuery>(x => x.IsSales);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<IEnumerable<BookQuery>>(result1);
            Assert.Equal(3, result1.Count());

            var result2 = _fixture._bookManager.FindAll<BookQuery>(x => x.Year > 2007 && x.Year < 2014);
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<IEnumerable<BookQuery>>(result2);
            Assert.Equal(3, result2.Count());

            var result3 = _fixture._bookManager.FindAll<BookQuery>(x => x.Price > 2000);
            Assert.NotNull(result3);
            Assert.IsAssignableFrom<IEnumerable<BookQuery>>(result3);
            Assert.Single(result3);
        }

        [Fact]
        public async Task FindAllAsync_Filter_ReturnsEntityQuery()
        {
            var result1 = await _fixture._bookManager.FindAllAsync<BookQuery>(x => x.IsSales);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<IEnumerable<BookQuery>>(result1);
            Assert.Equal(3, result1.Count());

            var result2 = await _fixture._bookManager.FindAllAsync<BookQuery>(x => x.Year > 2007 && x.Year < 2014);
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<IEnumerable<BookQuery>>(result2);
            Assert.Equal(3, result2.Count());

            var result3 = await _fixture._bookManager.FindAllAsync<BookQuery>(x => x.Price > 2000);
            Assert.NotNull(result3);
            Assert.IsAssignableFrom<IEnumerable<BookQuery>>(result3);
            Assert.Single(result3);
        }

        [Fact]
        public async Task List_ReturnsEntities()
        {
            var result1 = _fixture._bookManager.List();
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Count);
            Assert.IsAssignableFrom<IList<Book>>(result1);
        }

        [Fact]
        public async Task ListAsync_ReturnsEntities()
        {
            var result1 = await _fixture._bookManager.ListAsync();
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Count);
            Assert.IsAssignableFrom<IList<Book>>(result1);
        }

        [Fact]
        public async Task List_ReturnsEntitiesQuery()
        {
            var result1 = _fixture._bookManager.List<BookQuery>();
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Count);
            Assert.IsAssignableFrom<IList<BookQuery>>(result1);
        }

        [Fact]
        public async Task ListAsync_ReturnsEntitiesQuery()
        {
            var result1 = await _fixture._bookManager.ListAsync<BookQuery>();
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Count);
            Assert.IsAssignableFrom<IList<BookQuery>>(result1);
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

            var result1 = _fixture._bookManager.Create(entity);
            var result2 = _fixture._bookManager.Save();

            _fixture._bookManager.Delete(result1.Id);
            var result3 = _fixture._bookManager.Save();

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

            var result1 = await _fixture._bookManager.CreateAsync(entity);
            var result2 = await _fixture._bookManager.SaveAsync();

            await _fixture._bookManager.DeleteAsync(result1.Id);
            var result3 = await _fixture._bookManager.SaveAsync();

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

            var result1 = _fixture._bookManager.Create(entity);
            var result2 = _fixture._bookManager.Save();

            entity.Name += "_updated";
            entity.IsSales = !entity.IsSales;
            entity.Price += 10;
            entity.Year += 1;

            _fixture._bookManager.Update(entity.Id, entity);
            var result3 = _fixture._bookManager.Save();
            Assert.Equal(1, result3);

            var result4 = _fixture._bookManager.Find(entity.Id);
            Assert.Equal(entity.Name, result4.Name);
            Assert.Equal(entity.IsSales, result4.IsSales);
            Assert.Equal(entity.Price, result4.Price);
            Assert.Equal(entity.Year, result4.Year);

            _fixture._bookManager.Delete(result4.Id);
            _fixture._bookManager.Save();
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

            var result1 = await _fixture._bookManager.CreateAsync(entity);
            var result2 = await _fixture._bookManager.SaveAsync();

            entity.Name += "_updated";
            entity.IsSales = !entity.IsSales;
            entity.Price += 10;
            entity.Year += 1;

            await _fixture._bookManager.UpdateAsync(entity.Id, entity);
            var result3 = await _fixture._bookManager.SaveAsync();
            Assert.Equal(1, result3);

            var result4 = await _fixture._bookManager.FindAsync(entity.Id);
            Assert.Equal(entity.Name, result4.Name);
            Assert.Equal(entity.IsSales, result4.IsSales);
            Assert.Equal(entity.Price, result4.Price);
            Assert.Equal(entity.Year, result4.Year);

            await _fixture._bookManager.DeleteAsync(result4.Id);
            await _fixture._bookManager.SaveAsync();
        }

        [Fact]
        public async Task Update_Model_UpdatesEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._bookManager.Create(entity);
            var result2 = _fixture._bookManager.Save();

            BookEdit model = new BookEdit
            {
                Name = entity.Name + "_updated",
                IsSales = !entity.IsSales,
                Price = entity.Price + 10,
                Year = entity.Year + 1
            };

            _fixture._bookManager.Update(entity.Id, model);
            var result3 = _fixture._bookManager.Save();
            Assert.Equal(1, result3);

            var result4 = _fixture._bookManager.Find(entity.Id);
            Assert.Equal(model.Name, result4.Name);
            Assert.Equal(model.IsSales, result4.IsSales);
            Assert.Equal(model.Price, result4.Price);
            Assert.Equal(model.Year, result4.Year);

            _fixture._bookManager.Delete(result4.Id);
            _fixture._bookManager.Save();
        }

        [Fact]
        public async Task UpdateAsync_Model_UpdatesEntity()
        {
            Book entity = new Book
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._bookManager.CreateAsync(entity);
            var result2 = await _fixture._bookManager.SaveAsync();

            BookEdit model = new BookEdit
            {
                Name = entity.Name + "_updated",
                IsSales = !entity.IsSales,
                Price = entity.Price + 10,
                Year = entity.Year + 1
            };

            await _fixture._bookManager.UpdateAsync(entity.Id, model);
            var result3 = await _fixture._bookManager.SaveAsync();
            Assert.Equal(1, result3);

            var result4 = await _fixture._bookManager.FindAsync(entity.Id);
            Assert.Equal(model.Name, result4.Name);
            Assert.Equal(model.IsSales, result4.IsSales);
            Assert.Equal(model.Price, result4.Price);
            Assert.Equal(model.Year, result4.Year);

            await _fixture._bookManager.DeleteAsync(result4.Id);
            await _fixture._bookManager.SaveAsync();
        }
    }
}