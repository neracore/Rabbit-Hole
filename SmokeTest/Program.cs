using RabbitHole.Data;
using RabbitHole.Services;

// 1) Validate graph integrity
var ids = KnowledgeGraph.All.Select(t => t.Id).ToHashSet();
int problems = 0;
foreach (var t in KnowledgeGraph.All)
{
    if (t.Facts.Length == 0) { Console.WriteLine($"[FAIL] {t.Id}: no facts"); problems++; }
    if (t.Related.Length == 0) { Console.WriteLine($"[FAIL] {t.Id}: no related topics"); problems++; }
    foreach (var r in t.Related)
        if (!ids.Contains(r)) { Console.WriteLine($"[FAIL] {t.Id}: dangling link -> {r}"); problems++; }
    if (t.Related.Contains(t.Id)) { Console.WriteLine($"[FAIL] {t.Id}: links to itself"); problems++; }
}
Console.WriteLine($"Graph check: {KnowledgeGraph.All.Length} topics, {KnowledgeGraph.All.Sum(t => t.Facts.Length)} facts, {problems} problems");

// 2) Simulate 500 dives of up to 60 steps each
int dives = 0, steps = 0, exceptions = 0;
for (int i = 0; i < 500; i++)
{
    try
    {
        var engine = new JourneyEngine(KnowledgeGraph.All);
        engine.StartNew();
        int depth = 20 + (i % 60);
        for (int d = 0; d < depth; d++)
        {
            engine.GoDeeper();
        }
        dives++;
        steps += engine.Steps.Count;

        // The trail must be unique until every topic in the graph has been seen;
        // after that (infinite play) revisits are allowed, but never within 10 steps.
        int uniqueWindow = Math.Min(engine.Steps.Count, KnowledgeGraph.All.Length);
        var firstPass = engine.Steps.Take(uniqueWindow).Select(s => s.Topic.Id).ToList();
        if (firstPass.Distinct().Count() != firstPass.Count)
        {
            Console.WriteLine($"[FAIL] dive {i}: duplicate topic within first {uniqueWindow} steps");
            problems++;
        }
        var lastTen = engine.Steps.Skip(Math.Max(0, engine.Steps.Count - 10)).Select(s => s.Topic.Id).ToList();
        if (lastTen.Distinct().Count() != lastTen.Count)
        {
            Console.WriteLine($"[FAIL] dive {i}: repeat within last 10 steps");
            problems++;
        }
    }
    catch (Exception ex)
    {
        exceptions++;
        Console.WriteLine($"[FAIL] dive {i}: {ex.Message}");
    }
}
Console.WriteLine($"Engine check: {dives}/500 dives completed, avg depth {steps / Math.Max(1, dives):F1}, {exceptions} exceptions");
Console.WriteLine(problems == 0 ? "ALL OK" : "PROBLEMS FOUND");
Environment.Exit(problems == 0 ? 0 : 1);
