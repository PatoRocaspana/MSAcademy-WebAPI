using Moq;
using RentACarWebAPI.Interfaces.Repositories;
using RentACarWebAPI.Models;
using RentACarWebAPI.Services;
using System.Threading.Tasks;
using Xunit;

namespace RentACarTests.ServicesTests
{
    public class ClientServiceTests
    {
        #region CreateAsync
        [Fact]
        public async Task CreateAsync_ReturnsClientCreated()
        {
            //arrange
            var client = new Client() { Id = 1, Name = "Rupert", Dni = "13131311" };

            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.CreateAsync(client)).ReturnsAsync(client);
            mockClientRepository.Setup(repo => repo.DniExistsAsync(client)).ReturnsAsync(false);

            var clientService = new ClientService(mockClientRepository.Object);

            //act
            var result = await clientService.CreateAsync(client);

            //assert
            Assert.Equal(client.Dni, result.Dni);
            mockClientRepository.Verify(repo => repo.CreateAsync(client), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WhenDniAlreadyExists_ReturnsNull()
        {
            //arrange
            var client = new Client() { Id = 1, Name = "Rupert", Dni = "13131311" };

            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.DniExistsAsync(client)).ReturnsAsync(true);

            var clientService = new ClientService(mockClientRepository.Object);

            //act
            var result = await clientService.CreateAsync(client);

            //assert
            Assert.Null(result);
            mockClientRepository.Verify(repo => repo.CreateAsync(client), Times.Never);
        }
        #endregion

        #region UpdateAsync
        [Fact]
        public async Task UpdateAsync_ReturnsClientUpdated()
        {
            //arrange
            var client = new Client() { Id = 1, Name = "Rupert", Dni = "13131311" };

            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.GetAsync(client.Id)).ReturnsAsync(client);
            mockClientRepository.Setup(repo => repo.UpdateAsync(client, client.Id)).ReturnsAsync(client);
            mockClientRepository.Setup(repo => repo.DniExistsAsync(client)).ReturnsAsync(false);

            var clientService = new ClientService(mockClientRepository.Object);

            //act
            var result = await clientService.UpdateAsync(client, client.Id);

            //assert
            Assert.Equal(client.Dni, result.Dni);
            mockClientRepository.Verify(repo => repo.UpdateAsync(client, client.Id), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenClientNoExists_ReturnsNull()
        {
            //arrange
            var client = new Client() { Id = 1, Name = "Rupert", Dni = "13131311" };

            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.GetAsync(client.Id)).ReturnsAsync((Client)null);

            var clientService = new ClientService(mockClientRepository.Object);

            //act
            var result = await clientService.UpdateAsync(client, client.Id);

            //assert
            Assert.Null(result);
            mockClientRepository.Verify(repo => repo.UpdateAsync(client, client.Id), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenDniUpdatedAlreadyExists_ReturnsNull()
        {
            //arrange
            var newClient = new Client() { Id = 1, Name = "Rupert", Dni = "13131311" };
            var oldClient = new Client() { Id = 1, Name = "Rupert", Dni = "98798798" };

            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.GetAsync(newClient.Id)).ReturnsAsync(oldClient);
            mockClientRepository.Setup(repo => repo.DniExistsAsync(newClient)).ReturnsAsync(true);

            var clientService = new ClientService(mockClientRepository.Object);

            //act
            var result = await clientService.UpdateAsync(newClient, newClient.Id);

            //assert
            Assert.Null(result);
            mockClientRepository.Verify(repo => repo.UpdateAsync(newClient, newClient.Id), Times.Never);
        }
        #endregion
    }
}
