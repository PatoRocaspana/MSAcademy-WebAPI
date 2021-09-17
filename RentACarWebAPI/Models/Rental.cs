using RentACarWebAPI.Models.Base;
using System;

namespace RentACarWebAPI.Models
{
    public class Rental : Entity
    {
        public TimeSpan RentalDuration => GetRentalDuration();
        public Client Client { get; set; }
        public Car Car { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }

        private TimeSpan GetRentalDuration()
        {
            return ReturnDate - RentalDate;
        }
    }
}
