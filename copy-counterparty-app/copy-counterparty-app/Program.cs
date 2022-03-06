using System;
using System.Threading.Tasks;

namespace copy_counterparty_app
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello");

            MyHttpClient client = new MyHttpClient();
            
            await client.GetCounterparty();
        }
    }
}
