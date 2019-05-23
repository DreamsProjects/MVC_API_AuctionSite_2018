using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionSite1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using AuctionSite1.Models.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using AuctionSite1.Helper;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionSite1.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Services.IEmailSender _emailSender;
        private ApiController httpClient = new ApiController();

        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            Services.IEmailSender emailSender,
            ILoggerFactory loggerFactory,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<HomeController>();
            _roleManager = roleManager;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        public async void ReadOnlyWhenLoggedIn()
        {
            var user = User.Identity.Name;

            if (user != null)
            {
                var person = await _userManager.FindByEmailAsync(user);
                var userRole = await _userManager.GetRolesAsync(person);

                if (!userRole.Contains("Admin")) //Om ej Admin
                {
                    if (!userRole.Contains("Regular")) //Om ingenting
                    {
                        var getUser = _userManager.Users.FirstOrDefault(x => x.UserName == user);

                        await _userManager.AddToRoleAsync(getUser, "Regular");
                        await _signInManager.SignInAsync(person, false);
                    }
                }
            }
        }

        public async Task<IActionResult> Index() //Sätter roll vid inläsning av start sidan
        {
            var detailedResult = httpClient.Auctions();

            return View(detailedResult); 
        }

        [HttpGet]
        public IActionResult GetAuctionSearch(CreateAuctionViewModel id)
        {
            return View("ChangeAuction",id);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AuctionSites()
        {
            /*
                var httpClient = new WeatherHTTPClientController();
                var detailedResult = httpClient.GetByCityForecast(cityId);
             */
            return View();
        }

        [HttpGet]//Fungerar
        [Authorize(Roles = "Admin")]
        public IActionResult CreateAdmins()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();

            var admins = _userManager.GetUsersInRoleAsync("Admin");

            var model = new AdminCreatorViewModel
            {
                User = users.Select(x => new SelectListItem()
                {
                    Text = x.Email,
                    Value = x.Email,
                    Selected = false
                }),
                Roles = roles.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Name,
                    Selected = false
                }),
                Admins = admins
            };

            return View(model);
        }

        [HttpPost] //Fungerar
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAdmins(AdminCreatorViewModel user)
        {
            var findUser = _userManager.FindByIdAsync(user.Id);

            var person = await _userManager.FindByEmailAsync(user.Id);

            var getUser = _userManager.Users.FirstOrDefault(x => x.UserName == person.UserName);

            var getAllRoles = await _userManager.GetRolesAsync(person);

            //Tar bort alla befintliga roller på användaren för att inte få dubletter
            await _userManager.RemoveFromRolesAsync(person, getAllRoles);

            //Lägger till den nya rollen på användaren
            await _userManager.AddToRoleAsync(getUser, user.Role);
            await _signInManager.SignInAsync(getUser, false);

            _logger.LogInformation("User role updated");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost] //Fungerar, flytta ut senare
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                    ReadOnlyWhenLoggedIn();

                    _logger.LogInformation("User logged in.");
                    return Redirect("Index"); //Gå till inloggningssidan
                }
            }

            // If we got this far, something failed, redisplay form
            return PartialView("_Login");
        }

        public PartialViewResult LogIn()
        {
            return PartialView("_Login");
        }

        [HttpPost] //Fungerar, flytta ut senare
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //Kör bara en gång
                       //IdentityResult roleResult;

                       // string[] roleNames = { "Admin", "Regular" };

                       // foreach (var roleName in roleNames)
                       // {
                       //     var roleExist = await _roleManager.RoleExistsAsync(roleName);
                       //    //  ensure that the role does not exist
                       //     if (!roleExist)
                       //     {
                       //       //  create the roles and seed them to the database: 
                       //         roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                       //     }
                       // }
                        

                    await _userManager.AddToRoleAsync(user, "Regular");

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);


                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index"); //Konto finns redan
            }
            return Json("Error");
           // return PartialView("RegisterUser"); //Fyll uppgifter igen
        }

        public PartialViewResult RegisterUser() //PartialView för startsidan
        {
            return PartialView();
        }

        [HttpPost] //Fungerar
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult DeleteAuction(int id)
        //{
        //    var removing = httpClient.DeleteAuction(id);

        //    return View("Index", removing);
        //}


        /************** EXTERN PROVIDER **************/
        [HttpPost]//Verkar fungera
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Home", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }


        [HttpGet] //Verkar fungera
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }

            // If the user does not have an account, then ask the user to create an account.
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            return View("ExternalLogin", new ExternalLoginViewModel { Email = email });

        }

        [HttpPost]//Verkar fungera
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }



        /************** LÖSENORDS HANTERING **************/
        [HttpGet]
        public PartialViewResult ForgotPassword()
        {
            return PartialView("_forgotPassword");
        }

        [HttpPost]//Verkar fungera utom email
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Återställ lösenord",
                   $"Återställ ditt lösenord genom att klicka på: <a href='{callbackUrl}'>länken</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View("_forgotPassword");
        }

        [HttpGet] //Fungerar
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)//what is the code EMAIL CODE???
        {


            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Index");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }



        //HELPERS
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

    }
}
