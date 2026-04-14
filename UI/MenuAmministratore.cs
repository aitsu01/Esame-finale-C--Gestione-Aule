using PrenotazioneAuleStudio.Models;
using PrenotazioneAuleStudio.Services;

namespace PrenotazioneAuleStudio.UI
{
    public class MenuAmministratore
    {
        private readonly AulaService _aulaService;
        private readonly PrenotazioneService _prenotazioneService;

        public MenuAmministratore(AulaService aulaService, PrenotazioneService prenotazioneService)
        {
            _aulaService = aulaService;
            _prenotazioneService = prenotazioneService;
        }

        public void Avvia()
        {
            bool continua = true;

            while (continua)
            {
                Console.WriteLine();
                Console.WriteLine("=== MENU AMMINISTRATORE ===");
                Console.WriteLine("1. Visualizza tutte le prenotazioni");
                Console.WriteLine("2. Elimina una prenotazione");
                Console.WriteLine("3. Visualizza tutte le aule");
                Console.WriteLine("4. Aggiungi aula studio");
                Console.WriteLine("5. Elimina aula studio");
                Console.WriteLine("6. Aumenta capienza aula");
                Console.WriteLine("0. Esci");

                int scelta = InputHelper.LeggiIntero("Seleziona un'opzione: ");

                switch (scelta)
                {
                    case 1:
                        VisualizzaTutteLePrenotazioni();
                        break;
                    case 2:
                        EliminaPrenotazione();
                        break;
                    case 3:
                        VisualizzaTutteLeAule();
                        break;
                    case 4:
                        AggiungiAula();
                        break;
                    case 5:
                        EliminaAula();
                        break;
                    case 6:
                        AumentaCapienzaAula();
                        break;
                    case 0:
                        continua = false;
                        Console.WriteLine("Uscita dal menu amministratore.");
                        break;
                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;
                }
            }
        }

        private void VisualizzaTutteLePrenotazioni()
        {
            Console.WriteLine();
            Console.WriteLine("=== TUTTE LE PRENOTAZIONI ===");

            List<Prenotazione> prenotazioni = _prenotazioneService.OttieniTutteLePrenotazioni();

            if (!prenotazioni.Any())
            {
                Console.WriteLine("Non ci sono prenotazioni.");
                return;
            }

            foreach (var prenotazione in prenotazioni)
            {
                int postiDisponibili = _prenotazioneService.CalcolaPostiDisponibili(
                    prenotazione.AulaId,
                    prenotazione.Giorno,
                    prenotazione.FasciaOraria);

                Console.WriteLine(
                    $"ID: {prenotazione.Id} | " +
                    $"Studente: {prenotazione.NomeStudente} | " +
                    $"Aula: {prenotazione.NomeAula} | " +
                    $"Giorno: {prenotazione.Giorno:dd/MM/yyyy} | " +
                    $"Fascia: {prenotazione.FasciaOraria} | " +
                    $"Posti prenotati: {prenotazione.PostiRichiesti} | " +
                    $"Posti disponibili: {postiDisponibili}");
            }
        }

        private void EliminaPrenotazione()
        {
            Console.WriteLine();
            Console.WriteLine("=== ELIMINA PRENOTAZIONE ===");

            VisualizzaTutteLePrenotazioni();

            int idPrenotazione = InputHelper.LeggiIntero("Inserisci ID prenotazione da eliminare: ");
            _prenotazioneService.CancellaPrenotazioneAdmin(idPrenotazione, out string messaggio);

            Console.WriteLine(messaggio);
        }

        private void VisualizzaTutteLeAule()
        {
            Console.WriteLine();
            Console.WriteLine("=== TUTTE LE AULE ===");

            List<AulaStudio> aule = _aulaService.OttieniTutte();

            if (!aule.Any())
            {
                Console.WriteLine("Non ci sono aule disponibili.");
                return;
            }

            foreach (var aula in aule)
            {
                Console.WriteLine($"ID: {aula.Id} | Nome: {aula.Nome} | Capienza: {aula.Capienza} posti");
            }
        }

        private void AggiungiAula()
        {
            Console.WriteLine();
            Console.WriteLine("=== AGGIUNGI AULA ===");

            string nome = InputHelper.LeggiStringa("Inserisci il nome della nuova aula: ");
            int capienza = InputHelper.LeggiInteroPositivo("Inserisci la capienza massima: ");

            _aulaService.AggiungiAula(nome, capienza, out string messaggio);
            Console.WriteLine(messaggio);
        }

        private void EliminaAula()
        {
            Console.WriteLine();
            Console.WriteLine("=== ELIMINA AULA ===");

            VisualizzaTutteLeAule();

            int aulaId = InputHelper.LeggiIntero("Inserisci ID aula da eliminare: ");

            AulaStudio? aula = _aulaService.OttieniPerId(aulaId);
            if (aula == null)
            {
                Console.WriteLine("Aula non trovata.");
                return;
            }

            _prenotazioneService.CancellaPrenotazioniPerAula(aulaId);
            _aulaService.EliminaAula(aulaId, out string messaggio);

            Console.WriteLine(messaggio);
            Console.WriteLine("Sono state eliminate anche le prenotazioni associate all'aula.");
        }

        private void AumentaCapienzaAula()
        {
            Console.WriteLine();
            Console.WriteLine("=== AUMENTA CAPIENZA AULA ===");

            VisualizzaTutteLeAule();

            int aulaId = InputHelper.LeggiIntero("Inserisci ID aula: ");
            int nuovaCapienza = InputHelper.LeggiInteroPositivo("Inserisci la nuova capienza: ");

            _aulaService.AumentaCapienza(aulaId, nuovaCapienza, out string messaggio);
            Console.WriteLine(messaggio);
        }
    }
}
