using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduler.Models;

namespace WorkScheduler.Services.Interfaces
{
    public interface ICompanyService
    {
        public bool Create(CompanyModel company);
        public CompanyModel Get(int id);       
        public bool Update(DepartmentModel department);        
    }
}
