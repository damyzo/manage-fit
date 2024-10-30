namespace Entities.Trainer.Model
{
    using Entities.Client.Model;
    using Entities.Common;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Trainer() : Entity
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        [Column("ClientUid")]
        public List<Client> Clients { get; set; } = new List<Client>();
    }
}
