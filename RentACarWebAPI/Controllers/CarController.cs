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
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/<CarController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CarDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            var carEntityList = await _carService.GetAllAsync();

            if (carEntityList is null)
                return NotFound();

            var carDtoList = carEntityList
                    .Select(carEntity => new CarDto(carEntity))
                    .ToList();

            return Ok(carDtoList);
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var carEntity = await _carService.GetAsync(id);

            if (carEntity is null)
                return NotFound();

            var carDto = new CarDto(carEntity);

            return Ok(carDto);
        }

        // POST api/<CarController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CarDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostAsync([FromBody] CarDto carDto)
        {
            var carEntity = carDto.ToCarEntity(carDto);

            var carCreated = await _carService.CreateAsync(carEntity);

            if (carCreated is null)
                return NotFound();

            var carResponse = new CarDto(carCreated);

            return Ok(carResponse);
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync([FromBody] CarDto carDto, int id)
        {
            var carEntity = carDto.ToCarEntity(carDto);

            var carUpdated = await _carService.UpdateAsync(carEntity, id);

            if (carUpdated is null)
                return NotFound();

            var carResponse = new CarDto(carUpdated);

            return Ok(carResponse);
        }

        // DELETE api/<CarController>/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var carExist = await _carService.EntityExistsAsync(id);

            if (!carExist)
                return NotFound();

            await _carService.DeleteAsync(id);
            return NoContent();
        }
    }
}
