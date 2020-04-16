using System.Collections.Generic;

namespace WorkScheduler.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Logo { get; set; }
        public string StreetAndNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<DepartmentModel> Departments { get; set; }
    }
}
