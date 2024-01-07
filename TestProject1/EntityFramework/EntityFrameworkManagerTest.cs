using Xunit.Abstractions;

namespace TestProject1.EntityFramework
{
    [Collection("EntityFrameworkCollection")]
    public class EntityFrameworkManagerTest : IClassFixture<EntityFrameworkTestFixture>
    {
        private readonly EntityFrameworkTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public EntityFrameworkManagerTest(EntityFrameworkTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public async Task Create_ReturnsEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._songManager.Create(entity);
            var result2 = _fixture._songManager.Save();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            _fixture._songManager.Delete(result1.Id);
            _fixture._songManager.Save();
        }

        [Fact]
        public async Task CreateAsync_ReturnsEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._songManager.CreateAsync(entity);
            var result2 = await _fixture._songManager.SaveAsync();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            await _fixture._songManager.DeleteAsync(result1.Id);
            await _fixture._songManager.SaveAsync();
        }

        [Fact]
        public async Task Create_Model_ReturnsEntity()
        {
            SongCreate entity = new SongCreate
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._songManager.Create(entity);
            var result2 = _fixture._songManager.Save();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            _fixture._songManager.Delete(result1.Id);
            _fixture._songManager.Save();
        }

        [Fact]
        public async Task CreateAsync_Model_ReturnsEntity()
        {
            SongCreate entity = new SongCreate
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._songManager.CreateAsync(entity);
            var result2 = await _fixture._songManager.SaveAsync();
            Assert.Equal(1, result2);
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Id);

            await _fixture._songManager.DeleteAsync(result1.Id);
            await _fixture._songManager.SaveAsync();
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

            var result1 = _fixture._songManager.Create(entity);
            var result2 = _fixture._songManager.Save();

            var result3 = _fixture._songManager.Find(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            _fixture._songManager.Delete(result1.Id);
            _fixture._songManager.Save();
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

            var result1 = await _fixture._songManager.CreateAsync(entity);
            var result2 = await _fixture._songManager.SaveAsync();

            var result3 = await _fixture._songManager.FindAsync(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            await _fixture._songManager.DeleteAsync(result1.Id);
            await _fixture._songManager.SaveAsync();
        }

        [Fact]
        public async Task Find_EntityId_ReturnsEntityQuery()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._songManager.Create(entity);
            var result2 = _fixture._songManager.Save();

            var result3 = _fixture._songManager.Find<SongQuery>(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            _fixture._songManager.Delete(result1.Id);
            _fixture._songManager.Save();
        }

        [Fact]
        public async Task FindAsync_EntityId_ReturnsEntityQuery()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._songManager.CreateAsync(entity);
            var result2 = await _fixture._songManager.SaveAsync();

            var result3 = await _fixture._songManager.FindAsync<SongQuery>(entity.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Id, result3.Id);

            await _fixture._songManager.DeleteAsync(result1.Id);
            await _fixture._songManager.SaveAsync();
        }

        [Fact]
        public async Task Find_Filter_ReturnsEntity()
        {
            var result1 = _fixture._songManager.Find(x => x.Year == 1981);
            Assert.NotNull(result1);
            Assert.Equal(1981, result1.Year);

            var result2 = _fixture._songManager.Find(x => x.Name == "Summers CIC");
            Assert.NotNull(result2);
            Assert.Equal("Summers CIC", result2.Name);
        }

        [Fact]
        public async Task FindAsync_Filter_ReturnsEntity()
        {
            var result1 = await _fixture._songManager.FindAsync(x => x.Year == 1981);
            Assert.NotNull(result1);
            Assert.Equal(1981, result1.Year);

            var result2 = await _fixture._songManager.FindAsync(x => x.Name == "Summers CIC");
            Assert.NotNull(result2);
            Assert.Equal("Summers CIC", result2.Name);
        }

        [Fact]
        public async Task Find_Filter_ReturnsEntityQuery()
        {
            var result1 = _fixture._songManager.Find<SongQuery>(x => x.Year == 1981);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<SongQuery>(result1);
            Assert.Equal(1981, result1.Year);

            var result2 = _fixture._songManager.Find<SongQuery>(x => x.Name == "Summers CIC");
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<SongQuery>(result2);
            Assert.Equal("Summers CIC", result2.Name);
        }

        [Fact]
        public async Task FindAsync_Filter_ReturnsEntityQuery()
        {
            var result1 = await _fixture._songManager.FindAsync<SongQuery>(x => x.Year == 1981);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<SongQuery>(result1);
            Assert.Equal(1981, result1.Year);

            var result2 = await _fixture._songManager.FindAsync<SongQuery>(x => x.Name == "Summers CIC");
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<SongQuery>(result2);
            Assert.Equal("Summers CIC", result2.Name);
        }

        [Fact]
        public async Task FindAll_Filter_ReturnsEntity()
        {
            var result1 = _fixture._songManager.FindAll(x => x.IsSales);
            Assert.NotNull(result1);
            Assert.Equal(3, result1.Count());

            var result2 = _fixture._songManager.FindAll(x => x.Year > 2007 && x.Year < 2014);
            Assert.NotNull(result2);
            Assert.Equal(3, result2.Count());

            var result3 = _fixture._songManager.FindAll(x => x.Price > 2000);
            Assert.NotNull(result3);
            Assert.Single(result3);
        }

        [Fact]
        public async Task FindAllAsync_Filter_ReturnsEntity()
        {
            var result1 = await _fixture._songManager.FindAllAsync(x => x.IsSales);
            Assert.NotNull(result1);
            Assert.Equal(3, result1.Count());

            var result2 = await _fixture._songManager.FindAllAsync(x => x.Year > 2007 && x.Year < 2014);
            Assert.NotNull(result2);
            Assert.Equal(3, result2.Count());

            var result3 = await _fixture._songManager.FindAllAsync(x => x.Price > 2000);
            Assert.NotNull(result3);
            Assert.Single(result3);
        }

        [Fact]
        public async Task FindAll_Filter_ReturnsEntityQuery()
        {
            var result1 = _fixture._songManager.FindAll<SongQuery>(x => x.IsSales);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<IEnumerable<SongQuery>>(result1);
            Assert.Equal(3, result1.Count());

            var result2 = _fixture._songManager.FindAll<SongQuery>(x => x.Year > 2007 && x.Year < 2014);
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<IEnumerable<SongQuery>>(result2);
            Assert.Equal(3, result2.Count());

            var result3 = _fixture._songManager.FindAll<SongQuery>(x => x.Price > 2000);
            Assert.NotNull(result3);
            Assert.IsAssignableFrom<IEnumerable<SongQuery>>(result3);
            Assert.Single(result3);
        }

        [Fact]
        public async Task FindAllAsync_Filter_ReturnsEntityQuery()
        {
            var result1 = await _fixture._songManager.FindAllAsync<SongQuery>(x => x.IsSales);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<IEnumerable<SongQuery>>(result1);
            Assert.Equal(3, result1.Count());

            var result2 = await _fixture._songManager.FindAllAsync<SongQuery>(x => x.Year > 2007 && x.Year < 2014);
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<IEnumerable<SongQuery>>(result2);
            Assert.Equal(3, result2.Count());

            var result3 = await _fixture._songManager.FindAllAsync<SongQuery>(x => x.Price > 2000);
            Assert.NotNull(result3);
            Assert.IsAssignableFrom<IEnumerable<SongQuery>>(result3);
            Assert.Single(result3);
        }

        [Fact]
        public async Task List_ReturnsEntities()
        {
            var result1 = _fixture._songManager.List();
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Count);
            Assert.IsAssignableFrom<IList<Song>>(result1);
        }

        [Fact]
        public async Task ListAsync_ReturnsEntities()
        {
            var result1 = await _fixture._songManager.ListAsync();
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Count);
            Assert.IsAssignableFrom<IList<Song>>(result1);
        }

        [Fact]
        public async Task List_ReturnsEntitiesQuery()
        {
            var result1 = _fixture._songManager.List<SongQuery>();
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Count);
            Assert.IsAssignableFrom<IList<SongQuery>>(result1);
        }

        [Fact]
        public async Task ListAsync_ReturnsEntitiesQuery()
        {
            var result1 = await _fixture._songManager.ListAsync<SongQuery>();
            Assert.NotNull(result1);
            Assert.NotEqual(0, result1.Count);
            Assert.IsAssignableFrom<IList<SongQuery>>(result1);
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

            var result1 = _fixture._songManager.Create(entity);
            var result2 = _fixture._songManager.Save();

            _fixture._songManager.Delete(result1.Id);
            var result3 = _fixture._songManager.Save();

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

            var result1 = await _fixture._songManager.CreateAsync(entity);
            var result2 = await _fixture._songManager.SaveAsync();

            await _fixture._songManager.DeleteAsync(result1.Id);
            var result3 = await _fixture._songManager.SaveAsync();

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

            var result1 = _fixture._songManager.Create(entity);
            var result2 = _fixture._songManager.Save();

            entity.Name += "_updated";
            entity.IsSales = !entity.IsSales;
            entity.Price += 10;
            entity.Year += 1;

            _fixture._songManager.Update(entity.Id, entity);
            var result3 = _fixture._songManager.Save();
            Assert.Equal(1, result3);

            var result4 = _fixture._songManager.Find(entity.Id);
            Assert.Equal(entity.Name, result4.Name);
            Assert.Equal(entity.IsSales, result4.IsSales);
            Assert.Equal(entity.Price, result4.Price);
            Assert.Equal(entity.Year, result4.Year);

            _fixture._songManager.Delete(result4.Id);
            _fixture._songManager.Save();
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

            var result1 = await _fixture._songManager.CreateAsync(entity);
            var result2 = await _fixture._songManager.SaveAsync();

            entity.Name += "_updated";
            entity.IsSales = !entity.IsSales;
            entity.Price += 10;
            entity.Year += 1;

            await _fixture._songManager.UpdateAsync(entity.Id, entity);
            var result3 = await _fixture._songManager.SaveAsync();
            Assert.Equal(1, result3);

            var result4 = await _fixture._songManager.FindAsync(entity.Id);
            Assert.Equal(entity.Name, result4.Name);
            Assert.Equal(entity.IsSales, result4.IsSales);
            Assert.Equal(entity.Price, result4.Price);
            Assert.Equal(entity.Year, result4.Year);

            await _fixture._songManager.DeleteAsync(result4.Id);
            await _fixture._songManager.SaveAsync();
        }

        [Fact]
        public async Task Update_Model_UpdatesEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._songManager.Create(entity);
            var result2 = _fixture._songManager.Save();

            SongEdit model = new SongEdit
            {
                Name = entity.Name + "_updated",
                IsSales = !entity.IsSales,
                Price = entity.Price + 10,
                Year = entity.Year + 1
            };

            _fixture._songManager.Update(entity.Id, model);
            var result3 = _fixture._songManager.Save();
            Assert.Equal(1, result3);

            var result4 = _fixture._songManager.Find(entity.Id);
            Assert.Equal(model.Name, result4.Name);
            Assert.Equal(model.IsSales, result4.IsSales);
            Assert.Equal(model.Price, result4.Price);
            Assert.Equal(model.Year, result4.Year);

            _fixture._songManager.Delete(result4.Id);
            _fixture._songManager.Save();
        }

        [Fact]
        public async Task UpdateAsync_Model_UpdatesEntity()
        {
            Song entity = new Song
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._songManager.CreateAsync(entity);
            var result2 = await _fixture._songManager.SaveAsync();

            SongEdit model = new SongEdit
            {
                Name = entity.Name + "_updated",
                IsSales = !entity.IsSales,
                Price = entity.Price + 10,
                Year = entity.Year + 1
            };

            await _fixture._songManager.UpdateAsync(entity.Id, model);
            var result3 = await _fixture._songManager.SaveAsync();
            Assert.Equal(1, result3);

            var result4 = await _fixture._songManager.FindAsync(entity.Id);
            Assert.Equal(model.Name, result4.Name);
            Assert.Equal(model.IsSales, result4.IsSales);
            Assert.Equal(model.Price, result4.Price);
            Assert.Equal(model.Year, result4.Year);

            await _fixture._songManager.DeleteAsync(result4.Id);
            await _fixture._songManager.SaveAsync();
        }
    }
}