using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spinning.API.DTOs;
using Spinning.Models;
using Spinning.Models.Data;

namespace Spinning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeslotsController : ControllerBase
    {
        private readonly SpinningDBContext _context;
        private readonly IMapper _mapper;

        public TimeslotsController(SpinningDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Timeslots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timeslot_DTO>>> GetTimeslots()
        {
            var list = await _context.Timeslots.Include(t => t.Room).ToListAsync();
            var listDTO = list.Select(r => _mapper.Map<Timeslot_DTO>(r)).ToList();
            return listDTO;
        }

        // GET: api/Timeslots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Timeslot>> GetTimeslot(int id)
        {
            var timeslot = await _context.Timeslots.FindAsync(id);

            if (timeslot == null)
            {
                return NotFound();
            }

            return timeslot;
        }

        // PUT: api/Timeslots/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeslot(int id, Timeslot timeslot)
        {
            if (id != timeslot.Id)
            {
                return BadRequest();
            }

            _context.Entry(timeslot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeslotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Timeslots
        [HttpPost]
        public async Task<ActionResult<Timeslot>> PostTimeslot(Timeslot timeslot)
        {
            _context.Timeslots.Add(timeslot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimeslot", new { id = timeslot.Id }, timeslot);
        }

        // DELETE: api/Timeslots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Timeslot>> DeleteTimeslot(int id)
        {
            var timeslot = await _context.Timeslots.FindAsync(id);
            if (timeslot == null)
            {
                return NotFound();
            }

            _context.Timeslots.Remove(timeslot);
            await _context.SaveChangesAsync();

            return timeslot;
        }

        private bool TimeslotExists(int id)
        {
            return _context.Timeslots.Any(e => e.Id == id);
        }
    }
}
