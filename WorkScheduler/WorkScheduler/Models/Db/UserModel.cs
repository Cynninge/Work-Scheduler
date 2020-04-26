using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkScheduler.Models
{
    public class UserModel : IdentityUser
    { 
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
        public string Initials { get; set; }
        public string Position { get; set; }        
        public int WorkTimePerWeek { get; set; }
        public int OverHours { get; set; }        
        public ICollection<WorkHoursModel> WorkHours { get; set; }       
        public DepartmentModel Department { get; set; }
        public DateTime Created { get; set; }
        public IdentityRole RoleName { get; set; }
    }
}
