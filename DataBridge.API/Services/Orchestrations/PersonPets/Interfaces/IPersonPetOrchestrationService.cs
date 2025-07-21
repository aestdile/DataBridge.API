using DataBridge.API.Models.Orchestrations.PersonPets;

namespace DataBridge.API.Services.Orchestrations.PersonPets.Interfaces;

public interface IPersonPetOrchestrationService
{
    ValueTask<PersonPet> ProcessPersonWithPetsAsync(PersonPet personPet);
}
