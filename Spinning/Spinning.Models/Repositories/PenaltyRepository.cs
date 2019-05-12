using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spinning.Models.Data;

namespace Spinning.Models.Repositories
{
    public class PenaltyRepository : IPenaltyRepository
    {
        private readonly SpinningDBContext _context;

        public PenaltyRepository(SpinningDBContext context)
        {
            _context = context;
        }

        public async Task<List<Penalty>> GetAllAsync()
        {
            return await _context.Penalties.Include(p => p.SpinningUser).ToListAsync();
        }

        public async Task<Penalty> GetByIdAsync(int id)
        {
            return await _context.Penalties.Include(p => p.SpinningUser).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task RemoveAsync(Penalty penalty)
        {
            _context.Penalties.Remove(penalty);
            await _context.SaveChangesAsync();
        }

        public async Task<Penalty> CreateAsync(Penalty penalty)
        {
            await _context.Penalties.AddAsync(penalty);
            await _context.SaveChangesAsync();
            return penalty;
        }

        public async Task EditAsync(Penalty penalty)
        {
            _context.Entry(penalty).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool PenaltyExist(Penalty penalty)
        {
            return _context.Timeslots.Any(t => t.Id == penalty.Id);

        }
    }
}