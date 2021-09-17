using Microsoft.AspNetCore.Mvc;
using RentACarWebAPI.DTOs;
using RentACarWebAPI.Interfaces;
using System;
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
        private readonly IRentalRepository _rentalRepository;

        public RentalController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        // GET: api/<RentalController>
        [HttpGet]
        public IEnumerable<RentalDto> Get()
        {
            var rentalEntityList = _rentalRepository.GetAll();

            var rentalDtoList = rentalEntityList.Select(rentalEntity => new RentalDto(rentalEntity)).ToList();

            return rentalDtoList;
        }

        // GET api/<RentalController>/5
        [HttpGet("{id}")]
        public RentalDto Get(int id)
        {
            var rentalEntity = _rentalRepository.Get(id);

            var rentalDto = new RentalDto(rentalEntity);

            return rentalDto;
        }

        // POST api/<RentalController>
        [HttpPost]
        public RentalDto Post([FromBody] RentalDto rentalDto)
        {
            var rentalEntity = rentalDto.ToRentalEntity(rentalDto);

            var rentalCreated = _rentalRepository.Create(rentalEntity);

            var rentalResponse = new RentalDto(rentalCreated);

            return rentalResponse;
        }

        // PUT api/<RentalController>/5
        [HttpPut("{id}")]
        public RentalDto Put([FromBody] RentalDto rentalDto, int id)
        {
            var rentalEntity = rentalDto.ToRentalEntity(rentalDto);

            var rentalUpdated = _rentalRepository.Update(rentalEntity, id);

            var rentalResponse = new RentalDto(rentalUpdated);

            return rentalResponse;
        }

        // DELETE api/<RentalController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _rentalRepository.Delete(id);
        }
    }
}
