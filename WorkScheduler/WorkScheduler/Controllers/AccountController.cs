using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkScheduler.Models;
using WorkScheduler.Services.Interfaces;
using WorkScheduler.ViewModels;

namespace WorkScheduler.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserModel> signInManager;
        private readonly UserManager<UserModel> userManager;
        //private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(SignInManager<UserModel> _signInManager, UserManager<UserModel> _userManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            //roleManager = _roleManager;            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new UserModel 
                { 
                    UserName = viewModel.Email,
                    Email = viewModel.Email,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Initials = viewModel.Initials
                };

                var result = await userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                    else
                    {
                        var login = await signInManager.PasswordSignInAsync(viewModel.Email,
                                        viewModel.Password, true, false);
                        if (login.Succeeded)
                        {
                            #region
                            //tworzenie 365 dni dla uzytkownika
                            //var currentYear = DateTime.Now.Year;
                            //var dateIncrementation = new DateTime(currentYear, 1, 1);
                            //for (int i = 1; i < 365; i++)
                            //{                            
                            //    var userDays = new DayModel();   
                            //    _dayService.Create(userDays, user, dateIncrementation);
                            //    dateIncrementation = dateIncrementation.AddDays(1);
                            //}
                            #endregion
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Nie można się zalogować!");
                        }
                    }                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(viewModel.Email,
                                        viewModel.Password, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nie można się zalogować!");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
    
        //[HttpGet]
        //public async Task<IActionResult> EditUserData(EditUserViewModel user)
        //{
        //    var currentUser = userManager.FindByNameAsync(user.Email);
        //    await userManager.FindByIdAsync(User.Identity.Name);
        //    //var claims = await userManager.GetClaimsAsync(r);
        //    //model.FirstName = claims.Where(c => c.Type == "FirstName").Select(c => c.Value).ToString();
        //    var claimsIdentity =  User.Identity as ClaimsIdentity;
        //    if (claimsIdentity != null)
        //    {
        //        // the principal identity is a claims identity.
        //        // now we need to find the NameIdentifier claim
        //        var userIdClaim = claimsIdentity.Claims
        //            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        //        if (userIdClaim != null)
        //        {
        //            var userIdValue = userIdClaim.Value;
        //        }
        //    }
            
        //    return View(user);
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        //[HttpPost]
        //[AutoValidateAntiforgeryToken]
        //public async Task<IActionResult> EditUserData(EditUserDataViewModel model)
        //{
        //    // Validates the received password data based on the view model
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {

        //            //var user = User.Identity;
        //            //user = new EditUserDataViewModel {FirstName=model.FirstName,LastName=model.LastName,Email=model.Email };
        //            var r = await userManager.FindByNameAsync(User.Identity.Name);
        //            var claims = await userManager.GetClaimsAsync(r);
        //            var t = User;
        //            await userManager.RemoveClaimsAsync(r, claims);
        //            await userManager.AddClaimAsync(r, new Claim("FirstName", model.FirstName));
        //            await userManager.AddClaimAsync(r, new Claim("LastName", model.LastName));
        //            await userManager.GetClaimsAsync(r);
        //            await userManager.ChangeEmailAsync(r, model.Email, "");

        //            //var result = userManager.UpdateAsync(r);

        //            //var user = new UserModel();
        //            //user.Id = r.Id;
        //            //user.FirstName = model.FirstName;
        //            //user.LastName = model.LastName;
        //            // user.Email = model.Email;

        //            //model.FirstName = 
        //            //var result = await userManager.AddClaimAsync(r,new Claim("FirstName", model.FirstName));
        //            //result = await userManager.AddClaimAsync(r, new Claim("LastName", model.LastName));
        //            //result = await userManager.AddClaimAsync(r,new Claim("Email",model.Email));
        //            var result = await userManager.UpdateAsync(r);

        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("EditUserData", "Account");
        //            }
        //            else
        //            {
        //                return Redirect("Error");
        //            }
        //        }
        //        return Redirect("Error");
        //    }
        //    catch
        //    {
        //        return View(model);
        //    }
        //}
    }
}