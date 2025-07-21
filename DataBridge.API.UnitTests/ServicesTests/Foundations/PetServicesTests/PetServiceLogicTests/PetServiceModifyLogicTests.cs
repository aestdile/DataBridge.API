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
    public async Task ShouldModifyPetAsync()
    {
        // given
        Pet randomPet = CreateRandomPet();
        Pet inputPet = randomPet;
        Pet persistedPet = inputPet.DeepClone();
        Pet updatedPet = inputPet;
        Pet expectedPet = updatedPet.DeepClone();
        Guid InputPetId = inputPet.Id;

        this.storageBrokerMock.Setup(broker =>
            broker.SelectPetByIdAsync(InputPetId))
                .ReturnsAsync(persistedPet);

        this.storageBrokerMock.Setup(broker =>
            broker.UpdatePetAsync(inputPet))
                .ReturnsAsync(updatedPet);

        // when
        Pet actualPet =
            await this.petService
                .ModifyPetAsync(inputPet);

        // then
        actualPet.Should().BeEquivalentTo(expectedPet);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectPetByIdAsync(InputPetId), Times.Once);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdatePetAsync(inputPet), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}
