using System.Collections.Generic;
using copy_counterparty_app.Domain;

namespace copy_counterparty_app.Search
{
    public class SearchResult
    {
        public IEnumerable<Counterparty> Items { get; set; }

        public int TotalCount { get; set; }

        public int FilteredCount { get; set; }
    }
}
