using System.Reflection;

namespace PoC.Blazor.SSR.Shared.Versioning;

public record AppVersion
{
    public string Version { get; }

    public AppVersion(string? version)
    {
        Version = version ?? "Unknown";
    }

    public AppVersion() : this("Unknown") { }

    public static AppVersion FromEntryAssembly() =>
        new(Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version);
}