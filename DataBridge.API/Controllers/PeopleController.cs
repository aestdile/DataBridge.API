using DataBridge.API.Brokers.Loggings;
using DataBridge.API.Models.Foundations.ExternalPersons.Exceptions;
using DataBridge.API.Models.Foundations.Persons.Exceptions;
using DataBridge.API.Models.Orchestrations.PersonPets;
using DataBridge.API.Services.Coordinators.Interfaces;
using DataBridge.API.Services.Orchestrations.People.Interfaces;
using DataBridge.API.Services.Processings.ExternalPersonPets.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataBridge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : RESTFulController
{
    private readonly IExternalPersonPetInputProcessingService externalPersonPetInputProcessingService;
    private readonly IPersonPetEventCoordinationService personPetEventCoordinationService;
    private readonly IPersonOrchestrationService personOrchestrationService;
    private readonly ILoggingBroker loggingBroker;

    public PeopleController(
    IExternalPersonPetInputProcessingService externalPersonPetInputProcessingService,
    IPersonPetEventCoordinationService personPetEventCoordinationService,
    IPersonOrchestrationService personOrchestrationService,
    ILoggingBroker loggingBroker)
    {
        this.externalPersonPetInputProcessingService = externalPersonPetInputProcessingService;
        this.personPetEventCoordinationService = personPetEventCoordinationService;
        this.personOrchestrationService = personOrchestrationService;
        this.loggingBroker = loggingBroker;
    }


    [HttpPost("upload-data")]
    public async ValueTask<ActionResult<List<PersonPet>>> UploadAndStorePeople(IFormFile file)
    {
        this.loggingBroker.LogInformation("UploadAndStorePeople called.");

        try
        {
            this.loggingBroker.LogInformation("Uploading external person pet file...");
            await this.externalPersonPetInputProcessingService.UploadExternalPersonPetsFileAsync(file);

            this.loggingBroker.LogInformation("Coordinating external person pets...");
            List<PersonPet> storedPeople =
                await this.personPetEventCoordinationService.CoordinateExternalPersonPetsAsync();

            this.loggingBroker.LogInformation($"Successfully uploaded and stored {storedPeople.Count} people with pets.");

            return Ok(storedPeople);
        }
        catch (NullExternalPersonPetInputFileException ex)
        {
            this.loggingBroker.LogWarning(ex);
            return BadRequest(new { Error = ex.Message });
        }
        catch (EmptyExternalPersonPetInputFileException ex)
        {
            this.loggingBroker.LogWarning(ex);
            return BadRequest(new { Error = ex.Message });
        }
        catch (InvalidExternalPersonPetInputFileTypeException ex)
        {
            this.loggingBroker.LogWarning(ex);
            return BadRequest(new { Error = ex.Message });
        }
        catch (ExternalPersonPetDependencyException ex)
        {
            this.loggingBroker.LogError(ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Error = ex.Message });
        }
        catch (ExternalPersonPetServiceException ex)
        {
            this.loggingBroker.LogError(ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Error = ex.Message });
        }
    }


    [HttpGet("download-data")]
    public async ValueTask<ActionResult> DownloadPeopleWithPetsXml()
    {
        this.loggingBroker.LogInformation("DownloadPeopleWithPetsXml called.");

        try
        {
            this.loggingBroker.LogInformation("Exporting people with pets to XML...");
            await this.personOrchestrationService.ExportAllPeopleWithPetsToXmlAsync();

            this.loggingBroker.LogInformation("Retrieving XML file stream...");
            Stream xmlFileStream =
                await this.personOrchestrationService.GetPeopleWithPetsXmlFileAsync();

            this.loggingBroker.LogInformation("XML file stream successfully retrieved.");
            return File(xmlFileStream, "application/xml", "PeopleWithPets.xml");
        }
        catch (PersonDependencyException ex)
        {
            this.loggingBroker.LogError(ex);
            return InternalServerError(ex.InnerException);
        }
        catch (PersonServiceException ex)
        {
            this.loggingBroker.LogError(ex);
            return InternalServerError(ex.InnerException);
        }
    }

}
