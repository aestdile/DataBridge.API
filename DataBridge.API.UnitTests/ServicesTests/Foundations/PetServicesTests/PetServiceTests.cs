using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Brokers.Loggings;
using DataBridge.API.Brokers.Storages;
using DataBridge.API.Models.Foundations.Pets;
using DataBridge.API.Services.Foundations.Pets.Interfaces;
using DataBridge.API.Services.Foundations.Pets.Services;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PetServicesTests;

public partial class PetServiceTests
{
    private readonly Mock<IStorageBroker> storageBrokerMock;
    private readonly Mock<ILoggingBroker> loggingBrokerMock;
    private readonly IPetService petService;

    public PetServiceTests()
    {
        this.storageBrokerMock = new Mock<IStorageBroker>();
        this.loggingBrokerMock = new Mock<ILoggingBroker>();

        this.petService = new PetService(
            storageBroker: this.storageBrokerMock.Object,
            loggingBroker: this.loggingBrokerMock.Object);
    }

    private static Pet CreateRandomPet() =>
        CreatePetFiller().Create();

    private IQueryable<Pet> CreateRandomPets()
    {
        return CreatePetFiller()
            .Create(count: GetRandomNumber()).AsQueryable();
    }

    private static int GetRandomNumber() =>
        new IntRange(min: 2, max: 9).GetValue();

    private static string GetRandomString() =>
        new MnemonicString().GetValue();

    private static SqlException GetSqlError() =>
        (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

    private static T GetInvalidEnum<T>()
    {
        int randomNumber = GetRandomNumber();

        while (Enum.IsDefined(typeof(T), randomNumber) is true)
        {
            randomNumber = GetRandomNumber();
        }

        return (T)(object)randomNumber;
    }

    private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
        actualException => actualException.SameExceptionAs(expectedException);

    private static Filler<Pet> CreatePetFiller()
    {
        var filler = new Filler<Pet>();

        return filler;
    }
}
