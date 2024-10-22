namespace Entities.Client.Model
{
    using Entities.Common;
    using Entities.Trainer.Model;

    public class Client() : Entity
    {
        public required string Name { get; set; }

        public float Weight { get; set; }

        public float Height { get; set; }

        public required string Email { get; set; }

        public IEnumerable<Trainer> Trainers { get; set; } = new List<Trainer>();
    }
}
