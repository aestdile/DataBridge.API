using DataBridge.API.Models.Foundations.ExternalPersons;

namespace DataBridge.API.Brokers.Sheets;

public partial interface ISheetBroker
{
    ValueTask<List<ExternalPerson>> ReadAllExternalPersonPetsAsync();
}