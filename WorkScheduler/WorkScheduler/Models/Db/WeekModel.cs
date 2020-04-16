using System;
using System.Collections.Generic;

namespace WorkScheduler.Models
{
    public class WeekModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DepartmentModel Department { get; set; }
        public ICollection<DayModel> Days { get; set; }
    }
}
