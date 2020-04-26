using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace WorkScheduler.Models
{
    public class WorkHoursModel
    {        
        [Key]
        public int WorkHoursId { get; set; }   
        //public int UserId { get; set; }
        [Range(0, 23)]
        public int StartHour { get; set; }
        [Range(0, 59)]
        public int StartMinutes { get; set; }
        [Range(0, 23)]
        public int EndHour { get; set; }
        [Range(0, 59)]
        public int EndMinutes { get; set; }        
        public UserModel Employee { get; set; }
        public string DayName { get; set; }        
        public string AdditionalInfo { get; set; }
        public DateTime Date { get; set; }              
    }
}
