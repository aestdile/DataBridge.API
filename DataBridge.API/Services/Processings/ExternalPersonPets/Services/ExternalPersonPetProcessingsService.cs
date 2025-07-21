using DataBridge.API.Models.Foundations.ExternalPersons;
using DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;
using DataBridge.API.Services.Processings.ExternalPersonPets.Interfaces;

namespace DataBridge.API.Services.Processings.ExternalPersonPets.Services;

public class ExternalPersonPetProcessingService : IExternalPersonPetProcessingService
{
    private readonly IExternalPersonPetService externalPersonPetService;

    public ExternalPersonPetProcessingService(
        IExternalPersonPetService externalPersonPetService) =>
            this.externalPersonPetService = externalPersonPetService;

    public async ValueTask<List<ExternalPerson>> GetFormattedExternalPersonPetsAsync()
    {
        var retrievedExternalPersonPets =
            await this.externalPersonPetService.GetAllExternalPersonPetsAsync();

        List<ExternalPerson> formattedExternalPersonPets =
            FormatProperties(retrievedExternalPersonPets);

        return formattedExternalPersonPets;
    }

    private List<ExternalPerson> FormatProperties(List<ExternalPerson> retrievedExternalPersonPets)
    {
        var formattedExternalPersonPets = retrievedExternalPersonPets.Select(retrievedPersonPet =>
            new ExternalPerson()
            {
                PersonName = retrievedPersonPet.PersonName,
                Age = retrievedPersonPet.Age,
                PetOne = retrievedPersonPet.PetOne.Trim().Replace("-", string.Empty),
                PetOneType = retrievedPersonPet.PetOneType.Trim().Replace("-", string.Empty),
                PetTwo = retrievedPersonPet.PetTwo.Trim().Replace("-", string.Empty),
                PetTwoType = retrievedPersonPet.PetTwoType.Trim().Replace("-", string.Empty),
                PetThree = retrievedPersonPet.PetThree.Trim().Replace("-", string.Empty),
                PetThreeType = retrievedPersonPet.PetThreeType.Trim().Replace("-", string.Empty),
            });

        return formattedExternalPersonPets.ToList();
    }
}