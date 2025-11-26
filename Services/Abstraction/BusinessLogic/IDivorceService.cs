using Marry_Me.DTOs;
using Marry_Me.EF.Models;

namespace Marry_Me.Services.Abstraction.BusinessLogic
{
    public interface IDivorceService
    {
        Task<DivorceResultDTO> CreateDivorceAsync(DivorceDTO dto);
    }

}
