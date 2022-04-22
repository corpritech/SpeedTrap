using CorpriTech.SpeedTrap;

namespace Microsoft.AspNetCore.Http;

/// <summary>
/// SpeedTrap extensions for <see cref="HttpContext"/>.
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// Gets the trace for the current request.
    /// </summary>
    /// <param name="httpContext">The http context to use when retrieving the trace.</param>
    /// <returns>The trace for the current request.</returns>
    public static ITrace? GetTrace(this HttpContext httpContext)
        => RequestTraceAccessor.Trace.Value;
}