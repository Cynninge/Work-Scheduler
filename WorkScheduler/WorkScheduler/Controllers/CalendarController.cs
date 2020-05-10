using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public IActionResult OneWeekBack(CalendarViewModel calendar, DateTime setDate, int departmentId)
        {            
            return RedirectToAction("Display", "Calendar", new { setDate = setDate.AddDays(-7)});
        }

        [HttpGet]
        public IActionResult Display(CalendarViewModel calendar, DateTime setDate, int departmentId)
        {
            var dateCheck = new DateTime();
            if (DateTime.Equals(setDate, dateCheck))
            {
                setDate = DateTime.Now;
            }
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

            var date = setDate;
            var dayOfWeek = (int)date.DayOfWeek - 1;
            if (dayOfWeek < 0) dayOfWeek = 6;

            var thisWeeksMonday = date.AddDays(-dayOfWeek).Date;
            var lasWeeksMonday = thisWeeksMonday.AddDays(-7);

            string daty = $"{mondayOfLastWeek}\n{previousWeekStart} - {previousWeekEnd}\n{thisWeeksMonday} - {lasWeeksMonday}";
            ViewBag.daty = daty;
           

            //var user = userManager.FindByIdAsync(id);

            var users = userManager.Users;
            var departments = departmentService.GetAll();
            var department = departmentService.Get(departmentId);
            var depWorkers = departmentService.GetEmployees(department.Name);
            var workHours = _context.WorkHours.Where(x => x.Employee.Department.DepartmentId == departmentId).Where(x => x.Date >= thisWeeksMonday && x.Date <= thisWeeksMonday.AddDays(6));
            
            if (workHours.Count() < 7)
            {
                for (int i = workHours.Count(); i < 7; i++)
                {
                    workHours.ToList().Add(new WorkHoursModel());
                }
            }

            var sDays = new List<DateTime>();

            for (int i = 0; i <= 6; i++)
            {
                sDays.Add(thisWeeksMonday.AddDays(i));
            }

            foreach (var item in workHours)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(item.StartHour);
                sb.Append(":");
                sb.Append(item.StartMinutes);
                sb.Append("-");
                sb.Append(item.EndHour);
                sb.Append(":");
                sb.Append(item.EndMinutes);
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
                SevenDays = sDays
            };            

            return View(calendar);
        }
    }
}