namespace Entities.Client.Model
{
    using Entities.Common;
    using Entities.TrainerClient.Model;
    using Entities.WorkoutPlan.Model;

    public class Client : Entity
    {
        public required string Name { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public required string Email { get; set; }

        public virtual ICollection<TrainerClient> TrainerClients { get; set; } = [];

        public virtual ICollection<WorkoutPlan> Workouts { get; set; } = [];
    }
}
