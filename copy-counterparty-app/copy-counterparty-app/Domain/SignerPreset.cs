using copy_counterparty_app.Domain.Shared;

namespace copy_counterparty_app.Domain
{
    public class SignerPreset
    {
        public int CounterpartyId { get; set; }

        public Signer Value { get; set; }

        public SignerPreset( Signer signer )
        {
            Value = signer;
        }
    }
}
