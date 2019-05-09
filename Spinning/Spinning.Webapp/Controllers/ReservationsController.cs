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
    [Authorize(Roles = "Admin")]

    public class ReservationsController : Controller
    {
        private readonly SpinningDBContext _context;

        public ReservationsController(SpinningDBContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var spinningDBContext = _context.Reservations.Include(r => r.SpinningUser).Include(r => r.Timeslot).ThenInclude(t=>t.Room);
            return View(await spinningDBContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.SpinningUser)
                .Include(r => r.Timeslot)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["SpinningUserId"] = new SelectList(_context.SpinningUsers, "Id", "UserName");
            var timeslotViewData = _context.Timeslots.Select(t => new
            {
                t.Id,
                NameDisplay = "Room: " + t.Room.RoomNr + " on: " + t.Date.ToShortDateString() +" "+ t.Date.ToShortTimeString()
            });
            ViewData["TimeSlotId"] = new SelectList(timeslotViewData, "Id", "NameDisplay");

            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TimeSlotId,SpinningUserId")] Reservation reservation)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpinningUserId"] = new SelectList(_context.SpinningUsers, "Id", "Id", reservation.SpinningUserId);
            ViewData["TimeSlotId"] = new SelectList(_context.Timeslots, "Id", "Id", reservation.TimeSlotId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }


            ViewData["SpinningUserId"] = new SelectList(_context.SpinningUsers, "Id", "UserName");
            var timeslotViewData = _context.Timeslots.Select(t => new
            {
                t.Id,
                NameDisplay = "Room: " + t.Room.RoomNr + " on: " + t.Date.ToShortDateString() + " " + t.Date.ToShortTimeString()
            });
            ViewData["TimeSlotId"] = new SelectList(timeslotViewData, "Id", "NameDisplay");
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TimeSlotId,SpinningUserId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["SpinningUserId"] = new SelectList(_context.SpinningUsers, "Id", "Id", reservation.SpinningUserId);
            ViewData["TimeSlotId"] = new SelectList(_context.Timeslots, "Id", "Id", reservation.TimeSlotId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.SpinningUser)
                .Include(r => r.Timeslot)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
