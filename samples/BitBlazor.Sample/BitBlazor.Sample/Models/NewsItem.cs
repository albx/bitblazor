namespace BitBlazor.Sample.Models;

public record NewsItem(
    int Id,
    string Title,
    string Summary,
    string Category,
    DateOnly PublishedAt);
