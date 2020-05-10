using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
