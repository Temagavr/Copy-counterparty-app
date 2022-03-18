using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Search
{
    public class DadataAddress
    {
        public string Unrestricted_Value { get; set; }

        public DadataAddress(string value)
        {
            Unrestricted_Value = value;
        }
    }
}
