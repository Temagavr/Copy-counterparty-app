using System.Collections.Generic;

namespace copy_counterparty_app.Grammatical
{
    public class GrammaticalCases
    {
        /// <summary>
        /// Выражение в именительном падеже
        /// </summary>
        public string Nominative { get; private set; }

        /// <summary>
        /// Выражение в родительном падеже
        /// </summary>
        public string Genitive { get; private set; }

        public GrammaticalCases( string nominative, string genitive )
        {
            Nominative = nominative;
            Genitive = genitive;
        }

        public GrammaticalCases Copy()
        {
            return new GrammaticalCases(
                Nominative,
                Genitive );
        }

        protected IEnumerable<object> GetEqualityComponents()
        {
            yield return Nominative;
            yield return Genitive;
        }
    }
}
