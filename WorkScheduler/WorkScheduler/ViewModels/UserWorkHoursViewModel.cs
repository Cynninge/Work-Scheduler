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
        public UserWorkHoursViewModel()
        {            
            Workers = new List<UserModel>();
            WorkHours = new List<WorkHoursModel>();
        }

        public string DepartmentId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Company { get; set; }
        public IList<UserModel> Workers { get; set; }
        public IList<WorkHoursModel> WorkHours { get; set; }
    }
}
