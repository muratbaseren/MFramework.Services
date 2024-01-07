using MongoDB.Bson;
using TestProject1.MongoTests.MongoObjects;
using Xunit.Abstractions;

namespace TestProject1.MongoTests
{
    [Collection("MongoCollection")]
    public class MongoManagerTest : IClassFixture<MongoTestFixture>
    {
        private readonly MongoTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public MongoManagerTest(MongoTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public async Task HasDocument_ChecksTrue()
        {
            var result1 = _fixture._albumManager.HasDocument();

            Assert.True(result1);
        }

        [Fact]
        public async Task HasDocumentAsync_ChecksTrue()
        {
            var result1 = await _fixture._albumManager.HasDocumentAsync();

            Assert.True(result1);
        }

        [Fact]
        public async Task List_ChecksResultType()
        {
            var result1 = _fixture._albumManager.List();

            Assert.NotNull(result1);
            Assert.IsAssignableFrom<IEnumerable<Album>>(result1);
        }

        [Fact]
        public async Task ListAsync_ChecksResultType()
        {
            var result1 = await _fixture._albumManager.ListAsync();

            Assert.NotNull(result1);
            Assert.IsAssignableFrom<IEnumerable<Album>>(result1);
        }

        [Fact]
        public async Task List_TEntity_ChecksResultType()
        {
            var result1 = _fixture._albumManager.List<AlbumQuery>();

            Assert.NotNull(result1);
            Assert.IsAssignableFrom<IEnumerable<AlbumQuery>>(result1);
        }

        [Fact]
        public async Task ListAsync_TEntity_ChecksResultType()
        {
            var result1 = await _fixture._albumManager.ListAsync<AlbumQuery>();

            Assert.NotNull(result1);
            Assert.IsAssignableFrom<IEnumerable<AlbumQuery>>(result1);
        }

        [Fact]
        public async Task Create_Entity_CreatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._albumManager.Create(album);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _fixture._albumManager.Delete(result1.Id);
        }

        [Fact]
        public async Task CreateAsync_Entity_CreatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._albumManager.CreateAsync(album);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _fixture._albumManager.Delete(result1.Id);
        }

        [Fact]
        public async Task Create_Model_CreatesEntity()
        {
            AlbumCreate model = new AlbumCreate
            {
                Name = "test",
                IsSales = true,
                Price = 100,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._albumManager.Create(model);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _fixture._albumManager.Delete(result1.Id);
        }

        [Fact]
        public async Task CreateAsync_Model_CreatesEntity()
        {
            AlbumCreate model = new AlbumCreate
            {
                Name = "test",
                IsSales = true,
                Price = 100,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._albumManager.CreateAsync(model);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _fixture._albumManager.Delete(result1.Id);
        }

        [Fact]
        public async Task Update_Entity_UpdatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            // Create new sample entity.
            var result1 = _fixture._albumManager.Create(album);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _output.WriteLine("Entity : " + result1.ToJson());

            // The created entity props are changed.
            result1.Name += "_updated";
            result1.IsSales = !album.IsSales;
            result1.Price += 10;
            result1.Year += 1;

            _output.WriteLine("Entity(changed) : " + result1.ToJson());

            // The created entity is updated.
            var result2 = _fixture._albumManager.Update(result1.Id, result1);
            Assert.Equal(1, result2);

            // Find the updated entity from db.
            var result3 = _fixture._albumManager.Find(result1.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Name, result3.Name);
            Assert.Equal(result1.IsSales, result3.IsSales);
            Assert.Equal(result1.Price, result3.Price);
            Assert.Equal(result1.Year, result3.Year);

            _output.WriteLine("Entity(find-updated) : " + result3.ToJson());

            // Delete the entity.
            _fixture._albumManager.Delete(result1.Id);
        }

        [Fact]
        public async Task UpdateAsync_Entity_UpdatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            // Create new sample entity.
            var result1 = await _fixture._albumManager.CreateAsync(album);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _output.WriteLine("Entity : " + result1.ToJson());

            // The created entity props are changed.
            result1.Name += "_updated";
            result1.IsSales = !album.IsSales;
            result1.Price += 10;
            result1.Year += 1;

            _output.WriteLine("Entity(changed) : " + result1.ToJson());

            // The created entity is updated.
            var result2 = await _fixture._albumManager.UpdateAsync(result1.Id, result1);
            Assert.Equal(1, result2);

            // Find the updated entity from db.
            var result3 = await _fixture._albumManager.FindAsync(result1.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Name, result3.Name);
            Assert.Equal(result1.IsSales, result3.IsSales);
            Assert.Equal(result1.Price, result3.Price);
            Assert.Equal(result1.Year, result3.Year);

            _output.WriteLine("Entity(find-updated) : " + result3.ToJson());

            // Delete the entity.
            await _fixture._albumManager.DeleteAsync(result1.Id);
        }

        [Fact]
        public async Task Update_Model_UpdatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            // Create new sample entity.
            var result1 = _fixture._albumManager.Create(album);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _output.WriteLine("Entity : " + result1.ToJson());

            // Update model is created.
            AlbumEdit model = new AlbumEdit
            {
                Name = album.Name + "_updated",
                IsSales = !album.IsSales,
                Price = album.Price + 10,
                Year = album.Year + 1
            };

            _output.WriteLine("Entity(changed) : " + result1.ToJson());

            // The entity updated with model.
            var result2 = _fixture._albumManager.Update(result1.Id, model);
            Assert.Equal(1, result2);

            // Find the updated entity from db.
            var result3 = _fixture._albumManager.Find(result1.Id);
            Assert.NotNull(result3);
            Assert.Equal(model.Name, result3.Name);
            Assert.Equal(model.IsSales, result3.IsSales);
            Assert.Equal(model.Price, result3.Price);
            Assert.Equal(model.Year, result3.Year);

            _output.WriteLine("Entity(find-updated) : " + result3.ToJson());

            // Delete the entity.
            _fixture._albumManager.Delete(result1.Id);
        }

        [Fact]
        public async Task UpdateAsync_Model_UpdatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            // Create new sample entity.
            var result1 = await _fixture._albumManager.CreateAsync(album);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _output.WriteLine("Entity : " + result1.ToJson());

            // Update model is created.
            AlbumEdit model = new AlbumEdit
            {
                Name = album.Name + "_updated",
                IsSales = !album.IsSales,
                Price = album.Price + 10,
                Year = album.Year + 1
            };

            _output.WriteLine("Entity(changed) : " + result1.ToJson());

            // The entity updated with model.
            var result2 = await _fixture._albumManager.UpdateAsync(result1.Id, model);
            Assert.Equal(1, result2);

            // Find the updated entity from db.
            var result3 = await _fixture._albumManager.FindAsync(result1.Id);
            Assert.NotNull(result3);
            Assert.Equal(model.Name, result3.Name);
            Assert.Equal(model.IsSales, result3.IsSales);
            Assert.Equal(model.Price, result3.Price);
            Assert.Equal(model.Year, result3.Year);

            _output.WriteLine("Entity(find-updated) : " + result3.ToJson());

            // Delete the entity.
            await _fixture._albumManager.DeleteAsync(result1.Id);
        }

        [Fact]
        public async Task UpdateProperties_Entity_UpdatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            // Create new sample entity.
            var result1 = _fixture._albumManager.Create(album);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _output.WriteLine("Entity : " + result1.ToJson());

            // The created entity props are changed.
            result1.Name += "_updated";
            result1.IsSales = !album.IsSales;
            result1.Price += 10;
            result1.Year += 1;

            _output.WriteLine("Entity(changed) : " + result1.ToJson());

            // The created entity is updated.
            var result2 = _fixture._albumManager.UpdateProperties(result1.Id, result1);
            Assert.Equal(1, result2);

            // Find the updated entity from db.
            var result3 = _fixture._albumManager.Find(result1.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Name, result3.Name);
            Assert.Equal(result1.IsSales, result3.IsSales);
            Assert.Equal(result1.Price, result3.Price);
            Assert.Equal(result1.Year, result3.Year);

            _output.WriteLine("Entity(find-updated) : " + result3.ToJson());

            // Delete the entity.
            _fixture._albumManager.Delete(result1.Id);
        }

        [Fact]
        public async Task UpdatePropertiesAsync_Entity_UpdatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            // Create new sample entity.
            var result1 = await _fixture._albumManager.CreateAsync(album);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _output.WriteLine("Entity : " + result1.ToJson());

            // The created entity props are changed.
            result1.Name += "_updated";
            result1.IsSales = !album.IsSales;
            result1.Price += 10;
            result1.Year += 1;

            _output.WriteLine("Entity(changed) : " + result1.ToJson());

            // The created entity is updated.
            var result2 = await _fixture._albumManager.UpdatePropertiesAsync(result1.Id, result1);
            Assert.Equal(1, result2);

            // Find the updated entity from db.
            var result3 = await _fixture._albumManager.FindAsync(result1.Id);
            Assert.NotNull(result3);
            Assert.Equal(result1.Name, result3.Name);
            Assert.Equal(result1.IsSales, result3.IsSales);
            Assert.Equal(result1.Price, result3.Price);
            Assert.Equal(result1.Year, result3.Year);

            _output.WriteLine("Entity(find-updated) : " + result3.ToJson());

            // Delete the entity.
            await _fixture._albumManager.DeleteAsync(result1.Id);
        }

        [Fact]
        public async Task UpdateProperties_Model_UpdatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            // Create new sample entity.
            var result1 = _fixture._albumManager.Create(album);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _output.WriteLine("Entity : " + result1.ToJson());

            // Update model is created.
            AlbumEdit model = new AlbumEdit
            {
                Name = album.Name + "_updated",
                IsSales = !album.IsSales,
                Price = album.Price + 10,
                Year = album.Year + 1
            };

            _output.WriteLine("Entity(changed) : " + result1.ToJson());

            // The entity updated with model.
            var result2 = _fixture._albumManager.UpdateProperties(result1.Id, model);
            Assert.Equal(1, result2);

            // Find the updated entity from db.
            var result3 = _fixture._albumManager.Find(result1.Id);
            Assert.NotNull(result3);
            Assert.Equal(model.Name, result3.Name);
            Assert.Equal(model.IsSales, result3.IsSales);
            Assert.Equal(model.Price, result3.Price);
            Assert.Equal(model.Year, result3.Year);

            _output.WriteLine("Entity(find-updated) : " + result3.ToJson());

            // Delete the entity.
            _fixture._albumManager.Delete(result1.Id);
        }

        [Fact]
        public async Task UpdatePropertiesAsync_Model_UpdatesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            // Create new sample entity.
            var result1 = await _fixture._albumManager.CreateAsync(album);

            Assert.NotNull(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);

            _output.WriteLine("New id : " + result1.Id);
            _output.WriteLine("Entity : " + result1.ToJson());

            // Update model is created.
            AlbumEdit model = new AlbumEdit
            {
                Name = album.Name + "_updated",
                IsSales = !album.IsSales,
                Price = album.Price + 10,
                Year = album.Year + 1
            };

            _output.WriteLine("Entity(changed) : " + result1.ToJson());

            // The entity updated with model.
            var result2 = await _fixture._albumManager.UpdatePropertiesAsync(result1.Id, model);
            Assert.Equal(1, result2);

            // Find the updated entity from db.
            var result3 = await _fixture._albumManager.FindAsync(result1.Id);
            Assert.NotNull(result3);
            Assert.Equal(model.Name, result3.Name);
            Assert.Equal(model.IsSales, result3.IsSales);
            Assert.Equal(model.Price, result3.Price);
            Assert.Equal(model.Year, result3.Year);

            _output.WriteLine("Entity(find-updated) : " + result3.ToJson());

            // Delete the entity.
            await _fixture._albumManager.DeleteAsync(result1.Id);
        }

        [Fact]
        public async Task Delete_EntityId_DeletesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._albumManager.Create(album);
            _output.WriteLine("New id : " + result1.Id);

            _fixture._albumManager.Delete(result1.Id);

            var result2 = _fixture._albumManager.Find(result1.Id);

            Assert.Null(result2);
        }

        [Fact]
        public async Task DeleteAsync_EntityId_DeletesEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._albumManager.CreateAsync(album);
            _output.WriteLine("New id : " + result1.Id);

            await _fixture._albumManager.DeleteAsync(result1.Id);

            var result2 = await _fixture._albumManager.FindAsync(result1.Id);

            Assert.Null(result2);
        }

        [Fact]
        public async Task Find_EntityId_FindsEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = _fixture._albumManager.Create(album);
            _output.WriteLine("New id : " + result1.Id);

            var result2 = _fixture._albumManager.Find(result1.Id);
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<Album>(result2);
            Assert.NotEqual(ObjectId.Empty, result2.Id);

            var result3 = _fixture._albumManager.Find<AlbumQuery>(result1.Id);
            Assert.NotNull(result3);
            Assert.IsAssignableFrom<AlbumQuery>(result3);
            Assert.NotNull(result3.Id);
            Assert.NotEqual(string.Empty, result3.Id);

            _fixture._albumManager.Delete(result1.Id);
        }

        [Fact]
        public async Task FindAsync_EntityId_FindsEntity()
        {
            Album album = new Album
            {
                Name = "test",
                IsSales = true,
                Price = 50,
                Year = DateTime.Now.Year
            };

            var result1 = await _fixture._albumManager.CreateAsync(album);
            _output.WriteLine("New id : " + result1.Id);

            var result2 = await _fixture._albumManager.FindAsync(result1.Id);
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<Album>(result2);
            Assert.NotEqual(ObjectId.Empty, result2.Id);

            var result3 = await _fixture._albumManager.FindAsync<AlbumQuery>(result1.Id);
            Assert.NotNull(result3);
            Assert.IsAssignableFrom<AlbumQuery>(result3);
            Assert.NotNull(result3.Id);
            Assert.NotEqual(string.Empty, result3.Id);

            await _fixture._albumManager.DeleteAsync(result1.Id);
        }

        [Fact]
        public async Task Find_Filter_FindsEntity()
        {
            var result1 = _fixture._albumManager.Find(x => x.Year == 1983);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<Album>(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);
            Assert.Equal(1983, result1.Year);

            var result2 = _fixture._albumManager.Find(x => x.Name == "Summers CIC");
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<Album>(result2);
            Assert.NotEqual(ObjectId.Empty, result2.Id);
            Assert.Equal("Summers CIC", result2.Name);

            var result3 = _fixture._albumManager.Find(x => x.Price == 316.24m);
            Assert.NotNull(result3);
            Assert.IsAssignableFrom<Album>(result3);
            Assert.NotEqual(ObjectId.Empty, result3.Id);
            Assert.Equal(316.24m, result3.Price);


            var result4 = _fixture._albumManager.Find<AlbumQuery>(x => x.Year == 1983);
            Assert.NotNull(result4);
            Assert.IsAssignableFrom<AlbumQuery>(result4);
            Assert.NotNull(result4.Id);
            Assert.NotEqual(string.Empty, result4.Id);
            Assert.Equal(1983, result4.Year);

            var result5 = _fixture._albumManager.Find<AlbumQuery>(x => x.Name == "Summers CIC");
            Assert.NotNull(result5);
            Assert.IsAssignableFrom<AlbumQuery>(result5);
            Assert.NotNull(result5.Id);
            Assert.NotEqual(string.Empty, result5.Id);
            Assert.Equal("Summers CIC", result5.Name);

            var result6 = _fixture._albumManager.Find<AlbumQuery>(x => x.Price == 316.24m);
            Assert.NotNull(result6);
            Assert.IsAssignableFrom<AlbumQuery>(result6);
            Assert.NotNull(result6.Id);
            Assert.NotEqual(string.Empty, result6.Id);
            Assert.Equal(316.24m, result6.Price);
        }

        [Fact]
        public async Task FindAsync_Filter_FindsEntity()
        {
            var result1 = await _fixture._albumManager.FindAsync(x => x.Year == 1983);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<Album>(result1);
            Assert.NotEqual(ObjectId.Empty, result1.Id);
            Assert.Equal(1983, result1.Year);

            var result2 = await _fixture._albumManager.FindAsync(x => x.Name == "Summers CIC");
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<Album>(result2);
            Assert.NotEqual(ObjectId.Empty, result2.Id);
            Assert.Equal("Summers CIC", result2.Name);

            var result3 = await _fixture._albumManager.FindAsync(x => x.Price == 316.24m);
            Assert.NotNull(result3);
            Assert.IsAssignableFrom<Album>(result3);
            Assert.NotEqual(ObjectId.Empty, result3.Id);
            Assert.Equal(316.24m, result3.Price);


            var result4 = await _fixture._albumManager.FindAsync<AlbumQuery>(x => x.Year == 1983);
            Assert.NotNull(result4);
            Assert.IsAssignableFrom<AlbumQuery>(result4);
            Assert.NotNull(result4.Id);
            Assert.NotEqual(string.Empty, result4.Id);
            Assert.Equal(1983, result4.Year);

            var result5 = await _fixture._albumManager.FindAsync<AlbumQuery>(x => x.Name == "Summers CIC");
            Assert.NotNull(result5);
            Assert.IsAssignableFrom<AlbumQuery>(result5);
            Assert.NotNull(result5.Id);
            Assert.NotEqual(string.Empty, result5.Id);
            Assert.Equal("Summers CIC", result5.Name);

            var result6 = await _fixture._albumManager.FindAsync<AlbumQuery>(x => x.Price == 316.24m);
            Assert.NotNull(result6);
            Assert.IsAssignableFrom<AlbumQuery>(result6);
            Assert.NotNull(result6.Id);
            Assert.NotEqual(string.Empty, result6.Id);
            Assert.Equal(316.24m, result6.Price);
        }

        [Fact]
        public async Task FindAll_Filter_FindsEntity()
        {
            var result1 = _fixture._albumManager.FindAll(x =>
                x.Year > 1980 && x.Year < 1984);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<IEnumerable<Album>>(result1);
            Assert.Equal(2, result1?.Count());

            var result2 = _fixture._albumManager.FindAll(x => x.Name.StartsWith("Thomp"));
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<IEnumerable<Album>>(result2);
            Assert.Equal(1, result2?.Count());

            var result3 = _fixture._albumManager.FindAll(x => x.IsSales && x.Price > 80 && x.Price < 400);
            Assert.NotNull(result3);
            Assert.IsAssignableFrom<IEnumerable<Album>>(result3);
            Assert.Equal(2, result3?.Count());


            var result4 = _fixture._albumManager.FindAll<AlbumQuery>(x =>
                x.Year > 1980 && x.Year < 1984);
            Assert.NotNull(result4);
            Assert.IsAssignableFrom<IEnumerable<AlbumQuery>>(result4);
            Assert.Equal(2, result4?.Count());

            var result5 = _fixture._albumManager.FindAll<AlbumQuery>(x => x.Name.StartsWith("Thomp"));
            Assert.NotNull(result5);
            Assert.IsAssignableFrom<IEnumerable<AlbumQuery>>(result5);
            Assert.Equal(1, result5?.Count());

            var result6 = _fixture._albumManager.FindAll<AlbumQuery>(x => x.IsSales && x.Price > 80 && x.Price < 400);
            Assert.NotNull(result6);
            Assert.IsAssignableFrom<IEnumerable<AlbumQuery>>(result6);
            Assert.Equal(2, result6?.Count());
        }

        [Fact]
        public async Task FindAllAsync_Filter_FindsEntity()
        {
            var result1 = await _fixture._albumManager.FindAllAsync(x =>
                x.Year > 1980 && x.Year < 1984);
            Assert.NotNull(result1);
            Assert.IsAssignableFrom<IEnumerable<Album>>(result1);
            Assert.Equal(2, result1?.Count());

            var result2 = await _fixture._albumManager.FindAllAsync(x => x.Name.StartsWith("Thomp"));
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<IEnumerable<Album>>(result2);
            Assert.Equal(1, result2?.Count());

            var result3 = await _fixture._albumManager.FindAllAsync(x => x.IsSales && x.Price > 80 && x.Price < 400);
            Assert.NotNull(result3);
            Assert.IsAssignableFrom<IEnumerable<Album>>(result3);
            Assert.Equal(2, result3?.Count());


            var result4 = await _fixture._albumManager.FindAllAsync<AlbumQuery>(x =>
                x.Year > 1980 && x.Year < 1984);
            Assert.NotNull(result4);
            Assert.IsAssignableFrom<IEnumerable<AlbumQuery>>(result4);
            Assert.Equal(2, result4?.Count());

            var result5 = await _fixture._albumManager.FindAllAsync<AlbumQuery>(x => x.Name.StartsWith("Thomp"));
            Assert.NotNull(result5);
            Assert.IsAssignableFrom<IEnumerable<AlbumQuery>>(result5);
            Assert.Equal(1, result5?.Count());

            var result6 = await _fixture._albumManager.FindAllAsync<AlbumQuery>(x => x.IsSales && x.Price > 80 && x.Price < 400);
            Assert.NotNull(result6);
            Assert.IsAssignableFrom<IEnumerable<AlbumQuery>>(result6);
            Assert.Equal(2, result6?.Count());
        }
    }
}
