using DataBridge.API.Models.Foundations.Pets;

namespace DataBridge.API.Brokers.Storages;

public partial interface IStorageBroker
{
    ValueTask<Pet> InsertPetAsync(Pet pet);
    IQueryable<Pet> SelectAllPets();
    ValueTask<Pet> SelectPetByIdAsync(Guid petId);
    ValueTask<Pet> UpdatePetAsync(Pet pet);
}