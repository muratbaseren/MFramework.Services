using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace MFramework.Services.CLI;

public class InitDataAccessCommand : Command<InitDataAccessCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [CommandOption("-t|--type")]
        [DefaultValue("mongo")]
        [Description("The type of the ORM.(mongo or ef)")]
        public string Type { get; set; } = string.Empty;

        [CommandOption("-s|--namespace")]
        [DefaultValue("WebApplication1")]
        [Description("The root namespace of the project.")]
        public string Namespace { get; set; } = string.Empty;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        settings.Type = settings.Type.Trim();
        settings.Namespace = settings.Namespace.Trim();

        if (string.IsNullOrEmpty(settings.Type) || string.IsNullOrEmpty(settings.Namespace))
        {
            AnsiConsole.MarkupLine("[red]All parameters (--type, --namespace) must be provided.[/]");
            return -1; // Return error code
        }

        // Logic to initialize entities based on the provided settings
        Console.WriteLine($"Initializing data access with type: {settings.Type} and namespace: {settings.Namespace}");

        string rootDirectory = Directory.GetCurrentDirectory();
        string filePath = string.Empty;

        string dataAccessDirectory = Path.Combine(rootDirectory, "DataAccess");
        Directory.CreateDirectory(dataAccessDirectory);
        AnsiConsole.MarkupLine($"[green]Directory created : {dataAccessDirectory}[/]");

        // Middlewares directory
        string middlewaresDirectory = Path.Combine(rootDirectory, "DataAccess", "Middlewares");
        Directory.CreateDirectory(middlewaresDirectory);
        AnsiConsole.MarkupLine($"[green]Directory created : {middlewaresDirectory}[/]");

        // DataAccessSettingsMiddleware.cs file to repositories directory
        filePath = Path.Combine(middlewaresDirectory, "DataAccessSettingsMiddleware.cs");
        if (File.Exists(filePath))
        {
            AnsiConsole.MarkupLine($"[yellow]File already exists. Skipping creation. {filePath}[/]");
        }
        else
        {
            File.WriteAllText(filePath,
$@"
using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using {settings.Namespace}.DataAccess.Mongo.Context;
using {settings.Namespace}.DataAccess.Mongo.Repositories;

namespace {settings.Namespace}.DataAccess.Middlewares;

public static class DataAccessStartupMiddleware
{{
    public static IServiceCollection SetDependencyDataAccessMongo(this IServiceCollection services)
    {{
        BsonClassMap.RegisterClassMap<EntityBase<ObjectId>>(cm =>
        {{
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.SetIgnoreExtraElementsIsInherited(true);
        }});

        services.AddScoped<IMongoMigrationRepository, MongoMigrationRepository>();

        services.AddScoped<MongoDBContext>();

        return services;
    }}

    public static IApplicationBuilder UseMongoMigrate(this IApplicationBuilder app)
    {{
        IConfiguration configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;

        var mongoContext = serviceProvider.GetRequiredService<MongoDBContext>();
        MongoDatabaseInitializer mongoInitializer = new MongoDatabaseInitializer(configuration, serviceProvider);

        mongoInitializer.Migrate(mongoContext);

        return app;
    }}

    public static IApplicationBuilder UseMongoSetup(this IApplicationBuilder app)
    {{
        IConfiguration configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;

        var mongoContext = serviceProvider.GetRequiredService<MongoDBContext>();
        MongoDatabaseInitializer mongoInitializer = new MongoDatabaseInitializer(configuration, serviceProvider);

        // DB setup verileri için.
        mongoInitializer.Setup(mongoContext);

        return app;
    }}

    public static IApplicationBuilder UseMongoSeed(this IApplicationBuilder app, bool useSeedData = false)
    {{
        IConfiguration configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
        IServiceProvider serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;

        var mongoContext = serviceProvider.GetRequiredService<MongoDBContext>();
        MongoDatabaseInitializer mongoInitializer = new MongoDatabaseInitializer(configuration, serviceProvider);

        mongoInitializer.Seed(mongoContext, useSeedData);

        return app;
    }}
}}");

            AnsiConsole.MarkupLine($"[green]File created : DataAccessSettingsMiddleware at {filePath}[/]");
        }




        // Mongo directory
        string mongoDirectory = Path.Combine(rootDirectory, "DataAccess", "Mongo");
        Directory.CreateDirectory(mongoDirectory);
        AnsiConsole.MarkupLine($"[green]Directory created : {mongoDirectory}[/]");






        // Context directory
        string contextDirectory = Path.Combine(rootDirectory, "DataAccess", "Mongo", "Context");
        Directory.CreateDirectory(contextDirectory);
        AnsiConsole.MarkupLine($"[green]Directory created : {contextDirectory}[/]");

        // MongoDBContext.cs file to repositories directory
        filePath = Path.Combine(contextDirectory, "MongoDbContext.cs");
        if (File.Exists(filePath))
        {
            AnsiConsole.MarkupLine($"[yellow]File already exists. Skipping creation. {filePath}[/]");
        }
        else
        {
            File.WriteAllText(filePath,
$@"
using MFramework.Services.DataAccess.Mongo.Context;

namespace {settings.Namespace}.DataAccess.Mongo.Context;

public class MongoDBContext : MongoDBContextBase
{{
    public MongoDBContext(IConfiguration configuration) : base(configuration, configuration.GetConnectionString(""MongoDefaultConnection""), configuration.GetConnectionString(""MongoDefaultConnection"").Split('/').Last()) {{ }}
}}");

            AnsiConsole.MarkupLine($"[green]File created : MongoDBContext at {filePath}[/]");
        }

        // MongoDatabaseInitializer.cs file to repositories directory
        filePath = Path.Combine(contextDirectory, "MongoDatabaseInitializer.cs");
        if (File.Exists(filePath))
        {
            AnsiConsole.MarkupLine($"[yellow]File already exists. Skipping creation. {filePath}[/]");
        }
        else
        {
            File.WriteAllText(filePath,
$@"
using MFramework.Services.DataAccess.DatabaseInitializers;
using MFramework.Services.FakeData;
using System.Diagnostics;
using {settings.Namespace}.DataAccess.Mongo.Repositories;
using {settings.Namespace}.Entities;

namespace {settings.Namespace}.DataAccess.Mongo.Context;

internal class MongoDatabaseInitializer : DefaultDatabaseInitializer
{{
    private readonly IConfiguration configuration;
    private readonly IServiceProvider serviceProvider;

    public MongoDatabaseInitializer(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration)
    {{
        this.configuration = configuration;
        this.serviceProvider = serviceProvider;
    }}


    #region Migration

    public void Migrate(MongoDBContext context)
    {{
        IMongoMigrationRepository mongoMigration = serviceProvider.GetRequiredService<IMongoMigrationRepository>();

        //Migrate(context, mongoMigration, Migrate_UnitOrderNoAdd);    // migrate Migrate_UnitOrderNoAdd
        //Migrate(context, mongoMigration, Migrate_SubjectOrderNoAdd);   // migrate Migrate_SubjectOrderNoAdd
    }}

    private void Migrate(MongoDBContext context, IMongoMigrationRepository mongoMigration, Action<MongoDBContext> migrationMethod)
    {{
        if (mongoMigration.Queryable().Any(x => x.Name == migrationMethod.Method.Name) == false)
        {{
            migrationMethod(context);
            mongoMigration.Insert(new Migration {{ Date = DateTime.Now, Name = migrationMethod.Method.Name }});

            Debug.WriteLine($""Migration : {{migrationMethod.Method.Name}} applied."", ""Mongo Migration"");
        }}
        else
        {{
            Debug.WriteLine($""Migration : {{migrationMethod.Method.Name}} already applied."", ""Mongo Migration"");
        }}
    }}

    /// <summary>
    /// Sample migration. Migrates the album summary field that will add.
    /// </summary>
    /// <param name=""context"">The context.</param>
    private void Migrate_AlbumSummaryAdded(MongoDBContext context)
    {{
        //todo : Summary prop Album tipine eklenir.

        //IMongoAlbumRepository albumRepository = serviceProvider.GetRequiredService<IMongoAlbumRepository>();
        //var list = albumRepository.List();

        //list.ForEach(x =>
        //{{
        //    x.Summary = TextData.GetSentence();
        //    albumRepository.Update(x.Id, x);
        //}});
    }}

    /// <summary>
    /// Migrates the album summary field that will remove.
    /// </summary>
    /// <param name=""context"">The context.</param>
    private void Migrate_AlbumDescriptionRemove(MongoDBContext context)
    {{
        //todo : Description prop Album tipinden kaldırılır. (Seed data ya da önceki mig lerde bu prop ile ilgili kodlar comment yapılır)

        //IMongoAlbumRepository albumRepository = serviceProvider.GetRequiredService<IMongoAlbumRepository>();
        //var list = albumRepository.List();

        //list.ForEach(x =>
        //{{
        //    albumRepository.Update(x.Id, x);
        //}});
    }}

    #endregion

    #region Seed Data

    public void Seed(MongoDBContext context, bool useSeedData)
    {{
        if (useSeedData)
        {{
            //var albumRepo = serviceProvider.GetRequiredService<IMongoAlbumRepository>();
            //if (albumRepo.Queryable().Any()) return;

            //albumRepo.Insert(new Album
            //{{
            //    Name = NameData.GetBankName(),
            //    Description = TextData.GetSentence()
            //}});
        }}
    }}

    public void Setup(MongoDBContext context)
    {{
        //AppSettings appSettings = configuration.GetSection(AppSettings.SectionName).Get<AppSettings>();
    }}

    #endregion
}}");

            AnsiConsole.MarkupLine($"[green]File created : MongoDatabaseInitializer at {filePath}[/]");
        }



        // Repositories directory
        string repositoriesDirectory = Path.Combine(rootDirectory, "DataAccess", "Mongo", "Repositories");
        Directory.CreateDirectory(repositoriesDirectory);
        AnsiConsole.MarkupLine($"[green]Directory created : {repositoriesDirectory}[/]");

        // MongoMigrationRepository.cs file to repositories directory
        filePath = Path.Combine(repositoriesDirectory, "MongoMigrationRepository.cs");
        if (File.Exists(filePath))
        {
            AnsiConsole.MarkupLine($"[yellow]File already exists. Skipping creation. {filePath}[/]");
        }
        else
        {
            File.WriteAllText(filePath,
$@"
using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;
using MongoDB.Bson;
using {settings.Namespace}.DataAccess.Mongo.Context;
using {settings.Namespace}.Entities;

namespace {settings.Namespace}.DataAccess.Mongo.Repositories;

public interface IMongoMigrationRepository : IMongoRepository<Migration, ObjectId> {{ }}

[Collection(""_migrationsHistory"")]
public class MongoMigrationRepository : MongoRepository<Migration, ObjectId>, IMongoMigrationRepository
{{
    public MongoMigrationRepository(MongoDBContext mongoDbContext) : base(mongoDbContext) {{ }}
}}");

            AnsiConsole.MarkupLine($"[green]File created : MongoMigrationRepository at {filePath}[/]");
        }




        // Entities directory
        string entitiesDirectory = Path.Combine(rootDirectory, "Entities");
        Directory.CreateDirectory(entitiesDirectory);
        AnsiConsole.MarkupLine($"[green]Directory created : {entitiesDirectory}[/]");

        // Migration.cs file to entities directory
        filePath = Path.Combine(entitiesDirectory, "Migration.cs");
        if (File.Exists(filePath))
        {
            AnsiConsole.MarkupLine($"[yellow]File already exists. Skipping creation. {filePath}[/]");
        }
        else
        {
            File.WriteAllText(filePath,
$@"
using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;

namespace {settings.Namespace}.Entities;

public class Migration : EntityBase<ObjectId>
{{
    public string Name {{ get; set; }}
    public DateTime Date {{ get; set; }}
}}");

            AnsiConsole.MarkupLine($"[green]File created : Migration at {filePath}[/]");
        }

        // install nuget package MFramework.Services.FakeData
        // dotnet add package MFramework.Services.FakeData
        MyTool.AddPackageCommand("MFramework.Services.FakeData");
        AnsiConsole.MarkupLine("[yellow]MFramework.Services.FakeData package installed.[/]");

        // install nuget package MFramework.Services.DataAccess
        // dotnet add package MFramework.Services.DataAccess
        MyTool.AddPackageCommand("MFramework.Services.DataAccess");
        AnsiConsole.MarkupLine("[yellow]MFramework.Services.DataAccess package installed.[/]");

        // install nuget package MFramework.Services.DataAccess.Mongo
        // dotnet add package MFramework.Services.DataAccess.Mongo
        MyTool.AddPackageCommand("MFramework.Services.DataAccess.Mongo");
        AnsiConsole.MarkupLine("[yellow]MFramework.Services.DataAccess.Mongo package installed.[/]");

        return 0; // Return success code
    }
}