using Marry_Me.DTOs;
using Marry_Me.EF.Models;
using Marry_Me.Services.Abstraction.BusinessLogic;
using Marry_Me.Services.Abstraction.DataAccess;

namespace Marry_Me.Services.Concrete.BusinessLogic
{

    public class DivorceService : IDivorceService
    {
        private readonly IDivorceRepository _divorceRepo;
        private readonly IMarriageRepository _marriageRepo;
        private readonly IPersonRepository _personRepo;

        public DivorceService(IDivorceRepository divorceRepo, IMarriageRepository marriageRepo, IPersonRepository personRepo)
        {
            _divorceRepo = divorceRepo;
            _marriageRepo = marriageRepo;
            _personRepo = personRepo;
        }

        public async Task<DivorceResultDTO> CreateDivorceAsync(DivorceDTO dto)
        {
            var marriage = await _marriageRepo.GetByIdAsync(dto.MarriageId);
            if (marriage == null)
                throw new KeyNotFoundException($"Marriage with ID {dto.MarriageId} not found.");

            if (marriage.Divorce != null)
                throw new InvalidOperationException("This marriage has already been divorced.");

            var divorce = new Divorce
            {
                MarriageId = dto.MarriageId,
                UserId = dto.UserId,
            };

            var createdDivorce = await _divorceRepo.CreateDivorceAsync(divorce);

            await _marriageRepo.DeleteAsync(marriage.Id);

            if (marriage.FemaleId > 0)
            {
                var female = await _personRepo.GetByIdAsync(marriage.FemaleId);
                if (female != null)
                {
                    female.IsMarried = false;
                    await _personRepo.UpdateAsync(female);
                }
            }

            if (marriage.MaleId > 0)
            {
                var male = await _personRepo.GetByIdAsync(marriage.MaleId);
                if (male != null)
                {
                    male.IsMarried = false;
                    await _personRepo.UpdateAsync(male);
                }
            }

            return new DivorceResultDTO
            {
                UserId = createdDivorce.UserId,
                MarriageId = createdDivorce.MarriageId,
                CreatedAt = createdDivorce.CreatedAt
            };
        }
    }

}
