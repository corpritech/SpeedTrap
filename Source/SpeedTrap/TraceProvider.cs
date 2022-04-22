namespace CorpriTech.SpeedTrap;

/// <inheritdoc cref="ITraceProvider"/>
public class TraceProvider : ITraceProvider
{
    /// <inheritdoc cref="ITraceProvider.Traces"/>
    public IEnumerable<ITrace> Traces => _traces;

    private readonly List<Trace> _traces = new();

    /// <inheritdoc cref="ITraceProvider.StartTrace"/>
    public ITrace StartTrace(string name)
    {
        var trace = new Trace(name);

        _traces.Add(trace);

        return trace;
    }
}