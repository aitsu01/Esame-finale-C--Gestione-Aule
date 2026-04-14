using System.Globalization;

namespace PrenotazioneAuleStudio.UI
{
    public static class InputHelper
    {
        public static int LeggiIntero(string messaggio)
        {
            int valore;
            do
            {
                Console.Write(messaggio);
            }
            while (!int.TryParse(Console.ReadLine(), out valore));

            return valore;
        }

        public static string LeggiStringa(string messaggio)
        {
            string? valore;
            do
            {
                Console.Write(messaggio);
                valore = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(valore));

            return valore;
        }

        public static DateTime LeggiData(string messaggio)
        {
            DateTime data;
            do
            {
                Console.Write(messaggio);
            }
            while (!DateTime.TryParseExact(
                Console.ReadLine(),
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out data));

            return data;
        }

        public static int LeggiInteroPositivo(string messaggio)
        {
            int valore;
            do
            {
                Console.Write(messaggio);
            }
            while (!int.TryParse(Console.ReadLine(), out valore) || valore <= 0);

            return valore;
        }



    }
}