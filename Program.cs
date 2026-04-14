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
            JsonDataStore dataStore = new JsonDataStore();
            JsonStudentiStore studentiStore = new JsonStudentiStore();
            JsonAuleStore auleStore = new JsonAuleStore();

            AulaService aulaService = new AulaService(auleStore);
            List<AulaStudio> aule = aulaService.OttieniTutte();

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
                    AvviaAreaAmministratore(aulaService, prenotazioneService);
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

        private static void AvviaAreaAmministratore(
            AulaService aulaService,
            PrenotazioneService prenotazioneService)
        {
            MenuAmministratore menuAmministratore = new MenuAmministratore(aulaService, prenotazioneService);
            menuAmministratore.Avvia();
        }
    }
}