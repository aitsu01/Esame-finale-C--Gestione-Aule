using PrenotazioneAuleStudio.Data;
using PrenotazioneAuleStudio.Models;

namespace PrenotazioneAuleStudio.Services
{
    public class AulaService
    {
        private readonly JsonAuleStore _auleStore;
        private readonly ArchivioAule _archivioAule;

        public AulaService(JsonAuleStore auleStore)
        {
            _auleStore = auleStore;
            _archivioAule = _auleStore.Carica();
        }

        public List<AulaStudio> OttieniTutte()
        {
            return _archivioAule.Aule
                .OrderBy(a => a.Nome)
                .ToList();
        }

        public AulaStudio? OttieniPerId(int id)
        {
            return _archivioAule.Aule.FirstOrDefault(a => a.Id == id);
        }

        public bool AggiungiAula(string nome, int capienza, out string messaggio)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                messaggio = "Il nome dell'aula non può essere vuoto.";
                return false;
            }

            if (capienza <= 0)
            {
                messaggio = "La capienza deve essere maggiore di zero.";
                return false;
            }

            bool esisteGia = _archivioAule.Aule
                .Any(a => a.Nome.Trim().ToLower() == nome.Trim().ToLower());

            if (esisteGia)
            {
                messaggio = "Esiste già un'aula con questo nome.";
                return false;
            }

            AulaStudio nuovaAula = new AulaStudio
            {
                Id = _archivioAule.ProssimoId++,
                Nome = nome.Trim(),
                Capienza = capienza
            };

            _archivioAule.Aule.Add(nuovaAula);
            _auleStore.Salva(_archivioAule);

            messaggio = "Aula aggiunta con successo.";
            return true;
        }

        public bool EliminaAula(int aulaId, out string messaggio)
        {
            AulaStudio? aula = _archivioAule.Aule.FirstOrDefault(a => a.Id == aulaId);

            if (aula == null)
            {
                messaggio = "Aula non trovata.";
                return false;
            }

            _archivioAule.Aule.Remove(aula);
            _auleStore.Salva(_archivioAule);

            messaggio = "Aula eliminata con successo.";
            return true;
        }

        public bool AumentaCapienza(int aulaId, int nuovaCapienza, out string messaggio)
        {
            AulaStudio? aula = _archivioAule.Aule.FirstOrDefault(a => a.Id == aulaId);

            if (aula == null)
            {
                messaggio = "Aula non trovata.";
                return false;
            }

            if (nuovaCapienza <= aula.Capienza)
            {
                messaggio = $"La nuova capienza deve essere maggiore di quella attuale ({aula.Capienza}).";
                return false;
            }

            aula.Capienza = nuovaCapienza;
            _auleStore.Salva(_archivioAule);

            messaggio = "Capienza aggiornata con successo.";
            return true;
        }
    }
}