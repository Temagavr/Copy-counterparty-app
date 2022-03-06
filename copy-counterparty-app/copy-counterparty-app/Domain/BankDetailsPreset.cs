using copy_counterparty_app.Domain.Shared;

namespace copy_counterparty_app.Domain
{
    public class BankDetailsPreset
    {
        public int CounterpartyId { get; set; }

        public BankDetails Value { get; set; }

        public BankDetailsPreset( BankDetails bankDetails )
        {
            Value = bankDetails;
        }

    }
}
