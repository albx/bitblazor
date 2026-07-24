using BitBlazor.Sample.Models;

namespace BitBlazor.Sample.Services;

public interface IPraticheService
{
    Task<PraticheResult> GetPraticheAsync(int page, string? statoFiltro = null);
}
