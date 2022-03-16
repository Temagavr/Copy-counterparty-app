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
                List<OldGenAccommodation> oldGenAccommodations = GetAccommodationsByEntityId(counterparty.Legal_Entity_Id);

                counterparty.accommodations = oldGenAccommodations;
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

            if (oldCounterparty.Short_Name.Contains("ИП"))
                counterparty.Type = PartyType.IndividualEntrepreneur;
            else
                counterparty.Type = PartyType.ArtificialPerson;

            return counterparty;
        }
    }
}
