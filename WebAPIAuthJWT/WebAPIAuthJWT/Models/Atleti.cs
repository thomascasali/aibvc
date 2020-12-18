using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPIAuthJWT.Models
{
    public class Atleti
    {
        public int IDAtleta { get; set; }
        public int IDSocieta { get; set; }

        public string CodiceTessera { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Sesso { get; set; }
        public string CF { get; set; }
        public DateTime DataNascita { get; set; }
        public string IDComuneNascita { get; set; }
        public string IDComuneResidenza { get; set; }
        public string Indirizzo { get; set; }
        public string CAP { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public int Altezza { get; set; }
        public int Peso { get; set; }
        public DateTime DataScadenzaCertificato { get; set; }
    }
}