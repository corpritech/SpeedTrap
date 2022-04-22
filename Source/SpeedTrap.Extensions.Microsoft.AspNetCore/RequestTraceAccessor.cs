using CorpriTech.SpeedTrap;

namespace Microsoft.AspNetCore;

internal static class RequestTraceAccessor
{
    internal static AsyncLocal<ITrace> Trace { get; set; } = new();
}