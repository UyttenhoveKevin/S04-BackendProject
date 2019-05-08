using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spinning.Models;
using Spinning.Models.Data;

namespace Spinning.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]

    public class SpinningUsersController : Controller
    {
        private readonly SpinningDBContext _context;
        private readonly UserManager<SpinningUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SpinningUsersController(SpinningDBContext context, UserManager<SpinningUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Penalties
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpinningUsers.ToListAsync());
        }

        // GET: Penalties/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spinningUser = await _context.SpinningUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spinningUser == null)
            {
                return NotFound();
            }

            return View(spinningUser);
        }

        // GET: Penalties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Penalties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpinningUser spinningUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spinningUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spinningUser);
        }

        // GET: Penalties/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spinningUser = await _context.SpinningUsers.FindAsync(id);
            if (spinningUser == null)
            {
                return NotFound();
            }
            return View(spinningUser);
        }

        // POST: Penalties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,  SpinningUser spinningUser)
        {
            if (id != spinningUser.Id)
            {
                return NotFound();
            }

            try
            {
                var userInDb = await _context.SpinningUsers.FirstOrDefaultAsync(e => e.Id == id);
                IList<string> roles = await _userManager.GetRolesAsync(userInDb);
                await _userManager.RemoveFromRolesAsync(userInDb, roles);           

                userInDb.AccessFailedCount = spinningUser.AccessFailedCount;
                userInDb.Email = spinningUser.Email;
                userInDb.FirstName = spinningUser.FirstName;
                userInDb.LastName = spinningUser.LastName;
                userInDb.LockoutEnabled = spinningUser.LockoutEnabled;
                userInDb.PhoneNumber = spinningUser.PhoneNumber;
                userInDb.Reservations = spinningUser.Reservations;
                userInDb.UserName = spinningUser.UserName;
                _context.Entry(userInDb).State = EntityState.Modified;

                await _userManager.UpdateAsync(userInDb);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpinningUserExists(spinningUser.Id))
                {
                    return NotFound();
                }

                throw;
            }


            return RedirectToAction(nameof(Index));
        }

        // GET: Penalties/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spinningUser = await _context.SpinningUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spinningUser == null)
            {
                return NotFound();
            }

            return View(spinningUser);
        }

        // POST: Penalties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var spinningUser = await _context.SpinningUsers.FindAsync(id);
            _context.SpinningUsers.Remove(spinningUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpinningUserExists(string id)
        {
            return _context.SpinningUsers.Any(e => e.Id == id);
        }
    }
}
