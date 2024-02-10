using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _4everStore.Data;
using _4everStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace _4everStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class catgoryController : Controller
    {
        private readonly AppDbContext _context;

        public catgoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: catgory
        public async Task<IActionResult> Index()
        {
            return View(await _context.catgories.ToListAsync());
        }

        // GET: catgory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catgory = await _context.catgories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catgory == null)
            {
                return NotFound();
            }

            return View(catgory);
        }

        // GET: catgory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: catgory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Catgory catgory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catgory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catgory);
        }

        // GET: catgory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catgory = await _context.catgories.FindAsync(id);
            if (catgory == null)
            {
                return NotFound();
            }
            return View(catgory);
        }

        // POST: catgory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Catgory catgory)
        {
            if (id != catgory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catgory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatgoryExists(catgory.Id))
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
            return View(catgory);
        }

        // GET: catgory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catgory = await _context.catgories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catgory == null)
            {
                return NotFound();
            }

            return View(catgory);
        }

        // POST: catgory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catgory = await _context.catgories.FindAsync(id);
            if (catgory != null)
            {
                _context.catgories.Remove(catgory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatgoryExists(int id)
        {
            return _context.catgories.Any(e => e.Id == id);
        }
    }
}
