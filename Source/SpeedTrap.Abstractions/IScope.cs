namespace CorpriTech.SpeedTrap;

/// <summary>
/// Represents the time elapsed performing or processing a specific unit of work within a <see cref="ITrace"/>. A scope contain nested scopes to identify the time spent on
/// individual units of work.
/// </summary>
/// <remarks>
/// <see cref="IScope"/> implements <see cref="IDisposable"/> and <see cref="IAsyncDisposable"/>. Upon being disposed, the scope will automatically end.
/// </remarks>
public interface IScope : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// The name of the scope.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Date/time when the scope was started.
    /// </summary>
    DateTimeOffset StartedOn { get; }

    /// <summary>
    /// The total time elapsed within the scope.
    /// </summary>
    TimeSpan Duration { get; }

    /// <summary>
    /// Nested scopes within the scope.
    /// </summary>
    IEnumerable<IScope> Scopes { get; }
    
    /// <summary>
    /// Whether or not the scope is active.
    /// </summary>
    bool IsActive { get; }
    
    /// <summary>
    /// Creates and starts a nested scope within the scope.
    /// </summary>
    /// <param name="name">The scope name.</param>
    /// <returns>The newly created scope.</returns>
    IScope StartScope(string name);
}