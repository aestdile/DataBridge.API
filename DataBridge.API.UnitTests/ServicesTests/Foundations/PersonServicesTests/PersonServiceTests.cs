using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Brokers.Loggings;
using DataBridge.API.Brokers.Storages;
using DataBridge.API.Models.Foundations.Persons;
using DataBridge.API.Services.Foundations.Persons.Interfaces;
using DataBridge.API.Services.Foundations.Persons.Services;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PersonServicesTests;

public partial class PersonServiceTests
{
    private readonly Mock<IStorageBroker> storageBrokerMock;
    private readonly Mock<ILoggingBroker> loggingBrokerMock;
    private readonly IPersonService personService;

    public PersonServiceTests()
    {
        this.storageBrokerMock = new Mock<IStorageBroker>();
        this.loggingBrokerMock = new Mock<ILoggingBroker>();

        this.personService = new PersonService(
            storageBroker: this.storageBrokerMock.Object,
            loggingBroker: this.loggingBrokerMock.Object);
    }

    private static Person CreateRandomPerson() =>
        CreatePersonFiller().Create();

    private IQueryable<Person> CreateRandomPeople()
    {
        return CreatePersonFiller()
            .Create(count: GetRandomNumber()).AsQueryable();
    }

    private static int GetRandomNumber() =>
        new IntRange(min: 2, max: 9).GetValue();

    private static string GetRandomString() =>
        new MnemonicString().GetValue();

    private static SqlException GetSqlError() =>
        (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

    private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
        actualException => actualException.SameExceptionAs(expectedException);

    private static Filler<Person> CreatePersonFiller()
    {
        var filler = new Filler<Person>();
        int positiveNumber = GetRandomNumber();

        filler.Setup()
            .OnType<int>().Use(positiveNumber);

        return filler;
    }
}
