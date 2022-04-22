using CorpriTech.SpeedTrap;

namespace Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
/// SpeedTrap extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds SpeedTrap to the service collection.
    /// </summary>
    /// <param name="serviceCollection">The service collection SpeedTrap should be added to.</param>
    /// <returns>The original <see cref="IServiceCollection"/> instance so that additional calls may be chained.</returns>
    public static IServiceCollection AddSpeedTrap(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITraceProvider, TraceProvider>();
        return serviceCollection;
    }
}