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
        public string Name { get; set; }

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

        protected string TypeNameNominative { get; set; }
        protected string TypeNameGenitive { get; set; }

        /// <summary>
        /// Адрес средства размещения
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Url сайта
        /// </summary>
        public string SiteUrl { get; set; }

        /// <summary>
        /// Идентификатор в системе TL
        /// </summary>
        public int? TlId { get; set; }

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

        public bool Equals(Accommodation accommodation)
        {
            if (Name != accommodation.Name)
                return false;

            if (TypeNameNominative != accommodation.TypeNameNominative)
                return false;

            if (TypeNameGenitive != accommodation.TypeNameGenitive)
                return false;

            if (Address != accommodation.Address)
                return false;

            if (SiteUrl != accommodation.SiteUrl)
                return false;

            if (TlId != accommodation.TlId)
                return false;

            return true;
        }
    }
}
