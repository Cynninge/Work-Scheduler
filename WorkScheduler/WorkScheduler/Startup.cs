using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkScheduler.Context;
using WorkScheduler.Models;
using WorkScheduler.Services;
using WorkScheduler.Services.Interfaces;

namespace WorkScheduler
{
    public class Startup
    {
        public Startup(/*IConfiguration configuration*/)
        {
            //Configuration = configuration;
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddXmlFile("appsettings.xml");
            Configuration = configurationBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<EFCContext>(builder =>
            {
                builder.UseSqlServer(Configuration["DefaultConnection"]);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireClaim("Edit Role"));

                options.AddPolicy("CreateRolePolicy",
                    policy => policy.RequireClaim("Create Role"));
            });

            services.AddIdentity<UserModel, IdentityRole>().AddEntityFrameworkStores<EFCContext>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
