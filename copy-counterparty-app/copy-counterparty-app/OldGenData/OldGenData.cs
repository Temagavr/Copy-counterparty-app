using copy_counterparty_app.Domain;
using copy_counterparty_app.Domain.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.OldGen
{
    public static class OldGenData
    {
        public static List<OldGenCounterparty> oldGenCounterparties { get; set; }
        public static List<OldGenAccommodation> oldGenAccommodations { get; set; }
        public static List<OldGenSigner> oldGenSigners { get; set; }
        public static List<OldGenBank> oldGenBanks { get; set; }

        public static void ParseOldGenCounterpartiesData(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var reader = new StreamReader(fs);
                var text = reader.ReadToEnd();

                oldGenCounterparties = JsonConvert.DeserializeObject<List<OldGenCounterparty>>(text);
            }
        }

        public static void ParseOldGenAccommodationsData(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var reader = new StreamReader(fs);
                var text = reader.ReadToEnd();

                oldGenAccommodations = JsonConvert.DeserializeObject<List<OldGenAccommodation>>(text);
            }
        }

        public static void ParseOldGenSignersData(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var reader = new StreamReader(fs);
                var text = reader.ReadToEnd();

                oldGenSigners = JsonConvert.DeserializeObject<List<OldGenSigner>>(text);
            }
        }

        public static void ParseOldGenBanksData(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var reader = new StreamReader(fs);
                var text = reader.ReadToEnd();

                oldGenBanks = JsonConvert.DeserializeObject<List<OldGenBank>>(text);
            }
        }

        public static OldGenBank GetBankById(int id)
        {
            return oldGenBanks.Find(x => x.Bank_Id == id);
        }

        public static List<OldGenBank> GetBanksByEntityId(int id)
        {
            return oldGenBanks.FindAll(x => x.Legal_Entity_Id == id).ToList();
        }

        public static OldGenSigner GetSignerById(int id)
        {
            return oldGenSigners.Find(x => x.Signer_Id == id);
        }

        public static List<OldGenSigner> GetSignersByEntityId(int id)
        {
            return oldGenSigners.FindAll(x => x.Legal_Entity_Id == id).ToList();
        }

        public static OldGenAccommodation GetAccommodationById(int id)
        {
            return oldGenAccommodations.Find(x => x.Accommodation_Id == id);
        }

        public static List<OldGenAccommodation> GetAccommodationsByEntityId(int id)
        {
            return oldGenAccommodations.FindAll(x => x.Legal_Entity_Id == id).ToList();
        }

        public static OldGenCounterparty GetCounterpartyById(int? id)
        {
            return oldGenCounterparties.Find(x => x.Legal_Entity_Id == id);
        }

        public static OldGenCounterparty GetCounterpartyByInn(string inn)
        {
            return oldGenCounterparties.Find(x => x.Inn == inn);
        }

        public static void SetAccommodationsToCounterparties()
        {
            foreach(OldGenCounterparty counterparty in oldGenCounterparties)
            {
                List<OldGenAccommodation> accommodations = GetAccommodationsByEntityId(counterparty.Legal_Entity_Id);

                counterparty.Accommodations = accommodations;
            }
        }

        public static void SetSignersToCounterparties()
        {
            foreach (OldGenCounterparty counterparty in oldGenCounterparties)
            {
                List<OldGenSigner> signers = GetSignersByEntityId(counterparty.Legal_Entity_Id);

                counterparty.Signers = signers;
            }
        }
        public static void SetBanksToCounterparties()
        {
            foreach (OldGenCounterparty counterparty in oldGenCounterparties)
            {
                List<OldGenBank> banks = GetBanksByEntityId(counterparty.Legal_Entity_Id);

                counterparty.Banks = banks;
            }
        }

        public static Counterparty Map(this OldGenCounterparty oldCounterparty)
        {
            Counterparty counterparty = new Counterparty {
                OldCounterpartyId = oldCounterparty.Old_Legal_Entity_Id,
                ShortName = oldCounterparty.Short_Name,
                LegalAddress = oldCounterparty.Legal_Address,
                PostalAddress = oldCounterparty.Postal_Address,
                PhoneNumber = oldCounterparty.Phone_Number,
                MainEmail = oldCounterparty.Main_Email,
                BookkeepingEmail = oldCounterparty.Bookkeeping_Email,
                PaymentRegistersEmail = oldCounterparty.Payment_Registers_Email,
                Inn = oldCounterparty.Inn,
                Kpp = oldCounterparty.Kpp,
                Ogrn = oldCounterparty.Ogrn,
                IsBudgetaryInstitution = oldCounterparty.Is_Budgetary_Institution
            };

            if (counterparty.PhoneNumber == "")
                counterparty.PhoneNumber = null;

            if (counterparty.PaymentRegistersEmail == "")
                counterparty.PaymentRegistersEmail = null;

            if (counterparty.BookkeepingEmail == "")
                counterparty.BookkeepingEmail = null;

            if (counterparty.MainEmail == "")
                counterparty.MainEmail = null;

            if (counterparty.Kpp == "")
                counterparty.Kpp = null;

            foreach(OldGenAccommodation oldAccommodation in oldCounterparty.Accommodations)
            {
                counterparty.AddAccommodationAsPreset(oldAccommodation.Map());
            }

            foreach (OldGenSigner oldSigner in oldCounterparty.Signers)
            {
                counterparty.AddSignerAsPreset(oldSigner.Map());
            }

            foreach (OldGenBank oldBank in oldCounterparty.Banks)
            {
                counterparty.AddBankDetailsAsPreset(oldBank.Map());
            }

            if ( oldCounterparty.Short_Name.StartsWith("ИП") )
                counterparty.Type = PartyType.IndividualEntrepreneur;
            else
                counterparty.Type = PartyType.ArtificialPerson;

            if ( oldCounterparty.Short_Name.StartsWith("ООО") || // Явное проставление типа для случаев если название такое "ООО <ИПАРТ>"
                oldCounterparty.Short_Name.StartsWith("ЗАО") ||
                oldCounterparty.Short_Name.StartsWith("АО") ||
                oldCounterparty.Short_Name.StartsWith("ОАО") )
                counterparty.Type = PartyType.ArtificialPerson;

            return counterparty;
        }

        private static Accommodation Map(this OldGenAccommodation oldAccommodation)
        {
            Accommodation accommodation = new Accommodation(
                oldAccommodation.Name,
                new Grammatical.GrammaticalCases(
                    oldAccommodation.Type_Name_Nominative,
                    oldAccommodation.Type_Name_Genitive ),
                oldAccommodation.Address,
                oldAccommodation.Site_Url,
                oldAccommodation.Tl_Id );

            return accommodation;
        }

        private static Signer Map(this OldGenSigner oldSigner)
        {
            Signer signer = new Signer(
                new Grammatical.GrammaticalCases(
                    oldSigner.Full_Name_Nominative,
                    oldSigner.Full_Name_Genitive ),
                new Grammatical.GrammaticalCases(
                    oldSigner.Position_Nominative,
                    oldSigner.Position_Genitive ),
                new Grammatical.GrammaticalCases(
                    oldSigner.Basis_Action_Nominative,
                    oldSigner.Basis_Action_Genitive ) );
            
            return signer;
        }

        private static BankDetails Map(this OldGenBank oldBank)
        {
            BankDetails bank = new BankDetails(
                oldBank.Name,
                oldBank.Bic,
                oldBank.Correspondent_Account,
                oldBank.IsBudgetaryInstitution,
                oldBank.Settlement_Account,
                oldBank.Kbk,
                oldBank.Personal_Account );

            if (bank.Kbk == "")
                bank.Kbk = null;

            if (bank.PersonalAccount == "")
                bank.PersonalAccount = null;

            return bank;
        }
    }
}
