using System.Diagnostics;

namespace MFramework.Services.CLI
{
    public static class MyTool
    {
        public static void DotnetCommand(string command)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = command,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            })?.WaitForExit();
        }

        public static void AddPackageCommand(string packageName, string version = "")
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"add package {packageName} {(string.IsNullOrEmpty(version) ? "" : (" --version " + version))}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            })?.WaitForExit();
        }
    }
}
