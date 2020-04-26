using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Context;
using WorkScheduler.Models;
using WorkScheduler.Services.Interfaces;
using WorkScheduler.ViewModels;

namespace WorkScheduler.Services
{
    public class UserWorkHoursViewModelService : IUserWorkHoursViewModelService
    {
        private readonly EFCContext _context;
        public UserWorkHoursViewModelService(EFCContext context)
        {
            _context = context;
        }
        public IList<UserModel> DisplayWeek(DepartmentModel department)
        {            
            return _context.Users.Where(x => x.Department.DepartmentId == department.DepartmentId).ToList();
        }

        public IList<UserModel> GetAll()
        {
            return _context.Users.ToList();
        }
    }
}
