namespace Entities.WorkoutPlanClient.Model
{
    using Entities.Client.Model;
    using Entities.WorkoutPlan.Model;
    using System;

    public class WorkoutPlanClient
    {
        public Guid WorkoutPlansId { get; set; }

        public Guid ClientsId { get; set; }

        public virtual WorkoutPlan WorkoutPlans { get; set; } = null!;

        public virtual Client Clients { get; set; } = null!;
    }
}
