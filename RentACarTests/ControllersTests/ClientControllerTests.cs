using Microsoft.AspNetCore.Mvc;
using Moq;
using RentACarWebAPI.Controllers;
using RentACarWebAPI.DTOs;
using RentACarWebAPI.Interfaces.Services;
using RentACarWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RentACarTests.ControllersTests
{
    public class ClientControllerTests
    {
        private readonly Mock<IClientService> _mockClientService;
        private readonly ClientController _clientController;

        public ClientControllerTests()
        {
            _mockClientService = new Mock<IClientService>();
            _clientController = new ClientController(_mockClientService.Object);
        }

        #region GetAllAsync
        [Fact]
        public async Task GetAllAsync_ReturnsClientsDto()
        {
            //arrange
            var client = new Client() { Id = 1, Name = "Pedro", Dni = "13131311" };

            var clientList = new List<Client>
            {
                client,
                client,
                client
            };

            _mockClientService.Setup(serv => serv.GetAllAsync()).ReturnsAsync(clientList);

            //act
            var resultQuery = await _clientController.GetAsync();

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<OkObjectResult>(resultQuery);
            var listResult = Assert.IsType<List<ClientDto>>(objectResult.Value);
            Assert.Equal(clientList.Count, listResult.Count);
            _mockClientService.Verify(serv => serv.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoClients_ReturnsEmpty()
        {
            //arrange
            var clientList = new List<Client>();

            _mockClientService.Setup(serv => serv.GetAllAsync()).ReturnsAsync(clientList);

            //act
            var resultQuery = await _clientController.GetAsync();

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<OkObjectResult>(resultQuery);
            var listResult = Assert.IsType<List<ClientDto>>(objectResult.Value);
            Assert.Empty(listResult);
            _mockClientService.Verify(serv => serv.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoExists_ReturnsNotFound()
        {
            //arrange
            _mockClientService.Setup(serv => serv.GetAllAsync()).ReturnsAsync((List<Client>)null);

            //act
            var resultQuery = await _clientController.GetAsync();

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<NotFoundResult>(resultQuery);
            _mockClientService.Verify(serv => serv.GetAllAsync(), Times.Once);
        }
        #endregion

        #region GetByIdAsync
        [Fact]
        public async Task GetAsync_ReturnsClientDto()
        {
            //arrange
            var client = new Client() { Id = 10, Name = "Amber", Dni = "99888777" };

            _mockClientService.Setup(serv => serv.GetAsync(client.Id)).ReturnsAsync(client);

            //act
            var resultQuery = await _clientController.GetAsync(client.Id);

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<OkObjectResult>(resultQuery);
            var clientResult = Assert.IsType<ClientDto>(objectResult.Value);
            Assert.Equal(client.Name, clientResult.Name);
            Assert.Equal(client.Id, clientResult.Id);
            _mockClientService.Verify(serv => serv.GetAsync(client.Id), Times.Once);
        }

        [Fact]
        public async Task GetAsync_WhenClientNotExists_ReturnsNotFound()
        {
            //arrange
            var clientId = 2;

            _mockClientService.Setup(serv => serv.GetAsync(clientId)).ReturnsAsync((Client)null);

            //act
            var resultQuery = await _clientController.GetAsync();

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<NotFoundResult>(resultQuery);
            _mockClientService.Verify(serv => serv.GetAsync(clientId), Times.Never);
        }
        #endregion

        #region PostAsync
        [Fact]
        public async Task PostAsync_ReturnsClientDto()
        {
            //arrange
            var clientDto = new ClientDto() { Id = 999, Name = "Indian", Dni = "11333555", LastUpdate = System.DateTime.Now };
            var client = new Client() { Id = 999, Name = "Indian", Dni = "11333555", LastUpdate = System.DateTime.Now };

            _mockClientService.Setup(serv => serv.CreateAsync(It.Is<Client>(cl => cl.Id == client.Id))).ReturnsAsync(client);

            //act
            var resultQuery = await _clientController.PostAsync(clientDto);

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<OkObjectResult>(resultQuery);
            var clientDtoCreated = Assert.IsType<ClientDto>(objectResult.Value);
            Assert.Equal(clientDto.Id, clientDtoCreated.Id);
            _mockClientService.Verify(serv => serv.CreateAsync(It.Is<Client>(cl => cl.Id == client.Id)), Times.Once);
        }

        [Fact]
        public async Task PostAsync_WhenError_ReturnsBadRequest()
        {
            //arrange
            var clientDto = new ClientDto();

            _mockClientService.Setup(serv => serv.CreateAsync(It.IsAny<Client>())).ReturnsAsync((Client)null);

            //act
            var resultQuery = await _clientController.PostAsync(clientDto);

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<BadRequestResult>(resultQuery);
            _mockClientService.Verify(serv => serv.CreateAsync(It.IsAny<Client>()), Times.Once);
        }
        #endregion

        #region PutAsync
        [Fact]
        public async Task PutAsync_ReturnsUpdatedClientDto()
        {
            //arrange
            var clientDto = new ClientDto() { Id = 999, Name = "Indian", Dni = "11333555", City = "Firmat" };
            var client = new Client() { Id = 999, Name = "Indian", Dni = "11333555", City = "Rosario" };

            _mockClientService.Setup(serv => serv.UpdateAsync(It.Is<Client>(cl => cl.Id == client.Id), client.Id)).ReturnsAsync(client);

            //act
            var resultQuery = await _clientController.PutAsync(clientDto, clientDto.Id);

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<OkObjectResult>(resultQuery);
            var clientDtoUpdated = Assert.IsType<ClientDto>(objectResult.Value);
            Assert.Equal(clientDto.Id, clientDtoUpdated.Id);
            Assert.Equal(client.City, clientDtoUpdated.City);
            _mockClientService.Verify(serv => serv.UpdateAsync(It.Is<Client>(cl => cl.Id == client.Id), client.Id), Times.Once);
        }

        [Fact]
        public async Task PutAsync_WhenNoExists_ReturnsNotFound()
        {
            //arrange
            var clientDto = new ClientDto() { Id = 999, Name = "Indian", Dni = "11333555", City = "Rosario" };

            _mockClientService.Setup(serv => serv.UpdateAsync(It.Is<Client>(cl => cl.Id == clientDto.Id), clientDto.Id)).ReturnsAsync((Client)null);

            //act
            var resultQuery = await _clientController.PutAsync(clientDto, clientDto.Id);

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<NotFoundResult>(resultQuery);
            _mockClientService.Verify(serv => serv.UpdateAsync(It.Is<Client>(cl => cl.Id == clientDto.Id), clientDto.Id), Times.Once);
        }
        #endregion

        #region DeleteAsync
        [Fact]
        public async Task DeleteAsync_ReturnsNoContent()
        {
            //arrange
            var clientId = 7;
            _mockClientService.Setup(serv => serv.EntityExistsAsync(clientId)).ReturnsAsync(true);

            //act
            var resultQuery = await _clientController.DeleteAsync(clientId);

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<NoContentResult>(resultQuery);
            _mockClientService.Verify(serv => serv.DeleteAsync(clientId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenNoExists_ReturnsNotFound()
        {
            //arrange
            var clientId = 7;
            _mockClientService.Setup(serv => serv.EntityExistsAsync(clientId)).ReturnsAsync(false);

            //act
            var resultQuery = await _clientController.DeleteAsync(clientId);

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<NotFoundResult>(resultQuery);
            _mockClientService.Verify(serv => serv.DeleteAsync(clientId), Times.Never);
        }
        #endregion
    }
}
