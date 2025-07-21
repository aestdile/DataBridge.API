namespace DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;

public interface IExternalPersonPetInputService
{
    ValueTask UploadExternalPersonPetsFileAsync(IFormFile file);
}