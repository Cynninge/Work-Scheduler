using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkScheduler.ViewModels
{
    public class DepartmentUsersViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public bool IsSelected { get; set; }
    }
}
