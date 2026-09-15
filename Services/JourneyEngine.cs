using RabbitHole.Models;

namespace RabbitHole.Services;

/// <summary>
/// Drives a single dive: picks a random entry point, then follows surprising
/// connections downward while remembering the whole trail.
/// </summary>
public sealed class JourneyEngine
{
    private readonly Dictionary<string, Topic> _byId;
    private readonly List<Topic> _all;
    private readonly Random _rng = new();
    private readonly List<JourneyStep> _steps = new();
    private readonly HashSet<string> _visited = new(StringComparer.OrdinalIgnoreCase);
    private readonly Queue<string> _recent = new();

    public JourneyEngine(IEnumerable<Topic> topics)
    {
        _all = topics.ToList();
        _byId = _all.ToDictionary(t => t.Id, StringComparer.OrdinalIgnoreCase);
    }

    public Topic? Current { get; private set; }

    public bool Active { get; private set; }

    public IReadOnlyList<JourneyStep> Steps => _steps;

    public int Depth => _steps.Count;

    /// <summary>Begin a fresh journey from a random unseen topic.</summary>
    public Topic StartNew()
    {
        _steps.Clear();
        _visited.Clear();
        _recent.Clear();
        Active = true;
        var topic = Pick(t => !_visited.Contains(t.Id) && !_recent.Contains(t.Id));
        Push(topic);
        return topic;
    }

    /// <summary>Descend to a surprising connected topic (never one already visited on this dive).</summary>
    public Topic GoDeeper()
    {
        if (!Active || Current is null)
            throw new InvalidOperationException("No active journey.");

        var candidates = Current.Related
            .Select(id => _byId.TryGetValue(id, out var t) ? t : null)
            .Where(t => t is not null && !_visited.Contains(t!.Id) && !_recent.Contains(t.Id))
            .Cast<Topic>()
            .ToList();

        if (candidates.Count == 0)
            candidates = _all.Where(t => !_visited.Contains(t.Id) && !_recent.Contains(t.Id)).ToList();

        if (candidates.Count == 0)
        {
            // Every topic has been seen — open the map back up, but still never
            // repeat anything from the recent trail.
            _visited.Clear();
            candidates = _all.Where(t => !_recent.Contains(t.Id)).ToList();
        }

        if (candidates.Count == 0)
            candidates = _all; // Degenerate case: tiny graph. The hole must go on.

        var next = candidates[_rng.Next(candidates.Count)];
        Push(next);
        return next;
    }

    public void Reset()
    {
        _steps.Clear();
        _visited.Clear();
        _recent.Clear();
        Active = false;
        Current = null;
    }

    private void Push(Topic topic)
    {
        var fact = topic.Facts[_rng.Next(topic.Facts.Length)];
        _steps.Add(new JourneyStep(_steps.Count + 1, topic, fact));
        _visited.Add(topic.Id);
        _recent.Enqueue(topic.Id);
        while (_recent.Count > 10)
            _recent.Dequeue();
        Current = topic;
    }

    private Topic Pick(Func<Topic, bool> predicate)
    {
        var pool = _all.Where(predicate).ToList();
        return pool.Count > 0 ? pool[_rng.Next(pool.Count)] : _all[_rng.Next(_all.Count)];
    }
}
