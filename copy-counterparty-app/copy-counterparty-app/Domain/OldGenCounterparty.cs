using copy_counterparty_app.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Domain
{
    public class OldGenCounterparty
    {
        // Id текущего контрагента
        public int Legal_Entity_Id { get; set; }

        /// <summary>
        /// Id старого контрагента
        /// </summary>
        public int? Old_Legal_Entity_Id { get; set; }

        /// <summary>
        /// Тип контрагента
        /// </summary>
        //public PartyType Type { get; set; } // Нет такого поля в старом генераторе 

        /// <summary>
        /// Краткое название контрагента
        /// </summary>
        public string Short_Name { get; set; }

        /// <summary>
        /// Юридический адрес контрагента
        /// </summary>
        public string Legal_Address { get; set; }

        /// <summary>
        /// Почтовый адрес контрагента
        /// </summary>
        public string Postal_Address { get; set; }

        /// <summary>
        /// Телефонный номер
        /// </summary>
        public string Phone_Number { get; set; }

        /// <summary>
        /// Основной email
        /// </summary>
        public string Main_Email { get; set; }

        /// <summary>
        /// Email для направления бух. документов
        /// </summary>
        public string Bookkeeping_Email { get; set; }

        /// <summary>
        /// Email для направления реестров платежей
        /// </summary>
        public string Payment_Registers_Email { get; set; }

        /// <summary>
        /// ИНН (Идентификационный номер налогоплательщика)
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// КПП (Код причины постановки)
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// ОГРН (Основной государственный регистрационный номер)
        /// </summary>
        public string Ogrn { get; set; }

        /// <summary>
        /// Признак определяющий, является ли контрагент бюджетной организацией
        /// </summary>
        public bool Is_Budgetary_Institution { get; set; }

        public List<OldGenAccommodation> accommodations { get; set; }

        public OldGenCounterparty()
        {
        }
    }
}
