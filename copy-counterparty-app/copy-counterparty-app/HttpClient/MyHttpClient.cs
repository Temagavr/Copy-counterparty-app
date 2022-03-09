using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using copy_counterparty_app.Domain;
using copy_counterparty_app.Search;
using System.IO;
using Newtonsoft.Json;

namespace copy_counterparty_app
{
    public class MyHttpClient
    {
        private HttpClient _client = new HttpClient();

        private const string _counterpartiesListPath = "http://localhost:4200/api/v1/counterparties/4";
        private const string _counterpartiesSearchPath = "http://localhost:4200/api/v1/counterparties/search";
        private const string _counterpartiesAddPath = "http://localhost:4200/api/v1/counterparties/create";

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

        private async Task<bool> IsExistCounterparty(Counterparty counterparty)
        {
            SearchPattern searchPattern = new SearchPattern(counterparty.Inn);
            string jsonSearchPattern = JsonConvert.SerializeObject(searchPattern);

            var httpContent = new StringContent(jsonSearchPattern, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_counterpartiesSearchPath, httpContent);

            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(jsonResponse);

            if (searchResult.FilteredCount == 0 && !searchResult.Items.Contains(counterparty))
                return false;

            return true;
        }

        private async Task<Counterparty> GetCounterpartyByInn(string inn)
        {
            SearchPattern searchPattern = new SearchPattern(inn);
            string jsonSearchPattern = JsonConvert.SerializeObject(searchPattern);

            var httpContent = new StringContent(jsonSearchPattern, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_counterpartiesSearchPath, httpContent);

            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(jsonResponse);

            return searchResult.Items.First();
        }

        public async Task AddCounterparty(Counterparty counterparty)
        {
            counterparty.Inn = "433333333222";

            if (!await IsExistCounterparty(counterparty))
            {
                int oldCounterpartyId = 55;

                if(counterparty.OldCounterpartyId != null)
                {
                    if (!await IsExistCounterparty(counterparty.OldCounterparty))
                    {
                        //сначала добавление старого контрагента если он есть у текущего контрагента и при этом если его нет в базе 
                    }
                    else
                    {
                        var oldCounterparty = await GetCounterpartyByInn(counterparty.Inn);
                        oldCounterpartyId = oldCounterparty.Id;
                    }
                }

                CreateCounterpartyDto createCounterpartyDto = new CreateCounterpartyDto(
                    counterparty.Type,
                    counterparty.ShortName,
                    counterparty.LegalAddress,
                    counterparty.PostalAddress,
                    counterparty.PhoneNumber,
                    counterparty.MainEmail,
                    counterparty.BookkeepingEmail,
                    counterparty.PaymentRegistersEmail,
                    counterparty.Inn,
                    counterparty.Kpp,
                    counterparty.Ogrn,
                    counterparty.IsBudgetaryInstitution,
                    oldCounterpartyId
                    );

                string jsonCounterparty = JsonConvert.SerializeObject(counterparty);

                var httpContent = new StringContent(jsonCounterparty, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_counterpartiesAddPath, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Контрагент успешно добавлен!");
                }
                else
                {
                    Console.WriteLine("Ошибка при добавлении!");
                }
            }
            else
            {
                Console.WriteLine("Контрагент уже существует!!!");
            }
        }
    }
}
