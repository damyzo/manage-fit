namespace Contracts.Responses.Exercise
{
    public class DeleteExerciseRequest(
        string name,
        string description,
        string videoUrl,
        Guid id)
    {
        public string Name { get; } = name;

        public string Description { get; } = description;

        public string VideoUrl { get; } = videoUrl;

        public string Id { get; } = id.ToString();
    }
}
