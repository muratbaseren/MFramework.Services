
using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using WebApplicationTest.DataAccess.Mongo.Context;
using WebApplicationTest.DataAccess.Mongo.Repositories;

namespace WebApplicationTest.DataAccess.Middlewares;

public static class DataAccessStartupMiddleware
{
    public static IServiceCollection SetDependencyDataAccessMongo(this IServiceCollection services)
    {
        BsonClassMap.RegisterClassMap<EntityBase<ObjectId>>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.SetIgnoreExtraElementsIsInherited(true);
        });

        services.AddScoped<IMongoMigrationRepository, MongoMigrationRepository>();

        services.AddScoped<MongoDBContext>();

        return services;
    }

    public static IApplicationBuilder UseMongoMigrate(this IApplicationBuilder app)
    {
        IConfiguration configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;

        var mongoContext = serviceProvider.GetRequiredService<MongoDBContext>();
        MongoDatabaseInitializer mongoInitializer = new MongoDatabaseInitializer(configuration, serviceProvider);

        mongoInitializer.Migrate(mongoContext);

        return app;
    }

    public static IApplicationBuilder UseMongoSetup(this IApplicationBuilder app)
    {
        IConfiguration configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;

        var mongoContext = serviceProvider.GetRequiredService<MongoDBContext>();
        MongoDatabaseInitializer mongoInitializer = new MongoDatabaseInitializer(configuration, serviceProvider);

        // DB setup verileri için.
        mongoInitializer.Setup(mongoContext);

        return app;
    }

    public static IApplicationBuilder UseMongoSeed(this IApplicationBuilder app, bool useSeedData = false)
    {
        IConfiguration configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;

        var mongoContext = serviceProvider.GetRequiredService<MongoDBContext>();
        MongoDatabaseInitializer mongoInitializer = new MongoDatabaseInitializer(configuration, serviceProvider);

        mongoInitializer.Seed(mongoContext, useSeedData);

        return app;
    }
}