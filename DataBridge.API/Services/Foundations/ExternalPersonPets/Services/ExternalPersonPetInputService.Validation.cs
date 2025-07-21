using DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;

namespace DataBridge.API.Services.Foundations.ExternalPersonPets;

public partial class ExternalPersonPetInputService
{
    private static void ValidateFile(IFormFile file)
    {
        if (file == null)
        {
            throw new NullExternalPersonPetInputFileException();
        }

        if (file.Length == 0)
        {
            throw new EmptyExternalPersonPetInputFileException();
        }
        if (!file.FileName.EndsWith(".xlsx") && !file.FileName.EndsWith(".xls"))
        {
            throw new InvalidExternalPersonPetInputFileTypeException(file.FileName);
        }
    }
}