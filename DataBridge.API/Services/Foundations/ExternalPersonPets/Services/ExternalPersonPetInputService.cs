using DataBridge.API.Brokers.Loggings;
using DataBridge.API.Brokers.Sheets;
using DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;

namespace DataBridge.API.Services.Foundations.ExternalPersonPets;

public partial class ExternalPersonPetInputService : IExternalPersonPetInputService
{
    private readonly ISheetBroker sheetBroker;
    private readonly ILoggingBroker loggingBroker;

    public ExternalPersonPetInputService(
        ISheetBroker sheetBroker,
        ILoggingBroker loggingBroker)
    {
        this.sheetBroker = sheetBroker;
        this.loggingBroker = loggingBroker;
    }

    public async ValueTask UploadExternalPersonPetsFileAsync(IFormFile file) =>
        await TryCatch(async () =>
        {
            ValidateFile(file);

            await this.sheetBroker.UploadExternalPersonPetsFileAsync(file);
        });
}
