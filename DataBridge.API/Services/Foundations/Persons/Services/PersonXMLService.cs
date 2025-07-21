using DataBridge.API.Brokers.Sheets;
using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Services.Foundations.Persons.Interfaces;

namespace DataBridge.API.Services.Foundations.Persons.Services;

public class PersonXMLService : IPersonXMLService
{
    private readonly ISheetBroker sheetBroker;

    public PersonXMLService(ISheetBroker sheetBroker) =>
        this.sheetBroker = sheetBroker;

    public async ValueTask ExportPersonPetsToXml(
        IEnumerable<Person> persons, string filePath) =>
            await this.sheetBroker.SavePeopleWithPetsToXmlFile(persons, filePath);

    public async ValueTask<Stream> GetPersonPetsXml(string filePath) =>
        await this.sheetBroker.GetPeopleWithPetsXmlFile(filePath);
}