using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Pets.Exceptions;
using DataBridge.API.Models.Foundations.Pets;
using Microsoft.Data.SqlClient;
using Moq;
using FluentAssertions;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PetServicesTests;

public partial class PetServiceTests
{
    [Fact]
    public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
    {
        // given
        Guid someId = Guid.NewGuid();
        SqlException sqlException = GetSqlError();

        var failedPetStorageException =
            new FailedPetStorageException(sqlException);

        PetDependencyException expectedPetDependencyException =
            new PetDependencyException(failedPetStorageException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPetByIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(sqlException);

        // when
        ValueTask<Pet> retrievePetById =
            this.petService.GetPetByIdAsync(someId);

        PetDependencyException actualPetDependencyException =
            await Assert.ThrowsAsync<PetDependencyException>(
                retrievePetById.AsTask);

        // then
        actualPetDependencyException.Should()
            .BeEquivalentTo(expectedPetDependencyException);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(someId), Times.Once());

        this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedPetDependencyException))), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowServiceExceptionOnRetrieveByIdAsyncIfServiceErrorOccursAndLogItAsync()
    {
        // given
        Guid someId = Guid.NewGuid();
        Exception serverException = new Exception();

        var failedPetServiceException =
            new FailedPetServiceException(serverException);

        PetServiceException expectedPetServiceException =
            new PetServiceException(failedPetServiceException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPetByIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(serverException);

        // when
        ValueTask<Pet> retrievePetById =
            this.petService.GetPetByIdAsync(someId);

        PetServiceException actualPetServiceException =
            await Assert.ThrowsAsync<PetServiceException>(
                retrievePetById.AsTask);

        // then
        actualPetServiceException.Should().BeEquivalentTo(expectedPetServiceException);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(someId), Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetServiceException))), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
