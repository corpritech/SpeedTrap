using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CorpriTech.SpeedTrap;
using Xunit;

namespace SpeedTrap.Tests;

public class ScopeTests
{
    private const string TraceName = "SpeedTrap";
    private const string ScopeName = "Scope1";

    [Fact]
    public void ScopeStartedOnIsAccurate()
    {
        var provider = new TraceProvider();
        var currentDateTime = DateTimeOffset.Now;
        using var trace = provider.StartTrace(TraceName);
        using var scope = trace.StartScope(ScopeName);
        
        Assert.Equal(currentDateTime.Date, scope.StartedOn.Date);
        Assert.Equal(currentDateTime.Hour, scope.StartedOn.Hour);
        Assert.Equal(currentDateTime.Minute, scope.StartedOn.Minute);
        Assert.Equal(currentDateTime.Second, scope.StartedOn.Second);
        Assert.Equal(currentDateTime.Millisecond, scope.StartedOn.Millisecond);
    }

    [Fact]
    public void ScopeDurationIsAccurate()
    {
        const int secondsToWait = 1;
        var provider = new TraceProvider();
        using var trace = provider.StartTrace(TraceName);
        IScope scope;
        
        using (scope = trace.StartScope(ScopeName))
        {
            Thread.Sleep(TimeSpan.FromSeconds(secondsToWait));
        }

        Assert.Equal(1, scope.Duration.Seconds);
    }

    [Fact]
    public async Task ScopeIsActiveIsAccurate()
    {
        var provider = new TraceProvider();
        await using var trace = provider.StartTrace(TraceName);
        IScope scope;
        
        await using (scope = trace.StartScope(ScopeName))
        {
            Assert.True(scope.IsActive);
        }

        Assert.False(scope.IsActive);
    }
    
    [Fact]
    public void ScopeWithActiveScopesRemainsActive()
    {
        var provider = new TraceProvider();
        var trace = provider.StartTrace(TraceName);
        var scope = trace.StartScope(ScopeName);
        var innerScope = scope.StartScope($"Inner{ScopeName}");
        
        trace.Dispose();
        scope.Dispose();
        
        Assert.True(scope.IsActive);
        Assert.True(innerScope.IsActive);
        
        innerScope.Dispose();

        Assert.False(scope.IsActive);
        Assert.False(innerScope.IsActive);
    }

    [Fact]
    public void ScopeCanCreateScopes()
    {
        var provider = new TraceProvider();
        using var trace = provider.StartTrace(TraceName);
        using var scope = trace.StartScope(ScopeName);
        using var innerScope = scope.StartScope($"Inner{ScopeName}");
        
        Assert.NotEmpty(scope.Scopes);
        Assert.NotNull(scope.Scopes.FirstOrDefault(x => x.Name == $"Inner{ScopeName}"));
        Assert.Single(scope.Scopes);
    }
    
    [Fact]
    public void ScopeThrowsWhenCreatingScopesAfterDisposal()
    {
        var provider = new TraceProvider(); 
        using var trace = provider.StartTrace(TraceName);
        var scope = trace.StartScope(ScopeName);
        
        scope.Dispose();
        
        Assert.Throws<InvalidOperationException>(() => scope.StartScope($"Inner{ScopeName}"));
    }
}