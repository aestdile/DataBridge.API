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
    public async Task ShouldThrowValidationExceptionOnModifyIfPetIsNullAndLogItAsync()
    {
        // given
        Pet nullPet = null;
        var nullPetException = new NullPetException();

        var expectedPetValidationException =
            new PetValidationException(nullPetException);

        // when
        ValueTask<Pet> modifyPetTask =
            this.petService.ModifyPetAsync(nullPet);

        PetValidationException actualPetValidationException =
            await Assert.ThrowsAsync<PetValidationException>(
                modifyPetTask.AsTask);

        // then
        actualPetValidationException.Should()
            .BeEquivalentTo(expectedPetValidationException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetValidationException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePetAsync(It.IsAny<Pet>()), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task ShouldThrowValidationExceptionOnModifyIfPetIsInvalidAndLogItAsync(string invalidString)
    {
        // given
        Pet invalidPet = new Pet
        {
            Name = invalidString
        };

        var invalidPetException =
            new InvalidPetException();

        invalidPetException.AddData(
            key: nameof(Pet.Id),
            values: "Id is required");

        invalidPetException.AddData(
            key: nameof(Pet.Name),
            values: "Text is required");

        invalidPetException.AddData(
            key: nameof(Pet.PersonId),
            values: "Id is required");

        var expectedPetValidationException =
            new PetValidationException(invalidPetException);

        // when
        ValueTask<Pet> modifyPetTask =
            this.petService.ModifyPetAsync(invalidPet);

        PetValidationException actualPetValidationException =
            await Assert.ThrowsAsync<PetValidationException>(
                modifyPetTask.AsTask);

        // then
        actualPetValidationException.Should()
            .BeEquivalentTo(expectedPetValidationException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetValidationException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePetAsync(It.IsAny<Pet>()), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowValidationExceptionOnModifyIfPetDoesNotExistAndLogItAsync()
    {
        // given
        Pet randomPet = CreateRandomPet();
        Pet nonExistPet = randomPet;
        Pet nullPet = null;

        var notFoundPetException =
            new NotFoundPetException(nonExistPet.Id);

        var expectedPetValidationException =
            new PetValidationException(notFoundPetException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPetByIdAsync(nonExistPet.Id))
                .ReturnsAsync(nullPet);

        // when
        ValueTask<Pet> modifyPetTask =
            this.petService.ModifyPetAsync(nonExistPet);

        PetValidationException actualPetValidationException =
            await Assert.ThrowsAsync<PetValidationException>(
                modifyPetTask.AsTask);

        // then
        actualPetValidationException.Should()
            .BeEquivalentTo(expectedPetValidationException);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(nonExistPet.Id), Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetValidationException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePetAsync(nonExistPet), Times.Never);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
