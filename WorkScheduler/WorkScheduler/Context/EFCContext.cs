using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkScheduler.Models;
using WorkScheduler.ViewModels;

namespace WorkScheduler.Context
{
    public class EFCContext : IdentityDbContext<UserModel>
    {
        public EFCContext(DbContextOptions<EFCContext> options) : base(options)
        {

        }        
        public DbSet<CompanyModel> Company { get; set; }
        public DbSet<WorkHoursModel> WorkHours { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }        
        //public DbSet<UserModel> Employees { get; set; }      
    }
}
