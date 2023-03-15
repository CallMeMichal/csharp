using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Crawler
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            //1wymaganie
            if (args.Length == 0)
            {
                throw new ArgumentNullException();
            }

            string url = args[0];
            //2 wymaganie
            url pattern = "https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)\r\n";
            Regex urlRegex = new(pattern);
            if (!urlRegex.IsMatch(url))
            {
                throw new ArgumentException();
            }



            //stara wersja bez using
            HttpClient client = new();
            client.Dispose(); //zwalnianie miejsca

            //zrobienie scopa automatycznie uzywa sie dispose , zwoli sie automatycznie poprzez uzycie using
            using HttpClient httpClient = new();

            //asynchroniczna metoda do zwracania czegos, zwracaja taska . await odpakowywuje dane ktore wracają . jak chcemy uzywac metod asynchronicznych to dodajemy async do maina obok void
            // w asynchronicznosci nie ma blokowania głownego wątku
            // poprzez await rozpakowywuje sie i dotaje do srodka zapytania 
            HttpResponseMessage result = await httpClient.GetAsync(url);
            //3 wymaganie
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("bląd podczas pobierania strony")
            }
            
            string content = await result.Content.ReadAsStringAsync();

            //await rozpakowywuje strina i odstaje sie do pliku
            string htmlcontent = await result.Content.ReadAsStringAsync();
            // ignor case zabezpiecza przed duza litera jak ktos sie pomyli
            string emailPattern = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";
            Regex emailRegex new(emailPattern,RegexOptions.IgnoreCase);

            //zwraca emaile ktore spelnily oczekiwania
            //4 wymaganie
            MatchCollection matchedEmails = emailRegex.Matches(htmlcontent)
                if(matchedEmails.Count == 0) 
            {
                throwe new("Nie znaleziono adresow email");
            }
                //5 wymaganie :unikalne adresy email
            HashSet<string> uniqueEmails = new();

            foreach(var email in matchedEmails)
            {
                uniqueEmails.Add(email.ToString());
            }

            foreach (var email in uniqueEmails) 
            {
                Console.WriteLine(email);
            }

        }
    }
}


// zadanie http to klasa wbudowane
// ctr+kc comment
// ctr+ku uncomment
//kluczyki properties szesciany metody

//https://developer.mozilla.org/en-US/docs/Web/HTTP/Status