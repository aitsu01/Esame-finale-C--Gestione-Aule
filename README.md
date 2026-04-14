<!DOCTYPE html>
<html lang="it">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>Sistema di Gestione Prenotazione Aule Studio</title>
</head>
<body>

  <h1>Sistema di Gestione Prenotazione Aule Studio</h1>

  <p>
    Progetto sviluppato come <strong>applicazione console in C#</strong> per la gestione
    della prenotazione di aule studio da parte di <strong>studenti</strong> e
    <strong>amministratore</strong>.
  </p>

  <h2>Obiettivo del progetto</h2>
  <p>
    L'applicazione consente di gestire in modo semplice ed efficace la prenotazione
    delle aule studio, con <strong>persistenza dei dati tramite file JSON</strong>.
  </p>

  <p>Il sistema permette:</p>
  <ul>
    <li>agli <strong>studenti</strong> di prenotare un’aula, visualizzare, modificare e cancellare le proprie prenotazioni;</li>
    <li>all’<strong>amministratore</strong> di gestire prenotazioni e aule studio.</li>
  </ul>

  <hr />

  <h2>Funzionalità implementate</h2>

  <h3>Ruoli disponibili</h3>
  <p>All’avvio del programma è possibile scegliere tra due ruoli:</p>
  <ul>
    <li><strong>Studente</strong></li>
    <li><strong>Amministratore</strong></li>
  </ul>

  <h3>Area Studente</h3>
  <p>Lo studente può:</p>
  <ul>
    <li>inserire il proprio nome;</li>
    <li>essere riconosciuto dal sistema tramite un <strong>ID univoco</strong>;</li>
    <li>prenotare un’aula scegliendo:
      <ul>
        <li>aula;</li>
        <li>giorno;</li>
        <li>fascia oraria;</li>
        <li>numero di posti richiesti.</li>
      </ul>
    </li>
    <li>visualizzare solo le proprie prenotazioni;</li>
    <li>modificare le proprie prenotazioni;</li>
    <li>cancellare le proprie prenotazioni.</li>
  </ul>

  <h3>Area Amministratore</h3>
  <p>L’amministratore può:</p>
  <ul>
    <li>visualizzare tutte le prenotazioni;</li>
    <li>visualizzare tutte le aule studio;</li>
    <li>eliminare una prenotazione;</li>
    <li>aggiungere una nuova aula studio;</li>
    <li>eliminare un’aula studio;</li>
    <li>aumentare la capienza massima di un’aula.</li>
  </ul>

  <p>
    Quando un’aula viene eliminata, il sistema rimuove automaticamente
    anche tutte le prenotazioni associate.
  </p>

  <hr />

  <h2>Gestione dei posti disponibili</h2>
  <p>Ogni aula dispone di una <strong>capienza massima</strong>.</p>

  <p>Il sistema:</p>
  <ul>
    <li>accetta una prenotazione se i posti richiesti sono <strong>minori o uguali</strong> ai posti disponibili;</li>
    <li>rifiuta una prenotazione se i posti richiesti <strong>superano</strong> quelli disponibili per una determinata aula, in uno specifico giorno e in una specifica fascia oraria.</li>
  </ul>

  <hr />

  <h2>Persistenza dei dati</h2>
  <p>
    I dati vengono salvati in file <strong>JSON</strong> per essere mantenuti
    anche dopo la chiusura del programma.
  </p>

  <p>File utilizzati:</p>
  <ul>
    <li><code>prenotazioni.json</code></li>
    <li><code>studenti.json</code></li>
    <li><code>aule.json</code></li>
  </ul>

  <hr />

  <h2>Struttura del progetto</h2>
  <pre><code>PrenotazioneAuleStudio/
│
├── Models/
│   ├── Ruolo.cs
│   ├── Studente.cs
│   ├── AulaStudio.cs
│   ├── Prenotazione.cs
│   ├── ArchivioPrenotazioni.cs
│   ├── ArchivioStudenti.cs
│   └── ArchivioAule.cs
│
├── Data/
│   ├── JsonDataStore.cs
│   ├── JsonStudentiStore.cs
│   └── JsonAuleStore.cs
│
├── Services/
│   ├── PrenotazioneService.cs
│   ├── StudenteService.cs
│   └── AulaService.cs
│
├── UI/
│   ├── InputHelper.cs
│   ├── MenuStudente.cs
│   └── MenuAmministratore.cs
│
├── Program.cs
└── README.md</code></pre>

  <hr />

  <h2>User Story implementate</h2>

  <h3>User Story 1</h3>
  <p><strong>Come studente, voglio:</strong></p>
  <ul>
    <li>prenotare un’aula studio per un determinato giorno e per una determinata fascia oraria;</li>
    <li>visualizzare le mie prenotazioni;</li>
    <li>modificare le mie prenotazioni;</li>
    <li>cancellare le mie prenotazioni.</li>
  </ul>

  <h3>User Story 2</h3>
  <p><strong>Come sistema, devo:</strong></p>
  <ul>
    <li>rifiutare una prenotazione se supera i posti disponibili per quella determinata fascia oraria e per quel determinato giorno;</li>
    <li>accettare una prenotazione se i posti richiesti sono minori o uguali ai posti disponibili per quella determinata fascia oraria e per quel determinato giorno.</li>
  </ul>

  <h3>User Story 3</h3>
  <p><strong>Come amministratore, voglio:</strong></p>
  <ul>
    <li>visualizzare tutte le prenotazioni con le relative informazioni sugli utenti che hanno prenotato;</li>
    <li>eliminare prenotazioni;</li>
    <li>visualizzare tutte le aule studio;</li>
    <li>aggiungere nuove aule studio;</li>
    <li>eliminare aule studio e tutte le prenotazioni associate;</li>
    <li>modificare la capienza massima delle aule, solo aumentandola.</li>
  </ul>

  <hr />

  <h2>Informazioni mostrate nelle prenotazioni</h2>

  <h3>Studente</h3>
  <p>Lo studente visualizza le proprie prenotazioni con:</p>
  <ul>
    <li>ID prenotazione;</li>
    <li>nome aula;</li>
    <li>giorno;</li>
    <li>fascia oraria;</li>
    <li>posti richiesti.</li>
  </ul>

  <h3>Amministratore</h3>
  <p>L’amministratore visualizza tutte le prenotazioni con:</p>
  <ul>
    <li>ID prenotazione;</li>
    <li>nome studente;</li>
    <li>nome aula;</li>
    <li>giorno;</li>
    <li>fascia oraria;</li>
    <li>posti prenotati;</li>
    <li>posti ancora disponibili.</li>
  </ul>

  <hr />

  <h2>Tecnologie utilizzate</h2>
  <ul>
    <li><strong>C#</strong></li>
    <li><strong>.NET Console Application</strong></li>
    <li><strong>System.Text.Json</strong></li>
    <li><strong>JSON</strong> per la persistenza dei dati</li>
  </ul>

  <hr />

  <h2>Avvio del progetto</h2>

  <h3>Da Visual Studio</h3>
  <ol>
    <li>Aprire la solution o il progetto;</li>
    <li>compilare il progetto;</li>
    <li>avviare l’applicazione.</li>
  </ol>

  <h3>Da terminale</h3>
  <pre><code>dotnet build
dotnet run</code></pre>

  <hr />

  <h2>Note progettuali</h2>
  <ul>
    <li>i dati vengono salvati localmente in formato JSON;</li>
    <li>gli studenti vengono identificati tramite nome e associati a un ID univoco persistente;</li>
    <li>la capienza delle aule può essere modificata dall’amministratore solo in aumento;</li>
    <li>l’eliminazione di un’aula comporta anche l’eliminazione automatica delle relative prenotazioni.</li>
  </ul>

  <hr />

  <h2>Conclusione</h2>
  <p>
    Progetto realizzato come esercitazione / esame finale in C# per la gestione
    delle prenotazioni delle aule studio.
  </p>

</body>
</html>

