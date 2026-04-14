using System.Text.Json;
using PrenotazioneAuleStudio.Models;

namespace PrenotazioneAuleStudio.Data
{
    public class JsonAuleStore
    {
        private readonly string _percorsoFile;

        public JsonAuleStore(string nomeFile = "aule.json")
        {
            _percorsoFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeFile);
        }

        public ArchivioAule Carica()
        {
            try
            {
                if (!File.Exists(_percorsoFile))
                {
                    ArchivioAule archivioIniziale = CreaArchivioIniziale();
                    Salva(archivioIniziale);
                    return archivioIniziale;
                }

                string json = File.ReadAllText(_percorsoFile);

                if (string.IsNullOrWhiteSpace(json))
                {
                    ArchivioAule archivioIniziale = CreaArchivioIniziale();
                    Salva(archivioIniziale);
                    return archivioIniziale;
                }

                ArchivioAule? archivio = JsonSerializer.Deserialize<ArchivioAule>(json);
                return archivio ?? CreaArchivioIniziale();
            }
            catch
            {
                return CreaArchivioIniziale();
            }
        }

        public void Salva(ArchivioAule archivio)
        {
            JsonSerializerOptions opzioni = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(archivio, opzioni);
            File.WriteAllText(_percorsoFile, json);
        }

        private ArchivioAule CreaArchivioIniziale()
        {
            return new ArchivioAule
            {
                ProssimoId = 6,
                Aule = new List<AulaStudio>
                {
                    new AulaStudio { Id = 1, Nome = "Aula di Informatica", Capienza = 30 },
                    new AulaStudio { Id = 2, Nome = "Aula di Chimica", Capienza = 25 },
                    new AulaStudio { Id = 3, Nome = "Aula di Statistica", Capienza = 20 },
                    new AulaStudio { Id = 4, Nome = "Aula di Fisica", Capienza = 35 },
                    new AulaStudio { Id = 5, Nome = "Aula di Matematica", Capienza = 40 }
                }
            };
        }
    }
}