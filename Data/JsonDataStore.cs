using System.Text.Json;
using PrenotazioneAuleStudio.Models;

namespace PrenotazioneAuleStudio.Data
{
    public class JsonDataStore
    {
        private readonly string _percorsoFile;

        public JsonDataStore(string nomeFile = "prenotazioni.json")
        {
            _percorsoFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeFile);
        }

        public ArchivioPrenotazioni Carica()
        {
            try
            {
                if (!File.Exists(_percorsoFile))
                {
                    var archivioVuoto = new ArchivioPrenotazioni();
                    Salva(archivioVuoto);
                    return archivioVuoto;
                }

                string json = File.ReadAllText(_percorsoFile);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new ArchivioPrenotazioni();
                }

                ArchivioPrenotazioni? archivio =
                    JsonSerializer.Deserialize<ArchivioPrenotazioni>(json);

                return archivio ?? new ArchivioPrenotazioni();
            }
            catch
            {
                return new ArchivioPrenotazioni();
            }
        }

        public void Salva(ArchivioPrenotazioni archivio)
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