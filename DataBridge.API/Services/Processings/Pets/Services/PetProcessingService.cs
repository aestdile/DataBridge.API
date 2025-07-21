using DataBridge.API.Models.Foundations.Pets;
using DataBridge.API.Services.Foundations.Pets.Interfaces;
using DataBridge.API.Services.Processings.Pets.Interfaces;

namespace DataBridge.API.Services.Processings.Pets.Services;

public class PetProcessingService : IPetProcessingService
{
    private readonly IPetService petService;

    public PetProcessingService(IPetService petService) =>
        this.petService = petService;

    public async ValueTask<Pet> UpsertPetAsync(Pet pet)
    {
        Pet maybePet = RetrievePet(pet);

        return maybePet switch
        {
            null => await this.petService.AddPetAsync(pet),
            _ => await this.petService.ModifyPetAsync(pet)
        };
    }

    private Pet RetrievePet(Pet pet)
    {
        IQueryable<Pet> retrievedPets =
            this.petService.GetAllPets();

        return retrievedPets.FirstOrDefault(storagePet =>
            storagePet.Id == pet.Id);
    }

    public IQueryable<Pet> GetAllPets() =>
        this.petService.GetAllPets();
}