using Entities.Common;
using Entities.Gym.Model;

namespace Services.Validators
{
    public static class GymValidator
    {
        public static Result<Gym> Validate(Gym gym)
        {
            if (gym == null
                || string.IsNullOrEmpty(gym.Name)
                || string.IsNullOrEmpty(gym.Location))
            {
                return new Result<Gym>(
                    value: gym ?? new Gym() { Name = "", Location = "", Id = Guid.Empty },
                    isSuccess: false,
                    message: "Gym info is required.");
            }

            return new Result<Gym>(
                value: gym,
                isSuccess: true,
                message: "Valid Data");
        }
    }
}
