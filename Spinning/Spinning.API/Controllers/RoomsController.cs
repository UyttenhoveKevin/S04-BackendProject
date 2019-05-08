using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spinning.Models;
using Spinning.Models.Data;
using Spinning.Models.Repositories;

namespace Spinning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly SpinningDBContext _context;
        private readonly IRoomRepository _roomRepository;

        public RoomsController(SpinningDBContext context, IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
            _context = context;
        }

        //// GET: api/Rooms
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        //{
        //    return await _roomRepository.GetAllAsync();
        //}

        //// GET: api/Rooms/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Room>> GetRoom(int id)
        //{
        //    var room = await _roomRepository.GetByIdAsync(id);

        //    if (room == null)
        //    {
        //        return NotFound();
        //    }

        //    return room;
        //}

        //// PUT: api/Rooms/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRoom(int id, Room room)
        //{
        //    if (id != room.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(room).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RoomExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

    //    // POST: api/Rooms
    //    [HttpPost]
    //    public async Task<ActionResult<Room>> PostRoom(Room room)
    //    {
    //        _context.Rooms.Add(room);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetRoom", new { id = room.Id }, room);
    //    }

    //    // DELETE: api/Rooms/5
    //    [HttpDelete("{id}")]
    //    public async Task<ActionResult<Room>> DeleteRoom(int id)
    //    {
    //        var room = await _context.Rooms.FindAsync(id);
    //        if (room == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Rooms.Remove(room);
    //        await _context.SaveChangesAsync();

    //        return room;
    //    }

    //    private bool RoomExists(int id)
    //    {
    //        return _context.Rooms.Any(e => e.Id == id);
    //    }
    }
}
