namespace DataBridge.API.Brokers.Sheets;

public partial class SheetBroker
{
    public async ValueTask UploadExternalPersonPetsFileAsync(IFormFile file)
    {
        string filePath = GetSheetLocationWithName();

        string? directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
    }

}