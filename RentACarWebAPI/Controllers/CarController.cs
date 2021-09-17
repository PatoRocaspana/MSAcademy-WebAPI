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
        public IEnumerable<CarDto> Get()
        {
            var carEntityList = _carService.GetAll();

            var carDtoList = carEntityList.Select(carEntity => new CarDto(carEntity)).ToList();

            return carDtoList;
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public CarDto Get(int id)
        {
            var carEntity = _carService.Get(id);

            var carDto = new CarDto(carEntity);

            return carDto;
        }

        // POST api/<CarController>
        [HttpPost]
        public CarDto Post([FromBody] CarDto carDto)
        {
            var carEntity = carDto.ToCarEntity(carDto);

            var carCreated = _carService.Create(carEntity);

            var carResponse = new CarDto(carCreated);

            return carResponse;
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]
        public CarDto Put([FromBody] CarDto carDto, int id)
        {
            var carEntity = carDto.ToCarEntity(carDto);

            var carCreated = _carService.Update(carEntity, id);

            var carResponse = new CarDto(carCreated);

            return carResponse;
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _carService.Delete(id);
        }
    }
}
