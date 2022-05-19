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
using copy_counterparty_app.OldGen;
using System.Net.Http.Headers;
using copy_counterparty_app.Dtos;

namespace copy_counterparty_app
{
    public class MyHttpClient
    {
        private HttpClient _client = new HttpClient();

        private const string _baseUrl = "http://localhost:4200/api/v1/counterparties";

        private const string _counterpartiesListPath = _baseUrl + "/{0}";
        private const string _counterpartiesSearchPath = _baseUrl + "/search";
        private const string _counterpartiesAddPath = _baseUrl + "/create";
        private const string _counterpartiesBindPath = _baseUrl + "/bind";
        private const string _counterpartyAddAccommodationPath = _baseUrl + "/{0}/accommodation-presets/create";
        private const string _counterpartyAddBankDetailsPath = _baseUrl + "/{0}/bank-details-presets/create";
        private const string _counterpartyAddSignerPath = _baseUrl + "/{0}/signer-presets/create";

        private const string _dadataSearchCounterpartyPath = "https://dadata.ru/api/v2/suggest/party";

        public MyHttpClient(string email, string token)
        {
            _client.DefaultRequestHeaders.Add("Cookie",$"Email={email};Token={token}");
        }

        public async Task<Counterparty> GetCounterpartyByIdFromNewGen(int? id)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(string.Format(_counterpartiesListPath, id));
            Counterparty counterparty = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                counterparty = ParseCounterparty(responseMessage);
                ShowCounterparty(counterparty);
            }

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

        private async Task<bool> IsExistCounterpartyInNewGenByInn(Counterparty counterparty)
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

        private async Task<Counterparty> GetCounterpartyByInnFromNewGen(string inn)
        {
            SearchPattern searchPattern = new SearchPattern(inn);
            string jsonSearchPattern = JsonConvert.SerializeObject(searchPattern);

            var httpContent = new StringContent(jsonSearchPattern, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_counterpartiesSearchPath, httpContent);

            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(jsonResponse);

            if (searchResult.FilteredCount > 0)
            {
                return searchResult.Items.First();
            }

            return null;
        }

        public async Task AddCounterpartyToNewGen(Counterparty counterparty)
        {
            if (!await IsExistCounterpartyInNewGenByInn(counterparty))
            {
                int? oldCounterpartyId = null;

                

                CreateCounterpartyDto createCounterpartyDto = new CreateCounterpartyDto(
                    TypeToString[((int)counterparty.Type)],
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
                    null);

                string jsonCounterparty = JsonConvert.SerializeObject(createCounterpartyDto);

                var httpContent = new StringContent(jsonCounterparty, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_counterpartiesAddPath, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"\nКонтрагент {counterparty.ShortName} успешно добавлен!");

                    Counterparty newCounterparty = await GetCounterpartyByInnFromNewGen(counterparty.Inn);

                    if (counterparty.OldCounterpartyId != null)
                    {
                        var oldCounterparty = OldGenData.GetCounterpartyById(counterparty.OldCounterpartyId).Map();

                        if (!await IsExistCounterpartyInNewGenByInn(oldCounterparty))
                        {
                            //сначала добавление старого контрагента если он есть у текущего контрагента и при этом если его нет в базе 
                            await AddCounterpartyToNewGen(oldCounterparty);
                            var newOldCounterparty = await GetCounterpartyByInnFromNewGen(oldCounterparty.Inn);

                            if (newOldCounterparty != null)
                                oldCounterpartyId = newOldCounterparty.Id;
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            var newOldCounterparty = await GetCounterpartyByInnFromNewGen(oldCounterparty.Inn);
                            oldCounterpartyId = newOldCounterparty.Id;
                        }
                    }

                    // Добавление связи со страым контрагентом
                    if (oldCounterpartyId != null) 
                        if(await BindCounterparties(newCounterparty.Id, oldCounterpartyId))
                            // получение новых данных при успешном связывании 
                            newCounterparty = await GetCounterpartyByInnFromNewGen(counterparty.Inn);

                    //Добавление средств размещения к контрагенту в новом генераторе 
                    await AddAllAccommodations(newCounterparty, counterparty);

                    //Добавление банковских реквизитов к контрагенту в новом генераторе 
                    await AddAllBankDetails(newCounterparty, counterparty);

                    //Добавление подписантов к контрагенту в новом генераторе 
                    await AddAllSigners(newCounterparty, counterparty);
                }
                else
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;
                    ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);

                    Console.WriteLine($"\nОшибка при добавлении контрагента {counterparty.ShortName}, причина - {errorResponse.Details}!");

                    // при ошибке по огрн подтянуть новые данные(огрн, адреса и название) с стороннего сервиса по ИНН и попытаться добавить снова
                    DadataSearchResult actualData = await SearchInfoOnDadata(counterparty.Inn);
                    Counterparty updatedCounterparty = UpdateCounterpartyInfo(actualData, counterparty);

                    if (updatedCounterparty != null)
                        await AddCounterpartyToNewGen(updatedCounterparty);
                }
            }
            else
            {
                Console.WriteLine($"\nКонтрагент {counterparty.ShortName} уже существует!!!");

                Counterparty newCounterparty = await GetCounterpartyByInnFromNewGen(counterparty.Inn);

                //Добавление средств размещения к контрагенту в новом генераторе 
                await AddAllAccommodations(newCounterparty, counterparty);

                //Добавление банковских реквизитов к контрагенту в новом генераторе 
                await AddAllBankDetails(newCounterparty, counterparty);

                //Добавление подписантов к контрагенту в новом генераторе 
                await AddAllSigners(newCounterparty, counterparty);
            }
        }

        private Counterparty UpdateCounterpartyInfo(DadataSearchResult result, Counterparty counterparty)
        {
            if (result != null)
            {
                foreach (DadataSearchResultItem item in result.Suggestions)
                {
                    if (item.Data.Ogrn.Contains(counterparty.Ogrn))
                    {
                        Counterparty updatedCounterparty = counterparty.Copy(counterparty);

                        updatedCounterparty.ShortName = item.Value;
                        updatedCounterparty.Ogrn = item.Data.Ogrn;
                        updatedCounterparty.LegalAddress = item.Data.Address.Unrestricted_Value;

                        if (item.Data.Ogrn == counterparty.Ogrn)
                        {
                            Console.WriteLine("\nНевозможно добавить контрагента");

                            return null; // возвращаю null чтобы при повторной ошибке не упасть в бесконечную рекурсию
                        }

                        return updatedCounterparty;
                    }
                }
            }

            return null;
        }
        private async Task<DadataSearchResult> SearchInfoOnDadata(string inn)
        {
            Console.WriteLine("Загружаем актуальные данные для контрагента");

            DadataSearchQuery query = new DadataSearchQuery(inn);
            string jsonQuery = JsonConvert.SerializeObject(query);
            const string daDataToken = "db843b54d0653886c2d97189c8b0f8ee1ca71405";

            using (var request = new HttpRequestMessage(HttpMethod.Post, _dadataSearchCounterpartyPath))
            {

                var httpContent = new StringContent(jsonQuery, Encoding.UTF8, "application/json");

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", daDataToken);

                HttpResponseMessage response = await _client.PostAsync(_dadataSearchCounterpartyPath, httpContent);

                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                DadataSearchResult searchResult = JsonConvert.DeserializeObject<DadataSearchResult>(jsonResponse);

                if (searchResult.Suggestions.Count > 0)
                {
                    return searchResult;
                }

                return null;
            }
        }
        private async Task<bool> BindCounterparties(int newId, int? oldId)
        {
            BindCounterpartiesDto bindCounterpartiesDto = new BindCounterpartiesDto(newId, oldId);

            string jsonData = JsonConvert.SerializeObject(bindCounterpartiesDto);

            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_counterpartiesBindPath, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Контрагенту id = {newId} успешно присвоен родитель id = {oldId}!");
                return true;
            }
            else
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);

                Console.WriteLine($"Ошибка при присвоении родителя контрагенту id = {newId}, причина - {errorResponse.Details}!");
                
                return false;
            }
        }
        private async Task AddAllAccommodations(Counterparty counterparty, Counterparty oldCounterpartyData)
        {
            if (oldCounterpartyData.AccommodationPresets.Count > 0)
            {
                foreach (AccommodationPreset accommodation in oldCounterpartyData.AccommodationPresets)
                {
                    if (!counterparty.ContainsAccommodationPreset(accommodation.Value))
                        await AddAccommodationToCounterparty(counterparty.Id, accommodation);
                    else
                        Console.WriteLine($"У контрагента уже есть ср-во размещения {accommodation.Value.Name}");
                }
            }
        }
        private async Task AddAllBankDetails(Counterparty counterparty, Counterparty oldCounterpartyData)
        {
            if (oldCounterpartyData.BankPresets.Count > 0)
            {
                foreach (BankDetailsPreset bankDetails in oldCounterpartyData.BankPresets)
                {
                    if (!counterparty.ContainsBankDetailsPreset(bankDetails.Value))
                        await AddBankDetailsToCounterparty(counterparty.Id, bankDetails);
                    else
                        Console.WriteLine($"У контрагента уже есть реквизиты {bankDetails.Value.Name}");
                }
            }
        }
        private async Task AddAllSigners(Counterparty counterparty, Counterparty oldCounterpartyData)
        {
            if (oldCounterpartyData.SignerPresets.Count > 0)
            {
                foreach (SignerPreset signer in oldCounterpartyData.SignerPresets)
                {
                    if (!counterparty.ContainsSignerPreset(signer.Value))
                        await AddSignerToCounterparty(counterparty.Id, signer);
                    else
                        Console.WriteLine($"У контрагента уже есть подписант {signer.Value.FullName.Nominative}");
                }
            }
        }
        private async Task AddAccommodationToCounterparty(int counterpartyId, AccommodationPreset accommodation)
        {
            string requestString = string.Format(_counterpartyAddAccommodationPath, counterpartyId);

            string jsonAccommodation = JsonConvert.SerializeObject(accommodation.Value);

            var httpContent = new StringContent(jsonAccommodation, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(requestString, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Средство размещения {accommodation.Value.Name} успешно добавлено к контрагенту(id = {counterpartyId})!");
            }
            else
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);

                Console.WriteLine($"Ошибка при добавлении средства размещения {accommodation.Value.Name}, причина - {errorResponse.Details}!");
            }
        }
        private async Task AddBankDetailsToCounterparty(int counterpartyId, BankDetailsPreset bankDetails)
        {
            string requestString = string.Format(_counterpartyAddBankDetailsPath, counterpartyId);

            string jsonBankDetails = JsonConvert.SerializeObject(bankDetails.Value);

            var httpContent = new StringContent(jsonBankDetails, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(requestString, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Банковские реквизиты {bankDetails.Value.Name} успешно добавлены к контрагенту(id = {counterpartyId})!");
            }
            else
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);

                Console.WriteLine($"Ошибка при добавлении банковских реквизитов {bankDetails.Value.Name}, причина - {errorResponse.Details}!");
            }
        }
        private async Task AddSignerToCounterparty(int counterpartyId, SignerPreset signer)
        {
            string requestString = string.Format(_counterpartyAddSignerPath, counterpartyId);

            string jsonSigner = JsonConvert.SerializeObject(signer.Value);

            var httpContent = new StringContent(jsonSigner, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(requestString, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Подписант {signer.Value.FullName.Nominative} успешно добавлен к контрагенту(id = {counterpartyId})!");
            }
            else
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);

                Console.WriteLine($"Ошибка при добавлении подписанта {signer.Value.FullName.Nominative}, причина - {errorResponse.Details}!");
            }
        }

        private Dictionary<int, string> TypeToString = new Dictionary<int, string>()
        {
            { 1, "ArtificialPerson" },
            { 2, "IndividualEntrepreneur" }
        };
    }
}
