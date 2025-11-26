using Marry_Me.EF.Models;

namespace Marry_Me.Services.Abstraction.BusinessLogic
{
    public interface IMarriageService
    {
        Task<Marriage> GetMarriageByIdAsync(int id);
        Task<IEnumerable<Marriage>> GetAllMarriagesAsync();
        Task<Marriage> CreateMarriageAsync(Marriage marriage);
        Task UpdateMarriageAsync(Marriage marriage);
        Task DeleteMarriageAsync(int id);
        Task<bool> IsPersonMarriedAsync(int personId);
    }
}
