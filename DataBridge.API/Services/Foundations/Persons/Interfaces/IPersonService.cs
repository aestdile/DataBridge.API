using DataBridge.API.Models.Foundations.Persons;

namespace DataBridge.API.Services.Foundations.Persons.Interfaces;

public interface IPersonService
{
    ValueTask<Person> AddPersonAsync(Person person);
    IQueryable<Person> GetAllPeople();
    IQueryable<Person> GetAllPeopleWithPets();
    ValueTask<Person> GetPersonByIdAsync(Guid personId);
    ValueTask<Person> ModifyPersonAsync(Person person);
}