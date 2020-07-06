using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;
using WorkScheduler.Services.Interfaces;
using WorkScheduler.ViewModels;

namespace WorkScheduler.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<UserModel> userManager;

        public DepartmentController(RoleManager<IdentityRole> roleManager,
                                        UserManager<UserModel> userManager,
                                        IDepartmentService departmentService)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.departmentService = departmentService;
        }
        public IActionResult Details(int id)
        {
            var dep = departmentService.Get(id);
            return View(dep);
        }
        public IActionResult Index(DepartmentModel department)
        {
            UserWorkHoursViewModel userWorkHoursViewModel = new UserWorkHoursViewModel()
            {
                Workers = departmentService.GetEmployees(department.Name).ToList(),
                WorkHours = departmentService.GetEmployeesWorkHours(department.DepartmentId).ToList()
            };

            return View(userWorkHoursViewModel);
        }

        public IActionResult GetDepartment(List<UserWorkHoursViewModel> model, string name)
        {

            var department = departmentService.GetEmployees(name);
            var department1 = userManager.Users.Where(x => x.Department.Name == name);
            if (department == null)
            {
                ViewBag.ErrorMessage = $"Department with {name} cannot be found";
                return View("NotFound");
            }
            
            var userWorkHoursModel = new UserWorkHoursViewModel()
            {
                Name = name,
                Workers = department,
            };

            return View(userWorkHoursModel);
        }

        [HttpGet]
        public IActionResult ListDepartments()
        {
            var departmentsList = departmentService.GetAll();
            return View(departmentsList);
        }
        [Authorize(Roles = "Admin, Manager")]

        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,ShortName")][FromForm] DepartmentModel departmentModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    departmentService.Create(departmentModel);
                }
                catch (Exception)
                {
                    return View(departmentModel);
                }
            }
            return Redirect("ListDepartments");

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteDepartment(int departmentId)
        {
            var dep = departmentService.Get(departmentId);

            if (dep == null)
            {
                ViewBag.ErrorMessage = $"Department cannot be found";
                return View("NotFound");
            }
            else
            {
                departmentService.Delete(dep.DepartmentId);
            }

            return Redirect("ListDepartments");

        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult EditDepartment(int departmentId)
        {
            var dep = departmentService.Get(departmentId);

            if (dep == null)
            {
                ViewBag.ErrorMessage = $"Department cannot be found";
                return View("NotFound");
            }
            else
            {
                var departmentWorkers = userManager.Users.Where(x => x.Department.DepartmentId == dep.DepartmentId);

                var model = new DepartmentModel
                {
                    DepartmentId = dep.DepartmentId,
                    Name = dep.Name,
                    ShortName = dep.ShortName,
                    Employees = departmentWorkers.ToList(),
                    Company = dep.Company
                };

                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult EditDepartment(DepartmentModel model)
        {
            var dep = departmentService.Get(model.DepartmentId);

            if (dep == null)
            {
                ViewBag.ErrorMessage = $"Department cannot be found";
                return View("NotFound");
            }
            else
            {
                dep.Name = model.Name;
                dep.ShortName = model.ShortName;
                dep.Employees = model.Employees;

                if (ModelState.IsValid)
                {
                    departmentService.Update(dep);
                    return Redirect("ListDepartments");
                }
                else
                {
                    return View(dep);
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult ManageDepartmentWorkers(int departmentId)
        {
            ViewBag.departmentId = departmentId;

            var dep = departmentService.Get(departmentId);

            if (dep == null)
            {
                ViewBag.ErrorMessage = $"Department cannot be found";
                return View("NotFound");
            }

            var users = userManager.Users;
            var model = new List<DepartmentUsersViewModel>();

            foreach (var employee in users)
            {
                var departmentUsersViewModel = new DepartmentUsersViewModel
                {
                    UserId = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Position = employee.Position
                };

                if (employee.Department is null)
                {
                    departmentUsersViewModel.IsSelected = false;
                }
                else if (employee.Department.DepartmentId == dep.DepartmentId)
                {
                    departmentUsersViewModel.IsSelected = true;
                }                

                model.Add(departmentUsersViewModel);
            }
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> ManageDepartmentWorkers(List<DepartmentUsersViewModel> model, int departmentId)
        {
            var dep = departmentService.Get(departmentId);

            if (dep == null)
            {
                ViewBag.ErrorMessage = $"Department cannot be found";
                return View("NotFound");
            }
            
            var depEmployeesList = new List<UserModel>();
            var user = new UserModel();
            
            foreach (var worker in model)
            {
                if (worker.IsSelected == true)
                {
                    user = await userManager.FindByIdAsync(worker.UserId);
                    user.Department = dep;
                    var result = await userManager.UpdateAsync(user);
                    depEmployeesList.Add(user);
                }
                else
                {
                    user = await userManager.FindByIdAsync(worker.UserId);
                    user.Department = null;
                    var result = await userManager.UpdateAsync(user);
                }                
            }
            dep.Employees = depEmployeesList;

            return Redirect($"EditDepartment?departmentId={dep.DepartmentId}");
        }
    }
}