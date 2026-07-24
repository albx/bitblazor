using BitBlazor.Sample.Models;

namespace BitBlazor.Sample.Services;

public class NewsService : INewsService
{
    private const int PageSize = 10;
    private const int TotalItems = 50;

    private static readonly string[] Categories =
    [
        "Comunicati stampa",
        "Avvisi",
        "Bandi e Concorsi",
        "Ordinanze",
        "Notizie dal Comune"
    ];

    private static readonly string[] Summaries =
    [
        "Il Comune di Bitopoli rende noto che a partire dalla prossima settimana saranno attivi nuovi sportelli digitali per la cittadinanza.",
        "L'Amministrazione Comunale informa i cittadini delle modifiche al calendario dei servizi durante il periodo festivo.",
        "È indetta una selezione pubblica per la formazione di una graduatoria per assunzioni a tempo determinato.",
        "Si comunica l'ordinanza del Sindaco relativa alla regolamentazione del traffico nelle zone centrali.",
        "Il Comune presenta il nuovo servizio digitale per la gestione delle pratiche anagrafiche online.",
        "Pubblicato il bando per l'assegnazione dei contributi a supporto delle attività economiche locali.",
        "Avviso per i titolari di attività commerciali riguardo alle nuove normative sulle insegne pubblicitarie.",
        "L'ufficio tecnico informa della chiusura temporanea di alcuni uffici per lavori di manutenzione straordinaria.",
        "Sono aperte le iscrizioni ai corsi di formazione professionale finanziati dalla Regione.",
        "Il Comune di Bitopoli aderisce al programma nazionale per la rigenerazione urbana dei quartieri periferici."
    ];

    public Task<NewsResult> GetNewsAsync(int page)
    {
        var skip = (page - 1) * PageSize;
        var count = Math.Min(PageSize, TotalItems - skip);

        var items = Enumerable.Range(skip + 1, count)
            .Select(i => new NewsItem(
                i,
                $"Comunicazione n. {i:D3} — Aggiornamento servizi comunali",
                Summaries[(i - 1) % Summaries.Length],
                Categories[(i - 1) % Categories.Length],
                DateOnly.FromDateTime(DateTime.Now.AddDays(-(i - 1)))))
            .ToList();

        return Task.FromResult(new NewsResult(items, TotalItems));
    }
}
