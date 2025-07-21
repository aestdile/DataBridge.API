using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.Pets;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.PetServicesTests;

public partial class PetServiceTests
{
    [Fact]
    public async Task ShouldAddPetAsync()
    {
        // given
        Pet randomPet = CreateRandomPet();
        Pet inputPet = randomPet;
        Pet storagePet = inputPet;
        Pet expectedPet = storagePet.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.InsertPetAsync(inputPet))
                .ReturnsAsync(storagePet);

        // when
        Pet actualPet =
            await this.petService.AddPetAsync(inputPet);

        // then
        actualPet.Should().BeEquivalentTo(expectedPet);

        this.storageBrokerMock.Verify(broker =>
            broker.InsertPetAsync(inputPet),
                Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
