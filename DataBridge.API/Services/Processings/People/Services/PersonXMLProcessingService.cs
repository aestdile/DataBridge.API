using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Services.Foundations.Persons.Interfaces;
using DataBridge.API.Services.Processings.People.Interfaces;

namespace DataBridge.API.Services.Processings.People.Services;

public class PersonXMLProcessingService : IPersonXMLProcessingService
{
    private readonly IPersonXMLService personXMLService;

    public PersonXMLProcessingService(IPersonXMLService personXMLService) =>
        this.personXMLService = personXMLService;

    public ValueTask ExportPersonPetsToXml(IEnumerable<Person> persons, string filePath) =>
        this.personXMLService.ExportPersonPetsToXml(persons, filePath);

    public ValueTask<Stream> GetPersonPetsXml(string filePath) =>
        this.personXMLService.GetPersonPetsXml(filePath);
}
