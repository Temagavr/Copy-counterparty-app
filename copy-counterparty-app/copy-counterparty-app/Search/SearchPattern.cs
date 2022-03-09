using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Search
{
    public class SearchPattern
    {
        private const int _pageNumber = 1;

        private const int _onPageCount = 10;

        public int PageNumber { get; set; }

        public int OnPageCount { get; set; }

        public string SearchString { get; set; }

        public SearchPattern(string searchString)
        {
            PageNumber = _pageNumber;
            OnPageCount = _onPageCount;
            SearchString = searchString;
        }
    }
}
