namespace Dealership.Models
{
    using System.Collections.Generic;
    using Dealership.Common;
    using Dealership.Common.Enums;
    using Dealership.Contracts;

    public abstract class Vehicle : IVehicle
    {
        private int wheels;
        private string make;
        private string model;
        private decimal price;

        protected Vehicle(
            int wheels,
            VehicleType type,
            string make,
            string model,
            decimal price)
        {
            this.Wheels = wheels;
            this.Type = type;
            this.Make = make;
            this.Model = model;
            this.Price = price;
            this.Comments = new List<IComment>();
        }

        public int Wheels
        {
            get
            {
                return this.wheels;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.VehicleCannotBeNull);
                Validator.ValidateIntRange(
                    value, 
                    Constants.MinWheels, 
                    Constants.MaxWheels,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, "Wheels", Constants.MinWheels, Constants.MaxWheels));
                this.wheels = value;
            }
        }

        public VehicleType Type { get; private set; }

        public string Make
        {
            get
            {
                return this.make;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.VehicleCannotBeNull);
                Validator.ValidateIntRange(
                    value.Length,
                    Constants.MinMakeLength,
                    Constants.MaxMakeLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, "Make", Constants.MinMakeLength, Constants.MaxMakeLength));
                this.make = value;
            }
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.VehicleCannotBeNull);
                Validator.ValidateIntRange(
                    value.Length, 
                    Constants.MinModelLength, 
                    Constants.MaxModelLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, "Model", Constants.MinModelLength, Constants.MaxModelLength));
                this.model = value;
            }
        }

        public IList<IComment> Comments { get; set; }

        public decimal Price
        {
            get
            {
                return this.price;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.VehicleCannotBeNull);
                Validator.ValidateDecimalRange(
                    value, 
                    Constants.MinPrice, 
                    Constants.MaxPrice,
                    string.Format(Constants.NumberMustBeBetweenMinAndMax, "Price", Constants.MinPrice, Constants.MaxPrice));
                this.price = value;
            }
        }

        public abstract string PrintSpecificDetails();

        public void ValidateInt(string input, int minLength, int maxLength, string property)
        {
            Validator.ValidateNull(input, Constants.VehicleCannotBeNull);
            Validator.ValidateIntRange(
                input.Length, 
                minLength,
                maxLength,
                string.Format(Constants.StringMustBeBetweenMinAndMax, property, Constants.MinModelLength, Constants.MaxModelLength));
        }
    }
}