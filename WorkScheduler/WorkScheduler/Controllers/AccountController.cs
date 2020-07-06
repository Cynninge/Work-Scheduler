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
        private readonly IDepartmentService departmentService;
        

        public AccountController(SignInManager<UserModel> _signInManager, UserManager<UserModel> _userManager, IDepartmentService departmentService)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            this.departmentService = departmentService;              
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string name)
        {
            var user = await userManager.FindByNameAsync(name);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }

            var userDepartments = departmentService.GetAll();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Initials = user.Initials,
                PhoneNumber = user.PhoneNumber,
                WorkTimePerWeek = user.WorkTimePerWeek,
                Position = user.Position,
                Departments = userDepartments,                
                DepartmentName = user.Department?.Name
            };

            return View(model);
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ListUsersLoggedAccount()
        {
            var users = userManager.Users;
            return View(users);
        }


        [HttpGet]
        
        public async Task<IActionResult> EditUserData(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }

            var userDepartments = departmentService.GetAll();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Initials = user.Initials,
                PhoneNumber = user.PhoneNumber,
                WorkTimePerWeek = user.WorkTimePerWeek,
                Position = user.Position,
                Departments = userDepartments,                
                DepartmentName = user.Department?.Name
            };

            return View(model);

        }
       

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditUserData(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.Id = model.Id;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Initials = model.Initials;
                user.PhoneNumber = model.PhoneNumber;  

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Success");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }           
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}