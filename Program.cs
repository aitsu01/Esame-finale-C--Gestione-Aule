using PrenotazioneAuleStudio.Data;
using PrenotazioneAuleStudio.Models;
using PrenotazioneAuleStudio.Services;
using PrenotazioneAuleStudio.UI;

namespace PrenotazioneAuleStudio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<AulaStudio> aule = new()
            {
                new AulaStudio { Id = 1, Nome = "Aula Studio A" },
                new AulaStudio { Id = 2, Nome = "Aula Studio B" },
                new AulaStudio { Id = 3, Nome = "Aula Studio C" }
            };

            JsonDataStore dataStore = new JsonDataStore();
            PrenotazioneService prenotazioneService = new PrenotazioneService(dataStore);

            Console.WriteLine("=== SISTEMA PRENOTAZIONE AULE STUDIO ===");
            Console.WriteLine("1. Studente");
            Console.WriteLine("2. Amministratore");

            int sceltaRuolo = InputHelper.LeggiIntero("Scegli il ruolo: ");

            switch (sceltaRuolo)
            {
                case (int)Ruolo.Studente:
                    AvviaAreaStudente(aule, prenotazioneService);
                    break;

                case (int)Ruolo.Amministratore:
                    Console.WriteLine("Area amministratore non ancora implementata.");
                    break;

                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }

        private static void AvviaAreaStudente(List<AulaStudio> aule, PrenotazioneService prenotazioneService)
        {
            string nomeStudente = InputHelper.LeggiStringa("Inserisci il tuo nome: ");

            Studente studente = new Studente
            {
                Id = 1,
                Nome = nomeStudente
            };

            MenuStudente menuStudente = new MenuStudente(studente, aule, prenotazioneService);
            menuStudente.Avvia();
        }
    }
}
