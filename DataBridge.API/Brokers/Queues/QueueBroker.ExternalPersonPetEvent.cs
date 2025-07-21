using DataBridge.API.Models.Foundations.ExternalPersons;

namespace DataBridge.API.Brokers.Queues;

public partial class QueueBroker
{
    private static readonly Queue<List<ExternalPerson>> externalPersonPetsQueue = new();

    public ValueTask AddExternalPersonPets(List<ExternalPerson> externalPersonPets)
    {
        externalPersonPetsQueue.Enqueue(externalPersonPets);
        return ValueTask.CompletedTask;
    }

    public ValueTask<List<ExternalPerson>> ReadExternalPersonPets()
    {
        if (externalPersonPetsQueue.Count == 0)
        {
            return new ValueTask<List<ExternalPerson>>(new List<ExternalPerson>());
        }

        List<ExternalPerson> externalPersonPets = externalPersonPetsQueue.Dequeue();
        return new ValueTask<List<ExternalPerson>>(externalPersonPets);
    }
}