using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkScheduler.Services.Interfaces
{
    public interface ICalendarService
    {
        public DateTime SetDate(DateTime date);
        public DateTime OneWeekBack(DateTime date);
        public DateTime OneWeekForward(DateTime date);
        public DateTime CurrentWeek();

    }
}
