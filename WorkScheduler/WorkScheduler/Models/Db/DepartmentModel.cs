using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkScheduler.Models
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        [NotMapped]
        public UserModel Manager { get; set; }
        public CompanyModel Company { get; set; }
        public ICollection<UserModel> Users { get; set; }
        public ICollection<WeekModel> Weeks { get; set; }        
    }
}
