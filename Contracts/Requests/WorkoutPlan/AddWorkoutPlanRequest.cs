namespace Contracts.Requests.WorkoutPlan
{
    using System;
    using System.Collections.Generic;

    public class AddWorkoutPlanRequest(
        string name,
        Guid trainerUid,
        IEnumerable<AddWorkoutPlanExerciseRequest> exercises,
        IEnumerable<Guid> clientUids)
    {
        public string Name { get; } = name;
        public Guid TrainerUid { get; } = trainerUid;

        public IEnumerable<AddWorkoutPlanExerciseRequest> exercises {  get; } = exercises;

        public IEnumerable<Guid> ClientUids { get; } = clientUids;

    }

    public class AddWorkoutPlanExerciseRequest(
        Guid? id,
        string name,
        string description,
        string videoUrl)
    {
        public Guid? Id { get; } = id;

        public string Name { get; } = name;

        public string Description { get; } = description;

        public string VideoUrl { get; } = videoUrl;
    }
}
