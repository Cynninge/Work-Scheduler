﻿using System;
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
            //public UserWorkHoursController(IUserWorkHoursViewModelService userWorkHoursViewModelService)
            //{
            //    _userWorkHoursViewModelService = userWorkHoursViewModelService;
            //}
            // GET: UserWorkHours
        }
            public ActionResult Index()
        {
            //var usersWorkHours = new UserWorkHoursViewModel();
            //usersWorkHours.
            return View();
        }

        // GET: UserWorkHours/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserWorkHours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserWorkHours/Create
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

        // GET: UserWorkHours/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserWorkHours/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserWorkHours/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserWorkHours/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}