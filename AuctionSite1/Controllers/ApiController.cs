using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using AuctionSite1.Models;
using AuctionSite1.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuctionSite1.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class ApiController : Controller
    {
        public HttpClient client;
        public int variable = 0;
        public static List<AuktionValues> valueList = new List<AuktionValues>();

        public ApiController()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("http://nackowskis.azurewebsites.net/")
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public AuktionValues FromToAuktionValues(AuktionValues item)
        {
            var model = new AuktionValues()
            {
                AuktionID = item.AuktionID,
                Beskrivning = item.Beskrivning,
                Gruppkod = item.Gruppkod,
                SkapadAv = item.SkapadAv,
                SlutD = item.SlutD,
                StartD = item.StartD,
                SlutDatum = item.SlutDatum,
                StartDatum = item.StartDatum,
                Titel = item.Titel,
                Utropspris = item.Utropspris,
            };
            return model;
        }

        public CreateAuctionViewModel TurnAuctionsToView(AuktionValues item)
        {
            var model = new CreateAuctionViewModel()
            {
                AuktionID = item.AuktionID,
                Beskrivning = item.Beskrivning,
                Gruppkod = 1180,
                SkapadAv = item.SkapadAv,
                SlutD = item.SlutD,
                StartD = item.StartD,
                Titel = item.Titel,
                Utropspris = item.Utropspris,
            };

            return model;
        }

        public AuktionValues ToAuktionValues(CreateAuctionViewModel item)
        {
            var model = new AuktionValues()
            {
                AuktionID = item.AuktionID,
                Beskrivning = item.Beskrivning,
                Gruppkod = 1180,
                SkapadAv = User.Identity.Name,
                SlutD = item.SlutD,
                StartD = item.StartD,
                Titel = item.Titel,
                Utropspris = item.Utropspris,
            };

            return model;
        }

        [AllowAnonymous]
        public AuktionValues[] Auctions()
        {
            var response = client.GetAsync("Api/Auktion/1180").Result;
            var serializer = new DataContractJsonSerializer(typeof(AuktionValues[]));
            var responseStream = response.Content.ReadAsStreamAsync().Result;
            var auctionResult = (AuktionValues[])serializer.ReadObject(responseStream);

            var count = auctionResult.Count();

            var values = new AuktionValues[count];
            var i = 0;

            foreach (var item in auctionResult)
            {
                var closed = "";

                if (Convert.ToDateTime(item.SlutD) > DateTime.Now.Date)
                {
                    closed = "Öppen";
                }

                else
                {
                    closed = "Stängd";
                }

                var bidList = GetBid(item.AuktionID, closed);

                var money = 0;

                foreach (var highestBid in bidList)
                {
                    money = HighestValue(money, highestBid.Summa);
                }

                var model = FromToAuktionValues(item);

                model.HögstaSumma = money;
                model.Stängd = closed;

                values[i] = model;
                i++;
            }

            return values;
        }

        [AllowAnonymous]
        public AuktionValues GetAuction(int auctionId)
        {
            var response = client.GetAsync($"Api/Auktion/1180/{auctionId}").Result;
            var serializer = new DataContractJsonSerializer(typeof(AuktionValues));
            var responseStream = response.Content.ReadAsStreamAsync().Result;
            var auctionResult = (AuktionValues)serializer.ReadObject(responseStream);

            if (Convert.ToDateTime(auctionResult.SlutD) > DateTime.Now.Date)
            {
                auctionResult.Stängd = "Öppen";
                var listOfBids = GetBid(auctionId, "Öppen");
                auctionResult.Bud = listOfBids;

                var count = 0;

                foreach (var values in listOfBids)
                {
                    count = HighestValue(count, values.Summa);
                }

                auctionResult.HögstaSumma = count;
            }

            else
            {
                auctionResult.Stängd = "Stängd";
                var listOfBids = GetBid(auctionId, "Stängd");

                auctionResult.Bud = listOfBids;

                var count = 0;

                foreach (var values in listOfBids)
                {
                    count = HighestValue(count, values.Summa);
                }

                auctionResult.HögstaSumma = count;
            }

            return auctionResult;
        }

        public int HighestValue(int value1, int value2)
        {
            if (value1 > value2)
            {
                return value1;
            }

            else
            {
                return value2;
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateAuction()
        {
            return View("CreateAuction");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostAuction(CreateAuctionViewModel item)
        {
            if (ModelState.IsValid)
            {
                var model = ToAuktionValues(item);
                var json = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                var send = await client.PostAsync("/api/Auktion/1180", stringContent);
                return RedirectToAction("Index", "Auctions", Auctions());
            }

            return View("CreateAuction", item);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAuction(int id)
        {
            //Kolla användaren om den skapat auktionen
            var list = GetAuction(id);
            client.DeleteAsync("/api/Auktion/1180/" + id);

            var returnList = Auctions();

            return RedirectToAction("Index", "Auctions", returnList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ChangeAuction(int id)
        {
            variable = id;

            var getAuctionValues = GetAuction(id);

            var model = TurnAuctionsToView(getAuctionValues);

            //var model = new CreateAuctionViewModel()
            //{
            //    AuktionID = id,
            //    Beskrivning = getAuctionValues.Beskrivning,
            //    SlutD = getAuctionValues.SlutD,
            //    StartD = getAuctionValues.StartD,
            //    Titel = getAuctionValues.Titel,
            //    Utropspris = getAuctionValues.Utropspris
            //};

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAuction(CreateAuctionViewModel item) //Vilken admin som helst kan uppdatera
        {
            if (item.AuktionID == 0)
            {
                item.AuktionID = variable;
            }

            if (ModelState.IsValid)
            {
                var model = ToAuktionValues(item);

                var json = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var code = await client.PutAsync($"/api/Auktion/1180/{item.AuktionID}", stringContent);

                //return View("ChangeAuction", item);
                return RedirectToAction("Index", "Auctions", Auctions());
            }

            return View("ChangeAuction", item);
        }

        [AllowAnonymous]
        public AuktionValues[] SearchAuctions(string text) //Fungerar!
        {
            valueList.Clear(); //Tömmer listan ifall det är värden kvar där i
            var serializer = new DataContractJsonSerializer(typeof(AuktionValues[]));
            var response = client.GetAsync("Api/Auktion/1180").Result;
            var responseStream = response.Content.ReadAsStreamAsync().Result;
            var auctionResult = (AuktionValues[])serializer.ReadObject(responseStream);

            var list = auctionResult.Where(x => x.Beskrivning.Contains(text) || x.Titel.Contains(text));

            var count = list.Count();

            var values = new AuktionValues[count];
            var i = 0;

            foreach (var item in list)
            {
                var closed = "";

                if (Convert.ToDateTime(item.SlutD) > DateTime.Now.Date)
                {
                    closed = "Öppen";
                }

                else
                {
                    closed = "Stängd";
                }

                var model = new AuktionValues()
                {
                    AuktionID = item.AuktionID,
                    Beskrivning = item.Beskrivning,
                    Gruppkod = item.Gruppkod,
                    SkapadAv = item.SkapadAv,
                    SlutD = item.SlutD,
                    StartD = item.StartD,
                    SlutDatum = item.SlutDatum,
                    StartDatum = item.StartDatum,
                    Titel = item.Titel,
                    Utropspris = item.Utropspris,
                    Stängd = closed
                };

                valueList.Add(model);

                values[i] = model;
                i++;
            }

            return values;
        }

        /******* Bud hantering*******/
        [AllowAnonymous]
        public Bids[] GetBid(int id, string closedOrNot) //Fungerar!
        {
            var response = client.GetAsync($"Api/Bud/1180/{id}").Result;
            var serializer = new DataContractJsonSerializer(typeof(Bids[]));
            var responseStream = response.Content.ReadAsStreamAsync().Result;
            var auctionResult = (Bids[])serializer.ReadObject(responseStream);

            if (closedOrNot == "Stängd")
            {
                auctionResult.OrderByDescending(x => x.Summa).FirstOrDefault();
            }

            else
            {
                auctionResult.OrderByDescending(x => x.Summa);
            }


            return auctionResult;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddBid(AuktionValues item)
        {
            var value = 0;
            var itemValue = item.Summa;
            var checkList = GetBid(item.AuktionID, "Öppen");

            foreach (var values in checkList)
            {
                value = HighestValue(value, values.Summa);
            }

            if (itemValue > value)
            {
                var model = new Bids()
                {
                    Summa = item.Summa,
                    AuktionID = item.AuktionID,
                    Budgivare = User.Identity.Name
                };

                var json = JsonConvert.SerializeObject(model);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                var accept = await client.PostAsync("/api/Bud/1180", stringContent);
            }

            return RedirectToAction("Auctions", "Auctions", new { id = item.AuktionID });
        }
    }
}