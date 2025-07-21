using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Models.Foundations.Persons.Exceptions;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PersonServicesTests;

public partial class PersonServiceTests
{
    [Fact]
    public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
    {
        // given
        Person somePerson = CreateRandomPerson();
        SqlException sqlException = GetSqlError();
        var failedPersonStorageException = new FailedPersonStorageException(sqlException);

        var expectedPersonDependencyException =
            new PersonDependencyException(failedPersonStorageException);

        this.storageBrokerMock.Setup(broker =>
            broker.InsertPersonAsync(somePerson))
                .ThrowsAsync(sqlException);

        // when
        ValueTask<Person> addPersonTask =
            this.personService.AddPersonAsync(somePerson);

        // then
        await Assert.ThrowsAsync<PersonDependencyException>(() =>
            addPersonTask.AsTask());

        this.storageBrokerMock.Verify(broker =>
            broker.InsertPersonAsync(somePerson),
                Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedPersonDependencyException))),
                    Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
    {
        // given
        Person somePerson = CreateRandomPerson();
        string someMessage = GetRandomString();
        var duplicateKeyException =
            new DuplicatePersonException(someMessage);

        var alreadyExistPersonException =
            new AlreadyExistsPersonException(duplicateKeyException);

        var expectedPersonDependencyValidationException =
            new PersonDependencyValidationException(alreadyExistPersonException);

        this.storageBrokerMock.Setup(broker =>
            broker.InsertPersonAsync(somePerson))
                .ThrowsAsync(duplicateKeyException);

        // when
        ValueTask<Person> addPersonTask =
            this.personService.AddPersonAsync(somePerson);

        // then
        await Assert.ThrowsAsync<PersonDependencyValidationException>(() =>
            addPersonTask.AsTask());

        this.storageBrokerMock.Verify(broker =>
            broker.InsertPersonAsync(somePerson),
                Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonDependencyValidationException))),
                    Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
    {
        // given
        Person somePerson = CreateRandomPerson();
        var serviceException = new Exception();

        var failedPersonServiceException =
            new FailedPersonServiceException(serviceException);

        var expectedPersonServiceException =
            new PersonServiceException(failedPersonServiceException);

        this.storageBrokerMock.Setup(broker =>
            broker.InsertPersonAsync(somePerson))
                .ThrowsAsync(serviceException);

        // when
        ValueTask<Person> addPersonTask =
            this.personService.AddPersonAsync(somePerson);

        // then
        await Assert.ThrowsAsync<PersonServiceException>(() =>
            addPersonTask.AsTask());

        this.storageBrokerMock.Verify(broker =>
            broker.InsertPersonAsync(somePerson),
                Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonServiceException))),
                    Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
