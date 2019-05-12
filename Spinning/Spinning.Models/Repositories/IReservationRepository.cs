using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spinning.Models.Repositories
{
    public interface IReservationRepository
    {
        
        Task<List<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        Task RemoveAsync(Reservation reservation);
        Task<Reservation> CreateAsync(Reservation reservation);
        Task EditAsync(Reservation reservation);
        bool ReservationExist(Reservation reservation);
    }
}