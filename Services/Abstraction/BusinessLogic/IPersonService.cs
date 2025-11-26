using Marry_Me.EF.Models;

namespace Marry_Me.Services.Abstraction.BusinessLogic
{
    public interface IPersonService
    {
        Task<Person> GetPersonByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person> CreatePersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(int id);

    }
}
