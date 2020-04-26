using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.ViewModels
{
    public class EditUserDataViewModel
    {        
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Initials { get; set; }
        public string Position { get; set; }
        public int WorkTimePerWeek { get; set; }
        public DepartmentModel Department { get; set; }
        public RoleViewModel Role { get; set; }



    }
}
