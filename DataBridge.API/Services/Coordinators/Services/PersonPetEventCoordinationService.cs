using DataBridge.API.Models.Foundations.ExternalPersons;
using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Models.Foundations.Pets;
using DataBridge.API.Models.Orchestrations.PersonPets;
using DataBridge.API.Services.Coordinators.Interfaces;
using DataBridge.API.Services.Orchestrations.ExternalPersonPets.Interfaces;
using DataBridge.API.Services.Orchestrations.PersonPets.Interfaces;

namespace DataBridge.API.Services.Coordinators.Services;

public class PersonPetEventCoordinationService : IPersonPetEventCoordinationService
{
    private readonly IExternalPersonPetOrchestrationService externalPersonPetOrchestrationService;
    private readonly IExternalPersonPetEventOrchestrationService externalPersonPetEventOrchestrationService;
    private readonly IPersonPetOrchestrationService personPetOrchestrationService;

    public PersonPetEventCoordinationService(
        IExternalPersonPetOrchestrationService externalPersonPetOrchestrationService,
        IExternalPersonPetEventOrchestrationService externalPersonPetEventOrchestrationService,
        IPersonPetOrchestrationService personPetOrchestrationService)
    {
        this.externalPersonPetOrchestrationService = externalPersonPetOrchestrationService;
        this.externalPersonPetEventOrchestrationService = externalPersonPetEventOrchestrationService;
        this.personPetOrchestrationService = personPetOrchestrationService;
    }

    public async ValueTask<List<PersonPet>> CoordinateExternalPersonPetsAsync()
    {
        await this.externalPersonPetOrchestrationService.GetAndAddFormattedExternalPersonPetsAsync();

        List<ExternalPerson> externalPersons =
            await this.externalPersonPetEventOrchestrationService.GetExternalPersonPets();

        var personsWithPets = new List<PersonPet>();

        foreach (var externalPerson in externalPersons)
        {
            var personId = Guid.NewGuid();

            var person = new Person
            {
                Id = personId,
                Name = externalPerson.PersonName,
                Age = externalPerson.Age
            };

            var pets = MapPets(externalPerson, personId);

            var personPet = new PersonPet
            {
                Person = person,
                Pets = pets
            };

            PersonPet processedPersonPet =
                await this.personPetOrchestrationService.ProcessPersonWithPetsAsync(personPet);

            personsWithPets.Add(processedPersonPet);
        }

        return personsWithPets;
    }

    private List<Pet> MapPets(ExternalPerson externalPerson, Guid personId)
    {
        var pets = new List<Pet>();

        if (!string.IsNullOrWhiteSpace(externalPerson.PetOne))
        {
            pets.Add(new Pet
            {
                Id = Guid.NewGuid(),
                Name = externalPerson.PetOne,
                Type = MapToPetType(externalPerson.PetOneType),
                PersonId = personId
            });
        }

        if (!string.IsNullOrWhiteSpace(externalPerson.PetTwo))
        {
            pets.Add(new Pet
            {
                Id = Guid.NewGuid(),
                Name = externalPerson.PetTwo,
                Type = MapToPetType(externalPerson.PetTwoType),
                PersonId = personId
            });
        }

        if (!string.IsNullOrWhiteSpace(externalPerson.PetThree))
        {
            pets.Add(new Pet
            {
                Id = Guid.NewGuid(),
                Name = externalPerson.PetThree,
                Type = MapToPetType(externalPerson.PetThreeType),
                PersonId = personId
            });
        }

        return pets;
    }

    private PetType MapToPetType(string petType)
    {
        return Enum.TryParse(petType, ignoreCase: true, out PetType mappedPetType)
            ? mappedPetType
            : PetType.Other;
    }
}