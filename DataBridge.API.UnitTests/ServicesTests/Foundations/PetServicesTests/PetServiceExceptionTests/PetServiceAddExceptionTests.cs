using DataBridge.API.Models.Foundations.Pets;
using DataBridge.API.Models.Foundations.Pets.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PetServicesTests;

public partial class PetServiceTests
{
    [Fact]
    public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
    {
        // given
        Pet somePet = CreateRandomPet();
        SqlException sqlException = GetSqlError();
        var failedPetStorageException = new FailedPetStorageException(sqlException);

        var expectedPetDependencyException =
            new PetDependencyException(failedPetStorageException);

        this.storageBrokerMock.Setup(broker =>
            broker.InsertPetAsync(somePet))
                .ThrowsAsync(sqlException);

        // when
        ValueTask<Pet> addPetTask =
            this.petService.AddPetAsync(somePet);

        // then
        await Assert.ThrowsAsync<PetDependencyException>(() =>
            addPetTask.AsTask());

        this.storageBrokerMock.Verify(broker =>
            broker.InsertPetAsync(somePet),
                Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedPetDependencyException))),
                    Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
    {
        // given
        Pet somePet = CreateRandomPet();
        string someMessage = GetRandomString();

        var duplicateKeyException =
            new DuplicatePetException(someMessage);

        var alreadyExistPetException =
            new AlreadyExistsPetException(duplicateKeyException);

        var expectedPetDependencyValidationException =
            new PetDependencyValidationException(alreadyExistPetException);

        this.storageBrokerMock.Setup(broker =>
            broker.InsertPetAsync(somePet))
                .ThrowsAsync(duplicateKeyException);

        // when
        ValueTask<Pet> addPetTask =
            this.petService.AddPetAsync(somePet);

        // then
        await Assert.ThrowsAsync<PetDependencyValidationException>(() =>
            addPetTask.AsTask());

        this.storageBrokerMock.Verify(broker =>
            broker.InsertPetAsync(somePet),
                Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetDependencyValidationException))),
                    Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
    {
        // given
        Pet somePet = CreateRandomPet();
        var serviceException = new Exception();

        var failedPetServiceException =
            new FailedPetServiceException(serviceException);

        var expectedPetServiceException =
            new PetServiceException(failedPetServiceException);

        this.storageBrokerMock.Setup(broker =>
            broker.InsertPetAsync(somePet))
                .ThrowsAsync(serviceException);

        // when
        ValueTask<Pet> addPetTask =
            this.petService.AddPetAsync(somePet);

        // then
        await Assert.ThrowsAsync<PetServiceException>(() =>
            addPetTask.AsTask());

        this.storageBrokerMock.Verify(broker =>
            broker.InsertPetAsync(somePet),
                Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetServiceException))),
                    Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
