using System.Diagnostics;

namespace CorpriTech.SpeedTrap;

internal class Trace : ITrace
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public DateTimeOffset StartedOn { get; } = DateTimeOffset.Now;
    
    public TimeSpan Duration => _stopwatch.Elapsed;
    public IEnumerable<IScope> Scopes => _scopes;
    public bool IsActive => !_isDisposed || _scopes.Any(scope => scope.IsActive);

    private bool _isDisposed;
    
    private readonly List<Scope> _scopes = new();
    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

    internal Trace(string name)
    {
        Name = name;
    }
    
    public IScope StartScope(string name)
    {
        if (_isDisposed)
        {
            throw new InvalidOperationException("Cannot create scope. Trace has been disposed.");
        }
        
        var scope = new Scope(name);
        
        _scopes.Add(scope);

        return scope;
    }
    
    public void Dispose()
    {
        _stopwatch.Stop();
        _isDisposed = true;
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }
}