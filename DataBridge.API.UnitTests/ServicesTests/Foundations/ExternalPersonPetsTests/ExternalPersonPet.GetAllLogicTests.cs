using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBridge.API.Models.Foundations.ExternalPersons;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace DataBridge.API.UnitTests.ServicesTests.Foundations.ExternalPersonPetsTests;

public partial class ExternalPersonPetServiceTests
{
    [Fact]
    public async Task ShouldRetrieveAllExternalPersonPets()
    {
        // given
        List<ExternalPerson> randomExternalPersonPets = CreateRandomExternalPersonPets();
        List<ExternalPerson> storageExternalPersonPets = randomExternalPersonPets;
        List<ExternalPerson> expectedExternalPersonPets = storageExternalPersonPets.DeepClone();

        this.sheetBrokerMock.Setup(broker =>
            broker.ReadAllExternalPersonPetsAsync())
                .ReturnsAsync(storageExternalPersonPets);

        // when
        List<ExternalPerson> actualExternalPersonPets =
            await this.externalPersonPetService.GetAllExternalPersonPetsAsync();

        // then
        actualExternalPersonPets.Should().BeEquivalentTo(expectedExternalPersonPets);

        this.sheetBrokerMock.Verify(broker =>
            broker.ReadAllExternalPersonPetsAsync(),
                Times.Once);

        this.sheetBrokerMock.VerifyNoOtherCalls();
        this.loggingBrokerMock.VerifyNoOtherCalls();
    }
}