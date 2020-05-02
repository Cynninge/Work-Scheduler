using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
            Departments = new List<DepartmentModel>();            
        }

        public string Id { get; set; }
        public int DepartmentId { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Position { get; set; }
        public int WorkTimePerWeek { get; set; }
        public string? DepartmentName { get; set; }
        public IList<DepartmentModel> Departments { get; set; }

        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
    }
}
