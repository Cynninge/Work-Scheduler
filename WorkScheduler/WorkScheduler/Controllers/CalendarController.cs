using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkScheduler.Context;
using WorkScheduler.Models;
using WorkScheduler.Services.Interfaces;
using WorkScheduler.ViewModels;

namespace WorkScheduler.Controllers
{
    public class CalendarController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<UserModel> userManager;
        private readonly IDepartmentService departmentService;
        private readonly ICalendarService calendarService;            
        private readonly EFCContext _context;        

        public CalendarController(RoleManager<IdentityRole> roleManager,
            UserManager<UserModel> userManager,
            IDepartmentService departmentService,
            ICalendarService calendarService,
            EFCContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.departmentService = departmentService;
            this.calendarService = calendarService;
            _context = context;
        }

        
        [HttpGet]
        public IActionResult ListDepartmentsForCalendar()
        {
            var departmentsList = departmentService.GetAll();
            return View(departmentsList);
        }     
        
        [HttpGet]
        public IActionResult OneWeekBack(DateTime date, int departmentId)
        {
            date = DateTime.Now;
            date = date.AddDays(-7);
            return RedirectToAction("Display", "Calendar", departmentId, date.ToString());
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, User")]
        public IActionResult Display(CalendarViewModel calendar, int departmentId, string dateFromString)
        {            
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.None = "-";
            #region
            var dataa = DateTime.Now;
            DateTime mondayOfLastWeek = dataa.AddDays(-(int)dataa.DayOfWeek - 6);

            //-----------------------------------------------------------------------------------

            DayOfWeek weekStart = DayOfWeek.Monday; // or Sunday, or whenever
            DateTime startingDate = DateTime.Today;

            while (startingDate.DayOfWeek != weekStart)
                startingDate = startingDate.AddDays(-1);

            DateTime previousWeekStart = startingDate.AddDays(-7);
            DateTime previousWeekEnd = startingDate.AddDays(-1);

            //--------------------------------------------------------------------------------------
            #endregion
            var date = new DateTime();
            if (dateFromString is null)
            {
                date = DateTime.Now;
            }
            else
            {
                date = DateTime.Parse(dateFromString);
            }
            
            var checkDate = new DateTime(01,01,0001, 00,00,00);
            if (date == checkDate)
            {
                date = DateTime.Now;
            }
            
            var dayOfWeek = (int)date.DayOfWeek - 1;
            if (dayOfWeek < 0) dayOfWeek = 6;
            
            var thisWeeksMonday = date.AddDays(-dayOfWeek).Date;
            var lasWeeksMonday = thisWeeksMonday.AddDays(-7);

            string daty = $"{mondayOfLastWeek}\n{previousWeekStart} - {previousWeekEnd}\n{thisWeeksMonday} - {lasWeeksMonday}";
            ViewBag.daty = daty;

            var sDays = new List<DateTime>();

            for (int i = 0; i <= 6; i++)
            {
                sDays.Add(thisWeeksMonday.AddDays(i));
            }
            
            var users = userManager.Users;
            var departments = departmentService.GetAll();
            var department = departmentService.Get(departmentId);
            var depWorkers = departmentService.GetEmployees(department.Name);
            var workHours = _context.WorkHours.Where(x => x.Employee.Department.DepartmentId == departmentId).Where(x => x.Date >= thisWeeksMonday && x.Date <= thisWeeksMonday.AddDays(6));

            
            foreach (var worker in depWorkers)
            {                
                var workerHoursList = new List<WorkHoursModel>();
                worker.WorkHours = new List<WorkHoursModel>();
                

                while (worker.WorkHours.Count() < 5)
                {
                    int count = 6 - worker.WorkHours.Count();
                    var emptyDay = new WorkHoursModel()
                    {
                        AdditionalInfo = "",
                        Date = DateTime.Now.AddDays(count),
                        DayName = DateTime.Now.AddDays(count).DayOfWeek.ToString(),
                        DisplayString = "-",
                        Employee = worker,
                        StartHour = 0,
                        StartMinutes = 0,
                        EndHour = 0,
                        EndMinutes = 0,
                    };
                    worker.WorkHours.Add(emptyDay);
                }
                worker.WorkHours.OrderBy(x => x.WorkHoursId);
            }

            foreach (var item in workHours)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(item.StartHour.ToString("00"));
                sb.Append(":");
                sb.Append(item.StartMinutes.ToString("00"));
                sb.Append("-");
                sb.Append(item.EndHour.ToString("00"));
                sb.Append(":");
                sb.Append(item.EndMinutes.ToString("00"));
                item.DisplayString = sb.ToString();
            }

            calendar = new CalendarViewModel
            {
                Workers = depWorkers,
                WorkHours = workHours.ToList(),
                Departments = departments,
                DepartmentId = department.DepartmentId,
                DepartmentName = department.Name,
                StartWeek = thisWeeksMonday,
                EndWeek = thisWeeksMonday.AddDays(6),
                SevenDays = sDays,
                Date = date                
            };       
            return View(calendar);
        }
    }
}