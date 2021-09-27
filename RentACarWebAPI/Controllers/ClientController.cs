using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var clientEntityList = _clientService.GetAll();

            if (clientEntityList is null)
                return NotFound();

            var clientDtoList = clientEntityList.Select(clientEntity => new ClientDto(clientEntity));

            return Ok(clientDtoList);
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var clientEntity = _clientService.Get(id);

            if (clientEntity is null)
                return NotFound();

            var clientDto = new ClientDto(clientEntity);

            return Ok(clientDto);
        }

        // POST api/<ClientController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] ClientDto clientDto)
        {
            var clientEntity = clientDto.ToClientEntity(clientDto);

            var clientCreated = _clientService.Create(clientEntity);

            if (clientCreated is null)
                return NotFound();

            var carResponse = new ClientDto(clientCreated);

            return Ok(carResponse);
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put([FromBody] ClientDto clientDto, int id)
        {
            var clientEntity = clientDto.ToClientEntity(clientDto);

            var clientUpdated = _clientService.Update(clientEntity, id);

            if (clientUpdated is null)
                return NotFound();

            var clientResponse = new ClientDto(clientUpdated);

            return Ok(clientResponse);
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var clientExist = _clientService.EntityExist(id);
            if (!clientExist)
                return NotFound();

            _clientService.Delete(id);
            return NoContent();
        }
    }
}
