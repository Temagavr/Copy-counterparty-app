using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using copy_counterparty_app.Domain;
using System.IO;
using Newtonsoft.Json;

namespace copy_counterparty_app
{
    public class MyHttpClient
    {
        private HttpClient _client = new HttpClient();

        private const string _counterpartiesListPath = "http://localhost:4200/api/v1/counterparties/1";
        public async Task<Counterparty> GetCounterparty()
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(_counterpartiesListPath);
            Counterparty counterparty = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                counterparty = ParseCounterparty(responseMessage);
            }
            ShowCounterparty(counterparty);

            return counterparty;
        } 

        private void ShowCounterparty(Counterparty counterparty)
        {
            Console.WriteLine($"{counterparty.ShortName}");
            Console.WriteLine($"{counterparty.Inn}");
            Console.WriteLine($"{counterparty.Ogrn}");
            Console.WriteLine($"{counterparty.MainEmail}");
            Console.WriteLine($"{counterparty.LegalAddress}");
        }

        private Counterparty ParseCounterparty(HttpResponseMessage responseMessage)
        {
            string jsonString = responseMessage.Content.ReadAsStringAsync().Result;
            Counterparty counterparty = JsonConvert.DeserializeObject<Counterparty>(jsonString);
            return counterparty;
        }
    }
}
