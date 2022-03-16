using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Domain.Shared
{
    public class OldGenBank
    {
        public int Bank_Id { get; set; }

        public int Legal_Entity_Id { get; set; }

        /// <summary>
        /// Название банка
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Банковский идентификационный код (БИК)
        /// </summary>
        public string Bic { get; set; }

        /// <summary>
        /// Корреспондентский счет
        /// </summary>
        public string Correspondent_Account { get; set; }

        /// <summary>
        /// Признак, определяющий, относятся ли банковские реквизиты для расчета с бюджетной организацией
        /// </summary>
        public bool IsBudgetaryInstitution { get; set; }

        /// <summary>
        /// Расчетный счет. Заполняется и используется для небюджетных организаций
        /// </summary>
        public string Settlement_Account { get; set; }

        /// <summary>
        /// Код бюджетной классификации (КБК). Заполняется и используется для бюджетных организаций
        /// </summary>
        public string Kbk { get; set; }

        /// <summary>
        /// Лицевой счет, Наименование УФК. Заполняется и используется для бюджетных организаций
        /// </summary>
        public string Personal_Account { get; set; }

        public OldGenBank(
            string name,
            string bic,
            string correspondentAccount,
            string settlementAccount,
            string kbk,
            string personalAccount)
        {
            Name = name;
            Bic = bic;
            Correspondent_Account = correspondentAccount;
            Settlement_Account = settlementAccount;
            Kbk = kbk;
            Personal_Account = personalAccount;
        }

    }
}
