namespace CorpriTech.SpeedTrap;

/// <summary>
/// Allows for creating and managing traces.
/// </summary>
public interface ITraceProvider
{
    /// <summary>
    /// All traces created by and stored within the provider.
    /// </summary>
    IEnumerable<ITrace> Traces { get; }
    
    /// <summary>
    /// Creates and starts a new trace with the provided name.
    /// </summary>
    /// <param name="name">The name of the trace.</param>
    /// <returns>The newly created trace.</returns>
    ITrace StartTrace(string name);
}