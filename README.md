# Sistema di Gestione Prenotazione Aule Studio

Progetto sviluppato in **C# Console App** per la gestione della prenotazione di aule studio da parte degli studenti.

## Obiettivo del progetto

L'applicazione permette agli studenti di:

- prenotare un'aula per un determinato giorno e fascia oraria
- visualizzare le proprie prenotazioni
- modificare una prenotazione esistente
- cancellare una prenotazione
- richiedere un certo numero di posti
- ottenere l'accettazione o il rifiuto della prenotazione in base ai posti disponibili

I dati vengono salvati in file **JSON**, così da mantenerli anche dopo la chiusura del programma.

---

## Funzionalità implementate

### Gestione ruolo
All'avvio del programma è possibile scegliere il ruolo:

- Studente
- Amministratore

Attualmente è implementata la parte **Studente**.

### Area Studente
Lo studente può:

- inserire il proprio nome
- essere riconosciuto dal sistema tramite un ID univoco
- prenotare un'aula scegliendo:
  - aula
  - giorno
  - fascia oraria
  - numero di posti richiesti
- visualizzare solo le proprie prenotazioni
- modificare le proprie prenotazioni
- cancellare le proprie prenotazioni

### Gestione posti disponibili
Ogni aula ha una propria capienza.

Il sistema:

- accetta una prenotazione se i posti richiesti sono minori o uguali ai posti disponibili
- rifiuta una prenotazione se i posti richiesti superano la disponibilità residua per quella aula, in quel giorno e in quella fascia oraria

### Persistenza dati
I dati vengono salvati in file JSON:

- `prenotazioni.json`
- `studenti.json`

---

## Struttura del progetto

```text
PrenotazioneAuleStudio/
│
├── Models/
│   ├── Ruolo.cs
│   ├── Studente.cs
│   ├── AulaStudio.cs
│   ├── Prenotazione.cs
│   ├── ArchivioPrenotazioni.cs
│   └── ArchivioStudenti.cs
│
├── Data/
│   ├── JsonDataStore.cs
│   └── JsonStudentiStore.cs
│
├── Services/
│   ├── PrenotazioneService.cs
│   └── StudenteService.cs
│
├── UI/
│   ├── InputHelper.cs
│   └── MenuStudente.cs
│
├── Program.cs
└── README.md

Aule disponibili

Attualmente il sistema gestisce le seguenti aule:

Aula di Informatica — 30 posti
Aula di Chimica — 25 posti
Aula di Statistica — 20 posti
Aula di Fisica — 35 posti
Aula di Matematica — 40 posti
Fasce orarie disponibili

Le fasce orarie prenotabili sono:

08:00-10:00
10:00-12:00
14:00-16:00
16:00-18:00
User Story implementate
User Story 1

Come studente, voglio:

prenotare un'aula studio per un determinato giorno e per una determinata fascia oraria
visualizzare le mie prenotazioni
modificare le mie prenotazioni
cancellare le mie prenotazioni
User Story 2

Come sistema, devo:

rifiutare una prenotazione se supera i posti disponibili per quella determinata fascia oraria e per quel determinato giorno
accettare una prenotazione se i posti richiesti sono minori o uguali ai posti disponibili per quella determinata fascia oraria e per quel determinato giorno
Tecnologie utilizzate
C#
.NET Console Application
System.Text.Json per la serializzazione dei dati
JSON per il salvataggio persistente

Avvio del progetto
Clonare la repository
Aprire il progetto in Visual Studio
Compilare ed eseguire l'applicazione

Oppure da terminale:

dotnet build
dotnet run


