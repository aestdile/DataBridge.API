using System.Text.Json.Serialization;

namespace DataBridge.API.Models.Foundations.Pets;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PetType
{ 
    Unknown = 0,
    Cat = 1,
    Dog = 2,
    Parrot = 3,
    Other = 4
}
