using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.ViewModels
{
    public class UserHoursViewModel
    {
        public UserModel user { get; set; }
        public WorkHoursModel hours { get; set; }
    }
}
