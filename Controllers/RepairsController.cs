using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MvcGarage.Data;
using MvcGarage.Models;

namespace MvcGarage.Controllers
{
    public class RepairsController : Controller {
        private readonly RepairContext _context;
        private ILogger _logger;

        public RepairsController(RepairContext ctx, ILoggerFactory logfactory) {
            _context = ctx;
            _logger = logfactory.CreateLogger("MvcGarage.Controllers.RepairsController");
        }

        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter, 
            string searchString,
            int? page)
        {
            _logger.LogDebug(1,"Executing Index() method....");
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            };
            ViewData["CurrentFilter"] = searchString;

            var repairs = from s in _context.Repairs select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                repairs = repairs.Where(s => s.Vehicle.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    repairs = repairs.OrderByDescending(s => s.Vehicle);
                    break;
                case "Date":
                    repairs = repairs.OrderBy(s => s.RepairDate);
                    break;
                case "date_desc":
                    repairs = repairs.OrderByDescending(s => s.RepairDate);
                    break;
                default:
                    repairs = repairs.OrderBy(s => s.Vehicle);
                    break;
            }
            // return View(await repairs.AsNoTracking().ToListAsync());
            int pageSize = 15;
            return View(await PaginatedList<Repair>.CreateAsync(repairs.AsNoTracking(), page ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _context.Repairs.SingleOrDefaultAsync(m => m.ID == id);

            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        // GET Repairs/Create
        public IActionResult Create() {
            Repair repair = new Repair();
            
            return View(repair);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Vehicle,RepairDate,RepairType,Miles,Workshop,Workdone")] Repair repair)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(repair);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException ex)
            {
                //Log the error (uncomment ex variable name and write a log.
                _logger.LogError("Encountered exception on create-save: " + ex.Message);
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(repair);
        }

        // GET: Repairs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _context.Repairs.SingleOrDefaultAsync(m => m.ID == id);
            if (repair == null)
            {
                return NotFound();
            }
            return View(repair);
        }

        // POST: Repairs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Vehicle,RepairDate,RepairType,Miles,Workshop,Workdone")] Repair repair)
        {
            if (id != repair.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ue)
                {
                    _logger.LogError("Encountered exception on edit-save: " + ue.Message);
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(repair);
        }

        // GET: Repairs/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Repairs
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "The delete database operation failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(student);
        }

        // POST: Repairs/Delete/5
        [HttpPost, ActionNameAttribute("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Repair rDelete = new Repair() { ID = id };
                _context.Entry(rDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
        }
    }
}