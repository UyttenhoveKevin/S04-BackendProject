using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spinning.Models;
using Spinning.Models.Data;

namespace Spinning.WebApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PenaltiesController : Controller
    {
        private readonly SpinningDBContext _context;

        public PenaltiesController(SpinningDBContext context)
        {
            _context = context;
        }

        // GET: Penalties
        public async Task<IActionResult> Index()
        {
            var spinningDBContext = _context.Penalties.Include(p => p.SpinningUser);
            return View(await spinningDBContext.ToListAsync());
        }

        // GET: Penalties/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalty = await _context.Penalties
                .Include(p => p.SpinningUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penalty == null)
            {
                return NotFound();
            }

            return View(penalty);
        }

        // GET: Penalties/Create
        public IActionResult Create()
        {
            
            ViewData["SpinningUserId"] = new SelectList(_context.SpinningUsers, "Id", "UserName");
            return View();
        }

        // POST: Penalties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpinningUserId,StartdatePenalty,EnddatePenalty")] Penalty penalty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(penalty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpinningUserId"] = new SelectList(_context.SpinningUsers, "Id", "UserName", penalty.SpinningUserId);
            return View(penalty);
        }

        // GET: Penalties/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalty = await _context.Penalties.FindAsync(id);
            if (penalty == null)
            {
                return NotFound();
            }
            ViewData["SpinningUserId"] = new SelectList(_context.SpinningUsers, "Id", "UserName", penalty.SpinningUserId);
            return View(penalty);
        }

        // POST: Penalties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SpinningUserId,StartdatePenalty,EnddatePenalty")] Penalty penalty)
        {
            if (id != penalty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penalty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenaltyExists(penalty.Id))
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
            ViewData["SpinningUserId"] = new SelectList(_context.SpinningUsers, "Id", "UserName", penalty.SpinningUserId);
            return View(penalty);
        }

        // GET: Penalties/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penalty = await _context.Penalties
                .Include(p => p.SpinningUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penalty == null)
            {
                return NotFound();
            }

            return View(penalty);
        }

        // POST: Penalties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var penalty = await _context.Penalties.FindAsync(id);
            _context.Penalties.Remove(penalty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenaltyExists(int id)
        {
            return _context.Penalties.Any(e => e.Id == id);
        }
    }
}
