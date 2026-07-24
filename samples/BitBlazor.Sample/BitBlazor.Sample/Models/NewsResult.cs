namespace BitBlazor.Sample.Models;

public record NewsResult(IReadOnlyList<NewsItem> Items, int TotalCount);
