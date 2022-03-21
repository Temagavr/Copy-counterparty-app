using System;
using System.Collections.Generic;
using System.Linq;
using copy_counterparty_app.Domain.Shared;

namespace copy_counterparty_app.Domain
{
    /// <summary>
    /// Контрагент
    /// </summary>
    public partial class Counterparty
    {
        // Id текущего контрагента
        public int Id { get; set; }

        /// <summary>
        /// Id старого контрагента
        /// </summary>
        public int? OldCounterpartyId { get; set; }

        /// <summary>
        /// Тип контрагента
        /// </summary>
        public PartyType Type { get; set; }

        /// <summary>
        /// Краткое название контрагента
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Юридический адрес контрагента
        /// </summary>
        public string LegalAddress { get; set; }

        /// <summary>
        /// Почтовый адрес контрагента
        /// </summary>
        public string PostalAddress { get; set; }

        /// <summary>
        /// Телефонный номер
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Основной email
        /// </summary>
        public string MainEmail { get; set; }

        /// <summary>
        /// Email для направления бух. документов
        /// </summary>
        public string BookkeepingEmail { get; set; }

        /// <summary>
        /// Email для направления реестров платежей
        /// </summary>
        public string PaymentRegistersEmail { get; set; }

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
        public bool IsBudgetaryInstitution { get; set; }

        /// <summary>
        /// Список средств размещения контрагента
        /// </summary>
        public virtual IReadOnlyList<AccommodationPreset> AccommodationPresets => _accommodationPresets;

        private readonly List<AccommodationPreset> _accommodationPresets = new List<AccommodationPreset>();

        /// <summary>
        /// Список банков контрагента
        /// </summary>
        public virtual IReadOnlyList<BankDetailsPreset> BankPresets => _bankPresets;

        private readonly List<BankDetailsPreset> _bankPresets = new List<BankDetailsPreset>();

        /// <summary>
        /// Список подписантов контрагента
        /// </summary>
        public virtual IReadOnlyList<SignerPreset> SignerPresets => _signerPresets;

        private readonly List<SignerPreset> _signerPresets = new List<SignerPreset>();
        
        public Counterparty()
        {
        }
        /*
        public void SetOldCounterparty( Counterparty oldCounterparty )
        {
            OldCounterparty = oldCounterparty;
        }
        */

        public void AddAccommodationAsPreset( Accommodation accommodation )
        {
            _accommodationPresets.Add( new AccommodationPreset( accommodation ) );
        }

        public void AddBankDetailsAsPreset( BankDetails bankDetails )
        {
            _bankPresets.Add( new BankDetailsPreset( bankDetails ) );
        }

        public void AddSignerAsPreset( Signer signer )
        {
            _signerPresets.Add( new SignerPreset( signer ) );
        }
        /*

        public void UpdateAccommodationPreset( int presetId, Accommodation accommodation )
        {
            var accommodationPreset = _accommodationPresets.Single( x => x.Id == presetId );
            accommodationPreset.UpdatePreset( accommodation );
        }

        public void UpdateBankDetailsPreset( int presetId, BankDetails bankDetails )
        {
            bankDetails.ThrowIfArgumentNull( nameof( bankDetails ) );

            if ( IsBudgetaryInstitution != bankDetails.IsBudgetaryInstitution )
            {
                throw new DomainException( "Bank details do not match the counterparty" );
            }

            if ( !_bankPresets.Exists( x => x.Id == presetId ) )
            {
                throw new ArgumentException( $"BankDetails preset with Id={presetId} not exists" );
            }

            var bankDetailsPreset = _bankPresets.Single( x => x.Id == presetId );
            bankDetailsPreset.UpdatePreset( bankDetails );
        }

        public void UpdateSignerPreset( int presetId, Signer signer )
        {
            signer.ThrowIfArgumentNull( nameof( signer ) );
            if ( !_signerPresets.Exists( x => x.Id == presetId ) )
            {
                throw new ArgumentException( $"Signer preset with Id={presetId} not exists" );
            }

            var signerPreset = _signerPresets.Single( x => x.Id == presetId );
            signerPreset.UpdatePreset( signer );
        }

        public void RemoveAccommodationPreset( int presetId )
        {
            _accommodationPresets.RemoveAll( x => x.Id == presetId );
        }

        public void RemoveBankDetailsPreset( int presetId )
        {
            _bankPresets.RemoveAll( x => x.Id == presetId );
        }

        public void RemoveSignerPreset( int presetId )
        {
            _signerPresets.RemoveAll( x => x.Id == presetId );
        }
        */

        public bool ContainsAccommodationPreset( Accommodation accommodation )
        {
           return AccommodationPresets.Any( preset => preset.Value.Equals( accommodation ) );
        }

        public bool ContainsBankDetailsPreset( BankDetails bankDetails )
        {
            return BankPresets.Any( preset => preset.Value.Equals( bankDetails ) );
        }

        public bool ContainsSignerPreset( Signer signer )
        {
            return SignerPresets.Any( preset => preset.Value.Equals( signer ) );
        }

        public Counterparty Copy(Counterparty counterparty)
        {
            Counterparty copyCounterparty = new Counterparty();

            copyCounterparty.ShortName = counterparty.ShortName;
            copyCounterparty.Id = counterparty.Id;
            copyCounterparty.OldCounterpartyId = counterparty.OldCounterpartyId;
            copyCounterparty.Type = counterparty.Type;
            copyCounterparty.LegalAddress = counterparty.LegalAddress;
            copyCounterparty.PostalAddress = counterparty.PostalAddress;
            copyCounterparty.PhoneNumber = counterparty.PhoneNumber;
            copyCounterparty.MainEmail = counterparty.MainEmail;
            copyCounterparty.BookkeepingEmail = counterparty.BookkeepingEmail;
            copyCounterparty.PaymentRegistersEmail = counterparty.PaymentRegistersEmail;
            copyCounterparty.Inn = counterparty.Inn;
            copyCounterparty.Kpp = counterparty.Kpp;
            copyCounterparty.Ogrn = counterparty.Ogrn;
            copyCounterparty.IsBudgetaryInstitution = counterparty.IsBudgetaryInstitution;

            foreach(AccommodationPreset accommodation in counterparty.AccommodationPresets)
            {
                copyCounterparty.AddAccommodationAsPreset(accommodation.Value);
            }

            foreach(BankDetailsPreset bankDetails in counterparty.BankPresets)
            {
                copyCounterparty.AddBankDetailsAsPreset(bankDetails.Value);
            }

            foreach(SignerPreset signerPreset in counterparty.SignerPresets)
            {
                copyCounterparty.AddSignerAsPreset(signerPreset.Value);
            }

            return copyCounterparty;
        }
    }
}
