using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Pets.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PetServicesTests;


public partial class PetServiceTests
{
    [Fact]
    public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
    {
        // given
        SqlException sqlException = GetSqlError();

        var failedPetStorageException =
            new FailedPetStorageException(sqlException);

        var expectedPetDependencyException =
            new PetDependencyException(failedPetStorageException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectAllPets()).Throws(sqlException);

        // when
        Action retrieveAllPetsAction = () =>
            this.petService.GetAllPets();

        PetDependencyException actualPetDependencyException =
            Assert.Throws<PetDependencyException>(retrieveAllPetsAction);

        // then
        actualPetDependencyException.Should()
            .BeEquivalentTo(expectedPetDependencyException);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectAllPets(), Times.Once());

        this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedPetDependencyException))), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
    {
        // given
        string exceptionMessage = GetRandomString();
        var serverException = new Exception(exceptionMessage);

        var failedPetServiceException =
            new FailedPetServiceException(serverException);

        var expectedPetServiceException =
            new PetServiceException(failedPetServiceException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectAllPets()).Throws(serverException);

        // when
        Action retrieveAllPetActions = () =>
            this.petService.GetAllPets();

        PetServiceException actualPetServiceException =
            Assert.Throws<PetServiceException>(retrieveAllPetActions);

        //then
        actualPetServiceException.Should()
            .BeEquivalentTo(expectedPetServiceException);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectAllPets(), Times.Once());

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPetServiceException))), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}

