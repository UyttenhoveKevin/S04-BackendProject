using Microsoft.EntityFrameworkCore;
using Spinning.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinning.Models.Repositories
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly SpinningDBContext _context;
        public TimeSlotRepository(SpinningDBContext context)
        {
            _context = context;
        }

        public async Task<Timeslot> CreateAsync(Timeslot timeslot)
        {
            await _context.AddAsync(timeslot);
            await _context.SaveChangesAsync();
            return timeslot;
        }

        public async Task EditAsync(Timeslot timeslot)
        {
            _context.Entry(timeslot).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Timeslot>> GetAllAsync()
        {
            return await _context.Timeslots.Include(t=>t.Room).ToListAsync(); 
        }

        public async Task<IEnumerable<int>> GetAllRoomNrs()
        {
            var roomNrs = new List<int>();
            var rooms = _context.Rooms;
            foreach (var room in rooms)
            {
                roomNrs.Add(room.RoomNr);
            }

            return roomNrs;
        }

        public async Task<Timeslot> GetByIdAsync(int id)
        {
            return await _context.Timeslots.Include(t=>t.Room).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task RemoveAsync(Timeslot timeslot)
        {
            _context.Timeslots.Remove(timeslot);
            await _context.SaveChangesAsync();
        }

        public bool TimeslotExist(int id)
        {
            return _context.Rooms.Any(t => t.Id == id);
        }
    }
}
