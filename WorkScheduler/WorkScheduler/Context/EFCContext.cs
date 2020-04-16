using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkScheduler.Models;

namespace WorkScheduler.Context
{
    public class EFCContext : IdentityDbContext
    {
        public EFCContext(DbContextOptions<EFCContext> options) : base(options)
        {

        }

        //public override DbSet<UserModel> Users { get; set; }

        public DbSet<WeekModel> Weeks { get; set; }
        public DbSet<CompanyModel> Company { get; set; }
        public DbSet<DayModel> Days { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
    }
}
