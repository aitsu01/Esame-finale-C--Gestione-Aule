using PrenotazioneAuleStudio.Data;
using PrenotazioneAuleStudio.Models;

namespace PrenotazioneAuleStudio.Services
{
    public class PrenotazioneService
    {
        private readonly JsonDataStore _dataStore;
        private readonly ArchivioPrenotazioni _archivio;

        public List<string> FasceOrarieDisponibili { get; } = new()
        {
            "08:00-10:00",
            "10:00-12:00",
            "14:00-16:00",
            "16:00-18:00"
        };

        public PrenotazioneService(JsonDataStore dataStore)
        {
            _dataStore = dataStore;
            _archivio = _dataStore.Carica();
        }

        public List<Prenotazione> OttieniPrenotazioniStudente(int studenteId)
        {
            return _archivio.Prenotazioni
                .Where(p => p.StudenteId == studenteId)
                .OrderBy(p => p.Giorno)
                .ThenBy(p => p.FasciaOraria)
                .ToList();
        }

        public bool Prenota(Studente studente, AulaStudio aula, DateTime giorno, string fasciaOraria, out string messaggio)
        {
            if (giorno.Date < DateTime.Today)
            {
                messaggio = "Non puoi prenotare una data passata.";
                return false;
            }

            if (!FasceOrarieDisponibili.Contains(fasciaOraria))
            {
                messaggio = "Fascia oraria non valida.";
                return false;
            }

            bool aulaOccupata = _archivio.Prenotazioni.Any(p =>
                p.AulaId == aula.Id &&
                p.Giorno.Date == giorno.Date &&
                p.FasciaOraria == fasciaOraria);

            if (aulaOccupata)
            {
                messaggio = "L'aula è già prenotata per quel giorno e quella fascia oraria.";
                return false;
            }

            Prenotazione nuovaPrenotazione = new Prenotazione
            {
                Id = _archivio.ProssimoId++,
                StudenteId = studente.Id,
                NomeStudente = studente.Nome,
                AulaId = aula.Id,
                NomeAula = aula.Nome,
                Giorno = giorno.Date,
                FasciaOraria = fasciaOraria
            };

            _archivio.Prenotazioni.Add(nuovaPrenotazione);
            _dataStore.Salva(_archivio);

            messaggio = "Prenotazione effettuata con successo.";
            return true;
        }

        public bool ModificaPrenotazione(int prenotazioneId, int studenteId, DateTime nuovoGiorno, string nuovaFascia, out string messaggio)
        {
            Prenotazione? prenotazione = _archivio.Prenotazioni
                .FirstOrDefault(p => p.Id == prenotazioneId && p.StudenteId == studenteId);

            if (prenotazione == null)
            {
                messaggio = "Prenotazione non trovata.";
                return false;
            }

            if (nuovoGiorno.Date < DateTime.Today)
            {
                messaggio = "Non puoi spostare una prenotazione in una data passata.";
                return false;
            }

            if (!FasceOrarieDisponibili.Contains(nuovaFascia))
            {
                messaggio = "Nuova fascia oraria non valida.";
                return false;
            }

            bool conflitto = _archivio.Prenotazioni.Any(p =>
                p.Id != prenotazioneId &&
                p.AulaId == prenotazione.AulaId &&
                p.Giorno.Date == nuovoGiorno.Date &&
                p.FasciaOraria == nuovaFascia);

            if (conflitto)
            {
                messaggio = "L'aula è già occupata nel nuovo giorno e nella nuova fascia oraria.";
                return false;
            }

            prenotazione.Giorno = nuovoGiorno.Date;
            prenotazione.FasciaOraria = nuovaFascia;

            _dataStore.Salva(_archivio);

            messaggio = "Prenotazione modificata con successo.";
            return true;
        }

        public bool CancellaPrenotazione(int prenotazioneId, int studenteId, out string messaggio)
        {
            Prenotazione? prenotazione = _archivio.Prenotazioni
                .FirstOrDefault(p => p.Id == prenotazioneId && p.StudenteId == studenteId);

            if (prenotazione == null)
            {
                messaggio = "Prenotazione non trovata.";
                return false;
            }

            _archivio.Prenotazioni.Remove(prenotazione);
            _dataStore.Salva(_archivio);

            messaggio = "Prenotazione cancellata con successo.";
            return true;
        }
    }
}