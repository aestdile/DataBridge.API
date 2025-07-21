using DataBridge.API.Models.Foundations.Persons;

namespace DataBridge.API.Services.Processings.People.Interfaces;

public interface IPersonProcessingService
{
    ValueTask<Person> UpsertPersonAsync(Person person);
    IQueryable<Person> GetAllPeople();
    IQueryable<Person> GetAllPeopleWithPets();
}