
using MFramework.Services.DataAccess.DatabaseInitializers;
using MFramework.Services.FakeData;
using System.Diagnostics;
using WebApplicationTest.DataAccess.Mongo.Repositories;
using WebApplicationTest.Entities;

namespace WebApplicationTest.DataAccess.Mongo.Context;

internal class MongoDatabaseInitializer : DefaultDatabaseInitializer
{
    private readonly IConfiguration configuration;
    private readonly IServiceProvider serviceProvider;

    public MongoDatabaseInitializer(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration)
    {
        this.configuration = configuration;
        this.serviceProvider = serviceProvider;
    }


    #region Migration

    public void Migrate(MongoDBContext context)
    {
        IMongoMigrationRepository mongoMigration = serviceProvider.GetRequiredService<IMongoMigrationRepository>();

        //Migrate(context, mongoMigration, Migrate_UnitOrderNoAdd);    // migrate Migrate_UnitOrderNoAdd
        //Migrate(context, mongoMigration, Migrate_SubjectOrderNoAdd);   // migrate Migrate_SubjectOrderNoAdd
    }

    private void Migrate(MongoDBContext context, IMongoMigrationRepository mongoMigration, Action<MongoDBContext> migrationMethod)
    {
        if (mongoMigration.Queryable().Any(x => x.Name == migrationMethod.Method.Name) == false)
        {
            migrationMethod(context);
            mongoMigration.Insert(new Migration { Date = DateTime.Now, Name = migrationMethod.Method.Name });

            Debug.WriteLine($"Migration : {migrationMethod.Method.Name} applied.", "Mongo Migration");
        }
        else
        {
            Debug.WriteLine($"Migration : {migrationMethod.Method.Name} already applied.", "Mongo Migration");
        }
    }

    /// <summary>
    /// Sample migration. Migrates the album summary field that will add.
    /// </summary>
    /// <param name="context">The context.</param>
    private void Migrate_AlbumSummaryAdded(MongoDBContext context)
    {
        //todo : Summary prop Album tipine eklenir.

        //IMongoAlbumRepository albumRepository = serviceProvider.GetRequiredService<IMongoAlbumRepository>();
        //var list = albumRepository.List();

        //list.ForEach(x =>
        //{
        //    x.Summary = TextData.GetSentence();
        //    albumRepository.Update(x.Id, x);
        //});
    }

    /// <summary>
    /// Migrates the album summary field that will remove.
    /// </summary>
    /// <param name="context">The context.</param>
    private void Migrate_AlbumDescriptionRemove(MongoDBContext context)
    {
        //todo : Description prop Album tipinden kaldırılır. (Seed data ya da önceki mig lerde bu prop ile ilgili kodlar comment yapılır)

        //IMongoAlbumRepository albumRepository = serviceProvider.GetRequiredService<IMongoAlbumRepository>();
        //var list = albumRepository.List();

        //list.ForEach(x =>
        //{
        //    albumRepository.Update(x.Id, x);
        //});
    }

    #endregion

    #region Seed Data

    public void Seed(MongoDBContext context, bool useSeedData)
    {
        if (useSeedData)
        {
            //var albumRepo = serviceProvider.GetRequiredService<IMongoAlbumRepository>();
            //if (albumRepo.Queryable().Any()) return;

            //albumRepo.Insert(new Album
            //{
            //    Name = NameData.GetBankName(),
            //    Description = TextData.GetSentence()
            //});
        }
    }

    public void Setup(MongoDBContext context)
    {
        //AppSettings appSettings = configuration.GetSection(AppSettings.SectionName).Get<AppSettings>();
    }

    #endregion
}