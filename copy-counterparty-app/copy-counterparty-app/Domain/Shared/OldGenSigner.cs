using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace copy_counterparty_app.Domain.Shared
{
    public class OldGenSigner
    {
        public int Signer_Id { get; set; }

        public int Legal_Entity_Id { get; set; }

        /// <summary>
        /// Полное ФИО
        /// </summary>

        public string Full_Name_Nominative { get; set; }
        public string Full_Name_Genitive { get; set; }

        /// <summary>
        /// Должность подписанта
        /// </summary>

        public string Position_Nominative { get; set; }
        public string Position_Genitive { get; set; }

        /// <summary>
        /// Основание действия
        /// </summary>
        
        public string Basis_Action_Nominative { get; set; }
        public string Basis_Action_Genitive { get; set; }

        public OldGenSigner(
            string nameNominative,
            string nameGenitive,
            string positionNominative,
            string positionGenitive,
            string basicActionNominative,
            string basicActionGenitive)
        {
            Full_Name_Nominative = nameNominative;
            Full_Name_Genitive = nameGenitive;
            Position_Nominative = positionNominative;
            Position_Genitive = positionGenitive;
            Basis_Action_Nominative = basicActionNominative;
            Basis_Action_Genitive = basicActionGenitive;
        }
    }
}
