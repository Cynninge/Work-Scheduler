using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkScheduler.Models
{
    public class UserModel : IdentityUser<int>
    {   
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Position { get; set; }
        public int WorkTimePerWeek { get; set; }
        public int StartWorkHour { get; set; }
        public int EndWorkHour { get; set; }
        public int OverHours { get; set; }
        public DepartmentModel Department { get; set; }
        //public IdentityUser User { get; set; }
        public DateTime Created { get; set; }
        public IdentityRole RoleName { get; set; }
        //public GroupModel Group { get; set; }
    }
}
