﻿using System.Text.RegularExpressions;

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
            string content = await result.Content.ReadAsStringAsync();

           


        }
    }
}


// zadanie http to klasa wbudowane
// ctr+kc comment
// ctr+ku uncomment