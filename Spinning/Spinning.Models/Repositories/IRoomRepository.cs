﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spinning.Models.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
        Task<Room> CreateAsync(Room room);
        Task EditAsync(Room room);
        Task RemoveAsync(Room room);
        bool RoomExists(int id);
        Task<List<int>> GetRoomNrs();
    }
}