﻿using DataBridge.API.Models.Foundations.Pets;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PetServicesTests;

public partial class PetServiceTests
{
    [Fact]
    public void ShouldRetrieveAllPets()
    {
        // given
        IQueryable<Pet> randomPet = CreateRandomPets();
        IQueryable<Pet> storagePet = randomPet;
        IQueryable<Pet> expectedPet = storagePet.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.SelectAllPets())
                .Returns(storagePet);

        // when
        IQueryable<Pet> actualPet =
            this.petService.GetAllPets();

        // then
        actualPet.Should().BeEquivalentTo(expectedPet);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectAllPets(),
                Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}