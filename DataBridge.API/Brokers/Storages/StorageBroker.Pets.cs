using DataBridge.API.Models.Foundations.Pets;
using Microsoft.EntityFrameworkCore;

namespace DataBridge.API.Brokers.Storages;

public partial class StorageBroker
{
    public DbSet<Pet> Pets { get; set; }

    public async ValueTask<Pet> InsertPetAsync(Pet pet) =>
        await InsertAsync(pet);

    public IQueryable<Pet> SelectAllPets() => SelectAll<Pet>();

    public async ValueTask<Pet> SelectPetByIdAsync(Guid petId) =>
        await SelectAsync<Pet>(petId);

    public async ValueTask<Pet> UpdatePetAsync(Pet pet) =>
        await UpdateAsync(pet);
}