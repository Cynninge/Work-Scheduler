using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Context;
using WorkScheduler.Models;
using WorkScheduler.Services.Interfaces;

namespace WorkScheduler.Services
{
    public class WorkHoursService : IWorkHoursService
    {
        private readonly EFCContext _context;
        public WorkHoursService(EFCContext context)
        {
            _context = context;
        }


        public List<WorkHoursModel> UserWorkedHours (string id)
        {
            return _context.WorkHours.Where(userId => userId.Employee.Id == id).ToList();
        }

        public int MonthlyWorkedHours(string id, DateTime date)
        {
            var month = date.Date.Month;
            var workedHoursThisMonth = UserWorkedHours(id).Where(x => x.Date.Month == month);

            foreach (var item in workedHoursThisMonth)
            {
                
            }
            return 2;
        }
    }
}
