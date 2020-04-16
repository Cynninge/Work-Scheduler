using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkScheduler.Models;
using WorkScheduler.ViewModels;

namespace WorkScheduler.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        //private readonly UserModel userModel;
        public AccountController(SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        public IActionResult Index()
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
            //IdentityRole identity = new IdentityRole
            //{
            //    Name = "Administrator"
            //};
            //await roleManager.CreateAsync(identity);
            //var admin = new IdentityUser { UserName = "Administrator" };
            //var adminresult = await userManager.CreateAsync(admin, "Administrator");
            //await userManager.AddToRoleAsync(admin, "Administrator");
            if (ModelState.IsValid)
            {
                //var user = new UserModel { UserName = viewModel.UserName,Email=viewModel.UserName, FirstName=viewModel.FirstName,LastName=viewModel.LastName};
                var user = new IdentityUser { UserName = viewModel.Email };
                var result = await userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    var login = await signInManager.PasswordSignInAsync(viewModel.Email,
                                        viewModel.Password, true, false);
                    if (login.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Nie można się zalogować!");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(viewModel.UserName,
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
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identity = new IdentityRole
                {
                    Name = createRoleViewModel.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identity);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
    }
}