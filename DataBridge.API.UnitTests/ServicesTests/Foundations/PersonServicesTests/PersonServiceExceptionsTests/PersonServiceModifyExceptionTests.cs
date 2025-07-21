﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Models.Foundations.Persons.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PersonServicesTests;

public partial class PersonServiceTests
{
    [Fact]
    public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
    {
        // given
        Person randomPerson = CreateRandomPerson();
        Person somePerson = randomPerson;
        Guid personId = somePerson.Id;
        SqlException sqlException = GetSqlError();

        var failedPersonStorageException =
            new FailedPersonStorageException(sqlException);

        var expectedPersonDependencyException =
            new PersonDependencyException(failedPersonStorageException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPersonByIdAsync(personId)).Throws(sqlException);

        // when
        ValueTask<Person> modifyPersonTask =
            this.personService.ModifyPersonAsync(somePerson);

        PersonDependencyException actualPersonDependencyException =
            await Assert.ThrowsAsync<PersonDependencyException>(
                modifyPersonTask.AsTask);

        // then
        actualPersonDependencyException.Should()
            .BeEquivalentTo(expectedPersonDependencyException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogCritical(It.Is(SameExceptionAs(
                expectedPersonDependencyException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPersonByIdAsync(personId), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePersonAsync(somePerson), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowDependencyExceptionOnModifyIfDatabaseUpdateExceptionOccursAndLogItAsync()
    {
        // given
        Person randomPerson = CreateRandomPerson();
        Person somePerson = randomPerson;
        Guid personId = somePerson.Id;
        var databaseUpdateException = new DbUpdateException();

        var failedPersonStorageException =
            new FailedPersonStorageException(databaseUpdateException);

        var expectedPersonDependencyException =
            new PersonDependencyException(failedPersonStorageException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPersonByIdAsync(personId)).Throws(databaseUpdateException);

        // when
        ValueTask<Person> modifyPersonTask =
            this.personService.ModifyPersonAsync(somePerson);

        PersonDependencyException actualPersonDependencyException =
            await Assert.ThrowsAsync<PersonDependencyException>(
                modifyPersonTask.AsTask);

        // then
        actualPersonDependencyException.Should()
            .BeEquivalentTo(expectedPersonDependencyException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonDependencyException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPersonByIdAsync(personId), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePersonAsync(somePerson), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowDependencyValidationExceptionOnModifyIfDatabaseUpdateConcurrencyErrorOccursAndLogItAsync()
    {
        // given
        Person randomPerson = CreateRandomPerson();
        Person somePerson = randomPerson;
        Guid personId = somePerson.Id;
        var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

        var lockedPersonException =
            new LockedPersonException(dbUpdateConcurrencyException);

        var expectedPersonDependencyValidationException =
            new PersonDependencyValidationException(lockedPersonException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPersonByIdAsync(personId))
                .Throws(dbUpdateConcurrencyException);

        // when
        ValueTask<Person> modifyPersonTask =
            this.personService.ModifyPersonAsync(somePerson);

        PersonDependencyValidationException actualPersonDependencyValidationException =
            await Assert.ThrowsAsync<PersonDependencyValidationException>(
                modifyPersonTask.AsTask);

        // then
        actualPersonDependencyValidationException.Should()
            .BeEquivalentTo(expectedPersonDependencyValidationException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonDependencyValidationException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPersonByIdAsync(personId), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePersonAsync(somePerson), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowServiceExceptionOnModifyIfDatabaseUpdateErrorOccursAndLogItAsync()
    {
        // given
        Person randomPerson = CreateRandomPerson();
        Person somePerson = randomPerson;
        Guid personId = somePerson.Id;
        Exception serviceException = new Exception();

        var failedPersonServiceException =
            new FailedPersonServiceException(serviceException);

        var expectedPersonServiceException =
            new PersonServiceException(failedPersonServiceException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPersonByIdAsync(personId))
                .Throws(serviceException);

        // when
        ValueTask<Person> modifyPersonTask =
            this.personService.ModifyPersonAsync(somePerson);

        PersonServiceException actualPersonServiceException =
            await Assert.ThrowsAsync<PersonServiceException>(
                modifyPersonTask.AsTask);

        // then
        actualPersonServiceException.Should()
            .BeEquivalentTo(expectedPersonServiceException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonServiceException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPersonByIdAsync(personId), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePersonAsync(somePerson), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }
}
