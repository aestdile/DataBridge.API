using DataBridge.API.Models.Foundations.Persons;

namespace DataBridge.API.Services.Foundations.Persons.Interfaces;

public interface IPersonXMLService
{
    ValueTask ExportPersonPetsToXml(IEnumerable<Person> persons, string filePath);
    ValueTask<Stream> GetPersonPetsXml(string filePath);
}