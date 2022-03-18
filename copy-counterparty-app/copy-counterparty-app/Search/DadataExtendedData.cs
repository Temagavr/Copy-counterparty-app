using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Search
{
    public class DadataExtendedData
    {
        public string Ogrn { get; set; }

        public DadataAddress Address { get; set; }

        public DadataExtendedData(string ogrn, DadataAddress address)
        {
            Ogrn = ogrn;
            Address = address;
        }
    }
}
