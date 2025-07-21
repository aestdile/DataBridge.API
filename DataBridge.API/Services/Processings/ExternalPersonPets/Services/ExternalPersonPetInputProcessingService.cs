using DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;
using DataBridge.API.Services.Processings.ExternalPersonPets.Interfaces;

namespace DataBridge.API.Services.Processings.ExternalPersonPets.Services;

public class ExternalPersonPetInputProcessingService : IExternalPersonPetInputProcessingService
{
    private readonly IExternalPersonPetInputService externalPersonPetInputService;

    public ExternalPersonPetInputProcessingService(
        IExternalPersonPetInputService externalPersonPetInputService) =>
            this.externalPersonPetInputService = externalPersonPetInputService;

    public ValueTask UploadExternalPersonPetsFileAsync(IFormFile file) =>
        this.externalPersonPetInputService.UploadExternalPersonPetsFileAsync(file);
}