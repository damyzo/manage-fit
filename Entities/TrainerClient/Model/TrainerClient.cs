namespace Entities.TrainerClient.Model
{
    using Entities.Client.Model;
    using Entities.Common;
    using Entities.Trainer.Model;

    public class TrainerClient : Entity
    {
        public Guid TrainerId { get; set; }

        public Guid ClientId { get; set; }

        public Trainer Trainer { get; set; } = null!;

        public Client Client { get; set; } = null!;
    }   
}
