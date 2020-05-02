using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.Services.Interfaces
{
    public interface IDepartmentService
    {
        public bool Create(DepartmentModel department);
        public DepartmentModel Get(int departmentId);
        public IList<DepartmentModel> GetAll();
        public bool Update(DepartmentModel department);
        public bool Delete(int departmentId);
        public List<UserModel> GetEmployees(string name);
        public List<WorkHoursModel> GetEmployeesWorkHours(int id);
    }
}
