using System.Collections.Generic;
using copy_counterparty_app.Grammatical;

namespace copy_counterparty_app.Domain.Shared
{
    /// <summary>
    /// Банковские реквизиты
    /// </summary>
    public class BankDetails
    {
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
        public string CorrespondentAccount { get; set; }

        /// <summary>
        /// Признак, определяющий, относятся ли банковские реквизиты для расчета с бюджетной организацией
        /// </summary>
        public bool IsBudgetaryInstitution { get; set; }

        /// <summary>
        /// Расчетный счет. Заполняется и используется для небюджетных организаций
        /// </summary>
        public string SettlementAccount { get; set; }

        /// <summary>
        /// Код бюджетной классификации (КБК). Заполняется и используется для бюджетных организаций
        /// </summary>
        public string Kbk { get; set; }

        /// <summary>
        /// Лицевой счет, Наименование УФК. Заполняется и используется для бюджетных организаций
        /// </summary>
        public string PersonalAccount { get; set; }

        public BankDetails(
            string name,
            string bic,
            string correspondentAccount,
            bool isBudgetaryInstitution,
            string settlementAccount,
            string kbk,
            string personalAccount )
        {
            Name = name;
            Bic = bic;
            CorrespondentAccount = correspondentAccount;
            IsBudgetaryInstitution = isBudgetaryInstitution;
            SettlementAccount = settlementAccount;
            Kbk = kbk;
            PersonalAccount = personalAccount;
        }

        // Workaround for EF
        protected BankDetails()
        {
        }

        public BankDetails Copy()
        {
            return new BankDetails(
                Name,
                Bic,
                CorrespondentAccount,
                IsBudgetaryInstitution,
                SettlementAccount,
                Kbk,
                PersonalAccount );
        }

        public bool Equals(BankDetails bank)
        {
            if (Name != bank.Name)
                return false;

            if (Bic != bank.Bic)
                return false;

            if (CorrespondentAccount != bank.CorrespondentAccount)
                return false;

            if (IsBudgetaryInstitution != bank.IsBudgetaryInstitution)
                return false;

            if (SettlementAccount != bank.SettlementAccount)
                return false;

            if (Kbk != bank.Kbk)
                return false;

            if (PersonalAccount != bank.PersonalAccount)
                return false;

            return true;
        }
    }
}
