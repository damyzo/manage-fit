namespace Contracts.Requests.WorkoutPlan
{
    using System;
    using System.Collections.Generic;

    public class EditWorkoutPlanRequest(
        string name,
        IEnumerable<EditWorkoutPlanExerciseRequest> exercises,
        IEnumerable<Guid> clientUids)
    {
        public string Name { get; } = name;

        public IEnumerable<EditWorkoutPlanExerciseRequest> exercises {  get; } = exercises;

        public IEnumerable<Guid> ClientUids { get; } = clientUids;

    }

    public class EditWorkoutPlanExerciseRequest(
        Guid? id,
        string name,
        string description,
        string videoUrl,
        int set,
        int repetition,
        double time,
        int order)
    {
        public int Order { get; } = order;

        public Guid? Id { get; } = id;

        public int Set { get; } = set;

        public int Repetition { get; } = repetition;

        public double Time { get; } = time;

        public string Name { get; } = name;

        public string Description { get; } = description;

        public string VideoUrl { get; } = videoUrl;
    }
}
