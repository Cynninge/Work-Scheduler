using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkScheduler.Context;
using WorkScheduler.Models;
using WorkScheduler.ViewModels;

namespace WorkScheduler.Controllers
{
    
    public class WorkHoursController : Controller
    {
        private readonly EFCContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<UserModel> userManager;

        public WorkHoursController(RoleManager<IdentityRole> roleManager,
                                        UserManager<UserModel> userManager,
                                        EFCContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _context = context;
        }

      

        // GET: WorkHours
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkHours.ToListAsync());
        }

        // GET: WorkHours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workHoursModel = await _context.WorkHours
                .FirstOrDefaultAsync(m => m.WorkHoursId == id);
            if (workHoursModel == null)
            {
                return NotFound();
            }

            return View(workHoursModel);
        }

        // GET: WorkHours/Create
        public IActionResult Create()
        {
            

            return View();
        }

        // POST: WorkHours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkHoursId,StartHour,StartMinutes,EndHour,EndMinutes,DayName,AdditionalInfo,Date,Employee")] WorkHoursModel workHours, string id)
        {
            var user = userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            workHours.Employee = user;            
            workHours.DayName = workHours.Date?.DayOfWeek.ToString();


            if (ModelState.IsValid)
            {               
                _context.Add(workHours);
                await _context.SaveChangesAsync();
                return Redirect($"Calendar/Display/{user.Department.DepartmentId}");
            }
            return View(workHours);
        }

        // GET: WorkHours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workHoursModel = await _context.WorkHours.FindAsync(id);
            if (workHoursModel == null)
            {
                return NotFound();
            }
            return View(workHoursModel);
        }

        // POST: WorkHours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkHoursId,StartHour,StartMinutes,EndHour,EndMinutes,DayName,AdditionalInfo,Date")] WorkHoursModel workHoursModel)
        {
            if (id != workHoursModel.WorkHoursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workHoursModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkHoursModelExists(workHoursModel.WorkHoursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(workHoursModel);
        }

        // GET: WorkHours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workHoursModel = await _context.WorkHours
                .FirstOrDefaultAsync(m => m.WorkHoursId == id);
            if (workHoursModel == null)
            {
                return NotFound();
            }

            return View(workHoursModel);
        }

        // POST: WorkHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workHoursModel = await _context.WorkHours.FindAsync(id);
            _context.WorkHours.Remove(workHoursModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkHoursModelExists(int id)
        {
            return _context.WorkHours.Any(e => e.WorkHoursId == id);
        }
    }
}
