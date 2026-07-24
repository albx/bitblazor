using BitBlazor.Sample.Models;

namespace BitBlazor.Sample.Services;

public interface INewsService
{
    Task<NewsResult> GetNewsAsync(int page);
}
