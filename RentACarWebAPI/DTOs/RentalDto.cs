using RentACarWebAPI.DTOs.Base;
using RentACarWebAPI.Models;
using System;

namespace RentACarWebAPI.DTOs
{
    public class RentalDto : EntityDto
    {
        public TimeSpan RentalDuration { get; set; }
        public ClientDto ClientDto { get; set; }
        public CarDto CarDto { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public RentalDto() { }

        public RentalDto(Rental rentalEntity)
        {
            Id = rentalEntity.Id;
            RentalDuration = rentalEntity.RentalDuration;
            ClientDto = new ClientDto(rentalEntity.Client);
            CarDto = new CarDto(rentalEntity.Car);
            RentalDate = rentalEntity.RentalDate;
            ReturnDate = rentalEntity.ReturnDate;
        }

        public Rental ToRentalEntity(RentalDto rentalDto)
        {
            var rentalEntity = new Rental()
            {
                Id = rentalDto.Id,
                Client = rentalDto.ClientDto.ToClientEntity(rentalDto.ClientDto),
                Car = rentalDto.CarDto.ToCarEntity(rentalDto.CarDto),
                RentalDate = rentalDto.RentalDate,
                ReturnDate = rentalDto.ReturnDate
            };

            return rentalEntity;
        }
    }
}
