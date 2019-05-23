using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AuctionSite1.Models
{
    [DataContract]
    public class Bids
    {
        [DataMember]
        public int BudID { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.Currency, ErrorMessage = "Budet måste vara heltal")]
        public int Summa { get; set; }

        [DataMember]
        [Required]
        public int AuktionID { get; set; }

        [DataMember]
        [Required]
        public string Budgivare { get; set; }
    }
}
