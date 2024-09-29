using Entities.Common;

namespace Entities.Client.Model
{
    public class Client() : Entity
    {
        public required string Name { get; set; }

        public float Weight { get; set; }

        public float Height { get; set; }

        public required string Email { get; set; }
    }
}
