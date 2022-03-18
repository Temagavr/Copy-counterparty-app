using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Search
{
    public class DadataSearchResult
    {
        public List<DadataSearchResultItem> Suggestions { get; set; }

        public DadataSearchResult(List<DadataSearchResultItem> items)
        {
            Suggestions = items;
        }
    }
}
