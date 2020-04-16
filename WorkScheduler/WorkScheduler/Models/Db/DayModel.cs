using System;

namespace WorkScheduler.Models
{
    public class DayModel
    {
        public int Id { get; set; }
        public string DayName { get; set; }
        public bool IsItHoliday { get; set; }       
        public DateTime Date { get; set; }
    }
}
