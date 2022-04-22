using System.Linq;
using CorpriTech.SpeedTrap;
using Xunit;

namespace SpeedTrap.Tests;

public class TraceProviderTests
{
    private const string TraceName = "SpeedTrap";

    [Fact]
    public void TraceProviderProvidesTrace()
    {
        var provider = new TraceProvider();
        using var trace = provider.StartTrace(TraceName);
        
        Assert.NotNull(trace);
        Assert.Equal(TraceName, trace.Name);
        Assert.NotEmpty(provider.Traces);
        Assert.NotNull(provider.Traces.FirstOrDefault(x => x.Name == TraceName));
    }
}