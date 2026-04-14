# Sistema di Gestione Prenotazione Aule Studio

Progetto sviluppato in **C# Console App** per la gestione della prenotazione di aule studio da parte di **studenti** e **amministratore**.

## Obiettivo del progetto

L'applicazione consente di gestire in modo semplice ed efficace la prenotazione delle aule studio, con persistenza dei dati tramite file **JSON**.

Il sistema permette:

- agli **studenti** di prenotare un’aula, visualizzare, modificare e cancellare le proprie prenotazioni
- all’**amministratore** di gestire le prenotazioni e le aule studio

---

## Funzionalità implementate

### Ruoli disponibili

All’avvio del programma è possibile scegliere tra due ruoli:

- **Studente**
- **Amministratore**

---

## Area Studente

Lo studente può:

- inserire il proprio nome
- essere riconosciuto dal sistema tramite un **ID univoco**
- prenotare un’aula scegliendo:
  - aula
  - giorno
  - fascia oraria
  - numero di posti richiesti
- visualizzare solo le proprie prenotazioni
- modificare le proprie prenotazioni
- cancellare le proprie prenotazioni

---

## Area Amministratore

L’amministratore può:

- visualizzare tutte le prenotazioni
- visualizzare tutte le aule studio
- eliminare una prenotazione
- aggiungere una nuova aula studio
- eliminare un’aula studio
- aumentare la capienza massima di un’aula

Quando un’aula viene eliminata, il sistema elimina automaticamente anche tutte le prenotazioni associate a quell’aula.

---

## Gestione posti disponibili

Ogni aula ha una **capienza massima**.

Il sistema:

- accetta una prenotazione se i posti richiesti sono **minori o uguali** ai posti disponibili
- rifiuta una prenotazione se i posti richiesti **superano** i posti disponibili per quella determinata aula, in quello specifico giorno e in quella specifica fascia oraria

---

## Persistenza dei dati

I dati vengono salvati in file **JSON** per essere mantenuti anche dopo la chiusura del programma.

File utilizzati:

- `prenotazioni.json`
- `studenti.json`
- `aule.json`

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
└── README.md


User Story implementate
User Story 1

Come studente, voglio:

prenotare un’aula studio per un determinato giorno e per una determinata fascia oraria
visualizzare le mie prenotazioni
modificare le mie prenotazioni
cancellare le mie prenotazioni
User Story 2

Come sistema, devo:

rifiutare una prenotazione se supera i posti disponibili per quella determinata fascia oraria e per quel determinato giorno
accettare una prenotazione se i posti richiesti sono minori o uguali ai posti disponibili per quella determinata fascia oraria e per quel determinato giorno
User Story 3

Come amministratore, voglio:

visualizzare tutte le prenotazioni con le relative informazioni sugli utenti che hanno prenotato
eliminare prenotazioni
visualizzare tutte le aule studio
aggiungere nuove aule studio
eliminare aule studio e tutte le prenotazioni associate
modificare la capienza massima delle aule, solo aumentandola
Informazioni mostrate nelle prenotazioni
Studente

Lo studente visualizza le proprie prenotazioni con:

ID prenotazione
nome aula
giorno
fascia oraria
posti richiesti
Amministratore

L’amministratore visualizza tutte le prenotazioni con:

ID prenotazione
nome studente
nome aula
giorno
fascia oraria
posti prenotati
posti ancora disponibili
Tecnologie utilizzate
C#
.NET Console Application
System.Text.Json
JSON per la persistenza dei dati
Avvio del progetto
Da Visual Studio
Aprire la solution o il progetto
Compilare
Avviare l’applicazione
Da terminale
dotnet build
dotnet run
Note progettuali
i dati vengono salvati localmente in formato JSON
gli studenti vengono identificati tramite nome e associati a un ID univoco persistente
la capienza delle aule può essere solo aumentata dall’amministratore
l’eliminazione di un’aula provoca anche l’eliminazione delle relative prenotazioni


Progetto realizzato come esercitazione / esame finale in C# per la gestione delle prenotazioni delle aule studio.

