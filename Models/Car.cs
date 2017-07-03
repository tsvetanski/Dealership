namespace Dealership.Models
{
    using Dealership.Common;
    using Dealership.Common.Enums;
    using Dealership.Contracts;

    public class Car : Vehicle, ICar
    {
        private int seats;

        public Car(string make, string model, decimal price, int seats, VehicleType type = VehicleType.Car)
            : base((int)type, type, make, model, price)
        {
            this.Seats = seats;
        }

        public int Seats
        {
            get
            {
                return this.seats;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.VehicleCannotBeNull);
                Validator.ValidateIntRange(
                    value, 
                    Constants.MinSeats, 
                    Constants.MaxSeats,
                    string.Format(Constants.NumberMustBeBetweenMinAndMax, "Seats", Constants.MinSeats, Constants.MaxSeats));
                this.seats = value;
            }
        }

        public override string PrintSpecificDetails()
        {
            return string.Format("  Seats: {0}", this.Seats);
        }
    }
}