using Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Client.Model
{
    [Table("Client")]
    public class Client() : Entity
    {
        public required string Name { get; set; }

        public float Weight { get; set; }

        public float Height { get; set; }

        public required string Email { get; set; }
    }
}
