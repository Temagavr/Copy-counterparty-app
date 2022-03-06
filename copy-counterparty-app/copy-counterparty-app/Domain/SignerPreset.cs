using copy_counterparty_app.Domain.Shared;

namespace copy_counterparty_app.Domain
{
    public class SignerPreset
    {
        public int CounterpartyId { get; private set; }

        public Signer Value { get; private set; }

        public SignerPreset( Signer signer )
        {
            Value = signer;
        }
    }
}
