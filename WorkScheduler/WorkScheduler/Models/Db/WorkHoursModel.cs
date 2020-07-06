using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkScheduler.Models
{
    public class WorkHoursModel
    {

        public WorkHoursModel()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(StartHour);
            sb.Append(":");
            sb.Append(StartMinutes);
            sb.Append("-");
            sb.Append(EndHour);
            sb.Append(":");
            sb.Append(EndMinutes);
            DisplayString = sb.ToString();
        }

        [Key]
        public int WorkHoursId { get; set; } = 0;
       
        [Range(0, 23)]
        public int StartHour { get; set; } = 0;
        [Range(0, 59)]
        public int StartMinutes { get; set; } = 0;
        [Range(0, 23)]
        public int EndHour { get; set; } = 0;
        [Range(0, 59)]
        public int EndMinutes { get; set; } = 0;
        public UserModel Employee { get; set; }
        public string DayName { get; set; } = "-";
       
        public string AdditionalInfo { get; set; } = "-";
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        public string DisplayString { get; set; } = "-";
        [DataType(DataType.Time)]
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
