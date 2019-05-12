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
using Spinning.Models.Repositories;

namespace Spinning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SpinningDBContext _context;
        private readonly IReservationRepository _reservationRepository;

        public ReservationsController(SpinningDBContext context, IMapper mapper, IReservationRepository reservationRepository)
        {
            _context = context;
            _mapper = mapper;
            _reservationRepository = reservationRepository;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation_DTO>>> GetReservations()
        {
            List<Reservation> list = await _reservationRepository.GetAllAsync();
            List<Reservation_DTO> listDTO = list.Select(r => _mapper.Map<Reservation_DTO>(r)).ToList();
            return listDTO;
        }

        // GET: api/Reservations/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Reservation_DTO>> GetReservation(int id)
        {
            Reservation reservation = await _reservationRepository.GetByIdAsync(id);

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
            List<Reservation> reservations = await _context.Reservations.Where(r => r.SpinningUser.Id == id).ToListAsync();
            if (reservations == null)
            {
                return NotFound();
            }

            List<Reservation_DTO> listDTO = reservations.Select(r => _mapper.Map<Reservation_DTO>(r)).ToList();
            return listDTO;
            
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
        public async Task<ActionResult<Reservation_DTO>> PostReservation(Reservation_DTO reservationDto)
        {
            try
            {
                var reservation = _mapper.Map<Reservation>(reservationDto);
                await _reservationRepository.CreateAsync(reservation);
                return CreatedAtAction("GetReservation", new {id = reservation.Id}, reservation);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Reservations
        // Delete zonder gebruik te maken van Reservation Id
        [HttpDelete]
        public async Task<ActionResult<Reservation_DTO>> DeleteReservation(Reservation_DTO reservationDto)
        {
            Reservation reservation = await _context.Reservations.FirstOrDefaultAsync(r =>
                r.SpinningUserId == reservationDto.SpinningUserId && r.TimeSlotId == reservationDto.TimeSlotId);

            if (reservation == null)
            {
                return NotFound();
            }

            await _reservationRepository.RemoveAsync(reservation);

            return reservationDto;
        }
    }
}