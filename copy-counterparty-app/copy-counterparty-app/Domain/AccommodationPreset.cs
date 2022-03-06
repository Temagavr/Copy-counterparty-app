using copy_counterparty_app.Domain.Shared;

namespace copy_counterparty_app.Domain
{
    public class AccommodationPreset
    {
        public int CounterpartyId { get; set; }

        public Accommodation Value { get; set; }

        public AccommodationPreset( Accommodation accommodation )
        {
            Value = accommodation;
        }
    }
}
