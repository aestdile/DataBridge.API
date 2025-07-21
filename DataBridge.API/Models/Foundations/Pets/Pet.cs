namespace DataBridge.API.Models.Foundations.Pets;

public class Pet
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public PetType Type { get; set; }
    public Guid PersonId { get; set; }
}
