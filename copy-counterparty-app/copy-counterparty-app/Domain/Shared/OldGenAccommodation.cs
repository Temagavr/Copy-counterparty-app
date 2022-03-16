using copy_counterparty_app.Grammatical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Domain.Shared
{
    public class OldGenAccommodation
    {
        public int Accommodation_Id { get; set; }

        public int Legal_Entity_Id { get; set; }

        /// <summary>
        /// Название средства размещения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип средства размещения
        /// </summary>
        
        public string Type_Name_Nominative { get; set; }
        public string Type_Name_Genitive { get; set; }

        /// <summary>
        /// Адрес средства размещения
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Url сайта
        /// </summary>
        public string Site_Url { get; set; }

        /// <summary>
        /// Идентификатор в системе TL
        /// </summary>
        public int? Tl_Id { get; set; }

        public OldGenAccommodation(
            int accommodationId,
            int entityId,
            string name,
            string nominative,
            string genitive,
            string address,
            string siteUrl,
            int? tlId)
        {
            Accommodation_Id = accommodationId;
            Legal_Entity_Id = entityId;
            Name = name;
            Type_Name_Nominative = nominative;
            Type_Name_Genitive = genitive;
            Address = address;
            Site_Url = siteUrl;
            Tl_Id = tlId;
        }

    }
}
