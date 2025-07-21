using DataBridge.API.Services.Orchestrations.People.Interfaces;
using DataBridge.API.Services.Processings.People.Interfaces;
using Xeptions;

namespace DataBridge.API.Services.Orchestrations.People.Services;

public class PersonOrchestrationService : IPersonOrchestrationService
{
    private readonly IPersonProcessingService personProcessingService;
    private readonly IPersonXMLProcessingService personXMLProcessingService;

    private const string XmlFilePath = @"C:\Users\aestd\OneDrive\Desktop\DataBridge.API\DataBridge.API\UploadFiles\Template.xml";

    public PersonOrchestrationService(
        IPersonProcessingService personProcessingService,
        IPersonXMLProcessingService personXMLProcessingService)
    {
        this.personProcessingService = personProcessingService;
        this.personXMLProcessingService = personXMLProcessingService;
    }

    public async ValueTask ExportAllPeopleWithPetsToXmlAsync()
    {
        try
        {
            var peopleWithPets =
                this.personProcessingService.GetAllPeopleWithPets().ToList();

            await this.personXMLProcessingService.ExportPersonPetsToXml(
                peopleWithPets, XmlFilePath);
        }
        catch (Exception exception)
        {
            throw new Xeption(
                message: "Orchestration service error occurred, contact support",
                innerException: exception);
        }
    }

    public async ValueTask<Stream> GetPeopleWithPetsXmlFileAsync()
    {
        try
        {
            return await this.personXMLProcessingService
                .GetPersonPetsXml(XmlFilePath);
        }
        catch (Exception exception)
        {
            throw new Xeption(
                message: "Error occurred while retrieving the exported XML file.",
                innerException: exception);
        }
    }
}
