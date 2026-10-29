using Entities.Common;
using Entities.Gym.Model;

namespace Services.Validators
{
    public static class GymValidator
    {
        public static Result<Gym> Validate(Gym gym)
        {
            if (gym == null || string.IsNullOrEmpty(gym.Name))
            {
                return new Result<Gym>(
                    value: gym ?? new Gym() { Name = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: "Gym info is required.");
            }

            if (gym.Locations.Count == 0 || gym.Locations.Any(x => string.IsNullOrWhiteSpace(x.Address)))
            {
                return new Result<Gym>(
                    value: gym,
                    isSuccess: false,
                    message: "Gym should have at least one valid location.");
            }

            return new Result<Gym>(
                value: gym,
                isSuccess: true,
                message: "Valid Data");
        }
    }
}
