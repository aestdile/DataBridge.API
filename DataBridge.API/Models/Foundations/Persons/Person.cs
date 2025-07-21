using System.Text.Json.Serialization;
using DataBridge.API.Models.Foundations.Pets;

namespace DataBridge.API.Models.Foundations.Persons;

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Age { get; set; }

    [JsonIgnore]
    public List<Pet> Pets { get; set; }
}
