using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSite1.Models.ViewModels
{
    public class CreateAuctionViewModel
    {
        public int AuktionID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} måste vara minst {2} och max {1} bokstäver långa.", MinimumLength = 1)]
        public string Titel { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "{0} måste vara minst {2} och max {1} bokstäver långa.", MinimumLength = 1)]
        public string Beskrivning { get; set; }

        public int Gruppkod { get; set; }

        [Required]
        public int Utropspris { get; set; }

        public string SkapadAv { get; set; }

        [Required]
        public string StartD { get; set; }

        [Required]
        //[DataType(DataType.DateTime)]
        public string SlutD { get; set; } 
    }
}
