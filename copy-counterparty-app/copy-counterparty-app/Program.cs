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

            OldGenData oldData = new OldGenData();
            await oldData.ParseOldGenData(fileName);

            Console.WriteLine(oldData.oldGenCounterparties[1].Short_Name);
            Console.WriteLine(oldData.oldGenCounterparties[1].Postal_Address);
            Console.WriteLine(oldData.oldGenCounterparties[1].Legal_Entity_Id);
            Console.WriteLine(oldData.oldGenCounterparties[1].Main_Email);
            Console.WriteLine(oldData.oldGenCounterparties[1].Inn);

            /*
            MyHttpClient client = new MyHttpClient();
            
            Counterparty counterparty = await client.GetCounterpartyByIdFromNewGen(70);

            await client.AddCounterpartyToNewGen(counterparty);
            */
        }
    }
}
