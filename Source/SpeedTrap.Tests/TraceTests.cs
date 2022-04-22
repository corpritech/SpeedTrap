using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CorpriTech.SpeedTrap;
using Xunit;

namespace SpeedTrap.Tests;

public class TraceTests
{
    private const string TraceName = "SpeedTrap";
    
    [Fact]
    public void TraceIdIsNotEmpty()
    {
        var provider = new TraceProvider();
        using var trace = provider.StartTrace(TraceName);
        
        Assert.NotEqual(Guid.Empty, trace.Id);
    }

    [Fact]
    public void TraceStartedOnIsAccurate()
    {
        var provider = new TraceProvider();
        var currentDateTime = DateTimeOffset.Now;
        using var trace = provider.StartTrace(TraceName);
        
        Assert.Equal(currentDateTime.Date, trace.StartedOn.Date);
        Assert.Equal(currentDateTime.Hour, trace.StartedOn.Hour);
        Assert.Equal(currentDateTime.Minute, trace.StartedOn.Minute);
        Assert.Equal(currentDateTime.Second, trace.StartedOn.Second);
        Assert.Equal(currentDateTime.Millisecond, trace.StartedOn.Millisecond);
    }

    [Fact]
    public void TraceDurationIsAccurate()
    {
        const int secondsToWait = 1;
        var provider = new TraceProvider();
        ITrace trace;
        
        using (trace = provider.StartTrace(TraceName))
        {
            Thread.Sleep(TimeSpan.FromSeconds(secondsToWait));
        }

        Assert.Equal(1, trace.Duration.Seconds);
    }

    [Fact]
    public async Task TraceIsActiveIsAccurate()
    {
        var provider = new TraceProvider();
        ITrace trace;
        
        await using (trace = provider.StartTrace(TraceName))
        {
            Assert.True(trace.IsActive);
        }
        
        Assert.False(trace.IsActive);
    }
    
    [Fact]
    public void TraceWithActiveScopesRemainsActive()
    {
        var provider = new TraceProvider();
        var trace = provider.StartTrace(TraceName);
        var scope = trace.StartScope($"{TraceName}Scope");
        
        trace.Dispose();
        
        Assert.True(scope.IsActive);
        Assert.True(trace.IsActive);
        
        scope.Dispose();

        Assert.False(trace.IsActive);
        Assert.False(scope.IsActive);
    }

    [Fact]
    public void TraceCanCreateScopes()
    {
        var provider = new TraceProvider();
        using var trace = provider.StartTrace(TraceName);

        using var scope = trace.StartScope($"{TraceName}Scope");
        
        Assert.NotEmpty(trace.Scopes);
        Assert.NotNull(trace.Scopes.FirstOrDefault(x => x.Name == $"{TraceName}Scope"));
        Assert.Single(trace.Scopes);
    }

    [Fact]
    public void TraceThrowsWhenCreatingScopesAfterDisposal()
    {
        var provider = new TraceProvider(); 
        var trace = provider.StartTrace(TraceName);
        
        trace.Dispose();
        
        Assert.Throws<InvalidOperationException>(() => trace.StartScope($"{TraceName}Scope"));
    }
}