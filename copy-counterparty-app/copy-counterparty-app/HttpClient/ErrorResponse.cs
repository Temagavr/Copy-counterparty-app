using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app
{
    public class ErrorResponse
    {
        public string Title { get; set; }
        public string Details { get; set; }

        public ErrorResponse(
            string title,
            string details)
        {
            Title = title;
            Details = details;
        }
    }
}
