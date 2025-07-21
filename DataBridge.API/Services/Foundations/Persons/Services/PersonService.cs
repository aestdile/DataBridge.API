using DataBridge.API.Brokers.Loggings;
using DataBridge.API.Brokers.Storages;
using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Services.Foundations.Persons.Interfaces;

namespace DataBridge.API.Services.Foundations.Persons.Services;

public partial class PersonService : IPersonService
{
    private readonly IStorageBroker storageBroker;
    private readonly ILoggingBroker loggingBroker;

    public PersonService(
        IStorageBroker storageBroker,
        ILoggingBroker loggingBroker)
    {
        this.storageBroker = storageBroker;
        this.loggingBroker = loggingBroker;
    }

    public ValueTask<Person> AddPersonAsync(Person person) =>
    TryCatch(async () =>
    {
        ValidatePersonOnAdd(person);

        return await storageBroker.InsertPersonAsync(person);
    });

    public ValueTask<Person> GetPersonByIdAsync(Guid personId) =>
    TryCatch(async () =>
    {
        ValidatePersonId(personId);

        Person maybePerson = await this.storageBroker.SelectPersonByIdAsync(personId);

        ValidateStoragePerson(maybePerson, personId);

        return maybePerson;
    });

    public IQueryable<Person> GetAllPeople() =>
        TryCatch(() => this.storageBroker.SelectAllPeople());

    public IQueryable<Person> GetAllPeopleWithPets() =>
        TryCatch(() => this.storageBroker.SelectAllPeopleWithPets());

    public ValueTask<Person> ModifyPersonAsync(Person person) =>
    TryCatch(async () =>
    {
        ValidatePersonOnModify(person);

        Person maybePerson =
            await this.storageBroker.SelectPersonByIdAsync(person.Id);

        ValidateAgainstStoragePersonOnModify(person, maybePerson);

        return await storageBroker.UpdatePersonAsync(person);
    });
}