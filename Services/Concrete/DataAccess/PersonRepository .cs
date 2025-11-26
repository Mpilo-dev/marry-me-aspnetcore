using Marry_Me.EF.Context;
using Marry_Me.EF.Models;
using Marry_Me.Services.Abstraction.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Marry_Me.Services.Concrete.DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MarriageSystemDbContext _context;

        public PersonRepository(MarriageSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _context.Persons
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons
                .ToListAsync();
        }


        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var person = await GetByIdAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Persons.AnyAsync(p => p.Id == id);
        }

        public async Task<Person?> GetByIdNumberAsync(string idNumber)
        {
            return await _context.Persons
                .FirstOrDefaultAsync(p => p.IdNumber == idNumber);
        }

    }

}
