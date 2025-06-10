using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace MFramework.Services.CLI;

public class InitEntitiesCommand : Command<InitEntitiesCommand.Settings>
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

        [CommandOption("-n|--name")]
        [DefaultValue("Album")]
        [Description("The name of the entity to be generated.")]
        public string Name { get; set; } = string.Empty;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        settings.Type = settings.Type.Trim();
        settings.Namespace = settings.Namespace.Trim();
        settings.Name = settings.Name.Trim();

        if (string.IsNullOrEmpty(settings.Type) || string.IsNullOrEmpty(settings.Namespace) || string.IsNullOrEmpty(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]All parameters (--type, --namespace, --name) must be provided.[/]");
            return -1; // Return error code
        }

        // Logic to initialize entities based on the provided settings
        Console.WriteLine($"Initializing entities of type {settings.Type} in namespace {settings.Namespace} with name {settings.Name}");

        string rootDirectory = Directory.GetCurrentDirectory();
        string entitiesDirectory = Path.Combine(rootDirectory, "Entities");
        Directory.CreateDirectory(entitiesDirectory);
        AnsiConsole.MarkupLine($"[green]Directory created : {entitiesDirectory}[/]");

        string filePath = Path.Combine(entitiesDirectory, $"{settings.Name}.cs");
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

public class {settings.Name} : EntityBase<ObjectId>
{{
	public string Name {{ get; set; }}
	public string Description {{ get; set; }}
}}");

            AnsiConsole.MarkupLine($"[green]File created : {settings.Name} at {filePath}[/]");
        }

        // install nuget package MFramework.Services.Entities
        // dotnet add package MFramework.Services.Entities
        MyTool.AddPackageCommand("MFramework.Services.Entities");
        AnsiConsole.MarkupLine("[yellow]MFramework.Services.Entities package installed.[/]");

        return 0; // Return success code
    }
}