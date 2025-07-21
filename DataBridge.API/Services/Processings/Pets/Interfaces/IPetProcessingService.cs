using DataBridge.API.Models.Foundations.Pets;

namespace DataBridge.API.Services.Processings.Pets.Interfaces;

public interface IPetProcessingService
{
    ValueTask<Pet> UpsertPetAsync(Pet pet);
    IQueryable<Pet> GetAllPets();
}