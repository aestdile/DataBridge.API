using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Models.Foundations.Pets;

namespace DataBridge.API.Models.Orchestrations.PersonPets;

public class PersonPet
{
    public Person Person { get; set; }
    public List<Pet> Pets { get; set; }
}
