namespace Dealership.Models
{
    using Dealership.Common;
    using Dealership.Common.Enums;
    using Dealership.Contracts;

    public class Truck : Vehicle, ITruck
    {
        private int weightCapacity;

        public Truck(string make, string model, decimal price, int weightCapacity, VehicleType type = VehicleType.Truck)
            : base((int)type, type, make, model, price)
        {
            this.WeightCapacity = weightCapacity;
        }

        public int WeightCapacity
        {
            get
            {
                return this.weightCapacity;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.VehicleCannotBeNull);
                Validator.ValidateIntRange(
                    value, 
                    Constants.MinCapacity, 
                    Constants.MaxCapacity,
                    string.Format(Constants.NumberMustBeBetweenMinAndMax, "Weight capacity", Constants.MinCapacity, Constants.MaxCapacity));
                this.weightCapacity = value;
            }
        }

        public override string PrintSpecificDetails()
        {
            return string.Format("  Weight Capacity: {0}t", this.WeightCapacity);
        }
    }
}
