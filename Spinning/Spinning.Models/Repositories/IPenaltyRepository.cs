using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spinning.Models.Repositories
{
    public interface IPenaltyRepository
    {
        Task<List<Penalty>> GetAllAsync();
        Task<Penalty> GetByIdAsync(int id);
        Task RemoveAsync(Penalty penalty);
        Task<Penalty> CreateAsync(Penalty penalty);
        Task EditAsync(Penalty penalty);
        bool PenaltyExist(Penalty penalty);
    }
}