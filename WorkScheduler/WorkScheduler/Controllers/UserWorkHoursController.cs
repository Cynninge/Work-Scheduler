using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkScheduler.Models;
using WorkScheduler.Services.Interfaces;
using WorkScheduler.ViewModels;

namespace WorkScheduler.Controllers
{
    public class UserWorkHoursController : Controller
    {
        private readonly IUserWorkHoursViewModelService userWorkHoursViewModelService;
        private readonly IDepartmentService departmentService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<UserModel> userManager;

        public UserWorkHoursController(RoleManager<IdentityRole> roleManager,
                                        UserManager<UserModel> userManager,
                                        IDepartmentService departmentService,
                                        IUserWorkHoursViewModelService userWorkHoursViewModelService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.departmentService = departmentService;            
        }

        public ActionResult Index()
        {
            return View();
        }
                
        public ActionResult Details(int id)
        {
            return View();
        }
                
        public ActionResult Create()
        {
            return View();
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {            
            try
            {                
                var hours = new WorkHoursModel
                {                     
                    
                };

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}