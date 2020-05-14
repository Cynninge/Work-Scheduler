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

        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkHours.ToListAsync());
        }

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

        public IActionResult Create()
        {     
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkHoursId,StartHour,StartMinutes,EndHour,EndMinutes,DayName,AdditionalInfo,Date,Employee")] WorkHoursModel workHours, string id)
        {
            var user = userManager.FindByIdAsync(id);
            workHours.Employee = user.Result;            
            workHours.DayName = workHours.Date?.DayOfWeek.ToString();

            if (ModelState.IsValid)
            {               
                _context.Add(workHours);
                await _context.SaveChangesAsync();
                return RedirectToAction("ListDepartmentsForCalendar", "Calendar");
            }
            return View(workHours);
        }
                
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
