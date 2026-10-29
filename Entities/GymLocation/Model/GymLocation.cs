namespace Entities.GymLocation.Model
{
    using Entities.Common;

    public class GymLocation : Entity
    {
        public Guid GymId { get; set; }

        public required string Address { get; set; }

        public virtual Gym.Model.Gym Gym { get; set; } = null!;
    }
}
