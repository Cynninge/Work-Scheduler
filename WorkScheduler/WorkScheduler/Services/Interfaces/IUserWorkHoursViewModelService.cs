using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;
using WorkScheduler.ViewModels;

namespace WorkScheduler.Services.Interfaces
{
    public interface IUserWorkHoursViewModelService
    {
        public IList<UserModel> DisplayWeek(DepartmentModel department);
    }
}
