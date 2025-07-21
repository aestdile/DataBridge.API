﻿using DataBridge.API.Models.Foundations.Pets.Exceptions;
using DataBridge.API.Models.Foundations.Pets;

namespace DataBridge.API.Services.Foundations.Pets.Services;

public partial class PetService
{
    private void ValidatePetOnAdd(Pet pet)
    {
        ValidatePetNotNull(pet);

        Validate(
            (Rule: IsInvalid(pet.Id), Parameter: nameof(Pet.Id)),
            (Rule: IsInvalid(pet.Name), Parameter: nameof(Pet.Name)),
            (Rule: IsInvalid(pet.Type), Parameter: nameof(Pet.Type)),
            (Rule: IsInvalid(pet.PersonId), Parameter: nameof(Pet.PersonId)));
    }

    private static void ValidatePetNotNull(Pet pet)
    {
        if (pet is null)
        {
            throw new NullPetException();
        }
    }

    private static dynamic IsInvalid(Guid id) => new
    {
        Condition = id == Guid.Empty,
        Message = "Id is required"
    };

    private static dynamic IsInvalid(string text) => new
    {
        Condition = string.IsNullOrWhiteSpace(text),
        Message = "Text is required"
    };

    private static dynamic IsInvalid(PetType type) => new
    {
        Condition = Enum.IsDefined(type) is false,
        Message = "Value is invalid"
    };

    private static void ValidatePetId(Guid petId) =>
        Validate((Rule: IsInvalid(petId), Parameter: nameof(Pet.Id)));

    private static void ValidateStoragePet(Pet maybePet, Guid petId)
    {
        if (maybePet is null)
        {
            throw new NotFoundPetException(petId);
        }
    }

    private static void ValidatePetOnModify(Pet pet)
    {
        ValidatePetNotNull(pet);

        Validate(
            (Rule: IsInvalid(pet.Id), Parameter: nameof(Pet.Id)),
            (Rule: IsInvalid(pet.Name), Parameter: nameof(Pet.Name)),
            (Rule: IsInvalid(pet.Type), Parameter: nameof(Pet.Type)),
            (Rule: IsInvalid(pet.PersonId), Parameter: nameof(Pet.PersonId)));
    }

    private static void ValidateAgainstStoragePetOnModify(Pet pet, Pet storagePet)
    {
        ValidateStoragePet(storagePet, pet.Id);

        Validate(
            (Rule: IsInvalid(pet.Id), Parameter: nameof(Pet.Id)),
            (Rule: IsInvalid(pet.Name), Parameter: nameof(Pet.Name)),
            (Rule: IsInvalid(pet.Type), Parameter: nameof(Pet.Type)),
            (Rule: IsInvalid(pet.PersonId), Parameter: nameof(Pet.PersonId)));
    }

    private static void Validate(params (dynamic Rule, string Parameter)[] validations)
    {
        var invalidPetException = new InvalidPetException();

        foreach ((dynamic rule, string parameter) in validations)
        {
            if (rule.Condition)
            {
                invalidPetException.UpsertDataList(
                    key: parameter,
                    value: rule.Message);
            }
        }

        invalidPetException.ThrowIfContainsErrors();
    }
}
