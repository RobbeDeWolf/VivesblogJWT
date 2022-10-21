using VivesBlog.Model;

namespace VivesBlog.Services.Abstractions;

public interface IPersonService
{
    Task<IList<Person>> FindAsync();
    Task<Person?> GetAsync(int id);
    Task<Person?> CreateAsync(Person person);
    Task<Person?> UpdateAsync(int id, Person person);
    Task<bool> DeleteAsync(int id);
}