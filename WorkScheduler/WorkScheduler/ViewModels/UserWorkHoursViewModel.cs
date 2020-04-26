using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.ViewModels
{
    
    public class UserWorkHoursViewModel
    {
        //public int? DepartmentId { get; set; }
        public ICollection<UserModel> Employees { get; set; }
        //public ICollection<IdentityUser> User { get; set; }
        public ICollection<WorkHoursModel> WorkHours { get; set; }
        public ICollection<DepartmentModel> Departments { get; set; }
    }
}
