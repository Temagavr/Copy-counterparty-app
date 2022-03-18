using System.Collections.Generic;
using copy_counterparty_app.Grammatical;

namespace copy_counterparty_app.Domain.Shared
{
    /// <summary>
    /// Подписант (человек, подписывающий документ)
    /// </summary>
    public class Signer
    {
        public Signer(
            GrammaticalCases fullName,
            GrammaticalCases position,
            GrammaticalCases basicAction )
        {
            FullNameNominative = fullName.Nominative;
            FullNameGenitive = fullName.Genitive;
            PositionNominative = position.Nominative;
            PositionGenitive = position.Genitive;
            BasicActionNominative = basicAction.Nominative;
            BasicActionGenitive = basicAction.Genitive;
        }

        /// <summary>
        /// Полное ФИО
        /// </summary>
        public GrammaticalCases FullName
        {
            get
            {
                return new GrammaticalCases( FullNameNominative, FullNameGenitive );
            }
        }

        protected string FullNameNominative { get; set; }
        protected string FullNameGenitive { get; set; }

        /// <summary>
        /// Должность подписанта
        /// </summary>
        public GrammaticalCases Position
        {
            get
            {
                return new GrammaticalCases( PositionNominative, PositionGenitive );
            }
        }

        protected string PositionNominative { get; set; }
        protected string PositionGenitive { get; set; }

        /// <summary>
        /// Основание действия
        /// </summary>
        public GrammaticalCases BasicAction
        {
            get
            {
                return new GrammaticalCases( BasicActionNominative, BasicActionGenitive );
            }
        }

        protected string BasicActionNominative { get; set; }
        protected string BasicActionGenitive { get; set; }

        // Workaround for EF
        protected Signer()
        {
        }

        public Signer Copy()
        {
            return new Signer(
                FullName,
                Position,
                BasicAction );
        }

        public bool Equals(Signer signer)
        {
            if (FullNameNominative != signer.FullNameNominative)
                return false;

            if (FullNameGenitive!= signer.FullNameGenitive)
                return false;

            if (PositionNominative != signer.PositionNominative)
                return false;

            if (PositionGenitive != signer.PositionGenitive)
                return false;

            if (BasicActionNominative != signer.BasicActionNominative)
                return false;

            if (BasicActionGenitive != signer.BasicActionGenitive)
                return false;

            return true;
        }
    }
}
