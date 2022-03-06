using System.Collections.Generic;
using copy_counterparty_app.Grammatical;

namespace copy_counterparty_app.Domain.Shared
{
    /// <summary>
    /// Данные средства размещения
    /// </summary>
    public class Accommodation
    {
        /// <summary>
        /// Название средства размещения
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Тип средства размещения
        /// </summary>
        public GrammaticalCases TypeName
        {
            get
            {
                return new GrammaticalCases( TypeNameNominative, TypeNameGenitive );
            }
        }

        protected string TypeNameNominative { get; private set; }
        protected string TypeNameGenitive { get; private set; }

        /// <summary>
        /// Адрес средства размещения
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Url сайта
        /// </summary>
        public string SiteUrl { get; private set; }

        /// <summary>
        /// Идентификатор в системе TL
        /// </summary>
        public int? TlId { get; private set; }

        public Accommodation(
            string name,
            GrammaticalCases typeName,
            string address,
            string siteUrl,
            int? tlId )
        {
            Name = name;
            TypeNameNominative = typeName.Nominative;
            TypeNameGenitive = typeName.Genitive;
            Address = address;
            SiteUrl = siteUrl;
            TlId = tlId;
        }

        // Workaround for EF
        protected Accommodation()
        {
        }

        public Accommodation Copy()
        {
            return new Accommodation(
                Name,
                TypeName,
                Address,
                SiteUrl,
                TlId );
        }

        protected IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return TypeName;
            yield return Address;
            yield return SiteUrl;
            yield return TlId;
        }
    }
}
