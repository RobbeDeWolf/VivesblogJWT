using Microsoft.EntityFrameworkCore;
using VivesBlog.Data;
using VivesBlog.Model;
using VivesBlog.Services.Abstractions;

namespace VivesBlog.Services;

public class PersonService : IPersonService
{
    private readonly VivesBlogDbContext _dbContext;

    public PersonService(VivesBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Person>> FindAsync()
    {
        return await _dbContext.People
            .OrderBy(p => p.FirstName)
            .ThenBy(p => p.LastName)
            .ToListAsync();
    }

    public async Task<Person?> GetAsync(int id)
    {
        return await _dbContext.People.SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Person?> CreateAsync(Person person)
    {
        _dbContext.People.Add(person);

        await _dbContext.SaveChangesAsync();

        return person;
    }

    public async Task<Person?> UpdateAsync(int id, Person person)
    {
        var dbPerson = await GetAsync(id);

        if (dbPerson is null)
        {
            return null;
        }

        dbPerson.FirstName = person.FirstName;
        dbPerson.LastName = person.LastName;

        await _dbContext.SaveChangesAsync();

        return dbPerson;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var person = new Person { Id = id };
        _dbContext.People.Attach(person);
        _dbContext.People.Remove(person);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}