namespace Entities.Trainer.Model
{
    using Entities.Client.Model;
    using Entities.Common;

    public class Trainer : Entity
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public IEnumerable<Client> Clients { get; set; } = new List<Client>();
    }
}
