using System.Xml.Serialization;
using DataBridge.API.Models.Foundations.Persons;

namespace DataBridge.API.Brokers.Sheets;

public partial class SheetBroker
{
    public async ValueTask SavePeopleWithPetsToXmlFile(IEnumerable<Person> peopleWithPets, string filePath)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));
        await using FileStream fileStream = new FileStream(filePath, FileMode.Create);
        xmlSerializer.Serialize(fileStream, peopleWithPets);
    }

    public async ValueTask<MemoryStream> GetPeopleWithPetsXmlFile(string filePath)
    {
        byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
        return new MemoryStream(fileBytes);
    }
}