using Marry_Me.EF.Models;
using Marry_Me.Services.Abstraction.BusinessLogic;
using Marry_Me.Services.Abstraction.DataAccess;

namespace Marry_Me.Services.Concrete.BusinessLogic
{
    public class MarriageService : IMarriageService
    {
        private readonly IMarriageRepository _marriageRepository;
        private readonly IPersonRepository _personRepository;

        public MarriageService(IMarriageRepository marriageRepository, IPersonRepository personRepository)
        {
            _marriageRepository = marriageRepository;
            _personRepository = personRepository;
        }

        public async Task<Marriage> GetMarriageByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid marriage ID");

            var marriage = await _marriageRepository.GetByIdAsync(id);
            if (marriage == null)
                throw new KeyNotFoundException($"Marriage with ID {id} not found");

            return marriage;
        }

        public async Task<IEnumerable<Marriage>> GetAllMarriagesAsync()
        {
            return await _marriageRepository.GetAllAsync();
        }

        public async Task<Marriage> CreateMarriageAsync(Marriage marriage)
        {
            if (marriage == null)
                throw new ArgumentNullException(nameof(marriage));

            var female = await _personRepository.GetByIdAsync(marriage.FemaleId);
            if (female == null)
                throw new KeyNotFoundException($"Female person with ID {marriage.FemaleId} not found");

            var male = await _personRepository.GetByIdAsync(marriage.MaleId);
            if (male == null)
                throw new KeyNotFoundException($"Male person with ID {marriage.MaleId} not found");

            if (female.Gender != Gender.Female)
                throw new ArgumentException($"Person with ID {marriage.FemaleId} is not female");

            if (male.Gender != Gender.Male)
                throw new ArgumentException($"Person with ID {marriage.MaleId} is not male");

            if (await _marriageRepository.IsPersonMarriedAsync(marriage.FemaleId))
                throw new InvalidOperationException($"Female person with ID {marriage.FemaleId} is already married");

            if (await _marriageRepository.IsPersonMarriedAsync(marriage.MaleId))
                throw new InvalidOperationException($"Male person with ID {marriage.MaleId} is already married");

            if (marriage.FemaleId == marriage.MaleId)
                throw new ArgumentException("Cannot marry the same person");

            await _marriageRepository.AddAsync(marriage);

            female.IsMarried = true;
            male.IsMarried = true;
            await _personRepository.UpdateAsync(female);
            await _personRepository.UpdateAsync(male);
            return marriage;
        }

        public async Task UpdateMarriageAsync(Marriage marriage)
        {
            if (marriage == null)
                throw new ArgumentNullException(nameof(marriage));

            if (!await _marriageRepository.ExistsAsync(marriage.Id))
                throw new KeyNotFoundException($"Marriage with ID {marriage.Id} not found");

            await _marriageRepository.UpdateAsync(marriage);
        }

        public async Task DeleteMarriageAsync(int id)
        {
            if (!await _marriageRepository.ExistsAsync(id))
                throw new KeyNotFoundException($"Marriage with ID {id} not found");

            await _marriageRepository.DeleteAsync(id);
        }

        public async Task<bool> IsPersonMarriedAsync(int personId)
        {
            return await _marriageRepository.IsPersonMarriedAsync(personId);
        }
    }

}
