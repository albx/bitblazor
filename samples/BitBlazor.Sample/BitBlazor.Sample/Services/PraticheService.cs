using BitBlazor.Sample.Models;

namespace BitBlazor.Sample.Services;

public class PraticheService : IPraticheService
{
    private const int PageSize = 8;

    private static readonly IReadOnlyList<PraticaItem> AllPratiche =
    [
        new(1,  "Rinnovo Carta d'Identità Elettronica",         "Anagrafe",  "Completata",     "Richiesta di rinnovo della CIE scaduta. Documento ritirato allo sportello.",                            new DateOnly(2025, 11, 3)),
        new(2,  "Cambio di Residenza",                          "Anagrafe",  "In Lavorazione", "Dichiarazione di cambio residenza da Comune di Altamura. Istruttoria in corso.",                        new DateOnly(2026, 1, 14)),
        new(3,  "Richiesta Certificato di Residenza",           "Anagrafe",  "Completata",     "Certificato emesso in formato digitale con firma elettronica qualificata.",                              new DateOnly(2026, 2, 5)),
        new(4,  "Calcolo e Pagamento IMU 2025",                 "Tributi",   "Completata",     "Versamento IMU prima e seconda rata per immobile in Via Roma 12. Ricevute allegate.",                   new DateOnly(2025, 6, 10)),
        new(5,  "Dichiarazione TARI – Variazione Utenza",       "Tributi",   "In Attesa",      "Richiesta di variazione della superficie imponibile. In attesa di sopralluogo tecnico.",                new DateOnly(2026, 3, 2)),
        new(6,  "Rimborso IMU per Immobile Venduto",            "Tributi",   "In Lavorazione", "Istanza di rimborso per doppio versamento IMU. Documentazione al vaglio dell'ufficio tributi.",        new DateOnly(2026, 4, 18)),
        new(7,  "CILA – Ristrutturazione Bagno",                "Edilizia",  "Completata",     "Comunicazione inizio lavori per opere interne. Protocollata e accettata.",                              new DateOnly(2025, 9, 22)),
        new(8,  "Permesso di Costruire – Ampliamento Garage",   "Edilizia",  "In Lavorazione", "Domanda di permesso per ampliamento di 20 mq. Richiesta integrazione documentale ricevuta.",           new DateOnly(2026, 2, 28)),
        new(9,  "SCIA – Apertura Attività Commerciale",         "SUAP",      "Completata",     "Segnalazione certificata per apertura esercizio di vicinato. Attività avviata.",                       new DateOnly(2025, 7, 15)),
        new(10, "Modifica Orari Esercizio Pubblico",            "SUAP",      "Completata",     "Comunicazione di variazione orari per bar in Corso Italia. Ricevuta dal SUAP.",                        new DateOnly(2025, 12, 1)),
        new(11, "Richiesta Patrocinio Comunale",                "Cultura",   "In Attesa",      "Domanda di patrocinio per evento culturale estivo. In attesa di delibera della Giunta.",               new DateOnly(2026, 4, 30)),
        new(12, "Concessione Spazio Pubblico – Sagra",          "Cultura",   "In Lavorazione", "Richiesta occupazione piazza per manifestazione enogastronomica. Sopralluogo programmato.",            new DateOnly(2026, 5, 10)),
        new(13, "Iscrizione Asilo Nido Comunale",               "Sociale",   "Completata",     "Domanda di iscrizione per a.s. 2025/26. Ammesso con punteggio 42/60.",                                 new DateOnly(2025, 5, 20)),
        new(14, "Richiesta Assegno di Maternità",               "Sociale",   "Completata",     "Istanza presentata nei termini. Importo liquidato e accreditato.",                                     new DateOnly(2025, 8, 11)),
        new(15, "Bonus Affitto – Contributo Canone",            "Sociale",   "In Attesa",      "Domanda contributo affitto bando 2026. Graduatoria in definizione.",                                   new DateOnly(2026, 3, 25)),
        new(16, "Contrassegno Disabili H",                      "Anagrafe",  "Completata",     "Rinnovo contrassegno di parcheggio per persona con disabilità. Documento consegnato.",                 new DateOnly(2026, 1, 7)),
        new(17, "Rimborso TARI – Riduzione Compostaggio",       "Tributi",   "In Attesa",      "Richiesta di riduzione tariffaria per autoproduzione compost. Verifica sopralluogo da pianificare.",   new DateOnly(2026, 5, 2)),
        new(18, "Accesso Atti – Piano Regolatore",              "Edilizia",  "Completata",     "Richiesta di accesso agli atti del PRG vigente. Documentazione consegnata in formato PDF.",            new DateOnly(2026, 2, 14)),
        new(19, "Voltura TARI – Cambio Intestatario",           "Tributi",   "In Lavorazione", "Richiesta voltura utenza TARI a seguito di compravendita immobile. Ufficio tributi in lavorazione.",  new DateOnly(2026, 4, 4)),
        new(20, "Concessione Suolo Pubblico – Dehors",          "SUAP",      "In Lavorazione", "Istanza per posa struttura dehors stagionale. Richiesta parere commissione paesaggio.",               new DateOnly(2026, 4, 22)),
        new(21, "Richiesta Mensa Scolastica",                   "Sociale",   "Completata",     "Iscrizione al servizio mensa per anno scolastico 2025/26. Confermata con fascia ISEE C.",              new DateOnly(2025, 7, 3)),
        new(22, "Trasporto Scolastico – Iscrizione",            "Sociale",   "Completata",     "Iscrizione al servizio scuolabus per l'a.s. 2025/26. Fermata assegnata: Via Mazzini.",                 new DateOnly(2025, 7, 3)),
        new(23, "Denuncia di Morte",                            "Anagrafe",  "Completata",     "Dichiarazione di morte registrata in atti. Atto n. 34/2026.",                                          new DateOnly(2026, 3, 18)),
        new(24, "CILA – Installazione Impianto Fotovoltaico",  "Edilizia",  "In Attesa",      "Comunicazione lavori per installazione pannelli fotovoltaici in copertura. Attesa ricevuta formale.",  new DateOnly(2026, 5, 15)),
        new(25, "Richiesta Contributo Centri Estivi",           "Sociale",   "In Lavorazione", "Domanda voucher per frequenza centro estivo comunale. Istruttoria avviata.",                           new DateOnly(2026, 5, 18)),
        new(26, "Autorizzazione Manifesti Elettorali",          "Cultura",   "Completata",     "Autorizzazione per affissione manifesti in periodo elettorale. Spazi assegnati.",                      new DateOnly(2025, 10, 5)),
        new(27, "Aggiornamento ISEE 2026",                      "Sociale",   "Completata",     "DSU presentata tramite CAF convenzionato. ISEE 2026 registrato nel sistema.",                          new DateOnly(2026, 1, 28)),
        new(28, "Permesso ZTL – Residente",                     "Anagrafe",  "In Lavorazione", "Richiesta pass ZTL per veicolo intestato al richiedente. Verifica residenza in corso.",               new DateOnly(2026, 5, 5)),
        new(29, "Denuncia Inizio Attività – B&B",               "SUAP",      "In Attesa",      "DIA per avvio attività ricettiva B&B in appartamento privato. In attesa di parere ASL.",              new DateOnly(2026, 5, 12)),
        new(30, "Rettifica Atto di Nascita",                    "Anagrafe",  "In Lavorazione", "Istanza di rettifica per errore materiale in trascrizione atto di nascita. Pratiche in corso.",      new DateOnly(2026, 4, 9)),
    ];

    public Task<PraticheResult> GetPraticheAsync(int page, string? statoFiltro = null)
    {
        var filtered = string.IsNullOrEmpty(statoFiltro)
            ? AllPratiche
            : AllPratiche.Where(p => p.Stato == statoFiltro).ToList();

        var items = filtered
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        return Task.FromResult(new PraticheResult(items, filtered.Count));
    }
}
