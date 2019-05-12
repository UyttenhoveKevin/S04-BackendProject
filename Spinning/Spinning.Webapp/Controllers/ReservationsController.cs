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
using Spinning.Models.Repositories;

namespace Spinning.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReservationsController : Controller
    {
        private readonly SpinningDBContext _context;
        private readonly IReservationRepository _reservationRepository;

        public ReservationsController(SpinningDBContext context, IReservationRepository reservationRepository)
        {
            _context = context;
            _reservationRepository = reservationRepository;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            List<Reservation> reservations = await _reservationRepository.GetAllAsync();
            return View(reservations);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservation reservation = await _reservationRepository.GetByIdAsync(id.Value);
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
                NameDisplay = "Room: " + t.Room.RoomNr + " on: " + t.Date.ToShortDateString() + " " + t.Date.ToShortTimeString()
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
                await _reservationRepository.CreateAsync(reservation);
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

            Reservation reservation = await _reservationRepository.GetByIdAsync(id.Value);
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
                    await _reservationRepository.EditAsync(reservation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_reservationRepository.ReservationExist(reservation))
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

            Reservation reservation = await _reservationRepository.GetByIdAsync(id.Value);
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
            Reservation reservation = await _reservationRepository.GetByIdAsync(id);
            await _reservationRepository.RemoveAsync(reservation);
            return RedirectToAction(nameof(Index));
        }
    }
}