﻿using System;
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
using Microsoft.Extensions.Logging;

namespace Spinning.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoomsController : Controller
    {

        private readonly ILogger _logger;
        private readonly IRoomRepository _roomRepository;


        public RoomsController(IRoomRepository roomRepository, ILogger<RoomsController> logger)
        {
            _logger = logger;
            _roomRepository = roomRepository;
        }

        // GET: Rooms
        public async Task<IActionResult> Index()
        {
            try
            {
                
                return View(await _roomRepository.GetAllAsync());
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exeption: {ex}");
                throw;
            }
            
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var room = await _roomRepository.GetByIdAsync(id.GetValueOrDefault());
                if (room == null)
                {
                    return NotFound();
                }

                return View(room);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get details exeption: {ex}");
                throw;
            }
            
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BikeCount,RoomNr")] Room room)
        {
            //var CheckTimeSlot = await _context.Timeslots.Where(t => t.Date == timeslot.Date && t.RoomId == timeslot.RoomId).ToListAsync();

            //if (CheckTimeSlot.Count() == 0)
            //{
            List<Room> CheckRoom = await _roomRepository.CheckIfRoomExists(room);

            if (CheckRoom.Count == 0)
            {
                if (ModelState.IsValid)
                {
                    await _roomRepository.CreateAsync(room);
                    return RedirectToAction(nameof(Index));
                }
                return View(room);
            }

            ViewBag.ExistsError = "Room already exists";
            return View(room);

        }
        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomRepository.GetByIdAsync(id.GetValueOrDefault());
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BikeCount,RoomNr")] Room room)
        {
            if (_roomRepository.RoomExists(room) == false)
            {
                if (id != room.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _roomRepository.EditAsync(room);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!_roomRepository.RoomExists(room))
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
                return View(room);
            }
            ViewBag.ExistsError = "room already exists";
            return View(room);


        }

        //// GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomRepository.GetByIdAsync(id.GetValueOrDefault());
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            await _roomRepository.RemoveAsync(room);

            return RedirectToAction(nameof(Index));
        }


    }
}
