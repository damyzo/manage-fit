using System.ComponentModel.DataAnnotations;

namespace Entities.Common
{
    public class Entity
    {
        [Key]
        public Guid Uid { get; set; }
    }
}
