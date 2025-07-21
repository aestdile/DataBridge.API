using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Persons;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PersonServicesTests;

public partial class PersonServiceTests
{
    [Fact]
    public async Task ShouldRetrievePersonByIdAsync()
    {
        // given
        Guid randomPersonId = Guid.NewGuid();
        Guid inputPersonId = randomPersonId;
        Person randomPerson = CreateRandomPerson();
        Person persistedPerson = randomPerson;
        Person expectedPerson = persistedPerson.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPersonByIdAsync(inputPersonId))
                .ReturnsAsync(persistedPerson);

        // when
        Person actualPerson = await this
            .personService.GetPersonByIdAsync(inputPersonId);

        // then
        actualPerson.Should().BeEquivalentTo(expectedPerson);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPersonByIdAsync(inputPersonId), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
