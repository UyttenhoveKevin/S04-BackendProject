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
    public class TimeslotsController : Controller
    {
        private readonly ITimeSlotRepository _timeslotRepository;

        public TimeslotsController(ITimeSlotRepository timeSlotRepository)
        {
            _timeslotRepository = timeSlotRepository;
        }


        // GET: Timeslots
        public async Task<IActionResult> Index()
        {
            return View(await _timeslotRepository.GetAllAsync());
        }

        // GET: Timeslots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Timeslot timeslot = await _timeslotRepository.GetByIdAsync(id.GetValueOrDefault());
            if (timeslot == null)
            {
                return NotFound();
            }

            return View(timeslot);
        }

        // GET: Timeslots/Create
        public async Task<IActionResult> Create()
        {
            List<Room> timeslotdata = await _timeslotRepository.GetTimeSlotData();
            ViewData["RoomNr"] = new SelectList(timeslotdata, "Id", "RoomNr");
            return View();
        }

        // POST: Timeslots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,Date")] Timeslot timeslot)
        {
            //controleer als timeslot al bestaad
            List<Timeslot> checktimeslot = await _timeslotRepository.CheckTimeslot(timeslot);
            if (checktimeslot.Count == 0)
            {
                if (ModelState.IsValid)
                {
                    await _timeslotRepository.CreateAsync(timeslot);
                    return RedirectToAction(nameof(Index));
                }

                return View(timeslot);
            }

            ViewBag.ExistsError = "Timeslot already exists";
            List<Room> timeslotdata = await _timeslotRepository.GetTimeSlotData();
            ViewData["RoomNr"] = new SelectList(timeslotdata, "Id", "RoomNr");
            return View(timeslot);
        }

        // GET: Timeslots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Timeslot timeslot = await _timeslotRepository.GetByIdAsync(id.GetValueOrDefault());
            if (timeslot == null)
            {
                return NotFound();
            }

            //var timeslotdata = await _context.Rooms.ToListAsync();
            List<Room> timeslotdata = await _timeslotRepository.GetTimeSlotData();
            ViewData["RoomNr"] = new SelectList(timeslotdata, "Id", "RoomNr");
            return View(timeslot);
        }

        // POST: Timeslots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,Date")] Timeslot timeslot)
        {
            

            if (_timeslotRepository.TimeslotExist(timeslot) == false)
            {
                if (id != timeslot.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _timeslotRepository.EditAsync(timeslot);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!_timeslotRepository.TimeslotExist(timeslot))
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

                return View(timeslot);
            }

            List<Room> timeslotdata = await _timeslotRepository.GetTimeSlotData();
            ViewData["RoomNr"] = new SelectList(timeslotdata, "Id", "RoomNr");
            ViewBag.ExistsError = "Timeslot already exists";
            return View(timeslot);
        }

        // GET: Timeslots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Timeslot timeslot = await _timeslotRepository.GetByIdAsync(id.GetValueOrDefault());
            if (timeslot == null)
            {
                return NotFound();
            }

            return View(timeslot);
        }

        // POST: Timeslots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Timeslot timeslot = await _timeslotRepository.GetByIdAsync(id);
            await _timeslotRepository.RemoveAsync(timeslot);
            return RedirectToAction(nameof(Index));
        }
    }
}