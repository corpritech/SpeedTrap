using CorpriTech.SpeedTrap;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// SpeedTrap extensions for <see cref="IApplicationBuilder"/>.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds SpeedTrap middleware to the application pipeline.
    /// </summary>
    /// <remarks>
    /// A new trace will be started for every request. If a trace name factory is provided, it will be invoked per-request.
    /// </remarks>
    /// <param name="applicationBuilder">The application builder to add SpeedTrap middleware to.</param>
    /// <param name="traceNameFactory">An optional function to use when generating request trace names.</param>
    /// <returns>The original <see cref="IApplicationBuilder"/> instance so that additional calls may be chained.</returns>
    public static IApplicationBuilder UseSpeedTrap(this IApplicationBuilder applicationBuilder, Func<HttpContext, string>? traceNameFactory = null)
    {
        applicationBuilder.Use(async (ctx, next) =>
        {
            var traceName = traceNameFactory?.Invoke(ctx) ?? $"Request: {ctx.TraceIdentifier}";
            
            await using var trace = ctx.RequestServices.GetRequiredService<ITraceProvider>().StartTrace(traceName);
            
            RequestTraceAccessor.Trace.Value = trace;
            
            await next();
        });

        return applicationBuilder;
    }
}