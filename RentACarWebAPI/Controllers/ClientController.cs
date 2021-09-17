using Microsoft.AspNetCore.Mvc;
using RentACarWebAPI.DTOs;
using RentACarWebAPI.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentACarWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/<ClientController>
        [HttpGet]
        public IEnumerable<ClientDto> Get()
        {
            var clientEntityList = _clientService.GetAll();

            var clientDtoList = clientEntityList.Select(clientEntity => new ClientDto(clientEntity)).ToList();

            return clientDtoList;
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public ClientDto Get(int id)
        {
            var clientEntity = _clientService.Get(id);

            var clientDto = new ClientDto(clientEntity);

            return clientDto;
        }

        // POST api/<ClientController>
        [HttpPost]
        public ClientDto Post([FromBody] ClientDto clientDto)
        {
            var clientEntity = clientDto.ToClientEntity(clientDto);

            var clientCreated = _clientService.Create(clientEntity);

            var carResponse = new ClientDto(clientCreated);

            return carResponse;
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public ClientDto Put([FromBody] ClientDto clientDto, int id)
        {
            var clientEntity = clientDto.ToClientEntity(clientDto);

            var clientCreated = _clientService.Update(clientEntity, id);

            var clientResponse = new ClientDto(clientCreated);

            return clientResponse;
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _clientService.Delete(id);
        }
    }
}
