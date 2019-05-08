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
    public class ReservationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SpinningDBContext _context;

        public ReservationsController(SpinningDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation_DTO>>> GetReservations()
        {
            var list = await _context.Reservations.Include(r=>r.Timeslot).ToListAsync();
            var listDTO = list.Select(r => _mapper.Map<Reservation_DTO>(r)).ToList();
            return listDTO;
        }

        // GET: api/Reservations/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Reservation_DTO>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDTO = _mapper.Map<Reservation_DTO>(reservation);
            return reservationDTO;
        } 


        // GET: api/Reservations/{GUID}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<Reservation_DTO>>> GetReservationsForUserId(string id)
        {
            var reservation = await _context.Reservations.Include(r => r.Timeslot).ThenInclude(t=>t.Room).Where(r => r.SpinningUser.Id == id).ToListAsync();
            
            if (reservation == null)
            {
                return NotFound();
            }

            var list = await _context.Reservations.Where(r => r.SpinningUser.Id == id).ToListAsync();
            var listDTO = list.Select(r => _mapper.Map<Reservation_DTO>(r)).ToList();
            return listDTO;

            //var reservationDTO = _mapper.Map<Reservation_DTO>(reservation);
            //return reservation;
        }

        //// PUT: api/Reservations/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        //{
        //    if (id != reservation.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(reservation).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ReservationExists(id))
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

        // POST: api/Reservations
        [HttpPost]
        public async Task<ActionResult<Reservation_DTO>> PostReservation(Reservation reservation)
        {
            
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations
        // Delete zonder gebruik te maken van Reservation Id
        [HttpDelete]
        public async Task<ActionResult<Reservation_DTO>> DeleteReservation(Reservation_DTO reservation_DTO)
        {
            var reservation =await  _context.Reservations.FirstOrDefaultAsync(r => r.SpinningUserId == reservation_DTO.SpinningUserId && r.TimeSlotId == reservation_DTO.TimeSlotId);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return reservation_DTO;
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
