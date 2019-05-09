using System.Collections.Generic;
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
        bool RoomExists(Room room);
        Task<List<int>> GetRoomNrs();
        Task<List<Room>> CheckIfRoomExists(Room room);
    }
}