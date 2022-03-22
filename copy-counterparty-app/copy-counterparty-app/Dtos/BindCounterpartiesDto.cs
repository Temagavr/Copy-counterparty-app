using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Dtos
{
    public class BindCounterpartiesDto
    {
        public int? OldCounterpartyId { get; set; }
        public int NewCounterpartyId { get; set; }

        public BindCounterpartiesDto(int newId, int? oldId)
        {
            OldCounterpartyId = oldId;
            NewCounterpartyId = newId;
        }
    }
}
