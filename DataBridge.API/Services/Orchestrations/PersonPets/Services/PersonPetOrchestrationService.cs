using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Models.Foundations.Pets;
using DataBridge.API.Models.Orchestrations.PersonPets;
using DataBridge.API.Services.Orchestrations.PersonPets.Interfaces;
using DataBridge.API.Services.Processings.People.Interfaces;
using DataBridge.API.Services.Processings.Pets.Interfaces;

namespace DataBridge.API.Services.Orchestrations.PersonPets.Services;

public class PersonPetOrchestrationService : IPersonPetOrchestrationService
{
    private readonly IPersonProcessingService personProcessingService;
    private readonly IPetProcessingService petProcessingService;

    public PersonPetOrchestrationService(
        IPersonProcessingService personProcessingService,
        IPetProcessingService petProcessingService)
    {
        this.personProcessingService = personProcessingService;
        this.petProcessingService = petProcessingService;
    }

    public async ValueTask<PersonPet> ProcessPersonWithPetsAsync(PersonPet personPet)
    {
        Person processedPerson =
            await this.personProcessingService.UpsertPersonAsync(personPet.Person);

        PersonPet processedPersonPet = MapToPersonPet(processedPerson);

        foreach (Pet pet in personPet.Pets)
        {
            Pet processedPet = await this.petProcessingService.UpsertPetAsync(pet);
            processedPersonPet.Pets.Add(processedPet);
        }

        return processedPersonPet;
    }

    private static PersonPet MapToPersonPet(Person processedPerson)
    {
        return new PersonPet
        {
            Person = processedPerson,
            Pets = new List<Pet>()
        };
    }
}
