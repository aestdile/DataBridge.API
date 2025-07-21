using DataBridge.API.Models.Foundations.Persons;

namespace DataBridge.API.Brokers.Storages;

public partial interface IStorageBroker
{
    ValueTask<Person> InsertPersonAsync(Person person);
    IQueryable<Person> SelectAllPeople();
    IQueryable<Person> SelectAllPeopleWithPets();
    ValueTask<Person> SelectPersonByIdAsync(Guid personId);
    ValueTask<Person> UpdatePersonAsync(Person person);
}