using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Services.Foundations.Persons.Interfaces;
using DataBridge.API.Services.Processings.People.Interfaces;

namespace DataBridge.API.Services.Processings.People.Services;

public class PersonProcessingService : IPersonProcessingService
{
    private readonly IPersonService personService;

    public PersonProcessingService(IPersonService personService) =>
        this.personService = personService;

    public async ValueTask<Person> UpsertPersonAsync(Person person)
    {
        Person maybePerson = RetrievePerson(person);

        return maybePerson switch
        {
            null => await this.personService.AddPersonAsync(person),
            _ => await this.personService.ModifyPersonAsync(person)
        };
    }

    private Person RetrievePerson(Person person)
    {
        IQueryable<Person> retrievedPersons =
            this.personService.GetAllPeople();

        return retrievedPersons.FirstOrDefault(storagePerson =>
            storagePerson.Id == person.Id);
    }

    public IQueryable<Person> GetAllPeople() =>
        personService.GetAllPeople();

    public IQueryable<Person> GetAllPeopleWithPets() =>
        personService.GetAllPeopleWithPets();
}