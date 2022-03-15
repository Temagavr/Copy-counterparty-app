using copy_counterparty_app.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.OldGen
{
    public class OldGenData
    {
        public List<OldGenCounterparty> oldGenCounterparties { get; set; }

        public OldGenData() { }

        public async Task ParseOldGenData(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var reader = new StreamReader(fs);
                var text = reader.ReadToEnd();

                oldGenCounterparties = JsonConvert.DeserializeObject<List<OldGenCounterparty>>(text);
                Console.WriteLine($"Data count {oldGenCounterparties.Count}");
            }
        }
    }
}
