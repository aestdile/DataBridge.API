using DataBridge.API.Models.Foundations.ExternalPersons;

namespace DataBridge.API.Brokers.Queues;

public partial interface IQueueBroker
{
    ValueTask AddExternalPersonPets(List<ExternalPerson> externalPersonPets);
    ValueTask<List<ExternalPerson>> ReadExternalPersonPets();
}