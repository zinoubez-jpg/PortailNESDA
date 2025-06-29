using System;
using System.Collections.Generic;
using System.Text;

namespace Ansej.Sidjeme.Domain
{
    public partial class ContactAgenceWilaya : Referentiel
    {
      
        public string CodeWilaya { get; set; }        
        public string NomWilayaFr { get; set; }
        public string NomWilayaAr { get; set; }
        public string NomWilayaEn { get; set; }

        public string AdresseAr { get; set; }
        public string AdresseFr { get; set; }
        public string AdresseEn { get; set; }
        public string Latitude { get; set; }           
        public string Longitude { get; set; }         
        public string TelephoneFixe { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Observations { get; set; }      
    }
}
