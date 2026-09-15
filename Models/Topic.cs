namespace RabbitHole.Models;

/// <summary>A node in the offline knowledge graph.</summary>
public sealed record Topic(
    string Id,
    string Title,
    string Tagline,
    string Category,
    string[] Facts,
    string[] Related)
{
    public string AnchorFact => Facts.Length > 0 ? Facts[0] : string.Empty;
}

/// <summary>One stop on the journey through the hole.</summary>
public sealed record JourneyStep(int Depth, Topic Topic, string Fact);
