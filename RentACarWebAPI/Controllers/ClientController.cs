using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACarWebAPI.DTOs;
using RentACarWebAPI.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ClientDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            var clientEntityList = await _clientService.GetAllAsync();

            if (clientEntityList is null)
                return NotFound();

            var clientDtoList = clientEntityList
                    .Select(clientEntity => new ClientDto(clientEntity))
                    .ToList();

            return Ok(clientDtoList);
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var clientEntity = await _clientService.GetAsync(id);

            if (clientEntity is null)
                return NotFound();

            var clientDto = new ClientDto(clientEntity);

            return Ok(clientDto);
        }

        // POST api/<ClientController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClientDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] ClientDto clientDto)
        {
            var clientEntity = clientDto.ToClientEntity(clientDto);

            var clientCreated = await _clientService.CreateAsync(clientEntity);

            if (clientCreated is null)
                return BadRequest();

            var clientResponse = new ClientDto(clientCreated);

            return Ok(clientResponse);
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync([FromBody] ClientDto clientDto, int id)
        {
            var clientEntity = clientDto.ToClientEntity(clientDto);

            var clientUpdated = await _clientService.UpdateAsync(clientEntity, id);

            if (clientUpdated is null)
                return NotFound();

            var clientResponse = new ClientDto(clientUpdated);

            return Ok(clientResponse);
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var clientExist = await _clientService.EntityExistsAsync(id);
            if (!clientExist)
                return NotFound();

            await _clientService.DeleteAsync(id);
            return NoContent();
        }
    }
}
