using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkScheduler.Models;
using WorkScheduler.Services.Interfaces;
using WorkScheduler.ViewModels;

namespace WorkScheduler.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index(DepartmentModel department)
        {
            UserWorkHoursViewModel userWorkHoursViewModel = new UserWorkHoursViewModel()
            {
                Employees = _departmentService.GetEmployees(department.DepartmentId).ToList(),
                WorkHours = _departmentService.GetEmployeesWorkHours(department.DepartmentId).ToList()
            };

            return View(userWorkHoursViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,ShortName")][FromForm] DepartmentModel departmentModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentService.Create(departmentModel);
                }
                catch (Exception)
                {
                    return View(departmentModel);
                }
            }            
                return View(departmentModel);
            
        }
    }
}