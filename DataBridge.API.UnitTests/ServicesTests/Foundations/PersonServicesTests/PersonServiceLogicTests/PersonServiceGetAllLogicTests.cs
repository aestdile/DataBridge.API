using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Persons;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PersonServicesTests;

public partial class PersonServiceTests
{
    [Fact]
    public void ShouldRetrieveAllPeople()
    {
        // given
        IQueryable<Person> randomPerson = CreateRandomPeople();
        IQueryable<Person> storagePerson = randomPerson;
        IQueryable<Person> expectedPerson = storagePerson.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.SelectAllPeople())
                .Returns(storagePerson);

        // when
        IQueryable<Person> actualPerson =
            this.personService.GetAllPeople();

        // then
        actualPerson.Should().BeEquivalentTo(expectedPerson);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectAllPeople(),
                Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
