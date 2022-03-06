using copy_counterparty_app.Domain.Shared;

namespace copy_counterparty_app.Domain
{
    public class BankDetailsPreset
    {
        public int CounterpartyId { get; private set; }

        public BankDetails Value { get; private set; }

        public BankDetailsPreset( BankDetails bankDetails )
        {
            Value = bankDetails;
        }

    }
}
