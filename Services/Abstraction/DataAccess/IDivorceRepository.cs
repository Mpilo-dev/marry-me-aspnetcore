using Marry_Me.EF.Models;

namespace Marry_Me.Services.Abstraction.DataAccess
{
    public interface IDivorceRepository
    {
        Task<Divorce> GetByMarriageIdAsync(int marriageId);
        Task<Divorce> CreateDivorceAsync(Divorce divorce);
    }


}
