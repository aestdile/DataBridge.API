using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Pets.Exceptions;
using DataBridge.API.Models.Foundations.Pets;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PetServicesTests;

public partial class PetServiceTests
{
    [Fact]
    public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
    {
        // given
        Pet randomPet = CreateRandomPet();
        Pet somePet = randomPet;
        Guid petId = somePet.Id;
        SqlException sqlException = GetSqlError();

        var failedPetStorageException =
            new FailedPetStorageException(sqlException);

        var expectedPetDependencyException =
            new PetDependencyException(failedPetStorageException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPetByIdAsync(petId)).Throws(sqlException);

        // when
        ValueTask<Pet> modifyPetTask =
            this.petService.ModifyPetAsync(somePet);

        PetDependencyException actualPetDependencyException =
            await Assert.ThrowsAsync<PetDependencyException>(
                modifyPetTask.AsTask);

        // then
        actualPetDependencyException.Should()
            .BeEquivalentTo(expectedPetDependencyException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedPetDependencyException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(petId), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePetAsync(somePet), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowDependencyExceptionOnModifyIfDatabaseUpdateExceptionOccursAndLogItAsync()
    {
        // given
        Pet randomPet = CreateRandomPet();
        Pet somePet = randomPet;
        Guid petId = somePet.Id;
        var databaseUpdateException = new DbUpdateException();

        var failedPetStorageException =
            new FailedPetStorageException(databaseUpdateException);

        var expectedPetDependencyException =
            new PetDependencyException(failedPetStorageException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPetByIdAsync(petId)).Throws(databaseUpdateException);

        // when
        ValueTask<Pet> modifyPetTask =
            this.petService.ModifyPetAsync(somePet);

        PetDependencyException actualPetDependencyException =
            await Assert.ThrowsAsync<PetDependencyException>(
                modifyPetTask.AsTask);

        // then
        actualPetDependencyException.Should()
            .BeEquivalentTo(expectedPetDependencyException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetDependencyException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(petId), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePetAsync(somePet), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowDependencyValidationExceptionOnModifyIfDatabaseUpdateConcurrencyErrorOccursAndLogItAsync()
    {
        // given
        Pet randomPet = CreateRandomPet();
        Pet somePet = randomPet;
        Guid petId = somePet.Id;
        var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

        var lockedPetException =
            new LockedPetException(dbUpdateConcurrencyException);

        var expectedPetDependencyValidationException =
            new PetDependencyValidationException(lockedPetException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPetByIdAsync(petId))
                .Throws(dbUpdateConcurrencyException);

        // when
        ValueTask<Pet> modifyPetTask =
            this.petService.ModifyPetAsync(somePet);

        PetDependencyValidationException actualPetDependencyValidationException =
            await Assert.ThrowsAsync<PetDependencyValidationException>(
                modifyPetTask.AsTask);

        // then
        actualPetDependencyValidationException.Should()
            .BeEquivalentTo(expectedPetDependencyValidationException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetDependencyValidationException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(petId), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePetAsync(somePet), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowServiceExceptionOnModifyIfDatabaseUpdateErrorOccursAndLogItAsync()
    {
        // given
        Pet randomPet = CreateRandomPet();
        Pet somePet = randomPet;
        Guid petId = somePet.Id;
        Exception serviceException = new Exception();

        var failedPetServiceException =
            new FailedPetServiceException(serviceException);

        var expectedPetServiceException =
            new PetServiceException(failedPetServiceException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPetByIdAsync(petId))
                .Throws(serviceException);

        // when
        ValueTask<Pet> modifyPetTask =
            this.petService.ModifyPetAsync(somePet);

        PetServiceException actualPetServiceException =
            await Assert.ThrowsAsync<PetServiceException>(
                modifyPetTask.AsTask);

        // then
        actualPetServiceException.Should()
            .BeEquivalentTo(expectedPetServiceException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetServiceException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(petId), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePetAsync(somePet), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }
}
