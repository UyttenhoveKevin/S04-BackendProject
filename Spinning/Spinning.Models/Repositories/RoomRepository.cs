using Microsoft.EntityFrameworkCore;
using Spinning.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinning.Models.Repositories
{
    public class RoomRepository : IRoomRepository
    { 
        private readonly SpinningDBContext _context;
        public RoomRepository(SpinningDBContext context)
        {
            _context = context;
        }


        public async Task<Room> CreateAsync(Room room)
        {            
            await _context.AddAsync(room);
            await _context.SaveChangesAsync();
            return room;
        }

        

        public async Task<List<Room>> GetAllAsync()
        {

            return await _context.Rooms.ToListAsync();

        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        }

        

        public async Task RemoveAsync(Room room)
        {            
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();            
        }

        public bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }




        public async Task EditAsync(Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            //await _context.Update(room);
            //await _context.SaveChangesAsync();
        }

        public async Task<List<int>> GetRoomNrs()
        {
            var roomNrs = new List<int>();
            var rooms = _context.Rooms;
            foreach ( var room in rooms)
            {
                roomNrs.Add(room.RoomNr);
            }
            return roomNrs;
        }
    }
}
