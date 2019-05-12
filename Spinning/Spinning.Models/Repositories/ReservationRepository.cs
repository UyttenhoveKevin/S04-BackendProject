using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Spinning.Models.Data;

namespace Spinning.Models.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly SpinningDBContext _context;

        public ReservationRepository(SpinningDBContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.SpinningUser)
                .Include(r => r.Timeslot)
                .ThenInclude(t => t.Room)
                .ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.SpinningUser)
                .Include(r => r.Timeslot)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task RemoveAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task EditAsync(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool ReservationExist(Reservation reservation)
        {
            return _context.Reservations.Any(r => r.SpinningUserId == reservation.SpinningUserId && r.TimeSlotId == reservation.TimeSlotId);
        }
    }
}