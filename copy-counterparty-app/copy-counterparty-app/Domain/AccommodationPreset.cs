using copy_counterparty_app.Domain.Shared;

namespace copy_counterparty_app.Domain
{
    public class AccommodationPreset
    {
        public int CounterpartyId { get; private set; }

        public Accommodation Value { get; private set; }

        public AccommodationPreset( Accommodation accommodation )
        {
            Value = accommodation;
        }
    }
}
