using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSite1.Models
{  
    public class Reload
    {
        [Required]
        public string Text { get; set; }
    }
}
