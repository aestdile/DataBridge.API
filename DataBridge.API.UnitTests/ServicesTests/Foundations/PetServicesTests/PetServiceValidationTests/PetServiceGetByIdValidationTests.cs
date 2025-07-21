using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Pets.Exceptions;
using DataBridge.API.Models.Foundations.Pets;
using Moq;
using FluentAssertions;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PetServicesTests;

public partial class PetServiceTests
{
    [Fact]
    public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
    {
        // given
        Guid invalidPetId = Guid.Empty;
        var invalidPetException = new InvalidPetException();

        invalidPetException.AddData(
            key: nameof(Pet.Id),
            values: "Id is required");

        var expectedPetValidationException =
            new PetValidationException(invalidPetException);

        // when
        ValueTask<Pet> retrievePetById =
            this.petService.GetPetByIdAsync(invalidPetId);

        PetValidationException actualPetValidationException =
            await Assert.ThrowsAsync<PetValidationException>(retrievePetById.AsTask);

        // then
        actualPetValidationException.Should()
            .BeEquivalentTo(expectedPetValidationException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetValidationException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(It.IsAny<Guid>()), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfPetNotFoundAndLogItAsync()
    {
        // given
        Guid somePetId = Guid.NewGuid();
        Pet noPet = null;

        var notFoundPetException =
            new NotFoundPetException(somePetId);

        var expectedPetValidationException =
            new PetValidationException(notFoundPetException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPetByIdAsync(
                It.IsAny<Guid>())).ReturnsAsync(noPet);

        // when
        ValueTask<Pet> retriveByIdPetTask =
            this.petService.GetPetByIdAsync(somePetId);

        var actualPetValidationException =
            await Assert.ThrowsAsync<PetValidationException>(
                retriveByIdPetTask.AsTask);

        // then
        actualPetValidationException.Should().BeEquivalentTo(expectedPetValidationException);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(somePetId), Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetValidationException))), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
