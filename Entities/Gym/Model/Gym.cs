namespace Entities.Gym.Model
{
    using Entities.Client.Model;
    using Entities.Common;
    using Entities.Trainer.Model;

    public class Gym : Entity
    {
        public required string Name { get; set; }

        public required string Location { get; set; }

        public virtual ICollection<Trainer> Trainers { get; set; } = [];

        public virtual ICollection<Client> Clients { get; set; } = [];
    }
}
