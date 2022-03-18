using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Search
{
    public class DadataSearchResultItem
    {
        public string Value { get; set; }

        public DadataExtendedData Data { get; set; }

        
        public DadataSearchResultItem(
            string value,
            DadataExtendedData data)
        {
            Value = value;
            Data = data;
        }
    }
}
