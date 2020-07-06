using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Context;
using WorkScheduler.Models;
using WorkScheduler.Services.Interfaces;

namespace WorkScheduler.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EFCContext _context;
        public DepartmentService(EFCContext context)
        {
            _context = context;
        }
        public bool Create(DepartmentModel department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int departmentId)
        {
            var department = _context.Departments.SingleOrDefault(b => b.DepartmentId == departmentId);
            if (department == null)
                return false;

            _context.Departments.Remove(department);
            return _context.SaveChanges() > 0;
        }

        public List<UserModel> GetEmployees(string name)
        {
            return _context.Users.Where(x => x.Department.Name == name).ToList();
        }
        public List<WorkHoursModel> GetEmployeesWorkHours(int id)
        {
            return _context.WorkHours.Where(x => x.Employee.Department.DepartmentId == id).ToList();
        }

        public DepartmentModel Get(int departmentId)
        {            
            return _context.Departments.SingleOrDefault(b => b.DepartmentId == departmentId);
        }       

        public IList<DepartmentModel> GetAll()
        {
            return _context.Departments.ToList();
        }

        public bool Update(DepartmentModel department)
        {
            _context.Departments.Update(department);
            return _context.SaveChanges() > 0;
        }
    }
}
