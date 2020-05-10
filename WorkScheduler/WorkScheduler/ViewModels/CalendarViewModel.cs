using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.ViewModels
{
    public class CalendarViewModel
    {
        public CalendarViewModel()
        {            
            Departments = new List<DepartmentModel>();
            Workers = new List<UserModel>();
            WorkHours = new List<WorkHoursModel>();
            DayName = DateTime.Now.DayOfWeek.ToString();
            SevenDays = new List<DateTime>();
        }

        public string? Id { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? UserName { get; set; }
        public string? DayName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Initials { get; set; }        
        public int? WorkTimePerWeek { get; set; }
        public string DisplayString { get; set; }
        public DateTime Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartWeek { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndWeek { get; set; }
        public IList<DepartmentModel>? Departments { get; set; }
        public IList<UserModel>? Workers { get; set; }
        public IList<WorkHoursModel>? WorkHours { get; set; }
        [DataType(DataType.Date)]
        public IList<DateTime>? SevenDays { get; set; }
    }
}
