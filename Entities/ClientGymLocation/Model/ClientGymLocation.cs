namespace Entities.ClientGymLocation.Model
{
    using Entities.Client.Model;
    using Entities.Common;
    using Entities.GymLocation.Model;

    public class ClientGymLocation : Entity
    {
        public Guid ClientId { get; set; }

        public Guid GymLocationId { get; set; }

        public virtual Client Client { get; set; } = null!;

        public virtual GymLocation GymLocation { get; set; } = null!;
    }
}
