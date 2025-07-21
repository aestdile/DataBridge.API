using DataBridge.API.Brokers.Loggings;
using DataBridge.API.Brokers.Storages;
using DataBridge.API.Models.Foundations.Pets;
using DataBridge.API.Services.Foundations.Pets.Interfaces;

namespace DataBridge.API.Services.Foundations.Pets.Services;

public partial class PetService : IPetService
{
    private readonly IStorageBroker storageBroker;
    private readonly ILoggingBroker loggingBroker;

    public PetService(
        IStorageBroker storageBroker,
        ILoggingBroker loggingBroker)
    {
        this.storageBroker = storageBroker;
        this.loggingBroker = loggingBroker;
    }

    public ValueTask<Pet> AddPetAsync(Pet pet) =>
    TryCatch(async () =>
    {
        ValidatePetOnAdd(pet);

        return await storageBroker.InsertPetAsync(pet);
    });

    public ValueTask<Pet> GetPetByIdAsync(Guid petId) =>
    TryCatch(async () =>
    {
        ValidatePetId(petId);

        Pet maybePet = await this.storageBroker.SelectPetByIdAsync(petId);

        ValidateStoragePet(maybePet, petId);

        return maybePet;
    });

    public IQueryable<Pet> GetAllPets() =>
        TryCatch(() => this.storageBroker.SelectAllPets());

    public ValueTask<Pet> ModifyPetAsync(Pet pet) =>
    TryCatch(async () =>
    {
        ValidatePetOnModify(pet);

        Pet maybePet =
            await this.storageBroker.SelectPetByIdAsync(pet.Id);

        ValidateAgainstStoragePetOnModify(pet, maybePet);

        return await storageBroker.UpdatePetAsync(pet);
    });
}