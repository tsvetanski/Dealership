namespace Dealership.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Dealership.Common;
    using Dealership.Common.Enums;
    using Dealership.Contracts;

    public class User : IUser
    {
        private string username;
        private string firstName;
        private string lastName;
        private string password;

        public User(string username, string firstName, string lastName, string password, Role role)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Role = role;
            this.Vehicles = new List<IVehicle>();
        }

        public string Username
        {
            get
            {
                return this.username;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.VehicleCannotBeNull);
                Validator.ValidateIntRange(
                    value.Length, 
                    Constants.MinNameLength,
                    Constants.MaxNameLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, "Username", Constants.MinNameLength, Constants.MaxNameLength));
                Validator.ValidateSymbols(
                    value,
                    Constants.UsernamePattern,
                    string.Format(Constants.InvalidSymbols, "Username"));
                this.username = value;
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.VehicleCannotBeNull);
                Validator.ValidateIntRange(
                    value.Length,
                    Constants.MinNameLength, 
                    Constants.MaxNameLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, "Firstname", Constants.MinNameLength, Constants.MaxNameLength));
                this.firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.VehicleCannotBeNull);
                Validator.ValidateIntRange(
                    value.Length,
                    Constants.MinNameLength,
                    Constants.MaxNameLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, "Lastname", Constants.MinNameLength, Constants.MaxNameLength));
                this.lastName = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            private set
            {
                Validator.ValidateNull(value, Constants.VehicleCannotBeNull);
                Validator.ValidateIntRange(
                    value.Length, 
                    Constants.MinPasswordLength, 
                    Constants.MaxPasswordLength,
                    string.Format(Constants.StringMustBeBetweenMinAndMax, "Password", Constants.MinPasswordLength, Constants.MaxPasswordLength));
                Validator.ValidateSymbols(
                    value,
                    Constants.PasswordPattern,
                    string.Format(Constants.InvalidSymbols, "Password"));
                this.password = value;
            }
        }

        public Role Role { get; private set; }

        public IList<IVehicle> Vehicles { get; private set; }

        public void AddComment(IComment commentToAdd, IVehicle vehicleToAddComment)
        {
            vehicleToAddComment.Comments.Add(commentToAdd);
        }

        public void AddVehicle(IVehicle vehicle)
        {
            switch (Role)
            {
                case Role.Admin:
                    throw new ArgumentException(Constants.AdminCannotAddVehicles);

                case Role.Normal:
                    if (this.Vehicles.Count < Constants.MaxVehiclesToAdd)
                    {
                        this.Vehicles.Add(vehicle);
                    }
                    else
                    {
                        throw new ArgumentException(string.Format(Constants.NotAnVipUserVehiclesAdd, Vehicles.Count));
                    }

                    break;

                case Role.VIP:
                    this.Vehicles.Add(vehicle);
                    break;

                default:
                    break;
            }
        }

        public string PrintVehicles()
        {
            int iterator = 1;
            var output = new StringBuilder();
            output.AppendFormat("--USER {0}--", this.Username)
                  .AppendLine();

            int vehicleCount = this.Vehicles.Count;

            if (this.Vehicles.Count == 0)
            {
                output.AppendFormat("--NO VEHICLES--");
            }

            foreach (Vehicle vehicle in this.Vehicles)
            {
                output.AppendFormat("{0}. {1}:", iterator++, vehicle.Type)
                      .AppendLine()
                      .AppendFormat("  Make: {0}", vehicle.Make)
                      .AppendLine()
                      .AppendFormat("  Model: {0}", vehicle.Model)
                      .AppendLine()
                      .AppendFormat("  Wheels: {0}", vehicle.Wheels)
                      .AppendLine()
                      .AppendFormat("  Price: ${0}", vehicle.Price)
                      .AppendLine()
                      .AppendFormat(vehicle.PrintSpecificDetails())
                      .AppendLine();
                if (vehicle.Comments.Count > 0)
                {
                    output.AppendFormat("    --COMMENTS--")
                          .AppendLine();
                    foreach (IComment comment in vehicle.Comments)
                    {
                        output.AppendFormat("    ----------")
                              .AppendLine()
                              .AppendFormat("    {0}", comment.Content)
                              .AppendLine()
                              .AppendFormat("      User: {0}", comment.Author)
                              .AppendLine()
                              .AppendFormat("    ----------")
                              .AppendLine();
                    }

                    output.AppendFormat("    --COMMENTS--")
                           .AppendLine();
                }
                else
                {
                    output.AppendFormat("    --NO COMMENTS--");
                    if (iterator != vehicleCount + 1)
                    {
                        output.AppendLine();
                    }
                }
            }

            return output.ToString();
        }

        public void RemoveComment(IComment commentToRemove, IVehicle vehicleToRemoveComment)
        {
            Validator.ValidateSymbols(commentToRemove.Author, this.Username, Constants.YouAreNotTheAuthor);
            vehicleToRemoveComment.Comments.Remove(commentToRemove);
        }

        public void RemoveVehicle(IVehicle vehicle)
        {
            this.Vehicles.Remove(vehicle);
        }

        public override string ToString()
        {
            return string.Format("Username: {0}, FullName: {1} {2}, Role: {3}", this.Username, this.FirstName, this.LastName, Role.ToString());
        }
    }
}
