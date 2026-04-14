using PrenotazioneAuleStudio.Models;

namespace PrenotazioneAuleStudio.Models
{
    public class ArchivioStudenti
    {
        public int ProssimoId { get; set; } = 1;
        public List<Studente> Studenti { get; set; } = new();
    }
}