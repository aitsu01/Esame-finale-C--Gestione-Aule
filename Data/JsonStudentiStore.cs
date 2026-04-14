using System.Text.Json;
using PrenotazioneAuleStudio.Models;

namespace PrenotazioneAuleStudio.Data
{
    public class JsonStudentiStore
    {
        private readonly string _percorsoFile;

        public JsonStudentiStore(string nomeFile = "studenti.json")
        {
            _percorsoFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeFile);
        }

        public ArchivioStudenti Carica()
        {
            try
            {
                if (!File.Exists(_percorsoFile))
                {
                    ArchivioStudenti archivioVuoto = new ArchivioStudenti();
                    Salva(archivioVuoto);
                    return archivioVuoto;
                }

                string json = File.ReadAllText(_percorsoFile);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new ArchivioStudenti();
                }

                ArchivioStudenti? archivio = JsonSerializer.Deserialize<ArchivioStudenti>(json);
                return archivio ?? new ArchivioStudenti();
            }
            catch
            {
                return new ArchivioStudenti();
            }
        }

        public void Salva(ArchivioStudenti archivio)
        {
            JsonSerializerOptions opzioni = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(archivio, opzioni);
            File.WriteAllText(_percorsoFile, json);
        }
    }
}