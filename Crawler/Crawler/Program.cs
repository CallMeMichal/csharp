namespace Crawler
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException();
            }

            string url = args[0];

            // zadanie http to klasa wbudowane
            // ctr+kc comment
            // ctr+ku uncomment


            //stara wersja bez using
            HttpClient client = new();
            client.Dispose(); //zwalnianie miejsca

            //zrobienie scopa automatycznie uzywa sie dispose , zwoli sie automatycznie poprzez uzycie using
            using HttpClient httpClient = new();

            //asynchroniczna metoda do zwracania czegos, zwracaja taska . await odpakowywuje dane ktore wracają . jak chcemy uzywac metod asynchronicznych to dodajemy async do maina obok void
            // w asynchronicznosci nie ma blokowania głownego wątku
            // poprzez await rozpakowywuje sie i dotaje do srodka zapytania 
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string content = await result.Content.ReadAsStringAsync();

           


        }
    }
}