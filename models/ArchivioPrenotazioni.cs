using System.Collections.Generic;

namespace PrenotazioneAuleStudio.Models
{
    public class ArchivioPrenotazioni
    {
        public int ProssimoId { get; set; } = 1;
        public List<Prenotazione> Prenotazioni { get; set; } = new();
    }
}