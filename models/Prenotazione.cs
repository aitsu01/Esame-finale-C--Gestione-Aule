namespace PrenotazioneAuleStudio.Models
{
    public class Prenotazione
    {
        public int Id { get; set; }
        public int StudenteId { get; set; }
        public string NomeStudente { get; set; } = string.Empty;
        public int AulaId { get; set; }
        public string NomeAula { get; set; } = string.Empty;
        public DateTime Giorno { get; set; }
        public string FasciaOraria { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"ID: {Id} | Aula: {NomeAula} | Giorno: {Giorno:dd/MM/yyyy} | Fascia: {FasciaOraria}";
        }
    }
}
