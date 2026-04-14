using PrenotazioneAuleStudio.Data;
using PrenotazioneAuleStudio.Models;

namespace PrenotazioneAuleStudio.Services
{
    public class StudenteService
    {
        private readonly JsonStudentiStore _studentiStore;
        private readonly ArchivioStudenti _archivioStudenti;

        public StudenteService(JsonStudentiStore studentiStore)
        {
            _studentiStore = studentiStore;
            _archivioStudenti = _studentiStore.Carica();
        }

        public Studente OttieniOCreaStudente(string nomeStudente)
        {
            Studente? studenteEsistente = _archivioStudenti.Studenti
                .FirstOrDefault(s => s.Nome.Trim().ToLower() == nomeStudente.Trim().ToLower());

            if (studenteEsistente != null)
            {
                return studenteEsistente;
            }

            Studente nuovoStudente = new Studente
            {
                Id = _archivioStudenti.ProssimoId++,
                Nome = nomeStudente.Trim()
            };

            _archivioStudenti.Studenti.Add(nuovoStudente);
            _studentiStore.Salva(_archivioStudenti);

            return nuovoStudente;
        }

        public List<Studente> OttieniTutti()
        {
            return _archivioStudenti.Studenti
                .OrderBy(s => s.Nome)
                .ToList();
        }
    }
}