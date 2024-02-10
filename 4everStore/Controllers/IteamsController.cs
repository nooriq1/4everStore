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
    public class IteamsController : Controller
    {
        private readonly AppDbContext _context;

        public IteamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Iteams
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Iteams.Include(i => i.catgory);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Iteams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iteam = await _context.Iteams
                .Include(i => i.catgory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iteam == null)
            {
                return NotFound();
            }

            return View(iteam);
        }

       

        // GET: Iteams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iteam = await _context.Iteams.FindAsync(id);
            if (iteam == null)
            {
                return NotFound();
            }
            ViewData["CatgoryId"] = new SelectList(_context.catgories, "Id", "Id", iteam.CatgoryId);
            return View(iteam);
        }

        // POST: Iteams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,price,img1,img2,CatgoryId")] Iteam iteam)
        {
            if (id != iteam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iteam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IteamExists(iteam.Id))
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
            ViewData["CatgoryId"] = new SelectList(_context.catgories, "Id", "Id", iteam.CatgoryId);
            return View(iteam);
        }

        // GET: Iteams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iteam = await _context.Iteams
                .Include(i => i.catgory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iteam == null)
            {
                return NotFound();
            }

            return View(iteam);
        }

        // POST: Iteams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iteam = await _context.Iteams.FindAsync(id);
            if (iteam != null)
            {
                _context.Iteams.Remove(iteam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IteamExists(int id)
        {
            return _context.Iteams.Any(e => e.Id == id);
        }
    }
}
