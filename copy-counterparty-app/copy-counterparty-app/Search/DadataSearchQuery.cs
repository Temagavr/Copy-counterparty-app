using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Search
{
    public class DadataSearchQuery
    {
        public string query { get; set; }

        public DadataSearchQuery(string str)
        {
            query = str;
        }
    }
}
