using DataBridge.API.Models.Foundations.Pets;

namespace DataBridge.API.Services.Foundations.Pets.Interfaces;

public interface IPetService
{
    ValueTask<Pet> AddPetAsync(Pet pet);
    IQueryable<Pet> GetAllPets();
    ValueTask<Pet> GetPetByIdAsync(Guid petId);
    ValueTask<Pet> ModifyPetAsync(Pet pet);
}
