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
        #region GetAllAsync
        [Fact]
        public async Task GetAllAsync_ReturnsClientsDto()
        {
            //arrange
            var clientOneName = "Pedro";

            var client1 = new Client() { Id = 1, Name = clientOneName, Dni = "13131311" };
            var client2 = new Client() { Id = 3, Name = "Juanse", Dni = "13131313" };
            var client3 = new Client() { Id = 7, Name = "Sofia", Dni = "89444666" };

            var clientList = new List<Client>
            {
                client1,
                client2,
                client3
            };

            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.GetAllAsync()).ReturnsAsync(clientList);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.GetAsync();

            //assert
            var objectResult = Assert.IsType<OkObjectResult>(resultQuery);
            var listResult = Assert.IsType<List<ClientDto>>(objectResult.Value);
            Assert.Equal(clientList.Count, listResult.Count);
            Assert.Equal(clientOneName, listResult.Find(e => e.Id == 1).Name);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoClients_ReturnsEmpty()
        {
            //arrange
            var clientList = new List<Client>();

            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.GetAllAsync()).ReturnsAsync(clientList);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.GetAsync();

            //assert
            var objectResult = Assert.IsType<OkObjectResult>(resultQuery);
            var listResult = Assert.IsType<List<ClientDto>>(objectResult.Value);
            Assert.Empty(listResult);
            Assert.NotNull(resultQuery);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoExists_ReturnsNotFound()
        {
            //arrange
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.GetAllAsync()).ReturnsAsync((List<Client>)null);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.GetAsync();

            //assert
            var objectResult = Assert.IsType<NotFoundResult>(resultQuery);
        }
        #endregion

        #region GetByIdAsync
        [Fact]
        public async Task GetAsync_ReturnsClientDto()
        {
            //arrange
            var clientId = 10;
            var client = new Client() { Id = clientId, Name = "Amber", Dni = "99888777" };

            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.GetAsync(clientId)).ReturnsAsync(client);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.GetAsync(clientId);

            //assert
            var objectResult = Assert.IsType<OkObjectResult>(resultQuery);
            var clientResult = Assert.IsType<ClientDto>(objectResult.Value);
            Assert.Equal(client.Id, clientResult.Id);
            Assert.Equal(client.Name, clientResult.Name);

        }

        [Fact]
        public async Task GetAsync_WhenClientNotExists_ReturnsNotFound()
        {
            //arrange
            var clientId = 2;

            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.GetAsync(clientId)).ReturnsAsync((Client)null);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.GetAsync();

            //assert
            var objectResult = Assert.IsType<NotFoundResult>(resultQuery);
        }
        #endregion

        #region PostAsync
        [Fact]
        public async Task PostAsync_ReturnsClientDto()
        {
            //arrange
            var clientDto = new ClientDto() { Id = 999, Name = "Indian", Dni = "11333555", LastUpdate = System.DateTime.Now };
            var client = new Client() { Id = 999, Name = "Indian", Dni = "11333555", LastUpdate = System.DateTime.Now };

            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.CreateAsync(It.Is<Client>(cl => cl.Id == 999))).ReturnsAsync(client);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.PostAsync(clientDto);

            //assert
            var objectResult = Assert.IsType<OkObjectResult>(resultQuery);
            var clientDtoCreated = Assert.IsType<ClientDto>(objectResult.Value);
            Assert.Equal(clientDto.Id, clientDtoCreated.Id);
        }

        [Fact]
        public async Task PostAsync_WhenError_ReturnsBadRequest()
        {
            //arrange
            var clientDto = new ClientDto();

            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.CreateAsync(It.IsAny<Client>())).ReturnsAsync((Client)null);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.PostAsync(clientDto);

            //assert
            var objectResult = Assert.IsType<BadRequestResult>(resultQuery);
        }
        #endregion

        #region PutAsync
        [Fact]
        public async Task PutAsync_ReturnsUpdatedClientDto()
        {
            //arrange
            var oldCity = "Firmat";
            var newCity = "Rosario";

            var clientDto = new ClientDto() { Id = 999, Name = "Indian", Dni = "11333555", City = oldCity };
            var client = new Client() { Id = 999, Name = "Indian", Dni = "11333555", City = newCity };

            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.UpdateAsync(It.Is<Client>(cl => cl.Id == 999), client.Id)).ReturnsAsync(client);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.PutAsync(clientDto, clientDto.Id);

            //assert
            var objectResult = Assert.IsType<OkObjectResult>(resultQuery);
            var clientDtoUpdated = Assert.IsType<ClientDto>(objectResult.Value);
            Assert.Equal(clientDto.Id, clientDtoUpdated.Id);
            Assert.Equal(newCity, clientDtoUpdated.City);
            Assert.NotEqual(clientDto.City, clientDtoUpdated.City);
        }

        [Fact]
        public async Task PutAsync_WhenNoExists_ReturnsNotFound()
        {
            //arrange
            var clientDto = new ClientDto() { Id = 999, Name = "Indian", Dni = "11333555", City = "Rosario"};

            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.UpdateAsync(It.Is<Client>(cl => cl.Id == 999), clientDto.Id)).ReturnsAsync((Client)null);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.PutAsync(clientDto, clientDto.Id);

            //assert
            var objectResult = Assert.IsType<NotFoundResult>(resultQuery);
        }
        #endregion

        #region DeleteAsync
        [Fact]
        public async Task DeleteAsync_ReturnsNoContent()
        {
            //arrange
            var clientId = 7;
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.EntityExistsAsync(clientId)).ReturnsAsync(true);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.DeleteAsync(clientId);

            //assert
            var objectResult = Assert.IsType<NoContentResult>(resultQuery);
            mockClientService.Verify(serv => serv.DeleteAsync(clientId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenNoExists_ReturnsNotFound()
        {
            //arrange
            var clientId = 7;
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(serv => serv.EntityExistsAsync(clientId)).ReturnsAsync(false);

            var clientController = new ClientController(mockClientService.Object);

            //act
            var resultQuery = await clientController.DeleteAsync(clientId);

            //assert
            var objectResult = Assert.IsType<NotFoundResult>(resultQuery);
            mockClientService.Verify(serv => serv.DeleteAsync(clientId), Times.Never);
        }
        #endregion
    }
}
