using Marry_Me.DTOs;
using Marry_Me.EF.Context;
using Marry_Me.EF.Models;
using Marry_Me.Services.Abstraction.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Marry_Me.Services.Concrete.DataAccess
{
    public class DivorceRepository : IDivorceRepository
    {
        private readonly MarriageSystemDbContext _context;

        public DivorceRepository(MarriageSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Divorce> GetByMarriageIdAsync(int marriageId)
        {
            return await _context.Divorces
                .Include(d => d.Marriage)
                .FirstOrDefaultAsync(d => d.MarriageId == marriageId);
        }

        public async Task<Divorce> CreateDivorceAsync(Divorce divorce)
        {
            var marriage = await _context.Marriages
                .FirstOrDefaultAsync(m => m.Id == divorce.MarriageId);

            if (marriage == null)
                throw new KeyNotFoundException("Marriage not found.");

            if (await _context.Divorces.AnyAsync(d => d.MarriageId == divorce.MarriageId))
                throw new InvalidOperationException("Marriage is already divorced.");

            divorce.CreatedAt = DateTime.UtcNow;

            _context.Divorces.Add(divorce);
            await _context.SaveChangesAsync();

            return divorce;
        }
    }

}
