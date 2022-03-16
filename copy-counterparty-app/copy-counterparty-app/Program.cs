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
            const string fileName = "legal_entity.json";

            Console.WriteLine("Program started");

            OldGenData.ParseOldGenData(fileName);

            MyHttpClient client = new MyHttpClient();
            
            for(int i = 2; i < 8; ++i)
            {
                Counterparty counterparty = OldGenData.oldGenCounterparties[i].Map();

                await client.AddCounterpartyToNewGen(counterparty);
            }
            
        }
    }
}
