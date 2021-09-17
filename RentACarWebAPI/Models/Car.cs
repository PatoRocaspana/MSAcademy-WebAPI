using RentACarWebAPI.Models.Base;
using RentACarWebAPI.Models.Enums;

namespace RentACarWebAPI.Models
{
    public class Car : Entity
    {
        public Brand Brand { get; set; }
        public string Model { get; set; }
        public int DoorsQuantity { get; set; }
        public string Color { get; set; }
        public Transmission Transmission { get; set; }
    }
}
