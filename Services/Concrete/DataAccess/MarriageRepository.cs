using Marry_Me.EF.Context;
using Marry_Me.EF.Models;
using Marry_Me.Services.Abstraction.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Marry_Me.Services.Concrete.DataAccess
{
    public class MarriageRepository : IMarriageRepository
    {
        private readonly MarriageSystemDbContext _context;

        public MarriageRepository(MarriageSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Marriage> GetByIdAsync(int id)
        {
            return await _context.Marriages
                .Include(m => m.Female)
                .Include(m => m.Male)
                .Include(m => m.Divorce)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Marriage>> GetAllAsync()
        {
            return await _context.Marriages
                .Include(m => m.Female)
                .Include(m => m.Male)
                .Include(m => m.Divorce)
                .Where(m => m.Divorce == null)
                .ToListAsync();
        }


        public async Task AddAsync(Marriage marriage)
        {
            await _context.Marriages.AddAsync(marriage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Marriage marriage)
        {
            _context.Marriages.Update(marriage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var marriage = await GetByIdAsync(id);
            if (marriage != null)
            {
                _context.Marriages.Remove(marriage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Marriages.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> IsPersonMarriedAsync(int personId)
        {
            return await _context.Marriages
                .AnyAsync(m => (m.FemaleId == personId || m.MaleId == personId) && m.Divorce == null);
        }
    }

}
