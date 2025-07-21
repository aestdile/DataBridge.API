using DataBridge.API.Models.Foundations.Persons;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PersonServicesTests;

public partial class PersonServiceTests
{
    [Fact]
    public async Task ShouldAddPersonAsync()
    {
        // given
        Person randomPerson = CreateRandomPerson();
        Person inputPerson = randomPerson;
        Person storagePerson = inputPerson;
        Person expectedPerson = storagePerson.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.InsertPersonAsync(inputPerson))
                .ReturnsAsync(storagePerson);

        // when
        Person actualPerson =
            await this.personService.AddPersonAsync(inputPerson);

        // then
        actualPerson.Should().BeEquivalentTo(expectedPerson);

        this.storageBrokerMock.Verify(broker =>
            broker.InsertPersonAsync(inputPerson),
                Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
