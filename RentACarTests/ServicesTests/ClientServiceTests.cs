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
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly ClientService _clientService;

        public ClientServiceTests()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _clientService = new ClientService(_mockClientRepository.Object);
        }

        #region CreateAsync
        [Fact]
        public async Task CreateAsync_ReturnsClientCreated()
        {
            //arrange
            var client = new Client() { Id = 1, Name = "Rupert", Dni = "13131311" };

            _mockClientRepository.Setup(repo => repo.CreateAsync(client)).ReturnsAsync(client);
            _mockClientRepository.Setup(repo => repo.DniExistsAsync(client)).ReturnsAsync(false);

            //act
            var result = await _clientService.CreateAsync(client);

            //assert
            Assert.NotNull(result);
            Assert.Equal(client.Dni, result.Dni);
            _mockClientRepository.Verify(repo => repo.CreateAsync(client), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WhenDniAlreadyExists_ReturnsNull()
        {
            //arrange
            var client = new Client() { Id = 1, Name = "Rupert", Dni = "13131311" };

            _mockClientRepository.Setup(repo => repo.DniExistsAsync(client)).ReturnsAsync(true);

            //act
            var result = await _clientService.CreateAsync(client);

            //assert
            Assert.Null(result);
            _mockClientRepository.Verify(repo => repo.CreateAsync(client), Times.Never);
        }
        #endregion

        #region UpdateAsync
        [Fact]
        public async Task UpdateAsync_ReturnsClientUpdated()
        {
            //arrange
            var client = new Client() { Id = 1, Name = "Rupert", Dni = "13131311" };

            _mockClientRepository.Setup(repo => repo.GetAsync(client.Id)).ReturnsAsync(client);
            _mockClientRepository.Setup(repo => repo.UpdateAsync(client, client.Id)).ReturnsAsync(client);
            _mockClientRepository.Setup(repo => repo.DniExistsAsync(client)).ReturnsAsync(false);

            //act
            var result = await _clientService.UpdateAsync(client, client.Id);

            //assert
            Assert.NotNull(result);
            Assert.Equal(client.Dni, result.Dni);
            _mockClientRepository.Verify(repo => repo.UpdateAsync(client, client.Id), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenClientNoExists_ReturnsNull()
        {
            //arrange
            var client = new Client() { Id = 1, Name = "Rupert", Dni = "13131311" };

            _mockClientRepository.Setup(repo => repo.GetAsync(client.Id)).ReturnsAsync((Client)null);

            //act
            var result = await _clientService.UpdateAsync(client, client.Id);

            //assert
            Assert.Null(result);
            _mockClientRepository.Verify(repo => repo.UpdateAsync(client, client.Id), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_WhenDniUpdatedAlreadyExists_ReturnsNull()
        {
            //arrange
            var newClient = new Client() { Id = 1, Name = "Rupert", Dni = "13131311" };
            var oldClient = new Client() { Id = 1, Name = "Rupert", Dni = "98798798" };

            _mockClientRepository.Setup(repo => repo.GetAsync(newClient.Id)).ReturnsAsync(oldClient);
            _mockClientRepository.Setup(repo => repo.DniExistsAsync(newClient)).ReturnsAsync(true);

            //act
            var result = await _clientService.UpdateAsync(newClient, newClient.Id);

            //assert
            Assert.Null(result);
            _mockClientRepository.Verify(repo => repo.UpdateAsync(newClient, newClient.Id), Times.Never);
        }
        #endregion
    }
}
