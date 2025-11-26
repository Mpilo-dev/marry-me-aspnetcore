using Marry_Me.EF.Models;

namespace Marry_Me.Services.Abstraction.DataAccess
{
    public interface IMarriageRepository
    {
        Task<Marriage> GetByIdAsync(int id);
        Task<IEnumerable<Marriage>> GetAllAsync();
        Task AddAsync(Marriage marriage);
        Task UpdateAsync(Marriage marriage);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> IsPersonMarriedAsync(int personId);
    }
}
