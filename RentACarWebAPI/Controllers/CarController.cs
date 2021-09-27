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
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/<CarController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var carEntityList = _carService.GetAll();

            if (carEntityList is null)
                return NotFound();

            var carDtoList = carEntityList.Select(carEntity => new CarDto(carEntity));

            return Ok(carDtoList);
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var carEntity = _carService.Get(id);

            if (carEntity is null)
                return NotFound();

            var carDto = new CarDto(carEntity);

            return Ok(carDto);
        }

        // POST api/<CarController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CarDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] CarDto carDto)
        {
            var carEntity = carDto.ToCarEntity(carDto);

            var carCreated = _carService.Create(carEntity);

            if (carCreated is null)
                return NotFound();

            var carResponse = new CarDto(carCreated);

            return Ok(carResponse);
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put([FromBody] CarDto carDto, int id)
        {
            var carEntity = carDto.ToCarEntity(carDto);

            var carUpdated = _carService.Update(carEntity, id);

            if (carUpdated is null)
                return NotFound();

            var carResponse = new CarDto(carUpdated);

            return Ok(carResponse);
        }

        // DELETE api/<CarController>/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var carExist = _carService.EntityExists(id);

            if (!carExist)
                return NotFound();

            _carService.Delete(id);
            return NoContent();
        }
    }
}
