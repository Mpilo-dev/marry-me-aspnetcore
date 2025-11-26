using Marry_Me.EF.Models;

namespace Marry_Me.Services.Abstraction.DataAccess
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Person?> GetByIdNumberAsync(string idNumber);
    }
}
