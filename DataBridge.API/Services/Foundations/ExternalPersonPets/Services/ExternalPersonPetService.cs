using DataBridge.API.Brokers.Loggings;
using DataBridge.API.Brokers.Sheets;
using DataBridge.API.Models.Foundations.ExternalPersons;
using DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;

namespace DataBridge.API.Services.Foundations.ExternalPersonPets.Services;

public partial class ExternalPersonPetService : IExternalPersonPetService
{
    private readonly ISheetBroker sheetBroker;
    private readonly ILoggingBroker loggingBroker;

    public ExternalPersonPetService(
        ISheetBroker sheetBroker,
        ILoggingBroker loggingBroker)
    {
        this.sheetBroker = sheetBroker;
        this.loggingBroker = loggingBroker;
    }

    public ValueTask<List<ExternalPerson>> GetAllExternalPersonPetsAsync() =>
    TryCatch(async () =>
        await this.sheetBroker.ReadAllExternalPersonPetsAsync());
}