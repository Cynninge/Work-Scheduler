using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Services.Interfaces;

namespace WorkScheduler.Services
{
    public class CalendarService : ICalendarService
    {
        public DateTime CurrentWeek()
        {
            return DateTime.Now;            
        }

        public DateTime OneWeekBack(DateTime date)
        {
            return date.AddDays(-7);
        }

        public DateTime OneWeekForward(DateTime date)
        {
            return date.AddDays(7);
        }

        public DateTime SetDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
