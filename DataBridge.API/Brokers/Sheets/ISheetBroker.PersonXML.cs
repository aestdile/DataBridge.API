using DataBridge.API.Models.Foundations.Persons;

namespace DataBridge.API.Brokers.Sheets;

public partial interface ISheetBroker
{
    ValueTask SavePeopleWithPetsToXmlFile(IEnumerable<Person> peopleWithPets, string filePath);

    ValueTask<MemoryStream> GetPeopleWithPetsXmlFile(string filePath);
}
