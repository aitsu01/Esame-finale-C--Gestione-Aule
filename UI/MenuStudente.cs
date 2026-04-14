using PrenotazioneAuleStudio.Models;
using PrenotazioneAuleStudio.Services;

namespace PrenotazioneAuleStudio.UI
{
    public class MenuStudente
    {
        private readonly Studente _studente;
        private readonly List<AulaStudio> _aule;
        
        private readonly PrenotazioneService _prenotazioneService;

        public MenuStudente(Studente studente, List<AulaStudio> aule, PrenotazioneService prenotazioneService)
        {
            _studente = studente;
            _aule = aule;
            _prenotazioneService = prenotazioneService;
        }

        public void Avvia()
        {
            bool continua = true;

            while (continua)
            {
                Console.WriteLine();
                Console.WriteLine($"=== MENU STUDENTE - {_studente.Nome} ===");
                Console.WriteLine("1. Prenota aula studio");
                Console.WriteLine("2. Visualizza le mie prenotazioni");
                Console.WriteLine("3. Modifica una prenotazione");
                Console.WriteLine("4. Cancella una prenotazione");
                Console.WriteLine("0. Esci");

                int scelta = InputHelper.LeggiIntero("Seleziona un'opzione: ");

                switch (scelta)
                {
                    case 1:
                        PrenotaAula();
                        break;
                    case 2:
                        VisualizzaPrenotazioni();
                        break;
                    case 3:
                        ModificaPrenotazione();
                        break;
                    case 4:
                        CancellaPrenotazione();
                        break;
                    case 0:
                        continua = false;
                        Console.WriteLine("Uscita dal menu studente.");
                        break;
                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;
                }
            }
        }

        private void PrenotaAula()
        {
            Console.WriteLine();
            Console.WriteLine("=== PRENOTA AULA ===");

            MostraAule();

            int idAula = InputHelper.LeggiIntero("Inserisci ID aula: ");
            AulaStudio? aulaSelezionata = _aule.FirstOrDefault(a => a.Id == idAula);

            if (aulaSelezionata == null)
            {
                Console.WriteLine("Aula non trovata.");
                return;
            }

            DateTime giorno = InputHelper.LeggiData("Inserisci il giorno (gg/mm/aaaa): ");
            string fascia = SelezionaFasciaOraria();
            int postiRichiesti = InputHelper.LeggiInteroPositivo("Inserisci il numero di posti richiesti: ");

            bool esito = _prenotazioneService.Prenota(
                _studente,
                aulaSelezionata,
                giorno,
                fascia,
                postiRichiesti,
                out string messaggio);

            Console.WriteLine(messaggio);
        }

        private void VisualizzaPrenotazioni()
        {
            Console.WriteLine();
            Console.WriteLine("=== LE MIE PRENOTAZIONI ===");

            List<Prenotazione> prenotazioni = _prenotazioneService.OttieniPrenotazioniStudente(_studente.Id);

            if (!prenotazioni.Any())
            {
                Console.WriteLine("Non hai prenotazioni.");
                return;
            }

            foreach (var prenotazione in prenotazioni)
            {
                Console.WriteLine(prenotazione);
            }
        }

        private void ModificaPrenotazione()
        {
            Console.WriteLine();
            Console.WriteLine("=== MODIFICA PRENOTAZIONE ===");

            List<Prenotazione> prenotazioni = _prenotazioneService.OttieniPrenotazioniStudente(_studente.Id);

            if (!prenotazioni.Any())
            {
                Console.WriteLine("Non hai prenotazioni da modificare.");
                return;
            }

            foreach (var prenotazione in prenotazioni)
            {
                Console.WriteLine(prenotazione);
            }

            int idPrenotazione = InputHelper.LeggiIntero("Inserisci ID prenotazione da modificare: ");
            DateTime nuovoGiorno = InputHelper.LeggiData("Inserisci il nuovo giorno (gg/mm/aaaa): ");
            string nuovaFascia = SelezionaFasciaOraria();
            int nuoviPosti = InputHelper.LeggiInteroPositivo("Inserisci il nuovo numero di posti richiesti: ");

            bool esito = _prenotazioneService.ModificaPrenotazione(
                idPrenotazione,
                _studente.Id,
                nuovoGiorno,
                nuovaFascia,
                nuoviPosti,
                out string messaggio);

            Console.WriteLine(messaggio);
        }

        private void CancellaPrenotazione()
        {
            Console.WriteLine();
            Console.WriteLine("=== CANCELLA PRENOTAZIONE ===");

            List<Prenotazione> prenotazioni = _prenotazioneService.OttieniPrenotazioniStudente(_studente.Id);

            if (!prenotazioni.Any())
            {
                Console.WriteLine("Non hai prenotazioni da cancellare.");
                return;
            }

            foreach (var prenotazione in prenotazioni)
            {
                Console.WriteLine(prenotazione);
            }

            int idPrenotazione = InputHelper.LeggiIntero("Inserisci ID prenotazione da cancellare: ");

            bool esito = _prenotazioneService.CancellaPrenotazione(idPrenotazione, _studente.Id, out string messaggio);
            Console.WriteLine(messaggio);
        }

        private void MostraAule()
        {
            Console.WriteLine("Aule disponibili:");
            foreach (var aula in _aule)
            {
                Console.WriteLine($"{aula.Id}. {aula.Nome} - Capienza: {aula.Capienza} posti");
            }
        }

        private void MostraFasceOrarie()
        {
            Console.WriteLine("Fasce orarie disponibili:");
            for (int i = 0; i < _prenotazioneService.FasceOrarieDisponibili.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {_prenotazioneService.FasceOrarieDisponibili[i]}");
            }
        }

        private string SelezionaFasciaOraria()
        {
            MostraFasceOrarie();

            int scelta;
            do
            {
                scelta = InputHelper.LeggiIntero("Seleziona una fascia oraria: ");
            }
            while (scelta < 1 || scelta > _prenotazioneService.FasceOrarieDisponibili.Count);

            return _prenotazioneService.FasceOrarieDisponibili[scelta - 1];
        }


    }
}