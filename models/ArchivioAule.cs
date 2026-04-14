namespace PrenotazioneAuleStudio.Models
{
    public class ArchivioAule
    {
        public int ProssimoId { get; set; } = 1;
        public List<AulaStudio> Aule { get; set; } = new();
    }
}