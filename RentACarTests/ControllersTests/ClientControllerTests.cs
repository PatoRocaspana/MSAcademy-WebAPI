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
        public async Task GetAllAsync_WhenNotExists_ReturnsNotFound()
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
    }
}
