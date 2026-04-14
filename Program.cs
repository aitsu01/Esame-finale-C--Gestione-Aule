using PrenotazioneAuleStudio.Data;
using PrenotazioneAuleStudio.Models;
using PrenotazioneAuleStudio.Services;
using PrenotazioneAuleStudio.UI;

namespace PrenotazioneAuleStudio
{
    internal class Program
    {
        static void Main()
        {
            List<AulaStudio> aule = new()
            {
                new AulaStudio { Id = 1, Nome = "Aula di Informatica", Capienza = 30 },
                new AulaStudio { Id = 2, Nome = "Aula di Chimica", Capienza = 25 },
                new AulaStudio { Id = 3, Nome = "Aula di Statistica", Capienza = 20 },
                new AulaStudio { Id = 4, Nome = "Aula di Fisica", Capienza = 35 },
                new AulaStudio { Id = 5, Nome = "Aula di Matematica", Capienza = 40 }
            };

            JsonDataStore dataStore = new JsonDataStore();
            JsonStudentiStore studentiStore = new JsonStudentiStore();

            PrenotazioneService prenotazioneService = new PrenotazioneService(dataStore, aule);
            StudenteService studenteService = new StudenteService(studentiStore);

            Console.WriteLine("=== SISTEMA PRENOTAZIONE AULE STUDIO ===");
            Console.WriteLine("1. Studente");
            Console.WriteLine("2. Amministratore");

            int sceltaRuolo = InputHelper.LeggiIntero("Scegli il ruolo: ");

            switch (sceltaRuolo)
            {
                case (int)Ruolo.Studente:
                    AvviaAreaStudente(aule, prenotazioneService, studenteService);
                    break;

                case (int)Ruolo.Amministratore:
                    Console.WriteLine("Area amministratore non ancora implementata.");
                    break;

                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }

        private static void AvviaAreaStudente(
            List<AulaStudio> aule,
            PrenotazioneService prenotazioneService,
            StudenteService studenteService)
        {
            string nomeStudente = InputHelper.LeggiStringa("Inserisci il tuo nome: ");

            Studente studente = studenteService.OttieniOCreaStudente(nomeStudente);

            Console.WriteLine($"Benvenuto {studente.Nome}. Il tuo ID è {studente.Id}.");

            MenuStudente menuStudente = new MenuStudente(studente, aule, prenotazioneService);
            menuStudente.Avvia();
        }
    }
}