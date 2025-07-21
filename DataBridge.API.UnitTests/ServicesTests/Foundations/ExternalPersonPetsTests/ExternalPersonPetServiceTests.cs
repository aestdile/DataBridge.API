using System.Linq.Expressions;
using DataBridge.API.Brokers.Loggings;
using DataBridge.API.Brokers.Sheets;
using DataBridge.API.Models.Foundations.ExternalPersons;
using DataBridge.API.Services.Foundations.ExternalPersonPets.Interfaces;
using DataBridge.API.Services.Foundations.ExternalPersonPets.Services;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.ExternalPersonPetsTests;

public partial class ExternalPersonPetServiceTests
{
    private readonly Mock<ISheetBroker> sheetBrokerMock;
    private readonly Mock<ILoggingBroker> loggingBrokerMock;
    private readonly IExternalPersonPetService externalPersonPetService;

    public ExternalPersonPetServiceTests()
    {
        this.sheetBrokerMock = new Mock<ISheetBroker>();
        this.loggingBrokerMock = new Mock<ILoggingBroker>();

        this.externalPersonPetService = new ExternalPersonPetService(
            sheetBroker: this.sheetBrokerMock.Object,
            loggingBroker: this.loggingBrokerMock.Object);
    }

    private static List<ExternalPerson> CreateRandomExternalPersonPets() =>
        CreateExternalPersonPetFiller().Create(count: GetRandomNumber()).ToList();

    private static int GetRandomNumber() =>
        new IntRange(min: 2, max: 9).GetValue();
    private static string GetRandomString() =>
    new MnemonicString().GetValue();
    private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
        actualException => actualException.SameExceptionAs(expectedException);

    private static Filler<ExternalPerson> CreateExternalPersonPetFiller()
    {
        var filler = new Filler<ExternalPerson>();
        return filler;
    }
}

