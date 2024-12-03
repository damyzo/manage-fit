namespace Entities.Client.Model
{
    using Entities.Common;
    using Entities.Trainer.Model;
    using Entities.WorkoutPlan.Model;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Client() : Entity
    {
        public required string Name { get; set; }

        public float Weight { get; set; }

        public float Height { get; set; }

        public required string Email { get; set; }

        [Column("TrainerId")]
        public List<Trainer> Trainers { get; set; } = [];

        public IEnumerable<WorkoutPlan> Workouts { get; set; } = new List<WorkoutPlan>();
    }
}
