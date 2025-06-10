using Spectre.Console.Cli;

namespace MFramework.Services.CLI;

internal class Program
{
    static int Main(string[] args)
    {
        var app = new CommandApp();

        app.Configure(config =>
        {
            config.SetApplicationName("mfs");
            config.AddCommand<InitEntitiesCommand>("init-entities")
                .WithDescription("Initializes entities for the specified ORM type and namespace.")
                .WithExample(new[] { "init-entities", "-t", "mongo", "-s", "WebApplication1", "-n", "Album" });
            config.AddCommand<InitDataAccessCommand>("init-dataaccess")
                .WithDescription("Initializes data access layer for the specified ORM type and namespace.")
                .WithExample(new[] { "init-dataaccess", "-t", "mongo", "-s", "WebApplication1" });
        });

        return app.Run(args);
    }
}