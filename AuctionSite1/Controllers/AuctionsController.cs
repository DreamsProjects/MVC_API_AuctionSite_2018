using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionSite1.Models;
using AuctionSite1.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSite1.Controllers
{
    [AllowAnonymous]
    public class AuctionsController : Controller
    {
        private ApiController httpClient = new ApiController();

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var detailedResult = httpClient.Auctions();

            return View(detailedResult);
        }

        [HttpGet]
        public IActionResult GetAuctionSearch(CreateAuctionViewModel id)
        {
            return View("ChangeAuction", id);
        }

        public IActionResult Auctions(int id)
        {
            var detailedResult = httpClient.GetAuction(id); //int? auctionId, string text

            return View(detailedResult);
        }

        public PartialViewResult Search()
        {
            return PartialView("_Search");
        }

        [HttpPost]
        public IActionResult AuctionSearch(string text)
        {
            var values = httpClient.SearchAuctions(text);

            return View(values);
        }

        public IActionResult ASearch(string id)
        {
            var list = ApiController.valueList;
            var AValues = new AuktionValues[list.Count()];

            int i = 0;

            if (id == "SlutDatum") { list = list.OrderByDescending(x => x.SlutD).ToList(); }
            else if (id == "Stigande") { list = list.OrderBy(x => x.Utropspris).ToList(); }
            else if (id == "Fallande") { list = list.OrderByDescending(x => x.Utropspris).ToList(); }

            foreach (var items in list)
            {
                var model = new AuktionValues()
                {
                    Beskrivning = items.Beskrivning,
                    Summa = items.Summa,
                    Utropspris = items.Utropspris,
                    Titel = items.Titel,
                    Stängd = items.Stängd,
                    SlutD = items.SlutD,
                    StartD = items.StartD,
                    SlutDatum = Convert.ToDateTime(items.SlutD),
                    StartDatum = Convert.ToDateTime(items.StartD)
                };

                AValues[i] = model;

                i++;
            }

            return View("AuctionSearch", AValues);
        }



        [Authorize(Roles = "Admin")]
        public IActionResult UpdateAuction()
        {
            return View("ChangeAuction");
        }
    }
}