namespace DataBridge.API.Services.Orchestrations.People.Interfaces;

public interface IPersonOrchestrationService
{
    ValueTask ExportAllPeopleWithPetsToXmlAsync();
    ValueTask<Stream> GetPeopleWithPetsXmlFileAsync();
}
