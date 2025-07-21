namespace DataBridge.API.Services.Processings.ExternalPersonPets.Interfaces;

public interface IExternalPersonPetInputProcessingService
{
    ValueTask UploadExternalPersonPetsFileAsync(IFormFile file);
}