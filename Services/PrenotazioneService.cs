using PrenotazioneAuleStudio.Data;
using PrenotazioneAuleStudio.Models;

namespace PrenotazioneAuleStudio.Services
{
    public class PrenotazioneService
    {
        private readonly JsonDataStore _dataStore;
        private readonly ArchivioPrenotazioni _archivio;
        private readonly List<AulaStudio> _aule;

        public List<string> FasceOrarieDisponibili { get; } = new()
        {
            "08:00-10:00",
            "10:00-12:00",
            "14:00-16:00",
            "16:00-18:00"
        };

        public PrenotazioneService(JsonDataStore dataStore, List<AulaStudio> aule)
        {
            _dataStore = dataStore;
            _aule = aule;
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

        public bool Prenota(
            Studente studente,
            AulaStudio aula,
            DateTime giorno,
            string fasciaOraria,
            int postiRichiesti,
            out string messaggio)
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

            if (postiRichiesti <= 0)
            {
                messaggio = "Il numero di posti richiesti deve essere maggiore di zero.";
                return false;
            }

            int postiGiaPrenotati = _archivio.Prenotazioni
                .Where(p => p.AulaId == aula.Id &&
                            p.Giorno.Date == giorno.Date &&
                            p.FasciaOraria == fasciaOraria)
                .Sum(p => p.PostiRichiesti);

            int postiDisponibili = aula.Capienza - postiGiaPrenotati;

            if (postiRichiesti > postiDisponibili)
            {
                messaggio = $"Prenotazione rifiutata. Posti disponibili: {postiDisponibili}, posti richiesti: {postiRichiesti}.";
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
                FasciaOraria = fasciaOraria,
                PostiRichiesti = postiRichiesti
            };

            _archivio.Prenotazioni.Add(nuovaPrenotazione);
            _dataStore.Salva(_archivio);

            messaggio = $"Prenotazione effettuata con successo. Posti residui: {postiDisponibili - postiRichiesti}.";
            return true;
        }

        public bool ModificaPrenotazione(
            int prenotazioneId,
            int studenteId,
            DateTime nuovoGiorno,
            string nuovaFascia,
            int nuoviPostiRichiesti,
            out string messaggio)
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

            if (nuoviPostiRichiesti <= 0)
            {
                messaggio = "Il numero di posti richiesti deve essere maggiore di zero.";
                return false;
            }

            int postiGiaPrenotati = _archivio.Prenotazioni
                .Where(p => p.Id != prenotazioneId &&
                            p.AulaId == prenotazione.AulaId &&
                            p.Giorno.Date == nuovoGiorno.Date &&
                            p.FasciaOraria == nuovaFascia)
                .Sum(p => p.PostiRichiesti);

            int capienzaAula = OttieniCapienzaAula(prenotazione.AulaId);
            int postiDisponibili = capienzaAula - postiGiaPrenotati;

            if (nuoviPostiRichiesti > postiDisponibili)
            {
                messaggio = $"Modifica rifiutata. Posti disponibili: {postiDisponibili}, posti richiesti: {nuoviPostiRichiesti}.";
                return false;
            }

            prenotazione.Giorno = nuovoGiorno.Date;
            prenotazione.FasciaOraria = nuovaFascia;
            prenotazione.PostiRichiesti = nuoviPostiRichiesti;

            _dataStore.Salva(_archivio);

            messaggio = $"Prenotazione modificata con successo. Posti residui: {postiDisponibili - nuoviPostiRichiesti}.";
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

        private int OttieniCapienzaAula(int aulaId)
        {
            AulaStudio? aula = _aule.FirstOrDefault(a => a.Id == aulaId);
            return aula?.Capienza ?? 0;
        }
    }
}