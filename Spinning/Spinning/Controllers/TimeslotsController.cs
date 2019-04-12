using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spinning.Models;
using Spinning.Models.Data;
using Spinning.Models.Repositories;

namespace Spinning.Controllers
{
    public class TimeslotsController : Controller
    {
        private readonly ITimeSlotRepository _TimeslotRepository;

        public TimeslotsController(ITimeSlotRepository timeSlotRepository)
        {
            _TimeslotRepository = timeSlotRepository;
        }



        // GET: Timeslots
        public async Task<IActionResult> Index()
        {
            return View(await _TimeslotRepository.GetAllAsync());
        }

        // GET: Timeslots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeslot = await _TimeslotRepository.GetByIdAsync(id.GetValueOrDefault());
            if (timeslot == null)
            {
                return NotFound();
            }

            return View(timeslot);
        }

        // GET: Timeslots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Timeslots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,Date")] Timeslot timeslot)
        {
            if (ModelState.IsValid)
            {
                await _TimeslotRepository.CreateAsync(timeslot);
                return RedirectToAction(nameof(Index));
            }
            return View(timeslot);
        }

        // GET: Timeslots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeslot = await _TimeslotRepository.GetByIdAsync(id.GetValueOrDefault());
            if (timeslot == null)
            {
                return NotFound();
            }
            //var educationList = _TimeslotRepository.GetAllRoomNrs();
            //ViewData["Educations"] = new SelectList(educationList, "Id", "Name");
            return View(timeslot);
        }

        // POST: Timeslots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,Date")] Timeslot timeslot)
        {
            if (id != timeslot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _TimeslotRepository.EditAsync(timeslot);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_TimeslotRepository.TimeslotExist(timeslot.Id))
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

        // GET: Timeslots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeslot = await _TimeslotRepository.GetByIdAsync(id.GetValueOrDefault());
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
            var timeslot = await _TimeslotRepository.GetByIdAsync(id);
            await _TimeslotRepository.RemoveAsync(timeslot);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
