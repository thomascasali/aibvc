//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TEST.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Atleta
    {
        public Atleta()
        {
        }
    
        public int IDAtleta { get; set; }
        public int IDSocieta { get; set; }
        public string CodiceTessera { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public char Sesso { get; set; }
        public string CF { get; set; }
        public System.DateTime DataNascita { get; set; }
        public string IDComuneNascita { get; set; }
        public string IDComuneResidenza { get; set; }
        public string Indirizzo { get; set; }
        public string CAP { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public int Altezza { get; set; }
        public int Peso { get; set; }
        public System.DateTime DataScadenzaCertificato { get; set; }
       
    }
}
