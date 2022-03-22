using copy_counterparty_app.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Domain
{
    public class CreateCounterpartyDto
    {
        public string Type { get; set; }

        public string ShortName { get; set; }

        public string LegalAddress { get; set; }

        public string PostalAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string MainEmail { get; set; }

        public string BookkeepingEmail { get; set; }

        public string PaymentRegistersEmail { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Ogrn { get; set; }

        public bool IsBudgetaryInstitution { get; set; }

        public int? OldCounterpartyId { get; set; }

        public CreateCounterpartyDto(
            string type,
            string shortName,
            string legalAddress,
            string postalAddress,
            string phoneNumber,
            string mainEmail,
            string bookkeepingEmail,
            string paymentRegistersEmail,
            string inn,
            string kpp,
            string ogrn,
            bool isBudgetaryInstitution,
            int? oldCounterpartyId = null)
        {
            Type = type;
            ShortName = shortName;
            LegalAddress = legalAddress;
            PostalAddress = postalAddress;
            PhoneNumber = phoneNumber;
            MainEmail = mainEmail;
            BookkeepingEmail = bookkeepingEmail;
            PaymentRegistersEmail = paymentRegistersEmail;
            Inn = inn;
            Kpp = kpp;
            Ogrn = ogrn;
            IsBudgetaryInstitution = isBudgetaryInstitution;
            OldCounterpartyId = oldCounterpartyId;
        }
    }
}
