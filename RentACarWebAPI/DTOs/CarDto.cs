using RentACarWebAPI.DTOs.Base;
using RentACarWebAPI.Models;
using RentACarWebAPI.Models.Enums;

namespace RentACarWebAPI.DTOs
{
    public class CarDto : EntityDto
    {
        public Brand Brand { get; set; }
        public string Model { get; set; }
        public int DoorsQuantity { get; set; }
        public string Color { get; set; }
        public Transmission Transmission { get; set; }

        public CarDto() { }

        public CarDto(Car carEntity)
        {
            Id = carEntity.Id;
            Brand = carEntity.Brand;
            Model = carEntity.Model;
            DoorsQuantity = carEntity.DoorsQuantity;
            Color = carEntity.Color;
            Transmission = carEntity.Transmission;
        }

        public Car ToCarEntity(CarDto carDto)
        {
            var carEntity = new Car()
            {
                Id = carDto.Id,
                Brand = carDto.Brand,
                Model = carDto.Model,
                DoorsQuantity = carDto.DoorsQuantity,
                Color = carDto.Color,
                Transmission = carDto.Transmission
            };

            return carEntity;
        }
    }
}
