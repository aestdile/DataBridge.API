﻿using System;
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
    public async Task ShouldThrowValidationExceptionOnModifyIfPersonIsNullAndLogItAsync()
    {
        // given
        Person nullPerson = null;
        var nullPersonException = new NullPersonException();

        var expectedPersonValidationException =
            new PersonValidationException(nullPersonException);

        // when
        ValueTask<Person> modifyPersonTask =
            this.personService.ModifyPersonAsync(nullPerson);

        PersonValidationException actualPersonValidationException =
            await Assert.ThrowsAsync<PersonValidationException>(
                modifyPersonTask.AsTask);

        // then
        actualPersonValidationException.Should()
            .BeEquivalentTo(expectedPersonValidationException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonValidationException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePersonAsync(It.IsAny<Person>()), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task ShouldThrowValidationExceptionOnModifyIfPersonIsInvalidAndLogItAsync(string invalidString)
    {
        // given
        Person invalidPerson = new Person
        {
            Name = invalidString
        };

        var invalidPersonException =
            new InvalidPersonException();

        invalidPersonException.AddData(
            key: nameof(Person.Id),
            values: "Id is required");

        invalidPersonException.AddData(
            key: nameof(Person.Name),
            values: "Text is required");

        invalidPersonException.AddData(
            key: nameof(Person.Age),
            values: "Value must be greater than 0");

        var expectedPersonValidationException =
            new PersonValidationException(invalidPersonException);

        // when
        ValueTask<Person> modifyPersonTask =
            this.personService.ModifyPersonAsync(invalidPerson);

        PersonValidationException actualPersonValidationException =
            await Assert.ThrowsAsync<PersonValidationException>(
                modifyPersonTask.AsTask);

        // then
        actualPersonValidationException.Should()
            .BeEquivalentTo(expectedPersonValidationException);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonValidationException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePersonAsync(It.IsAny<Person>()), Times.Never);

        this.loggingBrokerMock.VerifyNoOtherCalls();
        this.storageBrokerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task ShouldThrowValidationExceptionOnModifyIfPersonDoesNotExistAndLogItAsync()
    {
        // given
        Person randomPerson = CreateRandomPerson();
        Person nonExistPerson = randomPerson;
        Person nullPerson = null;

        var notFoundPersonException =
            new NotFoundPersonException(nonExistPerson.Id);

        var expectedPersonValidationException =
            new PersonValidationException(notFoundPersonException);

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPersonByIdAsync(nonExistPerson.Id))
                .ReturnsAsync(nullPerson);

        // when
        ValueTask<Person> modifyPersonTask =
            this.personService.ModifyPersonAsync(nonExistPerson);

        PersonValidationException actualPersonValidationException =
            await Assert.ThrowsAsync<PersonValidationException>(
                modifyPersonTask.AsTask);

        // then
        actualPersonValidationException.Should()
            .BeEquivalentTo(expectedPersonValidationException);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPersonByIdAsync(nonExistPerson.Id), Times.Once);

        this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonValidationException))), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePersonAsync(nonExistPerson), Times.Never);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
