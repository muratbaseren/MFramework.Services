using MongoDB.Bson;
using MongoDB.Driver;
using TestProject1.EntityFramework;
using TestProject1.MongoTests.MongoObjects;
using Xunit.Abstractions;

namespace TestProject1.MongoTests
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

        //[Fact]
        //public async Task Any_ReturnsTrue()
        //{
        //    var result1 = _fixture._songRepository.Any();
        //    Assert.True(result1);

        //    var result2 = await _fixture._songRepository.AnyAsync();
        //    Assert.True(result2);
        //}

        //[Fact]
        //public async Task Any_Filter_ReturnsTrue()
        //{
        //    var result1 = _fixture._songRepository.Any(x => x.IsSales == true);
        //    Assert.True(result1);

        //    var result2 = await _fixture._songRepository.AnyAsync(x => x.IsSales == true);
        //    Assert.True(result2);
        //}

        //[Fact]
        //public async Task Count_ReturnsCount()
        //{
        //    var result1 = _fixture._songRepository.Count();
        //    Assert.Equal(5, result1);

        //    var result2 = await _fixture._songRepository.CountAsync();
        //    Assert.Equal(5, result2);
        //}

        //[Fact]
        //public async Task Count_Filter_ReturnsCount()
        //{
        //    var result1 = _fixture._songRepository.Count(x => x.IsSales == true);
        //    Assert.Equal(3, result1);

        //    var result2 = await _fixture._songRepository.CountAsync(x => x.IsSales == true);
        //    Assert.Equal(3, result2);
        //}
    }
}