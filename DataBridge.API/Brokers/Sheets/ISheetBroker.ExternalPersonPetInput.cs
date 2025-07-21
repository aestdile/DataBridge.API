namespace DataBridge.API.Brokers.Sheets;

public partial interface ISheetBroker
{
    ValueTask UploadExternalPersonPetsFileAsync(IFormFile file);
}