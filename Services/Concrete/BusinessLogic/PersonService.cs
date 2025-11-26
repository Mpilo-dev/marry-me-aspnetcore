using Marry_Me.EF.Models;
using Marry_Me.Services.Abstraction.BusinessLogic;
using Marry_Me.Services.Abstraction.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Marry_Me.Services.Concrete.BusinessLogic
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid person ID");

            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                throw new KeyNotFoundException($"Person with ID {id} not found");

            return person;
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            if (string.IsNullOrEmpty(person.FirstName) || string.IsNullOrEmpty(person.LastName))
                throw new ArgumentException("First name and last name are required");

            await _personRepository.AddAsync(person);
            return person;
        }

        public async Task UpdatePersonAsync(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));

            if (!await _personRepository.ExistsAsync(person.Id))
                throw new KeyNotFoundException($"Person with ID {person.Id} not found");

            await _personRepository.UpdateAsync(person);
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                throw new KeyNotFoundException($"Person with ID {id} not found");

            if (person.IsMarried)
                throw new InvalidOperationException("Cannot delete a person who is currently married. Divorce must occur first.");

            await _personRepository.DeleteAsync(id);
        }
    }

}



