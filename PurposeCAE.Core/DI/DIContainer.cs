using Microsoft.Extensions.DependencyInjection;
using PurposeCAE.Core.Serialization.JsonStringsEqualityCheckers;

namespace PurposeCAE.Core.DI;

/// <summary>
/// Provides a dependency injection container for internal use.
/// </summary>
internal sealed class DIContainer
{

    private readonly ServiceProvider _serviceProvider;

    // This members are used to implement the singleton pattern.
    // This singleton lifes as long as the application lives.
    #region Singleton
    // This lazy wrapper guarantees thread safety and that this singleton is will not be instantiated until the first call is made.
    private static readonly Lazy<DIContainer> _lazy = new(() => new DIContainer());

    public static DIContainer Instance { get { return _lazy.Value; } }

    private DIContainer()
    {
        ServiceCollection services = new();

        ConfigureServices(services);

        _serviceProvider = services.BuildServiceProvider();
    } 
    #endregion

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<IJsonStringsEqualityChecker, JsonStringsEqualityChecker>();
    }

    /// <summary>
    /// Gets a service of the specified type.
    /// </summary>
    /// <typeparam name="T">Interface type of Service to get.</typeparam>
    /// <returns></returns>
    public T GetService<T>() where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}