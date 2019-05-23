using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AuctionSite1.Models
{
    [DataContract]
    public class AuktionValues
    {
        [DataMember]
        public int AuktionID { get; set; }

        [DataMember]
        public string Titel { get; set; }

        [DataMember]
        public string Beskrivning { get; set; }

        [DataMember]
        public int Gruppkod { get; set; }
        
        [DataMember]
        public int Utropspris { get; set; }

        [DataMember]
        public string SkapadAv { get; set; }

        [DataMember(Name = "StartDatum")]
        public string StartD { get; set; }

        [DataMember(Name = "SlutDatum")]
        public string SlutD { get; set; }


        public DateTime StartDatum { get; set; }

        public DateTime SlutDatum { get; set; }

        public string Stängd { get; set; }

        public Bids[] Bud { get; set; }

        public int HögstaSumma { get; set; }

        public int Summa { get; set; } //När vi tar summa från vyn till metoden

        public string Stigande = "Stigande";
        public string Fallande = "Fallande";
        public string SlutDatumText = "SlutDatum";
    }
}
