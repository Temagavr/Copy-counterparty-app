namespace copy_counterparty_app.Domain.Shared
{
    /// <summary>
    /// Тип организации
    /// </summary>
    public enum PartyType : byte
    {
        /// <summary>
        /// Юридическое лицо
        /// </summary>
        ArtificialPerson = 1,

        /// <summary>
        /// Индивидуальный предприниматель
        /// </summary>
        IndividualEntrepreneur = 2
    }
}
