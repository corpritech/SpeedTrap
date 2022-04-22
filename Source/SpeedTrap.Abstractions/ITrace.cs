namespace CorpriTech.SpeedTrap;

/// <summary>
/// Represents the time elapsed while performing or processing an action. A trace contain nested scopes to identify the time spent on individual units of work.
/// </summary>
/// <remarks>
/// <see cref="ITrace"/> implements <see cref="IDisposable"/> and <see cref="IAsyncDisposable"/>. Upon being disposed, the trace will automatically end.
/// </remarks>
public interface ITrace : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// The ID of the trace.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// The name of the trace.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Date/time when the trace was started.
    /// </summary>
    DateTimeOffset StartedOn { get; }

    /// <summary>
    /// The total time elapsed within the trace.
    /// </summary>
    TimeSpan Duration { get; }

    /// <summary>
    /// Scopes created by and stored within the trace.
    /// </summary>
    IEnumerable<IScope> Scopes { get; }

    /// <summary>
    /// Whether or not the trace is active.
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// Creates and starts a scope for the trace.
    /// </summary>
    /// <param name="name">The scope name.</param>
    /// <returns>The newly created scope.</returns>
    IScope StartScope(string name);
}