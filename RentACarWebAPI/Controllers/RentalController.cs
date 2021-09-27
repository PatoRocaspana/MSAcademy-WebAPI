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
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        // GET: api/<RentalController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RentalDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            var rentalEntityList = await _rentalService.GetAllAsync();

            if (rentalEntityList is null)
                return NotFound();

            var rentalDtoList = rentalEntityList.Select(rentalEntity => new RentalDto(rentalEntity)).ToList();

            return Ok(rentalDtoList);
        }

        // GET api/<RentalController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var rentalEntity = await _rentalService.GetAsync(id);

            if (rentalEntity is null)
                return NotFound();

            var rentalDto = new RentalDto(rentalEntity);

            return Ok(rentalDto);
        }

        // POST api/<RentalController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RentalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostAsync([FromBody] RentalDto rentalDto)
        {
            var rentalEntity = rentalDto.ToRentalEntity(rentalDto);

            var rentalCreated = await _rentalService.CreateAsync(rentalEntity);

            if (rentalCreated is null)
                return NotFound();

            var rentalResponse = new RentalDto(rentalCreated);

            return Ok(rentalResponse);
        }

        // PUT api/<RentalController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync([FromBody] RentalDto rentalDto, int id)
        {
            var rentalEntity = rentalDto.ToRentalEntity(rentalDto);

            var rentalUpdated = await _rentalService.UpdateAsync(rentalEntity, id);

            if (rentalUpdated is null)
                return NotFound();

            var rentalResponse = new RentalDto(rentalUpdated);

            return Ok(rentalResponse);
        }

        // DELETE api/<RentalController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var rentalExist = await _rentalService.EntityExistsAsync(id);
            if (!rentalExist)
                return NotFound();

            await _rentalService.DeleteAsync(id);
            return NoContent();
        }
    }
}
