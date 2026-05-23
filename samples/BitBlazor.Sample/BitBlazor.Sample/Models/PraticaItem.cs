namespace BitBlazor.Sample.Models;

public record PraticaItem(
    int Id,
    string Titolo,
    string Categoria,
    string Stato,
    string Descrizione,
    DateOnly DataPresentazione);
