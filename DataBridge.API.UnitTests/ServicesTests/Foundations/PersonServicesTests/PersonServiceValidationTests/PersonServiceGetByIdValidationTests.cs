using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Models.Foundations.Persons.Exceptions;
using FluentAssertions;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PersonServicesTests;

public partial class PersonServiceTests
{
    [Fact]
    public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
    {
        // given
        Guid invalidPersonId = Guid.Empty;
        var invalidPersonException = new InvalidPersonException();

        invalidPersonException.AddData(
            key: nameof(Person.Id),
            values: "Id is required");

        var expectedPersonValidationException =
            new PersonValidationException(invalidPersonException);

        // when
        ValueTask<Person> retrievePersonById =
            this.personService.GetPersonByIdAsync(invalidPersonId);

        PersonValidationException actualPersonValidationException =
            await Assert.ThrowsAsync<PersonValidationException>(retrievePersonById.AsTask);

        // then
        actualPersonValidationException.Should()
            .BeEquivalentTo(expectedPersonValidationException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonValidationException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPersonByIdAsync(It.IsAny<Guid>()), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfPersonNotFoundAndLogItAsync()
    {
        // given
        Guid somePersonId = Guid.NewGuid();
        Person noPerson = null;

        var notFoundPersonException =
            new NotFoundPersonException(somePersonId);

        var expectedPersonValidationException =
            new PersonValidationException(notFoundPersonException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPersonByIdAsync(
                It.IsAny<Guid>())).ReturnsAsync(noPerson);

        // when
        ValueTask<Person> retriveByIdPersonTask =
            this.personService.GetPersonByIdAsync(somePersonId);

        var actualPersonValidationException =
            await Assert.ThrowsAsync<PersonValidationException>(
                retriveByIdPersonTask.AsTask);

        // then
        actualPersonValidationException.Should().BeEquivalentTo(expectedPersonValidationException);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPersonByIdAsync(somePersonId), Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonValidationException))), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
