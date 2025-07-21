using DataBridge.API.Brokers.Queues;
using DataBridge.API.Models.Foundations.ExternalPersons;
using DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;

namespace DataBridge.API.Services.Foundations.ExternalPersonPets.Services;

public class ExternalPersonPetEventService : IExternalPersonPetEventService
{
    private readonly IQueueBroker queueBroker;

    public ExternalPersonPetEventService(IQueueBroker queueBroker) =>
        this.queueBroker = queueBroker;

    public ValueTask AddExternalPersonPets(List<ExternalPerson> externalPersonPets) =>
        this.queueBroker.AddExternalPersonPets(externalPersonPets);

    public ValueTask<List<ExternalPerson>> GetExternalPersonPets() =>
        this.queueBroker.ReadExternalPersonPets();
}