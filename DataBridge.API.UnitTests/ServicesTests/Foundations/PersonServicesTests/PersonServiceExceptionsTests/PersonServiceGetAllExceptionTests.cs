using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Persons.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PersonServicesTests;

public partial class PersonServiceTests
{
    [Fact]
    public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
    {
        // given
        SqlException sqlException = GetSqlError();

        var failedPersonStorageException =
            new FailedPersonStorageException(sqlException);

        var expectedPersonDependencyException =
            new PersonDependencyException(failedPersonStorageException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectAllPeople()).Throws(sqlException);

        // when
        Action retrieveAllPeopleAction = () =>
            this.personService.GetAllPeople();

        PersonDependencyException actualPersonDependencyException =
            Assert.Throws<PersonDependencyException>(retrieveAllPeopleAction);

        // then
        actualPersonDependencyException.Should()
            .BeEquivalentTo(expectedPersonDependencyException);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectAllPeople(), Times.Once());

        this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedPersonDependencyException))), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
    {
        // given
        string exceptionMessage = GetRandomString();
        var serverException = new Exception(exceptionMessage);

        var failedPersonServiceException =
            new FailedPersonServiceException(serverException);

        var expectedPersonServiceException =
            new PersonServiceException(failedPersonServiceException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectAllPeople()).Throws(serverException);

        // when
        Action retrieveAllPersonActions = () =>
            this.personService.GetAllPeople();

        PersonServiceException actualPersonServiceException =
            Assert.Throws<PersonServiceException>(retrieveAllPersonActions);

        //then
        actualPersonServiceException.Should()
            .BeEquivalentTo(expectedPersonServiceException);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectAllPeople(), Times.Once());

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonServiceException))), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
