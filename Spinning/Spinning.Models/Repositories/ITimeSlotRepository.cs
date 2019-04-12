using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spinning.Models.Repositories
{
    public interface ITimeSlotRepository
    {
        Task<List<Timeslot>> GetAllAsync();
        Task<Timeslot> GetByIdAsync(int id);
        Task RemoveAsync(Timeslot timeslot);
        Task <Timeslot> CreateAsync(Timeslot timeslot);
        Task EditAsync(Timeslot timeslot);
        bool TimeslotExist(int id);
        Task<IEnumerable<int>> GetAllRoomNrs();
    }
}