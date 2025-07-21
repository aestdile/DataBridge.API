using DataBridge.API.Models.Foundations.Persons;

namespace DataBridge.API.Services.Processings.People.Interfaces;

public interface IPersonXMLProcessingService
{
    ValueTask ExportPersonPetsToXml(IEnumerable<Person> persons, string filePath);
    ValueTask<Stream> GetPersonPetsXml(string filePath);
}
