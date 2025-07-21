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
    public async Task ShouldRetrievePetByIdAsync()
    {
        // given
        Guid randomPetId = Guid.NewGuid();
        Guid inputPetId = randomPetId;
        Pet randomPet = CreateRandomPet();
        Pet persistedPet = randomPet;
        Pet expectedPet = persistedPet.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPetByIdAsync(inputPetId))
                .ReturnsAsync(persistedPet);

        // when
        Pet actualPet = await this
            .petService.GetPetByIdAsync(inputPetId);

        // then
        actualPet.Should().BeEquivalentTo(expectedPet);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(inputPetId), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
