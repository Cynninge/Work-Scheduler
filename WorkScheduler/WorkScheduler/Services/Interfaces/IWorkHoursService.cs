using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.Services.Interfaces
{
    public interface IWorkHoursService
    {
        public List<WorkHoursModel> UserWorkedHours(string id);

        public int MonthlyWorkedHours(string id, DateTime month);
    }
}
