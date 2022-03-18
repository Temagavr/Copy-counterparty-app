using copy_counterparty_app.Domain;
using copy_counterparty_app.OldGen;
using System;
using System.IO;
using System.Threading.Tasks;

namespace copy_counterparty_app
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            const string fileCounterpartiesName = "legal_entity.json";
            const string fileAccommodationsName = "accommodation.json";
            const string fileSignersName = "signer.json";
            const string fileBanksName = "bank.json";

            Console.WriteLine("Program started");

            OldGenData.ParseOldGenCounterpartiesData(fileCounterpartiesName);
            OldGenData.ParseOldGenAccommodationsData(fileAccommodationsName);
            OldGenData.ParseOldGenSignersData(fileSignersName);
            OldGenData.ParseOldGenBanksData(fileBanksName);

            OldGenData.SetAccommodationsToCounterparties();
            OldGenData.SetSignersToCounterparties();
            OldGenData.SetBanksToCounterparties();
            
            MyHttpClient client = new MyHttpClient();
            
            for(int i = 0; i < 2; ++i)
            {
                Counterparty counterparty = OldGenData.oldGenCounterparties[i].Map();

                await client.AddCounterpartyToNewGen(counterparty);
            }

        }
    }
}
