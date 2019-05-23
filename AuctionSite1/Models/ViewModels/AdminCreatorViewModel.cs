using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSite1.Models.ViewModels
{
    public class AdminCreatorViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Role { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
        public IEnumerable<SelectListItem> User { get; set; }

        public Task<IList<IdentityUser>> Admins { get; set; }

        public AdminCreatorViewModel()
        {
            Roles = new List<SelectListItem>();
            User = new List<SelectListItem>();
            //Admins = new Task<IList<IdentityUser>>();
        }
    }
}
