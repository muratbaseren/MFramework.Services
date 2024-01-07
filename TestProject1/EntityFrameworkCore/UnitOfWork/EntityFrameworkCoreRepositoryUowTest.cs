using TestProject1.EntityFrameworkCore.UnitOfWork;
using Xunit.Abstractions;

namespace TestProject1.EntityFrameworkCore
{
    [Collection("EntityFrameworkCoreCollection")]
    public class EntityFrameworkCoreRepositoryUowTest : IClassFixture<EntityFrameworkCoreUowTestFixture>
    {
        private readonly EntityFrameworkCoreUowTestFixture _fixture;
        private readonly ITestOutputHelper _output;

        public EntityFrameworkCoreRepositoryUowTest(EntityFrameworkCoreUowTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        //[Fact]
        //public async Task Add_ReturnsEntity()
        //{
        //    Book entity = new Book
        //    {
        //        Name = "test",
        //        IsSales = true,
        //        Price = 50,
        //        Year = DateTime.Now.Year
        //    };

        //    var result1 = _fixture._uow.BookRepository.Add(entity);
        //    var result2 = _fixture._uow.BookRepository.Save();
        //    Assert.Equal(1, result2);
        //    Assert.NotNull(result1);
        //    Assert.NotEqual(0, result1.Id);

        //    _fixture._uow.BookRepository.Remove(result1);
        //    _fixture._uow.BookRepository.Save();
        //}

        //[Fact]
        //public async Task AddAsync_ReturnsEntity()
        //{
        //    Book entity = new Book
        //    {
        //        Name = "test",
        //        IsSales = true,
        //        Price = 50,
        //        Year = DateTime.Now.Year
        //    };

        //    var result1 = await _fixture._uow.BookRepository.AddAsync(entity);
        //    var result2 = await _fixture._uow.BookRepository.SaveAsync();
        //    Assert.Equal(1, result2);
        //    Assert.NotNull(result1);
        //    Assert.NotEqual(0, result1.Id);

        //    await _fixture._uow.BookRepository.RemoveAsync(result1);
        //    await _fixture._uow.BookRepository.SaveAsync();
        //}

        //[Fact]
        //public async Task Find_EntityId_ReturnsEntity()
        //{
        //    Book entity = new Book
        //    {
        //        Name = "test",
        //        IsSales = true,
        //        Price = 50,
        //        Year = DateTime.Now.Year
        //    };

        //    var result1 = _fixture._uow.BookRepository.Add(entity);
        //    var result2 = _fixture._uow.BookRepository.Save();

        //    var result3 = _fixture._uow.BookRepository.Find(entity.Id);
        //    Assert.NotNull(result3);
        //    Assert.Equal(result1.Id, result3.Id);

        //    _fixture._uow.BookRepository.Remove(result1);
        //    _fixture._uow.BookRepository.Save();
        //}

        //[Fact]
        //public async Task FindAsync_EntityId_ReturnsEntity()
        //{
        //    Book entity = new Book
        //    {
        //        Name = "test",
        //        IsSales = true,
        //        Price = 50,
        //        Year = DateTime.Now.Year
        //    };

        //    var result1 = await _fixture._uow.BookRepository.AddAsync(entity);
        //    var result2 = await _fixture._uow.BookRepository.SaveAsync();

        //    var result3 = await _fixture._uow.BookRepository.FindAsync(entity.Id);
        //    Assert.NotNull(result3);
        //    Assert.Equal(result1.Id, result3.Id);

        //    await _fixture._uow.BookRepository.RemoveAsync(result1);
        //    await _fixture._uow.BookRepository.SaveAsync();
        //}

        //[Fact]
        //public async Task FirstOrDefaultAsync_Filter_ReturnsEntity()
        //{
        //    var result1 = await _fixture._uow.BookRepository.FirstOrDefaultAsync(x => x.Year == 2013);
        //    Assert.NotNull(result1);
        //}

        //[Fact]
        //public async Task ListAsync_ReturnsEntities()
        //{
        //    var result1 = await _fixture._uow.BookRepository.ListAsync();
        //    Assert.NotNull(result1);
        //    Assert.NotEqual(0, result1.Count);
        //    Assert.IsAssignableFrom<IList<Book>>(result1);
        //}

        //[Fact]
        //public async Task Remove_EntityId_RemovesEntity()
        //{
        //    Book entity = new Book
        //    {
        //        Name = "test",
        //        IsSales = true,
        //        Price = 50,
        //        Year = DateTime.Now.Year
        //    };

        //    var result1 = _fixture._uow.BookRepository.Add(entity);
        //    var result2 = _fixture._uow.BookRepository.Save();

        //    _fixture._uow.BookRepository.Remove(result1.Id);
        //    var result3 = _fixture._uow.BookRepository.Save();

        //    Assert.Equal(1, result3);
        //}

        //[Fact]
        //public async Task RemoveAsync_EntityId_RemovesEntity()
        //{
        //    Book entity = new Book
        //    {
        //        Name = "test",
        //        IsSales = true,
        //        Price = 50,
        //        Year = DateTime.Now.Year
        //    };

        //    var result1 = await _fixture._uow.BookRepository.AddAsync(entity);
        //    var result2 = await _fixture._uow.BookRepository.SaveAsync();

        //    await _fixture._uow.BookRepository.RemoveAsync(result1.Id);
        //    var result3 = await _fixture._uow.BookRepository.SaveAsync();

        //    Assert.Equal(1, result3);
        //}

        //[Fact]
        //public async Task Remove_Entity_RemovesEntity()
        //{
        //    Book entity = new Book
        //    {
        //        Name = "test",
        //        IsSales = true,
        //        Price = 50,
        //        Year = DateTime.Now.Year
        //    };

        //    var result1 = _fixture._uow.BookRepository.Add(entity);
        //    var result2 = _fixture._uow.BookRepository.Save();

        //    _fixture._uow.BookRepository.Remove(result1);
        //    var result3 = _fixture._uow.BookRepository.Save();

        //    Assert.Equal(1, result3);
        //}

        //[Fact]
        //public async Task RemoveAsync_Entity_RemovesEntity()
        //{
        //    Book entity = new Book
        //    {
        //        Name = "test",
        //        IsSales = true,
        //        Price = 50,
        //        Year = DateTime.Now.Year
        //    };

        //    var result1 = await _fixture._uow.BookRepository.AddAsync(entity);
        //    var result2 = await _fixture._uow.BookRepository.SaveAsync();

        //    await _fixture._uow.BookRepository.RemoveAsync(result1);
        //    var result3 = await _fixture._uow.BookRepository.SaveAsync();

        //    Assert.Equal(1, result3);
        //}

        //[Fact]
        //public async Task Update_UpdatesEntity()
        //{
        //    Book entity = new Book
        //    {
        //        Name = "test",
        //        IsSales = true,
        //        Price = 50,
        //        Year = DateTime.Now.Year
        //    };

        //    var result1 = _fixture._uow.BookRepository.Add(entity);
        //    var result2 = _fixture._uow.BookRepository.Save();

        //    entity.Name += "_updated";
        //    entity.IsSales = !entity.IsSales;
        //    entity.Price += 10;
        //    entity.Year += 1;

        //    _fixture._uow.BookRepository.Update(entity.Id, entity);
        //    var result3 = _fixture._uow.BookRepository.Save();
        //    Assert.Equal(1, result3);

        //    var result4 = _fixture._uow.BookRepository.Find(entity.Id);
        //    Assert.Equal(entity.Name, result4.Name);
        //    Assert.Equal(entity.IsSales, result4.IsSales);
        //    Assert.Equal(entity.Price, result4.Price);
        //    Assert.Equal(entity.Year, result4.Year);

        //    _fixture._uow.BookRepository.Remove(result4.Id);
        //    _fixture._uow.BookRepository.Save();
        //}

        //[Fact]
        //public async Task UpdateAsync_UpdatesEntity()
        //{
        //    Book entity = new Book
        //    {
        //        Name = "test",
        //        IsSales = true,
        //        Price = 50,
        //        Year = DateTime.Now.Year
        //    };

        //    var result1 =await _fixture._uow.BookRepository.AddAsync(entity);
        //    var result2 = await _fixture._uow.BookRepository.SaveAsync();

        //    entity.Name += "_updated";
        //    entity.IsSales = !entity.IsSales;
        //    entity.Price += 10;
        //    entity.Year += 1;

        //    await _fixture._uow.BookRepository.UpdateAsync(entity.Id, entity);
        //    var result3 = await _fixture._uow.BookRepository.SaveAsync();
        //    Assert.Equal(1, result3);

        //    var result4 = await _fixture._uow.BookRepository.FindAsync(entity.Id);
        //    Assert.Equal(entity.Name, result4.Name);
        //    Assert.Equal(entity.IsSales, result4.IsSales);
        //    Assert.Equal(entity.Price, result4.Price);
        //    Assert.Equal(entity.Year, result4.Year);

        //    await _fixture._uow.BookRepository.RemoveAsync(result4.Id);
        //    await _fixture._uow.BookRepository.SaveAsync();
        //}
    }
}