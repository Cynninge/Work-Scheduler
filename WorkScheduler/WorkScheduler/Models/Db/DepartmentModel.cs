using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkScheduler.Models
{
    public class DepartmentModel
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        
        public CompanyModel Company { get; set; }
        public ICollection<UserModel> Employees { get; set; }
    }
}
